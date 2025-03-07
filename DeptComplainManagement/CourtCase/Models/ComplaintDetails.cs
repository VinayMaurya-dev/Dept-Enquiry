using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourtCase.Models
{
	public class ComplaintDetails
	{

        //Complaint Detail
        public string ComplainCurrentStatus { get; set; }
        public string ComplaintEnquiryType { get; set; }
        public string ComplaintRelatedTo { get; set; }
        public int ComplaintRelatedZoneId { get; set; }
        public int ComplaintRelatedCircleId { get; set; }
        public int ComplaintRelatedDivisionId { get; set; }
        public string ComplaintRelatedZoneName { get; set; } 
        public string ComplaintRelatedCircleName { get; set; }
        public string ComplaintRelatedDivisionName { get; set; }
        public int ComplainRelatedDesignationId { get; set; }
        public string ComplainRelatedDesignationName { get; set; }
        public string EmployeeId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string R_U { get; set; }
        public string ComplainantName { get; set; }
        public string PreviousComplaintRelatedTo { get; set; } 
        public int PreviousComplaintRelatedZoneId { get; set; }
        public int PreviousComplaintRelatedCircleId { get; set; }
        public int PreviousComplaintRelatedDivisionId { get; set; }
        public string PreviousComplaintRelatedZoneName { get; set; }
        public string PreviousComplaintRelatedCircleName { get; set; }
        public string PreviousComplaintRelatedDivisionName { get; set; }
        public int PreviousComplaintDesignationId { get; set; }
        public string PreviousComplaintDesignationName { get; set; }
        public string WorkDurationFrom { get; set; }
        public string WorkDurationTo { get; set; }
        public string Attachment { get; set; }
        public string IsFinancial { get; set; }
		public decimal FinancialAmount { get; set; }
		public string IsSuspension { get; set; }
		public string AssociatedOfficeName { get; set; }
		public string AffiliatedOfficesRelatedTo { get; set; }
		public int AffiliatedOfficesZoneId { get; set; }
		public int AffiliatedOfficesCircleId { get; set; }
		public int AffiliatedOfficesDivisionId { get; set; }
        public string AffiliatedOfficesZoneName { get; set; }
        public string AffiliatedOfficesCircleName { get; set; }
        public string AffiliatedOfficesDivisionName { get; set; }
        public int AffiliatedOfficesDesignationId { get; set; }
        public string AffiliatedOfficesName { get; set; }
        public string ComplaintNo { get; set; }
        //---------- Court Case ---------------
        public int CourtId { get; set; }
        public string CourtName { get; set; }
     
        public string ComputerNumber { get; set; }
        public int InvestigationId { get; set; }
        public string CaseNo { get; set; }
        public string OtherCourt { get; set; }
        public string OtherAgencyName { get; set; }
        public string CaseType { get; set; }
        public int YearId { get; set; } 
        public string CaseYear { get; set; } 
        public string Remark { get; set; }

        //----------Investigating officer Details-------------------------------------

        public string InvestigatingOfficerRelatedTo { get; set; }
        public int InvestigatingOfficerZoneId { get; set; }
        public string InvestigatingOfficerZoneName { get; set; }
        public int InvestigatingOfficerCircleId { get; set; }
        public string InvestigatingOfficerCircleName { get; set; }
        public int InvestigatingOfficerDivisionId { get; set; }
        public string InvestigatingOfficerDivisionName { get; set; }
        public int InvestigatingOfficerDesignationId { get; set; }
        public string InvestigatingOfficerUser_Number { get; set; }
        public string InvestigatingOfficerDesignationName { get; set; }
        public string InvestigatingOfficerName { get; set; }
        public string InvestigatingOfficerMobile { get; set; } 
        public string OfficeMemorandumDate { get; set; }
       
        public string InvestigatingOfficerRemark { get; set; }
        public string OfficeMemorandumNumber { get; set; }

        //  Aarop
        public bool IsAddedAarop { get; set; }
        public int AaropID { get; set; }
        public string AaropRemark { get; set; }
        public int NumberofCharges { get; set; }
        public string ChargeSheetNumber { get; set; }
        public string AaropDescription { get; set; }
        public string ChargeSheetAttachment { get; set; }
        public string DateOfIssueChargeSheet { get; set; }

        public string InvestigatingOfficerDepartment { get; set; }
        public string InvestigatingOfficerLetterNo { get; set; }

        public string DateofdispatchComplaint { get; set; }
        public string InvestigatingOfficerAppointedDate { get; set; }
        public string InvestigatingOfficerLetter { get; set; }
        public string RemarkBeforeInvestigatingOfficerAppointed { get; set; }


        public string ComplaintDate { get; set; }
        public string ComplaintType { get; set;} = null; //P/D
        // Final Status
        public string FinalOfficeMemorandumNo { get; set; }
        public string FinalDate { get; set; }
        public string FinalStatus { get; set; }
        public HttpPostedFileBase FinalAttatchmentDoc { get; set; }
        public string FinalAttatchment { get; set; }
        public string FinalRemarks { get; set; }
        public bool IsFinalStatusAdded { get; set; }
        public bool IsRevisionlStatusAdded { get; set; }
        // Reminder Representation
        public bool IsRepresentationAdded { get; set; }
        public bool IsRevisionStatusAdded { get; set; }
        public int ReminderCount { get; set; }
        public bool IsRepresentationReceipt { get; set; }
        public bool IsHearingAdded { get; set; }
        public int HearingCount { get; set; }
        public bool IsHearingRepresentationAdded { get; set; }
        public string ActionName { get; set; }
        public string LetterNumber { get; set; }
        public string Attatchment { get; set; }
        public HttpPostedFileBase AttatchmentDoc { get; set; }
        public HttpPostedFileBase AttachmentperusalDoc { get; set; }
        public string LetterDate { get; set; }
        public int Flag { get; set; }
        public string ReasonedDecision { get; set; }
        public Boolean ActionAdded { get; set; }

        //Complaintant Detail

        public string ComplainantMobile { get; set; }
		public string ComplainantAddress { get; set; }  //in case of public

		public string ComplaintByEmployeeFromZDC { get; set; }
		public int ComplainantByEmployeeZone { get; set; }
		public string ComplainantEmployeeZoneName { get; set; }
		public int ComplainantByEmployeeCircle { get; set; }
		public string ComplainantEmployeeCircleName { get; set; }
		public int ComplainantByEmployeeDivision { get; set; }
		public string ComplainantEmployeeDivisionName { get; set; }
		public string ComplainantByEmployeeDesignation { get; set; }
		public string ComplainantEmployeeDesignationName { get; set; }
		public string ComplainantByEmployeeDepartment { get; set; }
		public string ComplainantByEmployeeStatus { get; set; }

        //Guilty Detail
        public int ApId { get; set; }
        public string FileNumber { get; set; }
        public string Desctioption { get; set; }
        public string Attatchhment { get; set; }

        public string GuiltyEmployeeName { get; set; }
		public string GuiltyEmployeeFromWhereZDC { get; set; }
		public int GuiltyEmployeeZone { get; set; }
		public string GuiltyEmployeeZoneName { get; set; }
		public int GuiltyEmployeeCircle { get; set; }
		public string GuiltyEmployeeCircleName { get; set; }
		public int GuiltyEmployeeDivision { get; set; }
		public string GuiltyEmployeeDivisionName { get; set; }
		public string GuiltyEmployeeDesignation { get; set; }
		public string GuiltyEmployeeDesignationName { get; set; }
		public string GuiltyEmployeeDepartment { get; set; }


		public string ComplaintSubject { get; set; }
		public string ComplaintDescription { get; set; }
		



//-----------Complaint Mark to Z/C for remark/reply-------------------------------

		public string ComplaintMarkToZC { get; set; }
		public string ComplaintMarkToOfficer { get; set; }
		public int ComplaintMarkedZone { get; set; }
		public string ComplaintMarkedZoneName { get; set; }
		public int ComplaintMarkedCircle { get; set; }
		public string ComplaintMarkedCircleName { get; set; }
		public string ComplaintMarkedDate { get; set; }
		public string ComplaintMarkedRemarkSubject { get; set; }
		public int ComplaintMarkedRemarkSubjectId { get; set; }
		public string OtherComplaintMarkedRemarkSubject { get; set; }
		public string ComplaintMarkedRemark { get; set; }  // Remark when marking to a officer
		public string ComplaintMarkedRemarkDescription { get; set; }  
		public string ComplaintMarkedAttachment { get; set; }
		public string ComplaintMarkedNo { get; set; }

		// Complaint Final Status
		public string ComplaintFinalStatus { get; set; }
		public string ComplaintStatus { get; set; }
		public string ComplaintFinalRemark { get; set; }
		public string ComplaintUpdatedStatusDate { get; set; }



		public string CreatedBy_User_Number { get; set; }
		public string CreatedOn_Datetime { get; set; }
		public string IPAddress { get; set; }

		public string Action { get; set; }


		public string filepath { get; set; }
		public string filetype { get; set; }
		public string ZoneId { get; set; }
		public string CircleId { get; set; }
		public string DivisionId { get; set; }

		// Operator Remark start
		public string RemarkByDepartment { get; set; }
		public string RemarkByDesignation { get; set; }
		public string RemarkByUserNo { get; set; }

		// Forwarding Officer 
		public string ForwardingOfficerZCDH { get; set; }
		public string ForwadingOfficerUserNumber { get; set; }
		public string ForwardingOfficerDate { get; set; }
		public string ForwardingOfficerLastDate { get; set; }
		public string ForwardingOfficerComment { get; set; }
		public string ForwardingOfficerLetterNo { get; set; }
		public string ForwardingOfficerLetter { get; set; }
		public string AllotingOfficerUser_Number { get; set; }
        // Remark list
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public int IsActive { get; set; }
        public int InspectionId { get; set; }
        

        public string DesignationId { get; set; }
        public string ComplaintMarkedDesignation { get; set; }
        public string msg { get; set; }
        public string OtherComplaintDocument  { get; set; }
        public string InvestigatinglastAppointedDate { get; set; }
        public string InvestigatingEntryDate { get; set; }

        public bool IsRevisionAdded { get; set; }
        public bool IsAppealAdded { get; set; }
        public bool IsAgencyAdded { get; set; }

        //------------------------17/12/2024-------------------------- 
        public DataSet getDataSet()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlDataAdapter da = new SqlDataAdapter("proc_ComplaintDetails", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				da.SelectCommand.Parameters.AddWithValue("@ComplaintNo", this.ComplaintNo);
				da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
				da.SelectCommand.Parameters.AddWithValue("@CircleId", this.CircleId);
				da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.DivisionId);
				da.SelectCommand.Parameters.AddWithValue("@RemarkByDesignation", this.RemarkByDesignation); 
				da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);



				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
		}
        public void save(out string message)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("proc_ComplaintDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ComplaintNo", this.ComplaintNo);
                cmd.Parameters.AddWithValue("@R_U", this.R_U);
                cmd.Parameters.AddWithValue("@ComplaintEnquiryType", this.ComplaintEnquiryType);
                cmd.Parameters.AddWithValue("@EmployeeId", this.EmployeeId);
                cmd.Parameters.AddWithValue("@ComplainantName", this.ComplainantName);
                cmd.Parameters.AddWithValue("@MobileNo", this.MobileNo);
                cmd.Parameters.AddWithValue("@EmailId", this.EmailId);
                cmd.Parameters.AddWithValue("@ComplaintRelatedTo", this.ComplaintRelatedTo);
                cmd.Parameters.AddWithValue("@ComplaintRelatedZoneId", this.ComplaintRelatedZoneId==0?-1: this.ComplaintRelatedZoneId);
                cmd.Parameters.AddWithValue("@ComplaintRelatedCircleId", this.ComplaintRelatedCircleId == 0 ? -1 : this.ComplaintRelatedCircleId);
                cmd.Parameters.AddWithValue("@ComplaintRelatedDivisionId", this.ComplaintRelatedDivisionId == 0 ? -1 : this.ComplaintRelatedDivisionId);
                cmd.Parameters.AddWithValue("@ComplainRelatedDesignationId", this.ComplainRelatedDesignationId == 0 ? -1 : this.ComplainRelatedDesignationId);
                cmd.Parameters.AddWithValue("@Attachment", this.Attachment);
             

                cmd.Parameters.AddWithValue("@PreviousComplaintRelatedTo", this.PreviousComplaintRelatedTo);
                cmd.Parameters.AddWithValue("@PreviousComplaintRelatedZoneId", this.PreviousComplaintRelatedZoneId);
                cmd.Parameters.AddWithValue("@PreviousComplaintRelatedCircleId", this.PreviousComplaintRelatedCircleId);
                cmd.Parameters.AddWithValue("@PreviousComplaintRelatedDivisionId", this.PreviousComplaintRelatedDivisionId);
                cmd.Parameters.AddWithValue("@PreviousComplaintDesignationId", this.PreviousComplaintDesignationId);
                cmd.Parameters.AddWithValue("@WorkDurationFrom", this.WorkDurationFrom);
                cmd.Parameters.AddWithValue("@WorkDurationTo", this.WorkDurationTo);

                cmd.Parameters.AddWithValue("@IsFinancial", this.IsFinancial);
                cmd.Parameters.AddWithValue("@FinancialAmount", this.FinancialAmount);
                cmd.Parameters.AddWithValue("@IsSuspension", this.IsSuspension);
                cmd.Parameters.AddWithValue("@AffiliatedOfficesRelatedTo", this.AffiliatedOfficesRelatedTo);
                cmd.Parameters.AddWithValue("@AffiliatedOfficesZoneId", this.AffiliatedOfficesZoneId);
                cmd.Parameters.AddWithValue("@AffiliatedOfficesCircleId", this.AffiliatedOfficesCircleId);
                cmd.Parameters.AddWithValue("@AffiliatedOfficesDivisionId", this.AffiliatedOfficesDivisionId);
                cmd.Parameters.AddWithValue("@AffiliatedOfficesDesignationId", this.AffiliatedOfficesDesignationId);
                cmd.Parameters.AddWithValue("@AssociatedOfficeName", string.IsNullOrEmpty(this.AssociatedOfficeName) ? (object)DBNull.Value : this.AssociatedOfficeName);
                cmd.Parameters.AddWithValue("@ComplaintDescription", string.IsNullOrEmpty(this.ComplaintDescription) ? (object)DBNull.Value : this.ComplaintDescription);

                // Add Investigating Officer
                cmd.Parameters.AddWithValue("@InvestigatingOfficerRelatedTo", this.InvestigatingOfficerRelatedTo);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerZoneId", this.InvestigatingOfficerZoneId);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerCircleId", this.InvestigatingOfficerCircleId);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerDivisionId", this.InvestigatingOfficerDivisionId);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerDesignationId", this.InvestigatingOfficerDesignationId);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerUser_Number", this.InvestigatingOfficerUser_Number);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerName", this.InvestigatingOfficerName);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerMobile", this.InvestigatingOfficerMobile);
                cmd.Parameters.AddWithValue("@OfficeMemorandumNumber", this.OfficeMemorandumNumber);
                cmd.Parameters.AddWithValue("@OfficeMemorandumDate", this.OfficeMemorandumDate);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerRemark", this.InvestigatingOfficerRemark);
                cmd.Parameters.AddWithValue("@InvestigatingOfficerAppointedDate", this.InvestigatingOfficerAppointedDate);
                cmd.Parameters.AddWithValue("@DateofdispatchComplaint", this.DateofdispatchComplaint);
                 
                // Remark
                cmd.Parameters.AddWithValue("@RemarkByDepartment", this.RemarkByDepartment);
                cmd.Parameters.AddWithValue("@RemarkByDesignation", this.RemarkByDesignation);
                cmd.Parameters.AddWithValue("@RemarkByUserNo", this.RemarkByUserNo);
                cmd.Parameters.AddWithValue("@ComplaintMarkedRemarkSubjectId", this.ComplaintMarkedRemarkSubjectId);
                cmd.Parameters.AddWithValue("@ComplaintMarkedRemarkSubject", this.ComplaintMarkedRemarkSubject);
                cmd.Parameters.AddWithValue("@ComplaintMarkedRemarkDescription", this.ComplaintMarkedRemarkDescription);
                cmd.Parameters.AddWithValue("@OtherComplaintMarkedRemarkSubject", this.OtherComplaintMarkedRemarkSubject);
                cmd.Parameters.AddWithValue("@OtherComplaintDocument", this.OtherComplaintDocument);
                cmd.Parameters.AddWithValue("@ComplaintFinalStatus", this.ComplaintFinalStatus);

                //----------Added a Letter No and letter Date----17/12/2024----
                cmd.Parameters.AddWithValue("@LetterDate", this.LetterDate);
                cmd.Parameters.AddWithValue("@LetterNumber", this.LetterNumber); 
                cmd.Parameters.AddWithValue("@ReasonedDecision", this.ReasonedDecision);

                //// Add Forwarding Officer

                cmd.Parameters.AddWithValue("@ForwadingOfficerUserNumber", this.ForwadingOfficerUserNumber); 
                cmd.Parameters.AddWithValue("@ForwardingOfficerComment", this.ForwardingOfficerComment); 
                cmd.Parameters.AddWithValue("@ComplaintStatus", this.ComplaintStatus); 
                cmd.Parameters.AddWithValue("@CreatedBy_User_Number", this.CreatedBy_User_Number);
                cmd.Parameters.AddWithValue("@Action", this.Action); 
                cmd.Parameters.Add("@msg", System.Data.SqlDbType.NVarChar, 250);
                cmd.Parameters["@msg"].Direction = ParameterDirection.Output;
          
                con.Open();
                cmd.ExecuteNonQuery();

                message = cmd.Parameters["@msg"].Value.ToString();

            }
        }
         
        public bool GetObjectData()
        {

            DataSet ds = new DataSet();
            ds = getDataSet();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.ComplaintNo = dr["ComplaintNo"].ToString();
                    this.ComplaintDate = dr["ComplaintDate"].ToString();
                    this.ComplaintType = dr["ComplaintType"].ToString();
                    this.ComplaintEnquiryType = dr["ComplaintEnquiryType"].ToString();
                    this.ComplaintRelatedTo = dr["ComplaintRelatedTo"].ToString();
                    this.ComplaintRelatedZoneName = dr["ComplaintRelatedZone"].ToString();
                    this.ComplaintRelatedCircleName = dr["ComplaintRelatedCircle"].ToString();
                    this.ComplainantName = dr["ComplainantName"].ToString();
                    this.ComplainantMobile = dr["ComplainantMobile"].ToString();
                    this.ComplainantAddress = dr["ComplainantAddress"].ToString();
                    this.ComplaintDescription = dr["ComplaintDescription"].ToString();
                    this.ComplainantByEmployeeDepartment = dr["ComplainantByEmployeeDepartment"].ToString();
                    this.ComplainantEmployeeDesignationName = dr["ComplainantEmployeeDesignationName"].ToString();
                    this.ComplaintByEmployeeFromZDC = dr["ComplaintByEmployeeFromZDC"].ToString();
                    this.ComplainantEmployeeZoneName = dr["ComplainantEmployeeZoneName"].ToString();
                    this.ComplainantEmployeeCircleName = dr["ComplainantEmployeeCircleName"].ToString();
                    this.Attachment = dr["Attachment"].ToString();
                    this.GuiltyEmployeeName = dr["GuiltyEmployeeName"].ToString();
                    this.GuiltyEmployeeZoneName = dr["GuiltyEmployeeZoneName"].ToString();
                    this.GuiltyEmployeeCircleName = dr["GuiltyEmployeeCircleName"].ToString();
                    this.GuiltyEmployeeFromWhereZDC = dr["GuiltyEmployeeFromWhereZDC"].ToString();
                    //this.GuiltyEmployeeDivisionName = dr["GuiltyEmployeeDivisionName"].ToString();
                    this.GuiltyEmployeeDesignationName = dr["GuiltyEmployeeDesignationName"].ToString();
                    this.ComplaintSubject = dr["ComplaintSubject"].ToString();
                    this.ComplainantByEmployeeStatus = dr["ComplainantByEmployeeStatus"].ToString();
                    //this.InvestigatingOfficerAppointedDate = dr["InvestigatingOfficerAppointedDate"].ToString();
                    this.ComplaintMarkToZC = dr["ComplaintMarkToZC"].ToString();
                    this.ComplaintMarkedZoneName = dr["ComplaintMarkedZoneName"].ToString();
                    this.ComplaintMarkedCircleName = dr["ComplaintMarkedCircleName"].ToString();
                    this.ComplaintMarkToOfficer = dr["ComplaintMarkToOfficer"].ToString();
                    this.ComplaintMarkedDate = dr["ComplaintMarkedDate"].ToString();
                    this.ComplaintMarkedNo = dr["ComplaintMarkedNo"].ToString();
                    this.ComplaintMarkedRemark = dr["ComplaintMarkedRemark"].ToString();
                    this.InvestigatingEntryDate = dr["InvestigatingEntryDate"].ToString();
                    this.IsActive = Convert.ToInt32(dr["IsActive"]);
                    this.IsAddedAarop = Convert.ToBoolean(dr["IsAddedAarop"]);
                    this.NumberofCharges = Convert.ToInt32(dr["NumberofCharges"]);
                    this.ChargeSheetNumber = dr["ChargeSheetNumber"].ToString();
                    this.DateOfIssueChargeSheet = dr["DateOfIssueChargeSheet"].ToString();
                    this.ChargeSheetAttachment = dr["ChargeSheetAttachment"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool getObject()
		{

			DataSet ds = new DataSet();
			ds = getDataSet();
			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					DataRow dr = ds.Tables[0].Rows[0];
					this.ComplaintNo = dr["ComplaintNo"].ToString(); 
					this.ComplaintEnquiryType = dr["ComplaintEnquiryType"].ToString(); 
                    this.ComplaintRelatedTo = dr["ComplaintRelatedTo"].ToString();
					this.ComplaintRelatedZoneName = dr["ComplaintRelatedZoneName"].ToString();
					this.ComplaintRelatedCircleName = dr["ComplaintRelatedCircleName"].ToString();
					this.ComplaintRelatedDivisionName = dr["ComplaintRelatedDivisionName"].ToString();
					this.ComplainRelatedDesignationName = dr["ComplainRelatedDesignationName"].ToString();
                    this.ComplainantName = dr["ComplainantName"].ToString();
                    this.MobileNo = dr["MobileNo"].ToString();
                    this.EmailId = dr["EmailId"].ToString();
                    this.PreviousComplaintRelatedTo = dr["PreviousComplaintRelatedTo"].ToString();
                    this.PreviousComplaintRelatedZoneName = dr["PreviousComplaintRelatedZoneName"].ToString();
                    this.PreviousComplaintRelatedCircleName = dr["PreviousComplaintRelatedCircleName"].ToString();
                    this.PreviousComplaintRelatedDivisionName = dr["PreviousComplaintRelatedDivisionName"].ToString();
                    this.PreviousComplaintDesignationName = dr["PreviousComplaintDesignationName"].ToString();
                    this.WorkDurationFrom = dr["WorkDurationFrom"].ToString();
                    this.WorkDurationTo = dr["WorkDurationTo"].ToString();
                    this.OfficeMemorandumNumber = dr["OfficeMemorandumNumber"].ToString();
                    this.OfficeMemorandumDate = dr["OfficeMemorandumDate"].ToString();
                    this.EmployeeId = dr["EmployeeId"].ToString();
                    this.Attachment = dr["Attachment"].ToString();
                    this.IsFinancial = dr["IsFinancial"].ToString();
                    this.FinancialAmount = Convert.ToDecimal(dr["FinancialAmount"]);
                    this.IsSuspension = dr["IsSuspension"].ToString();
                    this.AffiliatedOfficesRelatedTo = dr["AffiliatedOfficesRelatedTo"].ToString();
                    this.AffiliatedOfficesZoneName = dr["AffiliatedOfficesZoneName"].ToString();
                    this.AffiliatedOfficesCircleName = dr["AffiliatedOfficesCircleName"].ToString();
                    this.AffiliatedOfficesDivisionName = dr["AffiliatedOfficesDivisionName"].ToString(); 
                    this.AssociatedOfficeName = dr["AssociatedOfficeName"].ToString();
                    this.AffiliatedOfficesName = dr["AffiliatedOfficesName"].ToString();
                    this.ComplaintDescription = dr["ComplaintDescription"].ToString();
                    this.InvestigatingOfficerRelatedTo = dr["InvestigatingOfficerRelatedTo"].ToString();
                    this.InvestigatingOfficerName = dr["InvestigatingOfficerName"].ToString();
                    this.InvestigatingOfficerMobile = dr["InvestigatingOfficerMobile"].ToString();
                    this.InvestigatingOfficerZoneName = dr["InvestigatingOfficerZoneName"].ToString();
                    this.InvestigatingOfficerCircleName = dr["InvestigatingOfficerCircleName"].ToString();
                    this.InvestigatingOfficerDivisionName = dr["InvestigatingOfficerDivisionName"].ToString();
                    this.InvestigatingOfficerDesignationName = dr["InvestigatingOfficerDesignationName"].ToString();
                    this.OfficeMemorandumNumber = dr["OfficeMemorandumNumber"].ToString();
                    this.OfficeMemorandumDate = dr["OfficeMemorandumDate"].ToString();
                    this.InvestigatingOfficerRemark = dr["InvestigatingOfficerRemark"].ToString();
                    this.IsAddedAarop =Convert.ToBoolean(dr["IsAddedAarop"]);
                    this.NumberofCharges =Convert.ToInt32(dr["NumberofCharges"]);
                    this.ChargeSheetNumber=dr["ChargeSheetNumber"].ToString();
                    this.DateOfIssueChargeSheet = dr["DateOfIssueChargeSheet"].ToString();
                    this.ChargeSheetAttachment= dr["ChargeSheetAttachment"].ToString();
                    this.ComplainCurrentStatus = dr["ComplainCurrentStatus"].ToString();
                    this.IsFinalStatusAdded = Convert.ToBoolean(dr["IsFinalStatusAdded"]);
                    this.IsRepresentationAdded = Convert.ToBoolean(dr["IsRepresentationAdded"]);
                    this.ReminderCount = Convert.ToInt32(dr["ReminderCount"]);
                    this.IsRepresentationReceipt = Convert.ToBoolean(dr["IsRepresentationReceipt"]);
                    this.IsHearingAdded = Convert.ToBoolean(dr["IsHearingAdded"]);
                    this.HearingCount = Convert.ToInt32(dr["HearingCount"]);
                    this.IsHearingRepresentationAdded = Convert.ToBoolean(dr["IsHearingRepresentationAdded"]);
                    this.CourtId = Convert.ToInt32(dr["CourtId"]);
                    this.CourtName = (dr["CourtName"].ToString());
                    this.CaseYear = (dr["CaseYear"].ToString());
                    this.ComputerNumber = (dr["ComputerNumber"].ToString());
                    this.R_U = (dr["R_U"].ToString());
                    this.InvestigationId = Convert.ToInt32(dr["InvestigationId"]);
                    this.IsRevisionAdded = Convert.ToBoolean(dr["IsRevisionAdded"]);
                    this.IsAppealAdded = Convert.ToBoolean(dr["IsAppealAdded"]);
                    this.IsAgencyAdded = Convert.ToBoolean(dr["IsAgencyAdded"]);
                    return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
        public DataSet GetDashboardData()
        {
            DataSet ds = new DataSet();
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString; 
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("proc_GetComplaintDashboard", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure; 
                    // Adding parameters
                    da.SelectCommand.Parameters.AddWithValue("@Id", this.ZoneId);
                    da.SelectCommand.Parameters.AddWithValue("@UserNumber", this.CreatedBy_User_Number);
                    da.SelectCommand.Parameters.AddWithValue("@action", this.Action);  
                    da.Fill(ds);  
                    return ds;  
                }
            }
            catch (SqlException ex) when (ex.Message.Contains("semaphore timeout period"))
            {
                throw;
            }
            catch (SqlException ex) when (ex.Number == -2)
            {
                 
            }
            catch (Exception ex)
            {
                
            }
            return ds;
        }

        public DataSet getInspectionDataSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("proc_InspectionCommonList", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@ComplaintNo", this.ComplaintNo);
                da.SelectCommand.Parameters.AddWithValue("@InspectionId", this.InspectionId);
                da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.InvestigatingOfficerZoneId);
                da.SelectCommand.Parameters.AddWithValue("@CircleId", this.InvestigatingOfficerCircleId);
                da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.InvestigatingOfficerDivisionId);
                da.SelectCommand.Parameters.AddWithValue("@DesignationId", this.InvestigatingOfficerDesignationId);
                da.SelectCommand.Parameters.AddWithValue("@HQId", this.InvestigatingOfficerDepartment);
                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
                da.SelectCommand.Parameters.AddWithValue("@ComplaintType", this.ComplaintType);
                da.SelectCommand.Parameters.AddWithValue("@RoleName", this.ComplaintRelatedTo);
                da.SelectCommand.Parameters.AddWithValue("@UserNumber", this.CreatedBy_User_Number); 
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        #region Om
        public DataSet getRevisionAuthorizedofficerSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("proc_AuthorizedOfficerCommonList", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@CreatedBy", this.CreatedBy_User_Number);

                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        #endregion

        public DataSet getAuthorizedofficerSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("proc_AuthorizedOfficerCommonList", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@AllotingOfficerUser_Number", this.AllotingOfficerUser_Number);
                da.SelectCommand.Parameters.AddWithValue("@ComplaintNo", this.ComplaintNo);
                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
                da.SelectCommand.Parameters.AddWithValue("@ComplaintType", string.IsNullOrEmpty(this.ComplaintType) ? (object)DBNull.Value : this.ComplaintType);
                da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
                da.SelectCommand.Parameters.AddWithValue("@CircleId", this.CircleId);
                da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.DivisionId);
                da.SelectCommand.Parameters.AddWithValue("@ComplaintRelatedTo", this.ComplaintRelatedTo);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }


        public DataSet getRevisionofficerSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("proc_AuthorizedOfficerCommonList", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@CreatedBy", this.CreatedBy_User_Number);
               
                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
                
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }


        public DataSet getCHMDInspectionDataSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("proc_CHMDCommonList", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure; 
                da.SelectCommand.Parameters.AddWithValue("@ComplaintNo", this.ComplaintNo);
                da.SelectCommand.Parameters.AddWithValue("@InspectionId", this.InspectionId);
                da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.InvestigatingOfficerZoneId);
                da.SelectCommand.Parameters.AddWithValue("@CircleId", this.InvestigatingOfficerCircleId);
                da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.InvestigatingOfficerDivisionId);
                da.SelectCommand.Parameters.AddWithValue("@DesignationId", this.InvestigatingOfficerDesignationId);
                da.SelectCommand.Parameters.AddWithValue("@HQId", this.InvestigatingOfficerDepartment);
                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
                da.SelectCommand.Parameters.AddWithValue("@ComplaintType", this.ComplaintType);
                da.SelectCommand.Parameters.AddWithValue("@RoleName", this.ComplaintRelatedTo);
                da.SelectCommand.Parameters.AddWithValue("@UserNumber", this.CreatedBy_User_Number); 
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        public DataSet getReportDashboard()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("Proc_ReportDashboard", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure; 
                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);  
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
    }
}