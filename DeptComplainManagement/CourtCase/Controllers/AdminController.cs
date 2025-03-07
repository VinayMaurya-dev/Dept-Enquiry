using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourtCase.Models;
using CourtCase.DAL;
using CourtCases.Controllers;
using System.Data;
using CourtCase.Filters;
using System.Web.UI.WebControls;
using System.Diagnostics.CodeAnalysis;

namespace CourtCase.Controllers
{
    [SessionCheck]
    public class AdminController : BaseController
    {
        // GET: Admin
        DBLayer db = new DBLayer();
        SessionManager SM = new SessionManager();
        result Res = new result();
        FunctionsInCommon _cic = new FunctionsInCommon();
        public ActionResult Index()
        {
            return View();
        }
       
        #region [OM][21/06/24]
        // Zone Master
        public ActionResult CreateZone(int? ZoneId)
        {
            if (ZoneId != null) { Session["ZoneId"] = ZoneId; }

            Admin Objcls = new Admin();
            object msg = "";

            //if (Session["User_Number"] != null)
            //{
            ViewBag.BtnName = "Save";
            if (ZoneId.HasValue)
            {
                ViewBag.BtnName = "Update";
                Objcls.Action = "Edit";
                Objcls.ZoneId = Convert.ToInt32(ZoneId);
                DataTable dt = db.AddZone(Objcls);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Objcls.ZoneId = Convert.ToInt32(row["ZoneId"]);
                        Objcls.ZoneName = row["ZoneName"].ToString();

                    }
                }
            }
            Objcls.Action = "List";

            //try { Objcls.ZoneId = Convert.ToInt32(Session["ZoneId"]); } catch (Exception E1) { }

            DataTable Table = db.AddZone(Objcls);
            if (Table.Rows.Count > 0)
            {
                Objcls.Table = Table;
            } 
            else
            { msg = "Session is expired"; }

