using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CourtCase.Models
{
    public class ShowCauseNoticCls
    {
        public int SId { get; set; }
        public string ShowcauseNumber { get; set; }
        public string IssuingAuthorityName { get; set; }
        public string NoticeRelatedTo { get; set; }
        public int NoticeRelatedZoneId { get; set; }
        public int NoticeRelatedCircleId { get; set; }
        public int NoticeRelatedDivisionId { get; set; }
        public int NoticeRelatedDesignationId { get; set; }
        public string IssueletterNumber { get; set; }
        public string IssueletterDate { get; set; }
        public string LastdateforSubmission { get; set; }
        public string NoticeDetails { get; set; }
        public string NoticeRelatedDesignationName { get; set; }
        public string NoticeRelatedDivisionName { get; set; }
        public string NoticeRelatedCircleName { get; set; }
        public string NoticeRelatedZoneName { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
        public string AttachmentPath { get; set; }
        public string Action { get; set; }
        public string ActionName { get; set; }
        public int AnushMarkId { get; set; }
        public HttpPostedFileBase AnushMarkAttachment { get; set; }
        public string AnushMarkDescription { get; set; }
        public string ComplainantName { get; set; }
        public string CreatedBy_User_Number { get; set; }
        public string ShowcauseForwordingDescription { get; set; }
        public string ShowCauseForwordingOfficer { get; set; }
        public string FinalOfficeMemorandumNo { get; set; }
        public string FinalDate { get; set; }
        public string FinalStatus { get; set; }
        public string CurrentStatus { get; set; }
        public HttpPostedFileBase FinalAttatchment { get; set; }
        public string FinalDescription { get; set; }
        public DataTable table { get; set; }
        public string todate { get; set; }
        public string Fromdate { get; set; }
        public string Status { get; set; }
    }
}