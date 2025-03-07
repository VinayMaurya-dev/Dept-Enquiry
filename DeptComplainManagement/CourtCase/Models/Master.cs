using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;

namespace CourtCase.Models
{
    
    public class CasesMaster
    {
        public int? Id { get; set; }
        public  string CaseType { get; set; }
        public string RelatedTo { get; set; }
        public string WritNo { get; set; }
        public string ApplicantName { get; set; }
        public string Designation { get; set; }
        public string Respondent { get; set; }
        public string ZoneName { get; set; }
        public string ZoneId { get; set; }
        public string DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string CircleId { get; set; }
        public string RoleTypeId { get; set; }
        public string CircleName { get; set; }
        public string ReplyTillDate { get; set; }
        public string SubjectMatter { get; set; }
        public string SubjectMatterId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string RetirementDate { get; set; }
        public HttpPostedFileBase  Attachment { get; set; }
        public string FileDoc { get; set; }
        public string action { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public string FirmName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DesignationName { get; set; }
        public int CaseId { get; set; }
        public string ComputerNo { get; set; }
        public string MarkedOfficer { get; set; }
    }
    public class UserLogin:UserMaster
    {
        public int ? Id { get; set; }
       // [Required(ErrorMessage = "*")]
        public string RoleId { get; set; }
        public string SelectedRoleType { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        public string EmailId { get; set; }
       // [Required(ErrorMessage = "*")]
      
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        public string UserNumber { get; set; }
        public string UserId { get; set; }
        public string LastLogin { get; set; }
        public string RoleTypeId { get; set; }
        public  int FaildCount { get; set; }
        public string CaseType { get; set; }
        public string ReplyTillDateByLaw { get; set; }
        public string CaseNo { get; set; }
        public string ComputerNo { get; set; }
        public string Name { get; set; }
        public string UserRelatedTo { get; set; }
        public string CaptchaCode { get; set; }
        public bool IsActive { get; set; }
        public string KeepLogin { get; set; }
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public bool IsOTPGenerated { get; set; }
        public string Message { get; set; }
        public int otp_input_1 { get; set; }
        public int otp_input_2 { get; set; }
        public int otp_input_3 { get; set; }
        public int otp_input_4 { get; set; }
        public int otp_input_5 { get; set; }
        public int otp_input_6 { get; set; }
    }
    public  class UserMaster
    {
        public  int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public int CircleId { get; set; }
        public string CircleName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string EntryDate { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public string action { get; set; }
        public string Msg { get; set; }
        public string Status { get; set; }
    }
    public class ChangePassword
    {
        public int? Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string action { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public string Msg { get; set; }
        public string UserNumber { get; set; }
    }
    public class TacEnquiry
    {
        public int TACID { get; set; }
        public string InstructionsReceived { get; set; }
        public string MemorandumIssued { get; set; }
        public string LastdateTAC { get; set; }
        public string ReminderPost { get; set; }
        public HttpPostedFileBase MemorandumIssuedAttachment { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
        public string AttachmentDoc { get; set; }
        public string MemorandumIssuedAttachmentDoc { get; set; }
        public string IPAddress { get; set; }
        public string User_ID { get; set; }
        public string Action { get; set; }

    }

    public class FinancialAudit
    {
        public int FAID { get; set; }
        public string InstructionsReceived { get; set; }
        public string MemorandumIssued { get; set; }
        public string LastdateTAC { get; set; }
        public string ReminderPost { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
        public string AttachmentDoc { get; set; }
        public HttpPostedFileBase MemorandumIssuedAttachment { get; set; }
        public string MemorandumIssuedAttachmentDoc { get; set; }
        public string IPAddress { get; set; }
        public string Action { get; set; }
        public string User_ID { get; set; }

    }