            return View(Objcls);
        }

        public JsonResult SaveUpdateZoneRecord(Admin obj)
        {
            Admin Objcls = obj;

            object msg = "";
            try
            {

                if (Objcls.ZoneId != 0)
                {
                    Objcls.Action = "Update";
                }
                else
                {
                    Objcls.Action = "Insert";
                }

                //if (Session["User_Number"] != null)
                //{
                Objcls.IP_Address = _cic.GetIP();
                Objcls.CreatedBy_User_Number = Convert.ToInt32(Session["UserNumber"]);
                try { Session["ZoneId"] = ViewBag.SelectedZoneId = Objcls.ZoneId; } catch (Exception E1) { }
                DataTable dt = db.AddZone(Objcls);
                if (dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                }

                else
                { msg = "Session is expired"; }

            }
            catch (Exception ex)
            {
                msg = "Something Went Wrong..";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
        // Circle Master

        public ActionResult CreateCircle(int? CircleId)
        {

            if (CircleId != null) { Session["CircleId"] = CircleId; }

            Circle Objcls = new Circle();
            object msg = "";

            //if (Session["User_Number"] != null)
            //{
            ViewBag.BtnName = "Save";
            if (CircleId.HasValue)
            {
                ViewBag.BtnName = "Update";
                Objcls.Action = "Edit";
                Objcls.CircleId = Convert.ToInt32(CircleId);
                DataTable dt = db.AddCircle(Objcls);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Objcls.CircleId = Convert.ToInt32(row["CircleId"]);
                        Objcls.CircleName = row["CircleName"].ToString();
                        Objcls.ZoneId = Convert.ToInt32(row["ZoneId"]);
                        TempData["ZoneId"] = Objcls.ZoneId;

                    }
                }
            }
            Objcls.Action = "List";



            DataTable Table = db.AddCircle(Objcls);
            if (Table.Rows.Count > 0)
            {
                Objcls.Table = Table;
            }




            else
            { msg = "Session is expired"; }

            return View(Objcls);
        }
        public JsonResult SaveUpdateCircleRecord(Circle obj)
        {
            Circle Objcls = obj;

            object msg = "";
            try
            {

                if (Objcls.CircleId != 0)
                {
                    Objcls.Action = "Update";

                }
                else
                {
                    Objcls.Action = "Insert";
                }

                //if (Session["User_Number"] != null)
                //{
                Objcls.IP_Address = _cic.GetIP();
                Objcls.CreatedBy_User_Number = Convert.ToInt32(Session["UserNumber"]);
                try { Session["CircleId"] = ViewBag.SelectedCircleId = Objcls.CircleId; } catch (Exception E1) { }
                DataTable dt = db.AddCircle(Objcls);
                if (dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                }

                else
                { msg = "Session is expired"; }

            }
            catch (Exception ex)
            {
                msg = "Something Went Wrong..";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
        // Division Master
        public ActionResult CreateDivision(int? DivisionId)
        {
            if (DivisionId != null) { Session["DivisionId"] = DivisionId; }

            Division Objcls = new Division();
            object msg = "";

            //if (Session["User_Number"] != null)
            //{
            ViewBag.BtnName = "Save";
            if (DivisionId.HasValue)
            {
                ViewBag.BtnName = "Update";
                Objcls.Action = "Edit";
                Objcls.DivisionId = Convert.ToInt32(DivisionId);
                DataTable dt = db.AddDivision(Objcls);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        Objcls.CircleId = Convert.ToInt32(row["CircleId"]);
                        Objcls.DivisionId = Convert.ToInt32(row["DivisionId"]);
                        Objcls.DivisionName = row["DivisionName"].ToString();
                        Objcls.ZoneId = Convert.ToInt32(row["ZoneId"]);
                        TempData["ZoneId"] = Objcls.ZoneId;
                        TempData["CircleId"] = Objcls.CircleId;
                    }
                }
            }
            Objcls.Action = "List";


            DataTable Table = db.AddDivision(Objcls);
            if (Table.Rows.Count > 0)
            {
                Objcls.Table = Table;
            }
            else
            { msg = "Session is expired"; }

            return View(Objcls);
        }
        public JsonResult SaveUpdateDivisionRecord(Division obj)
        {
            Division Objcls = obj;

            object msg = "";
            try
            {

                if (Objcls.DivisionId != 0)
                {
                    Objcls.Action = "Update";
                }
                else
                {
                    Objcls.Action = "Insert";
                }

                //if (Session["User_Number"] != null)
                //{
                Objcls.IP_Address = _cic.GetIP();
                Objcls.CreatedBy_User_Number = Convert.ToInt32(Session["UserNumber"]);
                try { Session["DivisionId"] = ViewBag.SelectedDivisionId = Objcls.DivisionId; } catch (Exception E1) { }
                DataTable dt = db.AddDivision(Objcls);
                if (dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                }

                else
                { msg = "Session is expired"; }

            }
            catch (Exception ex)
            {
                msg = "Something Went Wrong..";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
        // Designation Master
        public ActionResult CreateDesignation(int? DesignationId)
        {
            if (DesignationId != null) { Session["DesignationId"] = DesignationId; }

            Designation Objcls = new Designation();
            object msg = "";

            //if (Session["User_Number"] != null)
            //{
            ViewBag.BtnName = "Save";
            if (DesignationId.HasValue)
            {
                ViewBag.BtnName = "Update";
                Objcls.Action = "Edit";
                Objcls.DesignationId = Convert.ToInt32(DesignationId);
                DataTable dt = db.AddDesignation(Objcls);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        Objcls.DesignationId = Convert.ToInt32(row["DesignationId"]);

                        Objcls.DesignationName = row["DesignationName"].ToString();
                        Objcls.RoleTypeId = row["RoleTypeId"].ToString().Trim();
                        TempData["RoleTypeId"] = Objcls.RoleTypeId.Trim();


                    }
                }
            }
            Objcls.Action = "List";


            DataTable Table = db.AddDesignation(Objcls);
            if (Table.Rows.Count > 0)
            {
                Objcls.Table = Table;
            }
            else
            { msg = "Session is expired"; }

            return View(Objcls);
        }
        public JsonResult SaveUpdateDesignationRecord(Designation obj)
        {
            Designation Objcls = obj;

            object msg = "";
            try
            {

                if (Objcls.DesignationId != 0)
                {
                    Objcls.Action = "Update";
                }
                else
                {
                    Objcls.Action = "Insert";
                }

                //if (Session["User_Number"] != null)
                //{
                Objcls.IP_Address = _cic.GetIP();
                Objcls.CreatedBy_User_Number = Convert.ToInt32(Session["UserNumber"]);
                try { Session["DesignationId"] = ViewBag.SelectedDesignationId = Objcls.DesignationId; } catch (Exception E1) { }
                DataTable dt = db.AddDesignation(Objcls);
                if (dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                }

                else
                { msg = "Session is expired"; }

            }
            catch (Exception ex)
            {
                msg = "Something Went Wrong..";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
        // Subject Matter Master
        public ActionResult CreateSubjectMatter(int? subjectMatterId)
        {
            if (subjectMatterId != null) {
                Session["SubjectMatterId"] = subjectMatterId;
            }

            SubjectMaster Objcls = new SubjectMaster();
            object msg = "";

            //if (Session["User_Number"] != null)
            //{
            ViewBag.BtnName = "Save";
            if (subjectMatterId.HasValue)
            {
                ViewBag.BtnName = "Update";
                Objcls.Action = "Edit";
                Objcls.subjectMatterId = Convert.ToInt32(subjectMatterId);
                DataTable dt = db.AddSubjectMaster(Objcls);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        Objcls.subjectMatterId = Convert.ToInt32(row["subjectMatterId"]);

                        Objcls.subjectMatter = row["subjectMatter"].ToString();
                        Objcls.RelatedTo = row["RelatedTo"].ToString();
                        //TempData["RelatedTo"] = Objcls.RelatedTo;


                    }
                }
            }
            Objcls.Action = "List";


            DataTable Table = db.AddSubjectMaster(Objcls);
            if (Table.Rows.Count > 0)
            {
                Objcls.Table = Table;
            }
            else
            { msg = "Session is expired"; }

            return View(Objcls);
        }
        public JsonResult SaveUpdateSubjectMasterRecord(SubjectMaster obj)
        {
            SubjectMaster Objcls = obj;
            object msg = "";
            try
            {
                if (Objcls.subjectMatterId != 0)
                {
                    Objcls.Action = "Update";
                }
                else
                {
                    Objcls.Action = "Insert";
                }

                //if (Session["User_Number"] != null)
                //{
                Objcls.IP_Address = _cic.GetIP();
                Objcls.CreatedBy_User_Number = Convert.ToInt32(Session["UserNumber"]);
                try {
                    Session["SubjectMatterId"] = ViewBag.SelectedsubjectMatterId = Objcls.subjectMatterId; } catch (Exception E1) { }
                DataTable dt = db.AddSubjectMaster(Objcls);
                if (dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                }

                else
                { msg = "Session is expired"; }

            }
            catch (Exception ex)
            {
                msg = "Something Went Wrong..";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
		#endregion

		#region [User Login]
		[HttpGet]
		public ActionResult CreateCompUser()
		{
			return View();
		}
		[HttpPost]
		public ActionResult CreateCompUser(UserLogin model)
		{
			if (model.Id == null)
			{
				model.action = "AddUserLogin";
				model.CreatedBy = Session["UserNumber"].ToString();
				model.IPAddress = Encrypt.GetIp();
				var res = db.InserUpdateCompUserLogin(model);
				if (res != null)
				{

					TempData["Msg"] = res.First().Msg;

				}
				else
				{
					TempData["Msg"] = "Error";
				}
			}
			return RedirectToAction("CreateCompUser");
		}
		[HttpGet]
		public ActionResult CompUserLoginList()
		{
			UserLogin model = new UserLogin();
			if (model.Id == null)
			{
				model.action = "GetUserLoginList";
				var res = db.InserUpdateCompUserLogin(model);
				if (res != null)
				{
					ViewBag.list = res;
				}
			}
			return View();
		}
		#endregion

		#region  [Vinay 23-10-2024]

		public ActionResult MainMenu()
		{
			var menuItem = new MenuViewModel();
            menuItem.Action = "List";

			try
			{
				DataTable table = db.MainMenu(menuItem);

				if (table != null && table.Rows.Count > 0)
				{
					menuItem.Table = table;
				}

				return View(menuItem);
			}
			catch (Exception ex)
			{ 
				return RedirectToAction("Error", new { message = ex.Message });
			}
		}

		public ActionResult subMenu()
		{
			var menuItem = new MenuViewModel();
			menuItem.Action = "List";

            try
            {
                DataTable table = db.SubMenu(menuItem); 
                if (table != null && table.Rows.Count > 0)
				{
					menuItem.Table = table;
				}

				return View(menuItem);
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", new { message = ex.Message });
			}
		}
		public ActionResult Permissions()
		{ 
			ViewBag.MenuData = null;
			return View();
		}

        [HttpPost]
        public ActionResult Permissions(MenuViewModel model)
        {
            model.Action = "BindTreeMenu";
            TempData["RoleId"] = model.RoleId;
            DataSet menuData = db.GetDynamicMenu(model);
            ViewBag.MenuData = menuData;
            return View();
        }
        //[HttpPost]
        //public ActionResult Permissions(MenuViewModel model)
        //{
        //    model.Action = "BindTreeMenu";
        //    TempData["RoleId"] = model.RoleId;

        //    Retrieve all menu data

        //    DataSet menuData = db.GetDynamicMenu(model);

        //    if (menuData != null && menuData.Tables.Count >= 2)
        //    {
        //        Get main and sub-menu tables
        //       DataTable mainMenuTable = menuData.Tables[0];
        //        DataTable subMenuTable = menuData.Tables[1];

        //        Filter main menu for role - specific rows

        //       DataTable roleSpecificMainMenu = mainMenuTable.AsEnumerable()
        //           .Where(row => row["RoleId"].ToString() == model.RoleId.ToString())
        //           .CopyToDataTable();

        //       DataTable otherMainMenuRows = mainMenuTable.AsEnumerable()
        //           .Where(row => row["RoleId"].ToString() != model.RoleId.ToString())
        //           .CopyToDataTable();

        //        Filter sub-menu for role - specific rows

        //       DataTable roleSpecificSubMenu = subMenuTable.AsEnumerable()
        //           .Where(row => row["RoleId"].ToString() == model.RoleId.ToString())
        //           .CopyToDataTable();

        //       DataTable otherSubMenuRows = subMenuTable.AsEnumerable()
        //           .Where(row => row["RoleId"].ToString() != model.RoleId.ToString())
        //           .CopyToDataTable();

        //        Merge tables with role-specific rows first

        //        DataTable sortedMainMenuTable = roleSpecificMainMenu.Clone();
        //        sortedMainMenuTable.Merge(roleSpecificMainMenu);
        //        sortedMainMenuTable.Merge(otherMainMenuRows);

        //        DataTable sortedSubMenuTable = roleSpecificSubMenu.Clone();
        //        sortedSubMenuTable.Merge(roleSpecificSubMenu);
        //        sortedSubMenuTable.Merge(otherSubMenuRows);

        //        Replace original tables in dataset

        //        menuData.Tables.RemoveAt(0);
        //        menuData.Tables.RemoveAt(0); // Remove both tables
        //        menuData.Tables.Add(sortedMainMenuTable); // Add sorted main menu table
        //        menuData.Tables.Add(sortedSubMenuTable);  // Add sorted sub-menu table
        //    }

        //    ViewBag.MenuData = menuData;
        //    return View();
        //}



        public ActionResult MenuMasterUpdate(MenuViewModel model)
        {
            
            try
            {
				if (model.MainMenuId != 0)
				{
					model.Action = "Update";
				}
				else
				{
					model.Action = "Insert";
				}
				DataTable table = db.MainMenu(model);
                if (table.Rows.Count > 0)
                {
                    Res.SR = Convert.ToInt32(table.Rows[0]["Sr"]);
                    Res.Msg = table.Rows[0]["Msg"].ToString();

				}
                return Json(Res, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", new { message = ex.Message });
			} 
        }
		public ActionResult SubMenuMasterUpdate(MenuViewModel model)
		{

			try
			{
				if (model.SubMenuId != 0)
				{
					model.Action = "Update";
				}
				else
				{
					model.Action = "Insert";
				}
				DataTable table = db.SubMenu(model);
				if (table.Rows.Count > 0)
				{
					Res.SR = Convert.ToInt32(table.Rows[0]["Sr"]);
					Res.Msg = table.Rows[0]["Msg"].ToString();

				}
				return Json(Res, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", new { message = ex.Message });
			}
		}

        #endregion
        #region 02-01-2024
        public JsonResult UpdateUserProfile(UserLogin model)
        {
            if (Session["UserNumber"]?.ToString() != null)
            {
                model.action = "UpdateEmail";
                DataTable  dt= db.UpdateUserProfile(model);
                if (dt.Rows.Count > 0)
                {
                    Res.SR = Convert.ToInt32(dt.Rows[0]["Status"]);
                    Res.Msg = dt.Rows[0]["Msg"].ToString();
                }
            }
            return Json(Res, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}