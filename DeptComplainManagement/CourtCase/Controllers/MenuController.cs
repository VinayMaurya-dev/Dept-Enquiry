using CourtCase.DAL;
using CourtCase.Filters;
using CourtCase.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CourtCase.Controllers
{
	[SessionCheck]
	public class MenuController : Controller
	{
		string consString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
		private readonly DBLayer dBLayer = new DBLayer();

		public PartialViewResult GetMenuForRole(MenuViewModel model)
		{
			model.RoleId = Session["RoleId"].ToString();
			model.Action = "BindSideMenu";
			DataSet menuData = dBLayer.GetDynamicMenu(model);
			ViewBag.MenuData = menuData;
			return PartialView("Partial/_LeftMenu");
		}

		[HttpPost]
        public ActionResult SaveMenuSelection()
        {
            using (var reader = new StreamReader(Request.InputStream))
            {
                var json = reader.ReadToEnd();
                var selectedItems = JsonConvert.DeserializeObject<List<MenuViewModel>>(json);

                using (SqlConnection connection = new SqlConnection(consString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Set IsAccess = 0 for the given RoleId
                            if (selectedItems.Count > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand("UPDATE Permissions SET CanAccess = 0 WHERE RoleId = @RoleId", connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@RoleId", selectedItems[0].RoleId); // Assuming all items have the same RoleId
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            foreach (var mainMenu in selectedItems)
                            {
                                // Insert main menu item
                                using (SqlCommand cmd = new SqlCommand("proc_InsertPermission", connection, transaction))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@RoleId", mainMenu.RoleId);
                                    cmd.Parameters.AddWithValue("@MainMenuId", mainMenu.MainMenuId);
                                    cmd.Parameters.AddWithValue("@SubMenuId", 0); // Use 0 to represent no sub-menu
                                    cmd.Parameters.AddWithValue("@CanAccess", mainMenu.IsChecked ? 1 : 0);
                                    cmd.ExecuteNonQuery();
                                }

                                // Insert sub-menu items, if any
                                foreach (var subMenu in mainMenu.SubMenuItems)
                                {
                                    using (SqlCommand cmd = new SqlCommand("proc_InsertPermission", connection, transaction))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@RoleId", mainMenu.RoleId);
                                        cmd.Parameters.AddWithValue("@MainMenuId", mainMenu.MainMenuId);
                                        cmd.Parameters.AddWithValue("@SubMenuId", subMenu.SubMenuId);
                                        cmd.Parameters.AddWithValue("@CanAccess", subMenu.IsChecked ? 1 : 0);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Commit the transaction if all commands were successful
                            transaction.Commit();
                            return Json(new { success = true, message = "Selection saved successfully!" });
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if any error occurs
                            transaction.Rollback();
                            return Json(new { success = false, message = "An error occurred while saving selections: " + ex.Message });
                        }
                    }
                }
            }
        } 

    }
}