    public class ShowCauseNotice
    {
        public string Action { get; set; }
        public int CauseID { get; set; }
        public string Description { get; set; }
        public string NameofPersonnel { get; set; }
        public int Designation { get; set; }
        public string OfficeRelatedTo { get; set; }
        public int OfficeRelatedZoneId { get; set; }
        public int OfficeRelatedCircleId { get; set; }
        public int OfficeRelatedDivision { get; set; }
        public string CauseNoticeIssued { get; set; }
        public HttpPostedFileBase CauseNoticeIssuedAttachment { get; set; }
        public string CauseNoticeIssuedAttachmentDoc { get; set; }
        public string ExplanationReceivedfromPersonnel { get; set; }
        public HttpPostedFileBase ExplanationReceivedfromPersonnelAttachment { get; set; }
        public string ExplanationReceivedfromPersonnelAttachmentDoc { get; set; }
        public string DecisionTakenbytheAppointingAuthority { get; set; }
        public string OfficeMemoIssuedbytheAppointingAuthority { get; set; }
        public HttpPostedFileBase OfficeMemoIssuedbytheAppointingAuthorityAttachment { get; set; }
        public string OfficeMemoIssuedbytheAppointingAuthorityAttachmentDoc { get; set; }
        public int ForwardedOfficerDesignation { get; set; }
        public string User_ID { get; set; }
        public string IPAddress { get; set; }

    }
    public class Suspension
    {
        public string Action { get; set; }
        public int SuspensionID { get; set; }
        public string NameofPersonnel { get; set; }
        public int Designation { get; set; }
        public string OfficeRelatedTo { get; set; }
        public int OfficeRelatedZoneId { get; set; }
        public int OfficeRelatedCircleId { get; set; }
        public int OfficeRelatedDivision { get; set; }

        public string MemorandumSuspensionOrder { get; set; }
        public HttpPostedFileBase SuspensionOrderAttachment { get; set; }
        public string SuspensionOrderAttachmentDoc { get; set; }
        public string NameofAffiliationOffice { get; set; }
        public string User_ID { get; set; }
        public string IPAddress { get; set; }

    }
    public class Appeal
    {
        public int AID { get; set; }
        public string AppealDate { get; set; }
        public string AppealName { get; set; }
        public int Designation { get; set; }
        public string AppealRelatedTo { get; set; }
        public int AppealRelatedZoneId { get; set; }
        public int AppealRelatedCircleId { get; set; }
        public int AppealRelatedDivision { get; set; }
        public int AppellateAuthorityDesignation { get; set; }

        public string AppellateDecision { get; set; }
        public string OrdersDetails { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
        public string AttachmentDoc { get; set; }
        public string IPAddress { get; set; }
        public string User_ID { get; set; }
        public string Action { get; set; }


    }

    public class TelephoneDirectory
    {
        public int Id { get; set; }
        public string TelephoneDirectoryTitle { get; set; }
        public string TelephoneDirectoryDiscription { get; set; }
        public string EntryDate { get; set; }
        public string action { get; set; }
        public HttpPostedFileBase  Attachment { get; set; }
        public string  DocFile { get; set; }
        public string Msg { get; set; }
        public string IPAddress { get; set; }
        public string CreatedBy { get; set; }
        public string StatusId { get; set; }
    }
	 
	public class Permission
	{
		public int PermissionId { get; set; }
		public int RoleId { get; set; }
		public int MainMenuId { get; set; }
		public int SubMenuId { get; set; }
		public bool CanAccess { get; set; }
	}
	public class MenuViewModel: SubMenuViewModel
	{
		public int Sequence { get; set; }
		public int MainMenuId { get; set; }
		public string MenuIcon { get; set; }
		public string MenuName { get; set; }
		public string MainController { get; set; }
		public string MainAction { get; set; }
		public string MainURL { get; set; }
		public string Action { get; set; }
		public string RoleId { get; set; } 
		public DataTable Table { get; set; }

		public List<SubMenuViewModel> SubMenuItems { get; set; }
	}

