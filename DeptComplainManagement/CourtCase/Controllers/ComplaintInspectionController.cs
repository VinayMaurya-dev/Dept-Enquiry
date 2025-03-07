using CourtCase.DAL;
using CourtCase.Filters;
using CourtCase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourtCase.Controllers
{
    [SessionCheck]
    public class ComplaintInspectionController : Controller
    {
        // GET: ComplaintInspection
        DBLayer dbo = new DBLayer();
        public ActionResult Index()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "MarkedComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.getInspectionDataSet());
        }
        [EncryptedActionParameter]
        public ActionResult ReceivedComplaints(string ComplaintType = null)
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "GetComplaintByType";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            obj.ComplaintType = ComplaintType;
            return View(obj.getInspectionDataSet());
         
        }
        [EncryptedActionParameter]
        public ActionResult ClosedComplaints(string ComplaintType = null)
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "GetClosedComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            obj.ComplaintType = ComplaintType;
            return View(obj.getInspectionDataSet());

        }
        
        [EncryptedActionParameter]
        public ActionResult ComplaintDetails(string ComplaintNo)
		{
			ComplaintDetails obj = new ComplaintDetails();
			ComplaintDetails obj1 = new ComplaintDetails();
			obj.Action = "selectbycomplaintno";
			obj.ComplaintNo = ComplaintNo;

			if (obj.getObject())
			{
				TempData["obj"] = obj;
			}

			obj1.ComplaintNo = ComplaintNo;
			obj1.Action = "selectall";

			ViewBag.Replies = obj1.getDataSet();

			return View(obj);
		}

        #region Om
        public ActionResult InspectionRemark(ComplaintDetails obj, HttpPostedFileBase OtherComplaintDocument)
        {
            string msg = "";
            obj.Action = "insertremark";
            obj.RemarkByDepartment = "Inspection";
            obj.RemarkByDesignation = Session["DesignationId"].ToString();
            obj.ComplaintFinalStatus = "Replied";
            obj.RemarkByUserNo = Session["UserNumber"].ToString();
            if (OtherComplaintDocument != null && OtherComplaintDocument.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = "Attachment_" + Guid.NewGuid() + Path.GetExtension(OtherComplaintDocument.FileName);
                string filePath = Path.Combine(uploadPath, fileName);
                OtherComplaintDocument.SaveAs(filePath);
                obj.OtherComplaintDocument = "~/Uploads/Files/" + fileName;
            }
            else
            {
                obj.OtherComplaintDocument = null;
            }

            obj.save(out msg);

            if (msg != "inserted")
                msg = "error";

            TempData["msg"] = msg;

            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion
        [HttpPost]
        public ActionResult Inspectionstatement(ComplaintDetails obj/*, string aaropRemarkData*/)
        {
            //var aaropRemarkList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComplaintDetails>>(aaropRemarkData);
            DataTable dt = new DataTable();
            string msg = "";
            obj.RemarkByDepartment = "Inspection";
            obj.RemarkByDesignation = Session["DesignationId"].ToString();
            obj.ComplaintFinalStatus = "Replied";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            ComplaintDetails obj1 = uploadfiles("OtherComplaintDocument");
            if (obj1 == null)
            {
                TempData["msg"] = "uploadeeror";

                return RedirectToAction("DeptViewInvestigationComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
            }
            else if (obj1.filepath != "nofileuploaded")
            {
                obj.OtherComplaintDocument = obj1.filepath;
                obj.filetype = obj1.filetype;
            }
            obj.Action = "insertstatement";
            obj.save(out msg);
            //using (var transaction = dbo.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        foreach (var mark in aaropRemarkList)
            //        {
            //            obj.Action = "UpdateAarop";
            //            obj.AaropRemark = mark.AaropRemark.ToString();
            //            obj.AaropID = mark.AaropID;
            //            dt = dbo.ComplaintAarop(obj);
            //            if (dt == null || dt.Rows.Count == 0)
            //            {
            //                throw new Exception("Error while inserting Aarop details.");
            //            }
            //        }
            //        obj.Action = "insertstatement";
            //        obj.save(out msg);
            //        transaction.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        transaction.Rollback();
            //        msg = "An error occurred: " + ex.Message;
            //    }
            //}

            if (msg != "inserted")
                msg = "error";

            TempData["msg"] = msg;

            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
     
        public ComplaintDetails uploadfiles(string filetype)
        {
            string fname; string msg = ""; string extension = "";
            ComplaintDetails obj = new ComplaintDetails();
            try
            {
                HttpFileCollectionBase files = Request.Files;

                if (files.Count > 0)
                {
                    // upload only first file
                    HttpPostedFileBase file = files[0];

                    if (file.ContentLength > 0)
                    {
                        string nameOfFile;


                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                            extension = Path.GetExtension(fname);
                            string fileName = Path.GetFileNameWithoutExtension(fname);
                            fname = fileName + extension;
                        }
                        nameOfFile = fname.Replace("~", "-");
                        Stream fileStream = file.InputStream;
                        int fileLength = file.ContentLength;
                        byte[] fileData = new byte[fileLength];
                        fileStream.Read(fileData, 0, fileLength);
                        string signedPath = "~/Uploads/Files/" + filetype + Guid.NewGuid() + "_" + nameOfFile;
                         
                        file.SaveAs(Server.MapPath(signedPath));
                        msg = "File Uploaded Successfully..!|1";
                        obj.filepath = signedPath;
                        obj.filetype = filetype;
                    }

                    else
                    {
                        obj.filepath = "nofileuploaded";
                        return obj;
                    }
                }
                //}

                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        [HttpPost]
        public ActionResult ReminderRepresentation(ComplaintDetails obj)
        {
            string msg = "";
            try
            { 
                obj.Action = "UpdateStatus"; 
                if (obj.AttatchmentDoc != null)
                {
                    string filename = Path.GetFileName(obj.AttatchmentDoc.FileName);
                    string uniqueFileName = "RR_" + Guid.NewGuid().ToString() + "_" + filename;
                    string uploadPath = Server.MapPath("~/Uploads/Files/");
                    string fullPath = Path.Combine(uploadPath, uniqueFileName); 
                  
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                     
                    obj.AttatchmentDoc.SaveAs(fullPath); 
                    obj.Attatchment = "~/Uploads/Files/" + uniqueFileName;
                }


                if (Session["UserNumber"] != null)
                {
                    obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
                }
                else
                {
                    TempData["msg"] = "User session has expired. Please log in again.";
                    return RedirectToAction("CompUserLogin", "Home");
                }

                DataTable dataTable = dbo.ReminderRepresentation(obj);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    msg = dataTable.Rows[0]["Msg"].ToString();
                }
                else
                {
                    msg = "No response received from the database.";
                }
            }
            catch (Exception ex)
            {
                msg = "error";
            }
            TempData["msg"] = msg;
            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        
    }
}