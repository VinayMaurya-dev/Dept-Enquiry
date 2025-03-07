using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourtCase.Models;
using CourtCase.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;

namespace CourtCase.Controllers
{ 
    public class HomeController : Controller
    { 
        DBLayer db = new DBLayer(); 
        public ActionResult LogOut()
        {     
                Session.Clear();
                return RedirectToAction("CompUserLogin", "Home");
        }

		[HttpGet]
        public ActionResult CompUserLogin()
        {
            UserLogin model = new UserLogin();
            BindChartData();
            return View(model);
        }
        

        public void BindChartData()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "BindChartData";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            int totalEnquiry = 0;
            int pendingEnquiry = 0;
            int closedEnquiry = 0;
            int level1 = 0;
            int level2 = 0;
            int level3 = 0;
            int level4 = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                totalEnquiry = Convert.ToInt32(row["TotalEnquiry"]);
                pendingEnquiry = Convert.ToInt32(row["PendingEnquiry"]);
                closedEnquiry = Convert.ToInt32(row["ClosedEnquiry"]);
                level1 = Convert.ToInt32(row["Level1"]);
                level2 = Convert.ToInt32(row["Level2"]);
                level3 = Convert.ToInt32(row["Level3"]);
                level4 = Convert.ToInt32(row["Level4"]);
            }
            ViewBag.TotalEnquiry = totalEnquiry;
            ViewBag.PendingEnquiry = pendingEnquiry;
            ViewBag.ClosedEnquiry = closedEnquiry;
            ViewBag.Level1 = level1;
            ViewBag.Level2 = level2;
            ViewBag.Level3 = level3;
            ViewBag.Level4 = level4;
        }

        [HttpPost]
		public ActionResult CompUserLogin(UserLogin model)
		{
            BindChartData(); 
            if (ModelState.IsValid)
			{ 
				try
				{
                    if (Session["CaptchaCode"].ToString() == model.CaptchaCode)
                    {
                        model.action = "CheckUserLogin";
                        var res = db.InserUpdateCompUserLogin(model);
                        if (res != null)
                        {
                            if (res.First().Status != "0")
                            {
                                Session["UserNumber"] = res.First().UserNumber;
                                Session["UserId"] = res.First().UserId;
                                Session["RoleId"] = res.First().RoleId;
                                Session["DesignationId"] = res.First().DesignationId;
                                Session["ZoneId"] = res.First().ZoneId;
                                Session["CircleId"] = res.First().CircleId;
                                Session["DivisionId"] = res.First().DivisionId;
                                Session["MobileNo"] = res.First().MobileNo;
                                Session["EmailId"] = res.First().EmailId;
                                Session["LastLogin"] = res.First().LastLogin;
                                Session["DesignationName"] = res.First().DesignationName;
                                Session["RoleName"] = res.First().RoleName;
                                Session["ZoneName"] = res.First().ZoneName;
                                Session["CircleName"] = res.First().CircleName;
                                Session["DivisionName"] = res.First().DivisionName;
                                Session["Status"] = res.First().Status;
                                Session["Name"] = res.First().Name;
                                Session["UserRelatedTo"] = res.First().UserRelatedTo;
                                if (res.First().RoleId.Trim() == "AD")
                                {
                                    return RedirectToAction("Index", "Admin");
                                }
                                else if (res.First().RoleId.Trim() == "HO")
                                {
                                    return RedirectToAction("Index", "ComplaintAuthorized");

                                }
                                else if (res.First().RoleId.Trim() == "IO")
                                {
                                    return RedirectToAction("Index", "ComplaintInspection");
                                }
                                else if (res.First().RoleId.Trim() == "HO,IO")
                                {
                                    return RedirectToAction("Index", "CHMD");
                                }
                                else if (res.First().RoleId.Trim() == "SE")
                                {
                                    return RedirectToAction("Index", "ComplaintSE");
                                } 
                            }
                            else
                            {
                                ViewData["ErrorMsg"] = res.First().Msg;
                            }
                        }
                        else
                        {
                            ViewData["ErrorMsg"] = res.First().Msg;
                        }
                    }
                    else
                    {
                        ViewData["ErrorMsg"] = "Captcha code was not match.";
                    }
                }
				catch (Exception)
				{
                    ViewData["ErrorMsg"] = "An error occurred while processing your request.";
				}
			}
			else
			{
                ViewData["ErrorMsg"] = "Please Enter username or password !";
			}
			return View(model);
		}

      
        public ActionResult pagenotfound()
        {
            return View();
        }
        public ActionResult SessionExpired()
        {
            return View();
        }
      
        public ActionResult ChangePassword()
        {
            return PartialView("Partial/_ChangePassword");
        }
        public ActionResult CheckPassword(ChangePassword model)
        {
            string Msg = "";
            model.action = "CheckOldPassword";
            model.UserNumber = Session["UserNumber"].ToString();
            DataTable dt = db.ValidatePassword(model);
            if (dt.Rows.Count > 0 && dt != null)
            {
                Msg = "Success";
            }
            else
            {
                Msg = "Error";
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword model)
        {
            string Msg = "", Sr = "";
            model.action = "UpdatePassword";
            model.UserNumber = Session["UserNumber"].ToString();
            DataTable dt = db.ValidatePassword(model);
            if (dt.Rows.Count > 0 && dt != null)
            {
                Msg = dt.Rows[0]["Msg"].ToString();
                Sr = dt.Rows[0]["Sr"].ToString();
            }
            return Json(new { Msg = Msg, Sr = Sr }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerateOTP(UserLogin model)
        {
            BindChartData();
            if (!string.IsNullOrEmpty(model.MobileNo))
            {
                model.action = "Generate";
                DataTable dt = db.GenerateValidateOTP(model);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "Success")
                    {
                        model.Message = "Success";
                        model.OTP = dt.Rows[0]["OTP"].ToString();
                        model.IsOTPGenerated = true;
                        SendSMS(model.MobileNo, model.OTP);
                    }
                    else if (dt.Rows[0]["Status"].ToString() == "Failed")
                    {
                        model.Message = "Falied"; 
                        model.IsOTPGenerated = false; 
                    }
                    else if (dt.Rows[0]["Status"].ToString() == "TimeFailed")
                    {
                        model.Message = "TimeFalied";
                        model.IsOTPGenerated = false;
                    } 
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        } 
        public  string SendSMS(string MobileNo,string OTP)
        {
            DBLayer db = new DBLayer();
            string smsoff = string.Empty;
            try
            {
                var time = "5 Minutes";
                string SmsBody = $"Welcome to Departmental Enquiry System. Your OTP for login is {OTP}. It will expire after {time}. Do not share it with anyone. UTTAR PRADESH JAL NIGAM";

                string sender_Id = "UPJNRL";
                string template_id = "1007594658954821503"; 
                string Url = "http://164.52.195.161/API/SendMsg.aspx?uname=20250018&pass=jSTuV99F";

                Url += "&send=" + sender_Id + "&dest=" + MobileNo + "&template_id=" + template_id;
                Url += "&msg=" + SmsBody + "";
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(Url.Trim());
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                StreamReader reader1 = new StreamReader(response1.GetResponseStream());
                string result1 = reader1.ReadToEnd();
                return result1;

            }
            catch (Exception)
            {
                return "E";
            }
        }
        [HttpPost]
        public ActionResult VerifyOtp(UserLogin model)
        {
            try
            { 
                model.OTP = string.Join("", model.otp_input_1, model.otp_input_2, model.otp_input_3,model.otp_input_4, model.otp_input_5, model.otp_input_6);
                model.action = "Verify";  
                DataTable dt = db.GenerateValidateOTP(model); 

                // Check if the DataTable is not null and has rows
                if (dt != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    if (row["Status"].ToString() == "Success")
                    {    // Map the data row to a strongly-typed object
                        var res = new
                        {
                            UserNumber = row["UserNumber"]?.ToString(),
                            UserId = row["UserId"]?.ToString(),
                            RoleId = row["RoleId"]?.ToString(),
                            DesignationId = row["DesignationId"]?.ToString(),
                            ZoneId = row["ZoneId"]?.ToString(),
                            CircleId = row["CircleId"]?.ToString(),
                            DivisionId = row["DivisionId"]?.ToString(),
                            MobileNo = row["MobileNo"]?.ToString(),
                            EmailId = row["EmailId"]?.ToString(),
                            LastLogin = row["LastLogin"]?.ToString(),
                            DesignationName = row["DesignationName"]?.ToString(),
                            RoleName = row["RoleName"]?.ToString(),
                            ZoneName = row["ZoneName"]?.ToString(),
                            CircleName = row["CircleName"]?.ToString(),
                            DivisionName = row["DivisionName"]?.ToString(),
                            Status = row["Status"]?.ToString(),
                            Name = row["Name"]?.ToString(),
                            UserRelatedTo = row["UserRelatedTo"]?.ToString()
                        };
                        // Create session variables for user details
                        Session["UserNumber"] = res.UserNumber;
                        Session["UserId"] = res.UserId;
                        Session["RoleId"] = res.RoleId;
                        Session["DesignationId"] = res.DesignationId;
                        Session["ZoneId"] = res.ZoneId;
                        Session["CircleId"] = res.CircleId;
                        Session["DivisionId"] = res.DivisionId;
                        Session["MobileNo"] = res.MobileNo;
                        Session["EmailId"] = res.EmailId;
                        Session["LastLogin"] = res.LastLogin;
                        Session["DesignationName"] = res.DesignationName;
                        Session["RoleName"] = res.RoleName;
                        Session["ZoneName"] = res.ZoneName;
                        Session["CircleName"] = res.CircleName;
                        Session["DivisionName"] = res.DivisionName;
                        Session["Status"] = res.Status;
                        Session["Name"] = res.Name;
                        Session["UserRelatedTo"] = res.UserRelatedTo;
                        if (!string.IsNullOrEmpty(res.RoleId))
                        { 
                            switch (res.RoleId)
                            {
                                case "AD":
                                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Admin") }, JsonRequestBehavior.AllowGet);
                                case "HO":
                                    return Json(new { success = true, redirectUrl = Url.Action("Index", "ComplaintAuthorized") }, JsonRequestBehavior.AllowGet);
                                case "IO":
                                    return Json(new { success = true, redirectUrl = Url.Action("Index", "ComplaintInspection") }, JsonRequestBehavior.AllowGet);
                                case "HO,IO":
                                    return Json(new { success = true, redirectUrl = Url.Action("Index", "CHMD") }, JsonRequestBehavior.AllowGet);
                                case "SE":
                                    return Json(new { success = true, redirectUrl = Url.Action("Index", "ComplaintSE") }, JsonRequestBehavior.AllowGet);
                                default:
                                    return Json(new { success = true, redirectUrl = Url.Action("UserLogin", "Home") }, JsonRequestBehavior.AllowGet);

                            }
                        }
                    }
                    else
                    {
                        model.Message = "Failed to verify OTP.";
                    }
                }
                else
                {
                    model.Message = "Invalid OTP or no data returned.";
                }
            }
            catch (Exception ex)
            { 
                return Json(new { success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            } 
            return Json(new { success = false, Message=model.Message}, JsonRequestBehavior.AllowGet);
        }

    }
}