using CourtCase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourtCase.Filters;
using CourtCase.DAL;
using CourtCases.Controllers;
using System.Data.SqlClient;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Net.Mail;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;
using System.CodeDom.Compiler;
using System.Configuration;

namespace CourtCase.Controllers
{
    [SessionCheck]
    public class ComplaintSEController : Controller
    {
        string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        string Isemail = ConfigurationManager.AppSettings["IsEmailSend"];
        // GET: ComplaintSE
        DBLayer db = new DBLayer();
        FunctionsInCommon _cic = new FunctionsInCommon();
        EmailService service = new EmailService();
        public ActionResult Index()
        {
            try
            {
                ComplaintDetails obj = new ComplaintDetails();
                obj.Action = "TotalRegisteredComplaint";
                obj.CreatedBy_User_Number = Session["UserNumber"].ToString();

                // Fetch dashboard data
                var dashboardData = obj.GetDashboardData();
                return View(dashboardData);
            }
            catch (Exception ex)
            {
                // Handle error by displaying the custom HTML error page
                var errorHtml = GenerateErrorHtml(ex, "ComplaintSE/index");
                return Content(errorHtml, "text/html");
            }
        }

        private string GenerateErrorHtml(Exception ex, string requestedUrl)
        {
            return $@"
        <!DOCTYPE html>
        <html>
            <head>
                <title>The resource cannot be found.</title>
                <meta name='viewport' content='width=device-width' />
                <style>
                    body {{font-family:'Verdana';font-weight:normal;font-size: .7em;color:black;}} 
                    p {{font-family:'Verdana';font-weight:normal;color:black;margin-top: -5px;}}
                    b {{font-family:'Verdana';font-weight:bold;color:black;margin-top: -5px;}}
                    H1 {{ font-family:'Verdana';font-weight:normal;font-size:18pt;color:red; }}
                    H2 {{ font-family:'Verdana';font-weight:normal;font-size:14pt;color:maroon; }}
                    pre {{font-family:'Consolas','Lucida Console',Monospace;font-size:11pt;margin:0;padding:0.5em;line-height:14pt;}}
                    .marker {{font-weight: bold; color: black;text-decoration: none;}}
                    .version {{color: gray;}}
                    .error {{margin-bottom: 10px;}}
                    .expandable {{ text-decoration:underline; font-weight:bold; color:navy; cursor:pointer; }}
                    @media screen and (max-width: 639px) {{
                        pre {{ width: 440px; overflow: auto; white-space: pre-wrap; word-wrap: break-word; }}
                    }}
                    @media screen and (max-width: 479px) {{
                        pre {{ width: 280px; }}
                    }}
                </style>
            </head>
            <body bgcolor='white'>
                <span>
                    <H1>Server Error in '/' Application.<hr width=100% size=1 color=silver></H1>
                    <h2><i>The resource cannot be found.</i></h2>
                </span>
                <font face='Arial, Helvetica, Geneva, SunSans-Regular, sans-serif '>
                    <b>Description:</b> An error occurred: {ex.Message}<br><br>
                    <b>Requested URL:</b> {requestedUrl}<br><br>
                    <hr width=100% size=1 color=silver>
                    <b>Version Information:</b> Microsoft .NET Framework Version:4.0.30319; ASP.NET Version:4.8.4494.0
                </font>
            </body>
        </html>";
        }
         
