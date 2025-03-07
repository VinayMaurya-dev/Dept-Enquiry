using CourtCase.DAL;
using CourtCase.Filters;
using CourtCase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourtCase.Controllers
{
    [SessionCheck]
    public class ReportsController : Controller
    {
        // GET: Reports
        ComplaintDetails obj = new ComplaintDetails();
        DBLayer db = new DBLayer();
        public ActionResult Dashboard()
        {
            BindChartData();
            obj.Action = "TotalRegisteredComplaint"; 
            return PartialView("Partial/_CommonDashboard", obj.getReportDashboard());
        }
        public ActionResult ChartDashboard()
        {
            BindChartData(); 
            return PartialView("Partial/_ChartDashboard");
        }
        public void BindChartData()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "financial_nonfinancialchart";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            int financialEnquiry = 0;
            int NonfinancialEnquiry = 0;
            int level1 = 0;
            int level2 = 0;
            int level3 = 0;
            int level4 = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                financialEnquiry = Convert.ToInt32(row["financialEnquiry"]);
                NonfinancialEnquiry = Convert.ToInt32(row["NonfinancialEnquiry"]);
                level1 = Convert.ToInt32(row["Level1"]);
                level2 = Convert.ToInt32(row["Level2"]);
                level3 = Convert.ToInt32(row["Level3"]);
                level4 = Convert.ToInt32(row["Level4"]);
            }
            ViewBag.financialEnquiry = financialEnquiry;
            ViewBag.NonfinancialEnquiry = NonfinancialEnquiry;
            ViewBag.Level1 = level1;
            ViewBag.Level2 = level2;
            ViewBag.Level3 = level3;
            ViewBag.Level4 = level4;
        }
        public ActionResult DepartmentalDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint"; 
            return View(obj.getReportDashboard());
        }
        public ActionResult CourtCaseDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint"; 
            return View(obj.getReportDashboard());
        }
        [EncryptedActionParameter]
        public ActionResult CourtCaseLucknow(int CourtId = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "SelectCourtCaseLucknow"; 
            obj.CourtId = CourtId;
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        public ActionResult InvestigationDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint"; 
            return View(obj.getReportDashboard());
        }
        [EncryptedActionParameter]
        public ActionResult InvestigationDetails(int InvestigationId = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "SelectInvestigationDetails"; 
            obj.InvestigationId = InvestigationId;
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        #region  Vinay[25-12-2024]
        [EncryptedActionParameter]
        public ActionResult DepartmentalTypeComplaints(string ComplaintType = null)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetDepartmentalTypeComplaints"; 
            obj.EnquiryType = ComplaintType;
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            ViewBag.SelectedEnquiryType = ComplaintType;
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
        [HttpGet]
        public ActionResult Financial_NonfinancialWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetFinancialWiseComplaintList"; 
            obj.Id = Id;
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            ViewBag.Id = Id;
            return View(ds);
        }
        public ActionResult PendingComplaintAtEnquiryOfficer()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaintAtEnquiryOfficer";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
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
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
          
        public ActionResult ChargeSheetWiseComplaints()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetChargeSheet"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult ChargeSheetWiseComplaintsList(string ComplaintNO)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.ComplaintNO = ComplaintNO;
            obj.Action = "GetChargeSheetList"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult PendingRemarkatHigherOfficer()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaint_HO"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult PendingRemarkatHigherOfficerById(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaint_HOById"; 
            obj.Id = Id;
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult EmployeeWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeWiseComplaint"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult EmployeeWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeWiseComplaintByLevelId"; 
            obj.Id = Id;
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult ClosedEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "ClosedEnquiryList"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        public ActionResult DesignationWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetDesignationWiseComplaint"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult DesignationWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeComplaintByDesignationId"; 
            obj.Id = Id;
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        #endregion

        #region  27-12-2024


        public ActionResult SuspensionDashboard()
        {
            ComplaintDetails obj = new ComplaintDetails();
            obj.Action = "TotalRegisteredComplaint"; 
            return View(obj.getReportDashboard());
        }

        public ActionResult SuspensionLevelWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionLevelWiseComplaint"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        public ActionResult SuspensionDesignationWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionDesignationWiseComplaint"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult SuspensionLevelWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionComplaintByLevelId"; 
            obj.Id = Id;
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult SuspensionDesignationWiseComplaintList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionEmployeeComplaintByDesignationId"; 
            obj.Id = Id;
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        public ActionResult SuspensionAttachedOfficeWiseComplaint()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionAttachedOfficeWiseComplaint"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult SuspensionAttachedOfficeWiseComplaintList(string RoleId)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetSuspensionEmployeeComplaintByAttachedOffice"; 
            obj.RoleId = RoleId;
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }

        public ActionResult PendingComplaintsBasedontheDuration()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendingComplaintsBasedontheDurationAfterInspection"; 
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(obj);
            return View(ds);
        }
        [EncryptedActionParameter]
        public ActionResult PendingMonthlyWiseComplaintsBasedontheDuration(int Id = 0, string op = "")
        {
            var complaint = new DepartmentalComplaint
            {
                Action = "PendingMonthlyWiseComplaintsBasedontheDuration", 
                Id = Id,
                OptionalPr = op
            };
            ViewBag.op = op;
            DataSet ds = db.GetReportPendingComplaintEnquiryOfficer(complaint);
            return View(ds);
        }
        #endregion

        #region OM
        public ActionResult RevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "RevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
       
        public ActionResult PendingRevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendingRevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        public ActionResult ClosedRevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "ClosedRevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

      
        public ActionResult DepecdencyRevisionEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "DepedencyPendingRevisionEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        public ActionResult AppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "AppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        public ActionResult PendingAppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendingAppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        public ActionResult PendencyAppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "PendencyAppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        public ActionResult ClosedAppealEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "ClosedAppealEnquiryList";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        #endregion
        #region  Vinay[21-01-2025]
        public ActionResult PendingComplaintAtEnquiryOfficer2()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetPendingComplaintAtEnquiryOfficer-2"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult PendingComplaintAtEnquiryOfficer2ById(int Id = 0, string op = "", string InvestigatingOfficerRelatedTo = "", int InvestigatingOfficerZoneId = 0, int InvestigatingOfficerCircleId = 0, int InvestigatingOfficerDivisionId = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetPendingComplaintAtEnquiryOfficer-2-ById", 
                Id = Id,
                OptionalPr = op,
                ZoneId = InvestigatingOfficerZoneId,
                CircleId = InvestigatingOfficerCircleId,
                DivisionId = InvestigatingOfficerDivisionId
            };
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        public ActionResult NumberOfSampleFilesPostWise()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "NewNumberofSampleFilesPostWise"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        #endregion
        [EncryptedActionParameter]
        public ActionResult PendingSummaryComplaintAtEnquiryOfficerById(int Id = 0, string op = "", string InvestigatingOfficerRelatedTo = "", int InvestigatingOfficerZoneId = 0, int InvestigatingOfficerCircleId = 0, int InvestigatingOfficerDivisionId = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint
            {
                Action = "GetPendingSummaryComplaintAtEnquiryOfficerById", 
                Id = Id,
                OptionalPr = op,
                ZoneId = InvestigatingOfficerZoneId,
                CircleId = InvestigatingOfficerCircleId,
                DivisionId = InvestigatingOfficerDivisionId
            };
            ViewBag.op = op;
            ViewBag.Id = Id;
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        public ActionResult DesignationWiseEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetDesignationWiseEnquiry"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        [EncryptedActionParameter]
        public ActionResult DesignationWiseEnquiryList(int Id = 0)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "GetEmployeeEnquiryByDesignationId"; 
            obj.Id = Id;
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            ViewBag.Id = Id;
            return View(ds);
        }
        public ActionResult TotalEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TotalEnquiry"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }


        public ActionResult TopEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "Top10Newly";
            ViewBag.Url = "Reports";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return PartialView("Partial/_Top10Newly", ds);
        }
        public ActionResult OldEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "Top10Oldest";
            ViewBag.Url = "Reports";
            DataSet ds = db.GetDepartmentalComplaint(obj);
            return PartialView("Partial/_Top10Oldest", ds);
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
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
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
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
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
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        #endregion
        #region [OM Show Cause Report]
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

        #region Om Filter Report

        public ActionResult DepartmentalRuralEnquiryList()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TotalPendingRuralEnquriy"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
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
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }


        public ActionResult DepartmentalUrbanEnquiryList(string ComplaintType = null)
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TotalPendingUrbanEnquriy";
            obj.EnquiryType = ComplaintType;
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
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
            
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        public ActionResult TopFinancialEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TopFinancialEnquiry";
            ViewBag.Url = "Reports";
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return PartialView("Partial/_TopFinancialEnquiry", ds);
        }
        public ActionResult TopNonFinancialEnquiry()
        {
            DepartmentalComplaint obj = new DepartmentalComplaint();
            obj.Action = "TopNonFinancialEnquiry";
            ViewBag.Url = "Reports";
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return PartialView("Partial/_TopNonFinancialEnquiry", ds);
        }
        #endregion

        #region  Vinay[02-02-2025   Filer Section] 
        [HttpPost] 
        public ActionResult DepartmentalTypeComplaints(DepartmentalComplaint obj)
        {
            obj.Fromdate = obj.Fromdate ?? string.Empty;
            obj.todate = obj.todate ?? string.Empty;
            obj.Action = "GetDepartmentalTypeComplaints"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        [HttpPost]
        public ActionResult Financial_NonfinancialWiseComplaintList(DepartmentalComplaint obj)
        {
            obj.Action = "GetFinancialWiseComplaintList"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        #endregion

        #region Om Filter Report
        [HttpPost]
        public ActionResult PendingSummaryComplaintAtEnquiryOfficerById(DepartmentalComplaint obj)
        {
            obj.Action = "GetPendingSummaryComplaintAtEnquiryOfficerById";
            obj.CreatedBy = Session["UserNumber"].ToString();
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }
        [HttpPost]
        public ActionResult DesignationWiseEnquiryList(DepartmentalComplaint obj)
        { 
            obj.Action = "GetEmployeeEnquiryByDesignationId"; 
            DataSet ds = db.GetDepartmentalComplaintReport(obj);
            return View(ds);
        }

        #endregion
    }
}