	public class SubMenuViewModel
	{
		public int SubMenuId { get; set; }
		public string SubMenuName { get; set; }
		public string SubController { get; set; }
		public string SubAction { get; set; }
		public string SubURL { get; set; }
		public bool IsChecked { get; set; }
	}
    public class RepliedCaseDetails
    {
        public string ComputerNo { get; set; }
        public string CaseType { get; set; }
        public string WritNo { get; set; }
        public string CaseRelatedTo { get; set; }
        public string EmpContrAppName { get; set; }
        public string EmpPost { get; set; }
        public string EmployeeStatus { get; set; }
        public string DateofRetirementDeath { get; set; }
        public string AttachWritDocument { get; set; }
        public string FirmName { get; set; }
        public string SubjectMatter { get; set; }
        public string Respondent { get; set; }
        public string MarkToRoleType { get; set; }
        public string ZoneId { get; set; }
        public string CircleId { get; set; }
        public string DivisionId { get; set; }
        public string MarkedOfficer_User_Number { get; set; }
        public string ReplyRemark { get; set; }
        public string ReplyDate { get; set; }
        public string ReplyAttachDocument { get; set; }
        public string CourtOrderDate { get; set; }
        public string ComplianceDate { get; set; }
        public string CourtOrderAttachement { get; set; }
        public string ComplianceLetterfromLawDeptAttachment { get; set; }
        public string ComplianceReplyRemark { get; set; }
        public object ComplianceReplyAttachment { get; set; }
        public string ComplianceReplyDate { get; set; }
        public object IsActive { get; set; }
        public object IP_Address { get; set; }
        public object Is_Deleted { get; set; }
        public object CreatedBy_User_Number { get; set; }
        public object CreatedOn_Datetime { get; set; }
        public object UpdatedBy_User_Number { get; set; }
        public object UpdatedOn_Datetime { get; set; }
        public string Action { get; set; }
        public int isReplyByMarkedOfficer { get; set; }
        public object filepath { get; set; }
        public object filetype { get; set; }
        public object ReplyId { get; set; }
        public object Reply { get; set; }
        public object ReplyAttachment { get; set; }
        public object ReplyFileUpload { get; set; }
        public object ReplyOnDate { get; set; }
        public object ReplyByDepartment { get; set; }
        public object ReplyByDesignation { get; set; }
        public object ReplyByUserNo { get; set; }
        public object ConversationType { get; set; }
        public object CaseStatus { get; set; }
        public object CaseStatusDate { get; set; }
        public object ReplyTillDateByLaw { get; set; }
        public object ReplyTillDate { get; set; }
        public string TribunalCaseNo { get; set; }
        public string ContemptCaseNo { get; set; }
        public object ContemptFor { get; set; }
        public string ContemptCourtOrderDate { get; set; }
        public string ContemptComplianceDate { get; set; }
        public string ContemptCourtOrderAttachment { get; set; }
        public string ContemptComplianceLetterAttachment { get; set; }
        public string ContemptMarkTo { get; set; }
        public string ContemptZoneId { get; set; }
        public string ContemptCircleId { get; set; }
        public string ContemptDivisionId { get; set; }
        public string ContemptDesignation { get; set; }
        public object code { get; set; }
        public string Court { get; set; }
        public object OtherSubjectMatter { get; set; }
        public object DesignationName { get; set; }
        public object FromDate { get; set; }
        public object ToDate { get; set; }
        public object MarkedOfficer { get; set; }
        public object ZoneName { get; set; }
        public object CircleName { get; set; }
        public object DivisionName { get; set; }
        public object Remark { get; set; }
        public object IPAddress { get; set; }
        public object CreatedBy { get; set; }
        public object Writ { get; set; }
        public object Contempt { get; set; }
        public object Arbitration { get; set; }
        public object Commercial { get; set; }
        public object Tribunal { get; set; }
        public object UserNo { get; set; }
        public object EntryDate { get; set; }
        public object Status { get; set; }
        public object ArbitrationAgreementNo { get; set; }
        public object ArbitrationCaseNo { get; set; }
        public object DepartmentAdvocateName { get; set; }
        public object DepartmentAdvocateMobile { get; set; }
        public object DepartmentAdvocateEmail { get; set; }
        public object FirmAddress { get; set; }
        public object FirmRepresentative { get; set; }
        public object FirmRepresentativeMobile { get; set; }
        public object ArbitratorName { get; set; }
        public object ArbitratorMobile { get; set; }
        public object ArbitratorEmail { get; set; }
        public object DepartmentLetterAttachment { get; set; }
        public object NoticeAttachment { get; set; }
        public DateTime MeetingDateTime { get; set; }
        public object MeetingPlace { get; set; }
        public object JudgementAttachment { get; set; }
        public object JudgementDate { get; set; }
        public object JudgementRemark { get; set; }
        public object CommercialCourtCaseNo { get; set; }
        public object CommercialCourtName { get; set; }
        public object CommercialMarkTo { get; set; }
        public object CommercialZoneId { get; set; }
        public object CommercialCircleId { get; set; }
        public object CommercialDivisionId { get; set; }
        public object CommercialDesignation { get; set; }
        public object CommercialDepartmentLetter { get; set; }
        public object CommercialCourtOrder { get; set; }
        public object CommercialReplyTillDate { get; set; }
        public object PendingWrit { get; set; }
        public object PendingTribunal { get; set; }
        public object PendingContempt { get; set; }
        public object PendingArbitration { get; set; }
        public object PendingCommercial { get; set; }
        public object EmployeeCase { get; set; }
        public object ContractorCase { get; set; }
        public object PILCase { get; set; }
        public object TotalCase { get; set; }
        public object PendingCase { get; set; }
        public object CommercialCaseType { get; set; }
        public object OtherCommercialCaseType { get; set; }
        public object OtherRelatedTo { get; set; }
        public object ArbitrationFirmName { get; set; }
        public object ArbitrationDepartmentName { get; set; }
        public object CompliancetillDate { get; set; }
        public object CommercialCourtOrderDate { get; set; }
        public object CommercialCourtOrderNotice { get; set; }
        public object CommercialCourtOrderDepartmentLetter { get; set; }
        public object CommercialCourtOrderRemark { get; set; }
        public object TotalCourtCase { get; set; }
        public object SCI { get; set; }
        public object AHC { get; set; }
        public object ALB { get; set; }
        public object ALB1 { get; set; }
        public string CouncilName { get; set; }
        public string CouncilMobile { get; set; }
        public int CounselId { get; set; }
        public string RegisteredOn { get; set; }
        public string ContemptCouncilName { get; set; }
        public string ContemptCouncilMobile { get; set; }
        public string ContemptRegisteredOn { get; set; }
        public object CommercialCouncilName { get; set; }
        public object CommercialCouncilMobile { get; set; }
        public object CommercialCounselId { get; set; }
        public int ContemptCounselId { get; set; }
        public object CommercialRegisteredOn { get; set; }
        public int RegisteredOnint { get; set; }
        public object ReplyMarkedDate { get; set; }
        public object ReplyExpiryDate { get; set; }
        public object PendingDays { get; set; }
        public object ReplyStatus { get; set; }
        public object SearchBy { get; set; }
        public object Criteria { get; set; }
        public object RoleName { get; set; }
        public string OtherCourt { get; set; }
        public string ClaimAmount { get; set; }
        public string FinancialType { get; set; }
        public string ContemptName { get; set; }
        public object ArbitrationRelatedTo { get; set; }
    }

    public class ReplyTable
    {
        public string ComputerNo { get; set; }
        public string ContemptCourtOrderDate { get; set; }
        public string ContemptComplianceDate { get; set; }
        public string ContemptCourtOrderAttachment { get; set; }
        public string CreatedDate { get; set; }
        public string CouncilName { get; set; }
        public string CouncilMobile { get; set; }
        public string ContemptCaseNo { get; set; }
    }
    public class HearingTable
    {
        public string ComputerNo { get; set; }
        public string ContemptCourtOrderDate { get; set; }
        public string ContemptComplianceDate { get; set; }
        public string ContemptCourtOrderAttachment { get; set; }
        public string CreatedDate { get; set; }
        public string CouncilName { get; set; }
        public string CouncilMobile { get; set; }
        public string ContemptCaseNo { get; set; }
    }

    public class Replies
    {
        public List<ReplyTable> Table3 { get; set; }
    }
    public class Hearing
    {
        public List<HearingTable> Table1 { get; set; }
    }
    public class Root
    {
        public RepliedCaseDetails repliedCaseDetails { get; set; }
        public Replies replies { get; set; }
        public Hearing hearing { get; set; }
        public bool Status { get; set; }
    }

}