        public PartialViewResult _Notification()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return PartialView("_Notification", obj.GetDashboardData());
        }
        public ActionResult DepartmentalDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.GetDashboardData());
        }

        #region OM
        public ActionResult CourtCaseDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.GetDashboardData());
        }

        public ActionResult InvestigationDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.GetDashboardData());
        }
        #endregion
        public ActionResult RegisterComplaint()
        {
            return View();
        }


        #region OM
        [HttpPost]
        public ActionResult OperatorRemark(ComplaintDetails obj, HttpPostedFileBase OtherComplaintDocument)
        {
            string msg = "";
            obj.Action = "insertremark";
            obj.RemarkByDepartment = "Operator";
            obj.RemarkByDesignation = Session["DesignationId"].ToString();
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

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion
        [HttpPost]
        public ActionResult AddFinalStatus(ComplaintDetails obj)
        {
            string msg = "";
            try
            {
                // Set the action for the object
                obj.Action = "AddFinalStatus";

                // Handle file upload if the attachment exists
                if (obj.FinalAttatchmentDoc != null)
                {
                    string filename = Path.GetFileName(obj.FinalAttatchmentDoc.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                    string uploadPath = Server.MapPath("~/Uploads/Files/");
                    string fullPath = Path.Combine(uploadPath, uniqueFileName);

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Save the file
                    obj.FinalAttatchmentDoc.SaveAs(fullPath);

                    // Save the relative path to the object
                    obj.FinalAttatchment = "~/Uploads/Files/" + uniqueFileName;
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

                DataTable dataTable = db.AddFinalStatus(obj);

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
            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }

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
                    DataTable dt=db.DesignationBind(obj);
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

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }

        [HttpPost]
        public ActionResult ComplaintMarkToOfficer(ComplaintDetails obj)
        {
            string msg = "";
            obj.Action = "complaintmarktoofficer";

            obj.save(out msg);

            if (msg != "inserted")
                msg = "error";

            TempData["msg"] = msg;

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }


        [HttpPost]

        #region Upload Files

        public ComplaintDetail uploadDocfiles(string filetype)
        {
            string fname; string msg = ""; string extension = "";
            ComplaintDetail obj = new ComplaintDetail();
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
        #region[Remark History]
        public ActionResult ComplaintRemarkList()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "RemarkHistory";
            return View(obj.getDataSet());
        }
        [HttpPost]
        public ActionResult ComplaintRemarkList(ComplaintDetails model)
        {
            model.Action = "RemarkHistory";
            return View(model.getDataSet());
        }

        private List<Dictionary<string, object>> ConvertDataTableToList(DataSet dataSet)
        {
            var list = new List<Dictionary<string, object>>();
            if (dataSet.Tables.Count > 0)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn column in table.Columns)
                    {
                        dict[column.ColumnName] = row[column];
                    }
                    list.Add(dict);
                }
            }
            return list;
        }
        [EncryptedActionParameter]
        public ActionResult ViewComplaintList(string ComplaintNo = null)
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
        #endregion



        #region[Departmental Complaint]
        public ActionResult DepartmentalComplaint()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmentalComplaint(ComplaintDetails obj)
        {
            string msg = "";
            obj.Action = "insertcomplaint"; 
            try
            {
                ComplaintDetails obj1 = uploadfiles("ComplaintAttachment");

                if (obj1 == null)
                {
                    TempData["msg"] = "uploaderror";
                    return RedirectToAction("DepartmentalComplaint");
                }

                if (!string.IsNullOrEmpty(obj1.filepath) && obj1.filepath != "nofileuploaded")
                {
                    obj.Attachment = obj1.filepath;
                    obj.filetype = obj1.filetype;
                } 
                obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
                obj.IPAddress = _cic.GetIP();
                obj.save(out msg);

                if (msg == "error")
                {
                    TempData["msg"] = "error";
                }
                else
                {
                   // Get from config
                    string url = $"{baseUrl}/User/ViewEnquiry?ComplaintNo={UrlEncoding.EncryptURL(msg)}"; 
                    string qrCodePath = SaveQRCode(QRCodeHelper.GenerateQRCode(url), msg); 
                    string subject = $"Departmental Enquiry Initiated - Reference Number: {msg}";

                    // Ensure QR Code URL is properly formatted
                    string qrCodeUrl = $"{baseUrl}{qrCodePath.TrimStart('~')}";
                    string body = service.ComplainantEmailBody(obj.ComplainantName, msg, qrCodeUrl);
                    string smsbody = service.ComplainantSMSbody(msg);
                    if (Isemail == "Yes")
                    {
                        EmailService.SendEmail(obj.EmailId, subject, body);
                        EmailService.SendHindiSMS(obj.MobileNo, smsbody);
                    }
                   
                    TempData["msg"] = "inserted";
                }
            }
            catch (Exception ex)
            { 
                TempData["msg"] = "error";
            }

            return RedirectToAction("DepartmentalComplaint");
        } 
        public string SaveQRCode(string base64QRCode, string ComplaintNo)
        {
            try
            { 
                byte[] imageBytes = Convert.FromBase64String(base64QRCode.Split(',')[1]);
                 
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", "QR"); 
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                } 
                string fileName = "QR_" + ComplaintNo + ".png";
                string filePath = Path.Combine(folderPath, fileName);  
                System.IO.File.WriteAllBytes(filePath, imageBytes); 
                string relativePath = "~/Uploads/QR/" + fileName; 
                return relativePath;
            }
            catch (Exception ex)
            { 
                return null;
            }
        }
       
        public ActionResult DepartmentalComplaintsList(string ComplaintType = null)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "SelectDepartmentalComplaintList";
            obj.EnquiryType = ComplaintType;
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult DepartmentalComplaintDetails(string ComplaintNo = null) 
        {
            ComplaintDetails obj = new ComplaintDetails();
            try
            { 
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
            catch (Exception ex)
            {

            }

            return View("~/View/Shared/Partial/_DataSetPartial.cshtml", obj);
        }
        public ActionResult DepartmentalComplaintMarkToOfficer(DepartmentalComplaint obj)
        {
            string msg = "";
            obj.Action = "complaintmarktoofficer";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            msg = ds.Tables[0].Rows[0]["msg"].ToString();
            if (msg != "inserted")
                msg = "error";

            TempData["msg"] = msg;

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNO });
        }
        [EncryptedActionParameter]
        public ActionResult DepartmentalTypeComplaints(string ComplaintType = null)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            ViewBag.SelectedEnquiryType = ComplaintType;
            obj.Action = "GetDepartmentalTypeComplaints";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.EnquiryType = ComplaintType;
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        } 
        public ActionResult PendingDepartmentalComplaints()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.GetDashboardData());
        }
        #endregion 
        public AdverseEntry Attachment(string filetype)
        {
            string fname; string msg = ""; string extension = "";
            AdverseEntry obj = new AdverseEntry();
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


                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        } 
        public ActionResult EnquiryOfficers()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEnquiryOfficer";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        } 
        public ActionResult DepartmentalEnquiryConcludedUpload()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetDepartmentalEnquiryConcludedUpload";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }

        public ActionResult PendencyEnquiryOfficers()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendencyEnquiryOfficers";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }

        #region Om
         
        [HttpPost]
        public JsonResult AddComplaintdAarop(string AaropDescriptions, string ComplaintNumber, string ChargeSheetNumber, string DateOfIssueChargeSheet, int NumberOfCharges, HttpPostedFileBase ChargeSheetAttachmentDoc, string ComplainantName, string InspEmailId)
        {
            var aaropDescriptionsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComplaintDetails>>(AaropDescriptions);

            string msg = "";
            DataTable dt = new DataTable();
            var complaintDetails = new ComplaintDetails
            {
                Action = "Insert",
                ComplaintNo = ComplaintNumber,
                ChargeSheetNumber = ChargeSheetNumber,
                DateOfIssueChargeSheet = DateOfIssueChargeSheet,
                NumberofCharges = NumberOfCharges
            };

            // Handle file upload
            if (ChargeSheetAttachmentDoc != null && ChargeSheetAttachmentDoc.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = "Aarop_" + Guid.NewGuid() + Path.GetExtension(ChargeSheetAttachmentDoc.FileName);
                string filePath = Path.Combine(uploadPath, fileName);
                ChargeSheetAttachmentDoc.SaveAs(filePath);
                complaintDetails.ChargeSheetAttachment = "~/Uploads/Files/" + fileName;
            }
            else
            {
                complaintDetails.ChargeSheetAttachment = null;
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var description in aaropDescriptionsList)
                    {
                        complaintDetails.ComplaintDescription = description.AaropDescription.ToString();

                        dt = db.ComplaintAarop(complaintDetails);
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            throw new Exception("Error while inserting Aarop details.");
                        }
                    } 
                    transaction.Commit();

                    string subject = $"Departmental Enquiry - Reference Number: {ComplaintNumber}"; 
                    string body = service.InspectionEmailBody(ComplainantName, ComplaintNumber);
                    if (Isemail == "Yes")
                    {
                        EmailService.SendEmail(InspEmailId, subject, body);
                    }

                    msg = dt.Rows[0]["MSG"].ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    msg = "An error occurred: " + ex.Message;
                }
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region[16-12-2024]
        public ActionResult PendingComplaintAtEnquiryOfficer()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaintAtEnquiryOfficer";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);  
        }

        [EncryptedActionParameter]
        public ActionResult PendingComplaintAtEnquiryOfficerById(int Id = 0, string op = "", string InvestigatingOfficerRelatedTo = "", int InvestigatingOfficerZoneId = 0, int InvestigatingOfficerCircleId = 0, int InvestigatingOfficerDivisionId = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetPendingComplaintAtEnquiryOfficerById",
                CreatedBy = Session["UserNumber"].ToString(),
                Id = Id,
                OptionalPr = op,
                ZoneId = InvestigatingOfficerZoneId,
                CircleId = InvestigatingOfficerCircleId,
                DivisionId = InvestigatingOfficerDivisionId
            };
            ViewBag.op = op;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult ChargeSheetWiseComplaints()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetChargeSheet";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult ChargeSheetWiseComplaintsList(string ComplaintNO)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.ComplaintNO = ComplaintNO;
            obj.Action = "GetChargeSheetList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult Financial_NonfinancialWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetFinancialWiseComplaintList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            ViewBag.Id= Id;
            return View(ds);
        }
        public ActionResult PendingRemarkatHigherOfficer()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaint_HO";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult EmployeeWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeWiseComplaint";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult EmployeeWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeWiseComplaintByLevelId";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult PendingRemarkatHigherOfficerById(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaint_HOById";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        #endregion
        public ActionResult ClosedEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "ClosedEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        [HttpPost]
        public ActionResult ForWardToInvestigationOfficer(ComplaintDetails obj)
        {
            string msg = "";
            try
            {
                obj.Action = "MarkToInvestigationOfficer";
                if (obj.AttatchmentDoc != null)
                {
                    string filename = Path.GetFileName(obj.AttatchmentDoc.FileName);
                    string uniqueFileName = "InvMark_" + Guid.NewGuid().ToString() + "_" + filename;
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

                DataTable dataTable = db.ReminderRepresentation(obj);

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
            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }

        public ActionResult DesignationWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetDesignationWiseComplaint";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult DesignationWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeComplaintByDesignationId";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        public ActionResult SuspensionDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            return View(obj.GetDashboardData());
        }

        public ActionResult SuspensionLevelWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionLevelWiseComplaint";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult SuspensionDesignationWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionDesignationWiseComplaint";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult SuspensionLevelWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionComplaintByLevelId";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult SuspensionDesignationWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionEmployeeComplaintByDesignationId";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        public ActionResult SuspensionAttachedOfficeWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionAttachedOfficeWiseComplaint";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult SuspensionAttachedOfficeWiseComplaintList(string RoleId)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionEmployeeComplaintByAttachedOffice";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.RoleId = RoleId;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        public ActionResult PendingComplaintsBasedontheDuration()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendingComplaintsBasedontheDurationAfterInspection";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult PendingMonthlyWiseComplaintsBasedontheDuration(int Id = 0, string op = "")
        {
            var complaint = new DepartmentalComplaint
            {
                Action = "PendingMonthlyWiseComplaintsBasedontheDuration",
                CreatedBy = Session["UserNumber"].ToString(),
                Id = Id,
                OptionalPr = op
            };
            ViewBag.op = op;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(complaint);
            return View(ds);
        }


        #region  [06-01-2024]

        public ActionResult RevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "RevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        public ActionResult AppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "AppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
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

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion
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
                obj.Attachment = null;
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

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion
        #region OM
        [HttpPost]
        public ActionResult AddFinalRevisinAppealStatus(ComplaintDetails obj, HttpPostedFileBase FinalAttatchment)
        {
            string msg = "";
            obj.Action = "InsertRevisionFinalResult";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (FinalAttatchment != null && FinalAttatchment.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = "Revision" + Guid.NewGuid() + Path.GetExtension(FinalAttatchment.FileName);
                string filePath = Path.Combine(uploadPath, fileName);
                FinalAttatchment.SaveAs(filePath);
                obj.FinalAttatchment = "~/Uploads/Files/" + fileName;
            }
            else
            {
                obj.FinalAttatchment = null;
            }

            DataTable dataTable = db.AddFinalRevAppStatus(obj);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                msg = dataTable.Rows[0]["Msg"] != DBNull.Value ? dataTable.Rows[0]["Msg"].ToString() : "error";
            }
            else
            {
                msg = "error";
            }


            TempData["msg"] = msg;

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion

        #region OM
        [HttpPost]
        public ActionResult AddAppealFinalResultStatus(ComplaintDetails obj, HttpPostedFileBase FinalAttatchment)
        {
            string msg = "";
            obj.Action = "InsertAppealFinalResult";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (FinalAttatchment != null && FinalAttatchment.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = "Revision" + Guid.NewGuid() + Path.GetExtension(FinalAttatchment.FileName);
                string filePath = Path.Combine(uploadPath, fileName);
                FinalAttatchment.SaveAs(filePath);
                obj.FinalAttatchment = "~/Uploads/Files/" + fileName;
            }
            else
            {
                obj.Attachment = null;
            }

            DataTable dataTable = db.AddFinalRevAppStatus(obj);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                msg = dataTable.Rows[0]["Msg"] != DBNull.Value ? dataTable.Rows[0]["Msg"].ToString() : "error";
            }
            else
            {
                msg = "error";
            }


            TempData["msg"] = msg;

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion

        #region OM
        [HttpPost]
        public ActionResult ADDCourtCase(ComplaintDetails obj)
        {
            string msg = "";
            obj.Action = "InsertCourt";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();

            if (obj.FinalAttatchmentDoc != null)
            {
                string filename = Path.GetFileName(obj.FinalAttatchmentDoc.FileName);
                string uniqueFileName = "courtCase_"+Guid.NewGuid().ToString() + "_" + filename;
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                string fullPath = Path.Combine(uploadPath, uniqueFileName);

                // Ensure the directory exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Save the file
                obj.FinalAttatchmentDoc.SaveAs(fullPath);

                // Save the relative path to the object
                obj.Attachment = "~/Uploads/Files/" + uniqueFileName;
            }

            DataTable dataTable = db.Addcourtcaseinsert(obj);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                msg = dataTable.Rows[0]["Msg"] != DBNull.Value ? dataTable.Rows[0]["Msg"].ToString() : "error";
            }
            else
            {
                msg = "error";
            }


            TempData["msg"] = msg;

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        [HttpPost]
        public ActionResult ADDInvestigation(ComplaintDetails obj)
        {
            string msg = "";
            obj.Action = "InsertInvestigation";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            DataTable dataTable = db.Addcourtcaseinsert(obj);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                msg = dataTable.Rows[0]["Msg"] != DBNull.Value ? dataTable.Rows[0]["Msg"].ToString() : "error";
            }
            else
            {
                msg = "error";
            }
            TempData["msg"] = msg;

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }
        #endregion

        #region Om
        [EncryptedActionParameter]
        public ActionResult CourtCaseLucknow(int CourtId=0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "SelectCourtCaseLucknow";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.CourtId = CourtId;
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult InvestigationDetails(int InvestigationId  = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "SelectInvestigationDetails";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.InvestigationId = InvestigationId;
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }


        #endregion

        #region OM
       
        [HttpPost]
        public ActionResult ADDContemtCase(ComplaintDetails obj)
        {
            string msg = "";
            obj.Action = "InsertCourt";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (obj.FinalAttatchmentDoc != null)
            {
                string filename = Path.GetFileName(obj.FinalAttatchmentDoc.FileName);
                string uniqueFileName = "contemptCase_" + Guid.NewGuid().ToString() + "_" + filename;
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                string fullPath = Path.Combine(uploadPath, uniqueFileName);

                // Ensure the directory exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Save the file
                obj.FinalAttatchmentDoc.SaveAs(fullPath);

                // Save the relative path to the object
                obj.Attachment = "~/Uploads/Files/" + uniqueFileName;
            }

            DataTable dataTable = db.Addcourtcaseinsert(obj);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                msg = dataTable.Rows[0]["Msg"] != DBNull.Value ? dataTable.Rows[0]["Msg"].ToString() : "error";
            }
            else
            {
                msg = "error";
            }


            TempData["msg"] = msg;

            return RedirectToAction("DepartmentalComplaintDetails", new { ComplaintNo = obj.ComplaintNo });
        }

        #endregion

        #region OM
        public ActionResult PendingRevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendingRevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
      
        public ActionResult ClosedRevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "ClosedRevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        public ActionResult DepecdencyRevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "DepedencyPendingRevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }

        public ActionResult PendingAppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendingAppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }

        public ActionResult PendencyAppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendencyAppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }


        
        public ActionResult ClosedAppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "ClosedAppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        #endregion


        #region  Vinay[20-01-2025]
        public ActionResult PendingComplaintAtEnquiryOfficer2()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaintAtEnquiryOfficer-2";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult PendingComplaintAtEnquiryOfficer2ById(int Id = 0, string op = "", string InvestigatingOfficerRelatedTo = "", int InvestigatingOfficerZoneId = 0, int InvestigatingOfficerCircleId = 0, int InvestigatingOfficerDivisionId = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetPendingComplaintAtEnquiryOfficer-2-ById",
                CreatedBy = Session["UserNumber"].ToString(),
                Id = Id,
                OptionalPr = op,
                ZoneId = InvestigatingOfficerZoneId,
                CircleId = InvestigatingOfficerCircleId,
                DivisionId = InvestigatingOfficerDivisionId
            };
            ViewBag.DesignationId = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        public ActionResult NumberOfSampleFilesPostWise()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "NewNumberofSampleFilesPostWise";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        #endregion

        public ActionResult DesignationWiseEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetDesignationWiseEnquiry";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

       

        #region OM
        public ActionResult DepartmentalUrbanEnquiryList(string ComplaintType = null)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TotalPendingUrbanEnquriy";
            obj.EnquiryType = ComplaintType;
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        
        public ActionResult DepartmentalRuralEnquiryList(string ComplaintType = null)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TotalPendingRuralEnquriy";
            obj.EnquiryType = ComplaintType;
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        #endregion
        public ActionResult TotalEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TotalEnquiry";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }

        #region  Filter Section  OM 
        [HttpPost]
        public ActionResult PendingSummaryComplaintAtEnquiryOfficerById(DepartmentalComplaint obj)
        {
            obj.Action = "GetPendingSummaryComplaintAtEnquiryOfficerById";
            obj.CreatedBy = Session["UserNumber"].ToString(); 
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult DesignationWiseEnquiryList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeEnquiryByDesignationId";
            obj.CreatedBy = Session["UserNumber"].ToString();
            obj.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            ViewBag.Id = Id;
            return View(ds);
        }
        [HttpPost]
        public ActionResult DesignationWiseEnquiryList(DepartmentalComplaint obj)
        {
            obj.Action = "GetEmployeeEnquiryByDesignationId"; 
            obj.CreatedBy = Session["UserNumber"].ToString(); 
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [HttpPost]
        public ActionResult Financial_NonfinancialWiseComplaintList(DepartmentalComplaint obj)
        { 
            obj.Action = "GetFinancialWiseComplaintList";
            obj.CreatedBy = Session["UserNumber"].ToString(); 
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [HttpPost]
        public ActionResult DepartmentalUrbanEnquiryList(DepartmentalComplaint obj)
        {
            obj.Fromdate = obj.Fromdate ?? string.Empty;
            obj.todate = obj.todate ?? string.Empty;
            obj.EnquiryType = obj.EnquiryType ?? string.Empty;
            obj.ComplaintRelatedTo = obj.ComplaintRelatedTo;
            obj.PreviousComplaintDesignationId = obj.PreviousComplaintDesignationId;
            obj.ComplaintRelatedZoneId = obj.ComplaintRelatedZoneId;
            obj.ComplaintRelatedCircleId = obj.ComplaintRelatedCircleId;
            obj.ComplaintRelatedDivisionId = obj.ComplaintRelatedDivisionId;
            obj.EmployeeId = obj.EmployeeId;
            obj.ComplainantName = obj.ComplainantName;
            obj.Action = "TotalPendingUrbanEnquriy";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        [HttpPost]
        public ActionResult DepartmentalRuralEnquiryList(DepartmentalComplaint obj)
        {
            obj.Fromdate = obj.Fromdate ?? string.Empty;
            obj.todate = obj.todate ?? string.Empty;
            obj.EnquiryType = obj.EnquiryType ?? string.Empty;
            obj.ComplaintRelatedTo = obj.ComplaintRelatedTo;
            obj.PreviousComplaintDesignationId = obj.PreviousComplaintDesignationId;
            obj.ComplaintRelatedZoneId = obj.ComplaintRelatedZoneId;
            obj.ComplaintRelatedCircleId = obj.ComplaintRelatedCircleId;
            obj.ComplaintRelatedDivisionId = obj.ComplaintRelatedDivisionId;
            obj.EmployeeId = obj.EmployeeId;
            obj.ComplainantName = obj.ComplainantName;
            obj.Action = "TotalPendingRuralEnquriy";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds);
        }
        [HttpPost]
        public ActionResult DepartmentalComplaintsList(DepartmentalComplaint obj)
        {
            obj.Fromdate = obj.Fromdate ?? string.Empty;
            obj.todate = obj.todate ?? string.Empty;
            obj.EnquiryType = obj.EnquiryType ?? string.Empty;
            obj.ComplaintRelatedTo = obj.ComplaintRelatedTo;
            obj.PreviousComplaintDesignationId = obj.PreviousComplaintDesignationId;
            obj.ComplaintRelatedZoneId = obj.ComplaintRelatedZoneId;
            obj.ComplaintRelatedCircleId = obj.ComplaintRelatedCircleId;
            obj.ComplaintRelatedDivisionId = obj.ComplaintRelatedDivisionId;
            obj.EmployeeId = obj.EmployeeId;
            obj.ComplainantName = obj.ComplainantName;
            obj.Action = "SelectDepartmentalComplaintList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds); 
        }
        [HttpPost]
        public ActionResult DepartmentalTypeComplaints(DepartmentalComplaint obj)
        { 
            obj.Fromdate = obj.Fromdate ?? string.Empty;
            obj.todate = obj.todate ?? string.Empty; 
            obj.Action = "GetDepartmentalTypeComplaints";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return View(ds); 
        }
        [HttpPost]
        [EncryptedActionParameter]
        public ActionResult PendingComplaintAtEnquiryOfficer2ById(DepartmentalComplaint obj)
        { 
            obj.Action = "GetPendingComplaintAtEnquiryOfficer-2-ById";
            obj.CreatedBy = Session["UserNumber"].ToString();

            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);

            return View(ds);
        }
         
        [EncryptedActionParameter]
        public ActionResult PendingSummaryComplaintAtEnquiryOfficerById(int Id = 0, string op = "", string InvestigatingOfficerRelatedTo = "", int InvestigatingOfficerZoneId = 0, int InvestigatingOfficerCircleId = 0, int InvestigatingOfficerDivisionId = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetPendingSummaryComplaintAtEnquiryOfficerById",
                CreatedBy = Session["UserNumber"].ToString(),
                Id = Id,
                OptionalPr = op,
                ZoneId = InvestigatingOfficerZoneId,
                CircleId = InvestigatingOfficerCircleId,
                DivisionId = InvestigatingOfficerDivisionId
            };
            ViewBag.op = op;
            ViewBag.Id = Id;
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        } 
        #endregion
        public ActionResult TopEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "Top10Newly";
            ViewBag.Url = "ComplaintSE";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return PartialView("Partial/_Top10Newly", ds);
        }
        public ActionResult OldEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "Top10Oldest";
            ViewBag.Url = "ComplaintSE";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return PartialView("Partial/_Top10Oldest", ds);
        }
        public ActionResult TopFinancialEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TopFinancialEnquiry";
            ViewBag.Url = "ComplaintSE";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return PartialView("Partial/_TopFinancialEnquiry", ds);
        }
        public ActionResult TopNonFinancialEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TopNonFinancialEnquiry";
            ViewBag.Url = "ComplaintSE";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return PartialView("Partial/_TopNonFinancialEnquiry", ds);
        }
        #region Om
        [EncryptedActionParameter]
        public ActionResult NumberofDecisionn(int Id = 0, string op = "")
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetForwordingOfficer",
                Id = Id,

            };
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult GetHearingOfficer(int Id = 0, string op = "")
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetHearingOfficer",
                Id = Id,

            };
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult GetClosedShowList(int Id = 0, string op = "")
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetCloseList",
                Id = Id,

            };
            DataSet ds = db.GetPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        #endregion

        #region Om
        [HttpGet]
        public ActionResult ShowCauseNotice()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShowCauseNotice(ShowCauseNoticCls obj)
        {
            string msg = "";
            obj.Action = "InsertshowCauseofNotice";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (obj.Attachment != null)
            {
                string filename = Path.GetFileName(obj.Attachment.FileName);
                string uniqueFileName = "ShowCause_" + Guid.NewGuid().ToString() + "_" + filename;
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                string fullPath = Path.Combine(uploadPath, uniqueFileName);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                obj.Attachment.SaveAs(fullPath);
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

            return RedirectToAction("ShowCauseNotice", "ComplaintSE");
        }
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
            obj.Action = "SelectCauseOfNoticeList";
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
            obj.Action = "InsertForwordingOfficer";
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


        [HttpPost]
        public ActionResult AddFinalStatusShowCause(ShowCauseNoticCls obj)
        {
            obj.Action = "InsertFinalStatusShowCause";
            obj.CreatedBy_User_Number = Session["UserNumber"].ToString();
            if (obj.FinalAttatchment != null)
            {
                string filename = Path.GetFileName(obj.FinalAttatchment.FileName);
                string uniqueFileName = "FinalStatusShowcause_" + Guid.NewGuid().ToString() + "_" + filename;
                string uploadPath = Server.MapPath("~/Uploads/Files/");
                string fullPath = Path.Combine(uploadPath, uniqueFileName);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                obj.FinalAttatchment.SaveAs(fullPath);
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

        public ActionResult ClosedShowCauseNoticeList(ShowCauseNoticCls obj)
        {
            obj.Action = "ClosedShowCauseList";
            DataSet dataSet = db.ShowCauseInsertUpdateSelect(obj);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                obj.table = dataSet.Tables[0];
            }
            return View(obj);
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

        public ActionResult PendencyHigherOfficerShowCauseNoticeList(ShowCauseNoticCls obj)
        {
            obj.Action = "PendencyHigherOfficerShowCauseList";
            DataSet dataSet = db.ShowCauseInsertUpdateSelect(obj);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                obj.table = dataSet.Tables[0];
            }
            return View(obj);
        }

        #endregion

        #region Om [Show Cause  Report]
        public ActionResult ShowcauseNoticeAllReports()
        {
            ShowCauseNoticCls obj = new ShowCauseNoticCls();
            obj.Action = "SelectCauseOfNoticeListReports";
            DataSet dataSet = db.ShowCauseInsertUpdateSelect(obj);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                obj.table = dataSet.Tables[0];
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult ShowcauseNoticeAllReports(ShowCauseNoticCls obj)
        {
            obj.Action = "SelectCauseOfNoticeListReports";
            obj.CreatedBy_User_Number = Session["UserNumber"]?.ToString();
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