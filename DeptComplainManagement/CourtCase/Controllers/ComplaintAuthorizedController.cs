using CourtCase.DAL;
using CourtCase.Filters;
using CourtCase.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CourtCase.Controllers
{
    [SessionCheck]
    public class ComplaintAuthorizedController : Controller
    {
        DBLayer db = new DBLayer();
        EmailService service = new EmailService();
        string Isemail = ConfigurationManager.AppSettings["IsEmailSend"];
        // GET: ComplaintAuthorized
        public ActionResult Index()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "MarkedComplaint";
            obj.AllotingOfficerUser_Number = Session["UserNumber"].ToString();
            return View(obj.getAuthorizedofficerSet());
        }

        public ActionResult RevisionEnquiryList()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "RevisionEnquiryList";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.getRevisionAuthorizedofficerSet());
        }

        public ActionResult AppealEnquiryList()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "AppealEnquiryList";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.getRevisionAuthorizedofficerSet());
        }
        public PartialViewResult _Notification()
        {

            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "MarkedComplaint";
            obj.DesignationId = Session["UserNumber"].ToString();
            return PartialView("_Notification", obj.getAuthorizedofficerSet());
        }
        [EncryptedActionParameter]
        public ActionResult ComplaintList(string ComplaintType = null)
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "selectcomplaintsforauthorizedofficer";
            obj.AllotingOfficerUser_Number = Session["UserNumber"].ToString();
            obj.ComplaintType = ComplaintType;
            return View(obj.getAuthorizedofficerSet());

        }
        [EncryptedActionParameter]
        public ActionResult ComplaintDetails(string ComplaintNo,string op)
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
            ViewBag.AppealOrRivision = op;

            return View(obj);
        } 

        [HttpPost]
        public ActionResult AddInvestigatingOfficer(ComplaintDetails obj)
        {
            string msg = "";
            obj.Action = "addinvestigatingofficer";

            ComplaintDetails obj1 = uploadfiles("InvestigatingOfficerLetter");
            if (obj1 == null)
            {
                TempData["msg"] = "uploadeeror";

                return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
            }
            else if (obj1.filepath != "nofileuploaded")
            {
                obj.InvestigatingOfficerLetter = obj1.filepath;
                obj.filetype = obj1.filetype;
            }
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            obj.save(out msg);

            if (msg != "inserted")
                msg = "error";

            TempData["msg"] = msg;

            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
         

        [HttpPost]
        public ActionResult AddRemark(ComplaintDetails obj)
        {
            string msg = "";
            obj.Action = "insertremark";
            obj.RemarkByDepartment = "Allotted Officer";
            obj.RemarkByDesignation = Session["DesignationId"].ToString();
            obj.ComplaintFinalStatus = "Replied";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();

            ComplaintDetails obj1 = uploadfiles("OtherComplaintDocument");
            if (obj1 == null)
            {
                TempData["msg"] = "uploadeeror";

                return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
            }
            else if (obj1.filepath != "nofileuploaded")
            {
                obj.OtherComplaintDocument = obj1.filepath;
                obj.filetype = obj1.filetype;
            }
            obj.save(out msg);

            if (msg != "inserted")
                msg = "error";

            TempData["msg"] = msg;

            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }

        #region Upload Files

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
                        string signedPath = "~/Uploads/Files/" + filetype + "_" + nameOfFile;






                        // fname = Path.Combine(Server.MapPath("~/signature/"), fname);
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

        public List<ComplaintDetails> Multipleuploadfiles(List<string> filetypes)
        {
            List<ComplaintDetails> uploadedFiles = new List<ComplaintDetails>();

            try
            {
                HttpFileCollectionBase files = Request.Files;


                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        string fname = file.FileName;
                        string extension = Path.GetExtension(fname);
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fname);
                        fname = fileNameWithoutExtension + extension;

                        string nameOfFile = fname.Replace("~", "-");

                        string signedPath = "~/Uploads/Files/" + filetypes[i] + "_" + nameOfFile;

                        file.SaveAs(Server.MapPath(signedPath));

                        ComplaintDetails obj = new ComplaintDetails();
                        obj.filepath = signedPath;
                        obj.filetype = filetypes[i];

                        uploadedFiles.Add(obj);

                        // Exit the loop after handling the first file for this file type
                        //break;
                    }
                }


                return uploadedFiles;
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        #endregion

        [HttpPost]
        public ActionResult AddForwardingOfficer(ComplaintDetails obj)
        {
            string msg = "";
            try
            {
                obj.Action = "addforwardingofficer";
                if (obj.AttachmentperusalDoc != null)
                {
                    string filename = Path.GetFileName(obj.AttachmentperusalDoc.FileName);
                    string uniqueFileName = "forward_" + Guid.NewGuid().ToString() + "_" + filename;
                    string uploadPath = Server.MapPath("~/Uploads/Files/");
                    string fullPath = Path.Combine(uploadPath, uniqueFileName);

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    obj.AttachmentperusalDoc.SaveAs(fullPath);
                    obj.Attachment = "~/Uploads/Files/" + uniqueFileName;
                }
                obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
                obj.save(out msg);
                if (msg == "inserted")
                {
                    DataTable dt = db.DesignationBind(obj);
                    string subject = $"Departmental Enquiry - Reference Number: {obj.ComplaintNo}";
                    string body = service.HigherEmailBody(dt.Rows[0]["DesignationName"].ToString(), obj.ComplaintNo);
                    if (Isemail == "Yes")
                    {
                        EmailService.SendEmail(dt.Rows[0]["EmailId"].ToString(), subject, body);
                    }
                }
                else
                {
                    msg = "error";
                }
                   
            }
            catch (Exception ex)
            {

            }

            TempData["msg"] = msg;

            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        [HttpPost]
        public ActionResult AddHearing(ComplaintDetails obj)
        {
            string msg = "";
            try
            {
                obj.Action = "UpdateStatus";
                if (obj.AttachmentperusalDoc != null)
                {
                    string filename = Path.GetFileName(obj.AttachmentperusalDoc.FileName);
                    string uniqueFileName = "HL_" + Guid.NewGuid().ToString() + "_" + filename;
                    string uploadPath = Server.MapPath("~/Uploads/Files/");
                    string fullPath = Path.Combine(uploadPath, uniqueFileName);

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    obj.AttachmentperusalDoc.SaveAs(fullPath);
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

                DataTable dataTable = db.AddHearing(obj);

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

        #region OM
        [HttpPost]
        public ActionResult Revision(ComplaintDetails obj, HttpPostedFileBase Attatchhment)
        {
            string msg = "";
            obj.Action = "RevisionInsert";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (Attatchhment != null && Attatchhment.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = "Revision" + Guid.NewGuid() + Path.GetExtension(Attatchhment.FileName);
                string filePath = Path.Combine(uploadPath, fileName);
                Attatchhment.SaveAs(filePath);
                obj.Attatchhment = "~/Uploads/Files/" + fileName;
            }
            else
            {
                obj.Attatchhment = null;
            }

            DataTable dataTable = db.InsertUpdateRevision(obj);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                msg = dataTable.Rows[0]["Msg"] != DBNull.Value ? dataTable.Rows[0]["Msg"].ToString() : "error";
            }
            else
            {
                msg = "error";
            }


            TempData["msg"] = msg;

            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion

        #region OM
        [HttpPost]
        public ActionResult Appeal(ComplaintDetails obj, HttpPostedFileBase Attatchhment)
        {
            string msg = "";
            obj.Action = "AppealInsert";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (Attatchhment != null && Attatchhment.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = "Appeal_" + Guid.NewGuid() + Path.GetExtension(Attatchhment.FileName);
                string filePath = Path.Combine(uploadPath, fileName);
                Attatchhment.SaveAs(filePath);
                obj.Attatchhment = "~/Uploads/Files/" + fileName;
            }
            else
            {
                obj.Attachment = null;
            }

            DataTable dataTable = db.InsertUpdateAppeal(obj);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                msg = dataTable.Rows[0]["Msg"] != DBNull.Value ? dataTable.Rows[0]["Msg"].ToString() : "error";
            }
            else
            {
                msg = "error";
            }


            TempData["msg"] = msg;

            return RedirectToAction("ComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion
        #region Om SelectCauseOfNotice

        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult CauseNoticeListData(string ShowcauseNumber)
        {
            ShowCauseNoticCls obj = new ShowCauseNoticCls();
            try
            {
                obj.ShowcauseNumber = ShowcauseNumber;
                obj.Action = "SelectCauseOfNotice";
                DataSet dataSet = db.ShowCauseInsertUpdateSelect(obj);
                if (dataSet.Tables[0] != null)
                {
                    ViewBag.data = dataSet;
                }

                obj.Action = "selectbySIDNo";

                DataSet ds = db.ShowCauseInsertUpdateSelect(obj);
                if (ds.Tables[0] != null)
                {
                    obj.ShowcauseNumber = (ds.Tables[0].Rows[0]["ShowcauseNumber"].ToString());
                    obj.IssuingAuthorityName = ds.Tables[0].Rows[0]["IssuingAuthorityName"].ToString();
                    obj.ComplainantName = ds.Tables[0].Rows[0]["ComplainantName"].ToString();
                    obj.NoticeRelatedTo = ds.Tables[0].Rows[0]["NoticeRelatedTo"].ToString();
                    obj.NoticeRelatedZoneName = ds.Tables[0].Rows[0]["NoticeRelatedZoneName"].ToString();
                    obj.NoticeRelatedCircleName = ds.Tables[0].Rows[0]["NoticeRelatedCircleName"].ToString();
                    obj.NoticeRelatedDivisionName = ds.Tables[0].Rows[0]["NoticeRelatedDivisionName"].ToString();
                    obj.NoticeRelatedDesignationName = ds.Tables[0].Rows[0]["NoticeRelatedDesignationName"].ToString();
                    obj.IssueletterNumber = ds.Tables[0].Rows[0]["IssueletterNumber"].ToString();
                    obj.IssueletterDate = ds.Tables[0].Rows[0]["IssueletterDate"].ToString();
                    obj.LastdateforSubmission = ds.Tables[0].Rows[0]["LastdateforSubmission"].ToString();
                    obj.NoticeDetails = ds.Tables[0].Rows[0]["NoticeDetails"].ToString();
                    obj.AttachmentPath = ds.Tables[0].Rows[0]["Attachment"].ToString();
                    obj.CurrentStatus = ds.Tables[0].Rows[0]["CurrentStatus"].ToString();
                }

                return View(obj);
            }
            catch (Exception ex)
            {

            }

            return View("CauseNoticeListData", obj);
        }

        public ActionResult ShowCauseNoticeList(ShowCauseNoticCls obj)
        {
            obj.Action = "HigherOfficerSelectCauseOfNoticeList";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            DataSet dataSet = db.ShowCauseInsertUpdateSelect(obj);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                obj.table = dataSet.Tables[0];
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult ShowAnushMarkDetails(ShowCauseNoticCls obj)
        {
            obj.Action = "InsertAnushmark";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (obj.AnushMarkAttachment != null)
            {
                string filename = Path.GetFileName(obj.AnushMarkAttachment.FileName);
                string uniqueFileName = "AnushMark_" + Guid.NewGuid().ToString() + "_" + filename;
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                string fullPath = Path.Combine(uploadPath, uniqueFileName);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                obj.AnushMarkAttachment.SaveAs(fullPath);
                obj.AttachmentPath = "~/Uploads/Files/" + uniqueFileName;
            }
            DataSet ds = db.ShowCauseInsertUpdateSelect(obj);
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "inserted")
                {
                    TempData["msg"] = "inserted";
                }
                else
                {
                    TempData["msg"] = "error";
                }
            }
            else
            {
                TempData["msg"] = "error";
            }

            return RedirectToAction("CauseNoticeListData", new { ShowcauseNumber = obj.ShowcauseNumber });
        }

        [HttpPost]
        public ActionResult ShowHigherOfficerDetails(ShowCauseNoticCls obj)
        {
            obj.Action = "HigherOfficerInsertForwordingOfficer";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            DataSet ds = db.ShowCauseInsertUpdateSelect(obj);
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "inserted")
                {
                    TempData["msg"] = "inserted";
                }
                else
                {
                    TempData["msg"] = "error";
                }
            }
            else
            {
                TempData["msg"] = "error";
            }

            return RedirectToAction("CauseNoticeListData", new { ShowcauseNumber = obj.ShowcauseNumber });
        }

        public ActionResult PendingShowCauseNoticeList(ShowCauseNoticCls obj)
        {
            obj.Action = "PendingShowCauseList";
            DataSet dataSet = db.ShowCauseInsertUpdateSelect(obj);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                obj.table = dataSet.Tables[0];
            }
            return View(obj);
        }

        #endregion


    }


}