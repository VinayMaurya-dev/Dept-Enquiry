using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CourtCase.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace CourtCase.DAL
{
    public class DBLayer : DbContext
    {
        SQLHelper Db = new SQLHelper();
        public string action { get; set; }
        public int RegisteredOnint { get; set; }
        public string MarkedOfficer { get; set; }


        public DataSet getDataSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_GetDashboard", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@action", this.action);
                da.SelectCommand.Parameters.AddWithValue("@Id", this.MarkedOfficer);
                da.SelectCommand.Parameters.AddWithValue("@RegisteredOnint", this.RegisteredOnint);


                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        #region[21/06/24]

        public DataTable AddZone(Admin model)
        {
            SqlParameter[] parm = new SqlParameter[] {
             new SqlParameter("@ZoneId", model.ZoneId == 0 ? (object)DBNull.Value : (object)model.ZoneId),
              new SqlParameter { ParameterName = "@Action", Value = model.Action },

                new SqlParameter { ParameterName = "@ZoneName", Value = model.ZoneName },
                new SqlParameter { ParameterName = "@IP_Address", Value = model.IP_Address },
                new SqlParameter { ParameterName = "@CreatedBy_User_Number", Value = model.CreatedBy_User_Number },
};
            DataTable Table = Db.ExecProcDataTable("proc_AddZone", parm);

            return Table;
        }
        public DataTable AddCircle(Circle model)
        {
            SqlParameter[] parm = new SqlParameter[] {
             new SqlParameter("@CircleId", model.CircleId == 0 ? (object)DBNull.Value : (object)model.CircleId),
             new SqlParameter("@ZoneId", model.ZoneId == 0 ? (object)DBNull.Value : (object)model.ZoneId),
              new SqlParameter { ParameterName = "@Action", Value = model.Action },
              new SqlParameter { ParameterName = "@CircleName", Value = model.CircleName },
                //new SqlParameter { ParameterName = "@ZoneName", Value = model.ZoneName },
                new SqlParameter { ParameterName = "@IP_Address", Value = model.IP_Address },
                new SqlParameter { ParameterName = "@CreatedBy_User_Number", Value = model.CreatedBy_User_Number },
};
            DataTable Table = Db.ExecProcDataTable("proc_AddCircle", parm);

            return Table;
        }
        public IEnumerable<SelectListItem> GetDropDownList(Circle model)
        {
            //List<SelectListItem> lst = new List<SelectListItem>();
            var sqlParams = new SqlParameter[] {
                new SqlParameter { ParameterName = "@Action", Value =model.Action },
                new SqlParameter("@ZoneId", model.ZoneId == 0 ? (object)DBNull.Value : (object)model.ZoneId),


            };
            var sqlProc = @"dropdownBinderSP_Select @Action,@ZoneId";
            var sList = this.Database.SqlQuery<SelectListItem>(sqlProc, sqlParams).ToList();
            return sList;
        }
        public DataTable AddDivision(Division model)
        {
            SqlParameter[] parm = new SqlParameter[] {
             //new SqlParameter("@CircleId", model.CircleId == 0 ? (object)DBNull.Value : (object)model.CircleId),
             //new SqlParameter("@ZoneId", model.ZoneId == 0 ? (object)DBNull.Value : (object)model.ZoneId),
             //new SqlParameter("@DivisionId", model.DivisionId == 0 ? (object)DBNull.Value : (object)model.DivisionId),

              new SqlParameter { ParameterName = "@DivisionId", Value = model.DivisionId },
              new SqlParameter { ParameterName = "@CircleId", Value = model.CircleId },
              new SqlParameter { ParameterName = "@ZoneId", Value = model.ZoneId },
              new SqlParameter { ParameterName = "@Action", Value = model.Action },
              new SqlParameter { ParameterName = "@DivisionName", Value = model.DivisionName },
              new SqlParameter { ParameterName = "@IP_Address", Value = model.IP_Address },
                new SqlParameter { ParameterName = "@CreatedBy_User_Number", Value = model.CreatedBy_User_Number },
};
            DataTable Table = Db.ExecProcDataTable("proc_AddDivision", parm);

            return Table;
        }
        public IEnumerable<SelectListItem> GetDropDownBind(Division model)
        {
            //List<SelectListItem> lst = new List<SelectListItem>();
            var sqlParams = new SqlParameter[] {
                new SqlParameter { ParameterName = "@Action", Value =model.Action },
                new SqlParameter("@CircleId", model.CircleId == 0 ? (object)DBNull.Value : (object)model.CircleId),


            };
            var sqlProc = @"dropdownBinderSP_Select @Action,@CircleId";
            var sList = this.Database.SqlQuery<SelectListItem>(sqlProc, sqlParams).ToList();
            return sList;
        }
        public DataTable AddDesignation(Designation model)
        {
            SqlParameter[] parm = new SqlParameter[] {

              new SqlParameter { ParameterName = "@DesignationId", Value = model.DesignationId },
              new SqlParameter { ParameterName = "@DesignationName", Value = model.DesignationName },
              new SqlParameter { ParameterName = "@RoleTypeId", Value = model.RoleTypeId },
              new SqlParameter { ParameterName = "@Action", Value = model.Action },
              new SqlParameter { ParameterName = "@IP_Address", Value = model.IP_Address },
                new SqlParameter { ParameterName = "@CreatedBy_User_Number", Value = model.CreatedBy_User_Number },
};
            DataTable Table = Db.ExecProcDataTable("proc_AddDesignation", parm);

            return Table;
        }
        public DataTable AddSubjectMaster(SubjectMaster model)
        {
            SqlParameter[] parm = new SqlParameter[] {

              new SqlParameter { ParameterName = "@subjectMatterId", Value = model.subjectMatterId },
              new SqlParameter { ParameterName = "@subjectMatter", Value = model.subjectMatter },
              new SqlParameter { ParameterName = "@RelatedTo", Value = model.RelatedTo },
              new SqlParameter { ParameterName = "@Action", Value = model.Action },
              new SqlParameter { ParameterName = "@IP_Address", Value = model.IP_Address },
                new SqlParameter { ParameterName = "@CreatedBy_User_Number", Value = model.CreatedBy_User_Number },
};
            DataTable Table = Db.ExecProcDataTable("proc_AddSubjectMaster", parm);

            return Table;
        }
        #endregion

        #region[Change Password]
        public DataTable ValidatePassword(ChangePassword model)
        {
            SqlParameter[] parm = new SqlParameter[] {

              new SqlParameter { ParameterName = "@action", Value = model.action },           
              new SqlParameter { ParameterName = "@OldPassword", Value = model.OldPassword },           
              new SqlParameter { ParameterName = "@NewPassword", Value = model.NewPassword },           
              new SqlParameter { ParameterName = "@ConfirmPassword", Value = model.ConfirmPassword },           
              new SqlParameter { ParameterName = "@CreatedBy", Value = model.CreatedBy },           
              new SqlParameter { ParameterName = "@IPAddress", Value = model.IPAddress },           
              new SqlParameter { ParameterName = "@UserNumber", Value = model.UserNumber },           
};
            DataTable Table = Db.ExecProcDataTable("proc_ChangePassword", parm);

            return Table;

        }
        #endregion
		public IEnumerable<UserLogin> InserUpdateCompUserLogin(UserLogin model)
		{
			var sqlproc = @"proc_CompUserLogin @action,@Id,@RoleId,@ZoneId,@CircleId,@DivisionId
,@DesignationId,@MobileNo,@EmailId,@Password,@IPAddress,@CreatedBy,@ComputerNo,@Name,@UserRelatedTo";
			var parametere = new SqlParameter[]
			{
				new SqlParameter { ParameterName = "@action", Value =model.action??string.Empty},
				 new SqlParameter { ParameterName = "@Id", Value =model.Id==null?0:model.Id},
				 new SqlParameter { ParameterName = "@RoleId", Value =model.SelectedRoleType??string.Empty},
                 new SqlParameter { ParameterName = "@ZoneId", Value =model.ZoneId==0?-1:model.ZoneId},
                 new SqlParameter { ParameterName = "@CircleId", Value =model.CircleId==0?-1:model.CircleId},
                 new SqlParameter { ParameterName = "@DivisionId", Value =model.DivisionId==0?-1:model.DivisionId},
                 new SqlParameter { ParameterName = "@DesignationId", Value =model.DesignationId},
				 new SqlParameter { ParameterName = "@MobileNo", Value =model.MobileNo??string.Empty},
				 new SqlParameter { ParameterName = "@EmailId", Value =model.EmailId??string.Empty},
				 new SqlParameter { ParameterName = "@Password", Value =model.Password??string.Empty},
				 new SqlParameter { ParameterName = "@IPAddress", Value =model.IPAddress??string.Empty},
				 new SqlParameter { ParameterName = "@CreatedBy", Value =model.CreatedBy??string.Empty},
				 new SqlParameter { ParameterName = "@ComputerNo", Value =model.ComputerNo??string.Empty},
				 new SqlParameter { ParameterName = "@Name", Value =model.Name??string.Empty},
				 new SqlParameter { ParameterName = "@UserRelatedTo", Value =model.UserRelatedTo??string.Empty},
			};
			var sList = this.Database.SqlQuery<UserLogin>(sqlproc, parametere).ToList();
			return sList;
		}
          
		#region [Vinay 23-10-2024]
		public DataTable MainMenu(MenuViewModel model)
		{
			SqlParameter[] parm = {
			new SqlParameter("@Action", model.Action), 
			new SqlParameter("@MenuName", model.MenuName), 
			new SqlParameter("@MainController", model.MainController), 
			new SqlParameter("@MainAction", model.MainAction), 
			new SqlParameter("@MainURL", model.MainURL), 
			new SqlParameter("@MenuIcon", model.MenuIcon),
			new SqlParameter("@Sequence", model.Sequence),
			new SqlParameter("@MainMenuId", model.MainMenuId),
		};
			DataTable dt = Db.ExecProcDataTable("proc_MainMenu", parm);
			return dt;
		}
		public DataTable SubMenu(MenuViewModel model)
		{
			SqlParameter[] parm = { 
			new SqlParameter("@Action", model.Action),
			new SqlParameter("@MainMenuId", model.MainMenuId),
			new SqlParameter("@SubMenuId", model.SubMenuId),
			new SqlParameter("@SubMenuName", model.SubMenuName),
			new SqlParameter("@SubController", model.SubController),
			new SqlParameter("@SubAction", model.SubAction),
			new SqlParameter("@SubURL", model.SubURL), 
			
		};
			DataTable dt = Db.ExecProcDataTable("proc_SubMenu", parm);
			return dt;
		}
		public DataSet GetDynamicMenu(MenuViewModel model)
		{
            SqlParameter[] parm = { 
            new SqlParameter("@RoleId", model.RoleId),
            new SqlParameter("@Action", model.Action),
			};
			DataSet ds = Db.ExecuteDataSet("proc_GetDynamicMenu", parm);
			return ds;
		}
        #endregion
        #region Om
        public DataTable ComplaintAarop(ComplaintDetails model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Action", model.Action),  
            new SqlParameter("@ComplaintDescription", model.ComplaintDescription),
            new SqlParameter("@ComplaintNo", model.ComplaintNo),
            new SqlParameter("@ChargeSheetNumber", model.ChargeSheetNumber),
            new SqlParameter("@ChargeSheetAttachment", model.ChargeSheetAttachment),
            new SqlParameter("@DateOfIssueChargeSheet", model.DateOfIssueChargeSheet), 
            new SqlParameter("@NumberofCharges", model.NumberofCharges),
            new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
            new SqlParameter("@AaropRemark", model.AaropRemark),
            new SqlParameter("@AaropID", model.AaropID),
        };
            DataTable dt = Db.ExecProcDataTable("proc_Aarop", parm);
            return dt;
        }
        #endregion

     
         
        public DataTable AddFinalStatus(ComplaintDetails model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Action", model.Action),
            new SqlParameter("@ComplaintNo", model.ComplaintNo),
            new SqlParameter("@FinalOfficeMemorandumNo", model.FinalOfficeMemorandumNo),
            new SqlParameter("@FinalDate", model.FinalDate),
            new SqlParameter("@FinalStatus", model.FinalStatus),
            new SqlParameter("@FinalAttatchment", model.FinalAttatchment),
            new SqlParameter("@FinalRemarks", model.FinalRemarks), 
            new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
        };
            DataTable dt = Db.ExecProcDataTable("proc_AddFinalStatus", parm);
            return dt;
        }

        #region Om
        public DataTable AddFinalRevAppStatus(ComplaintDetails model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Action", model.Action),
            new SqlParameter("@ComplaintNo", model.ComplaintNo),
            new SqlParameter("@FinalOfficeMemorandumNo", model.FinalOfficeMemorandumNo),
            new SqlParameter("@FinalDate", model.FinalDate),
            new SqlParameter("@FinalStatus", model.FinalStatus),
            new SqlParameter("@FinalAttatchment", model.FinalAttatchment),
            new SqlParameter("@FinalRemarks", model.FinalRemarks),
            new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
        };
            DataTable dt = Db.ExecProcDataTable("Proc_RevisionAppealFinalResult", parm);
            return dt;
        }
        #endregion


        #region Om
        public DataTable Addcourtcaseinsert(ComplaintDetails model)
        {
            SqlParameter[] parm = {
                    new SqlParameter("@Action", model.Action),
                    new SqlParameter("@ComplaintNo", model.ComplaintNo),
                    new SqlParameter("@CaseNo", model.CaseNo),
                    new SqlParameter("@CourtId", model.CourtId),
                    new SqlParameter("@InvestigationId", model.InvestigationId),
                    new SqlParameter("@OtherCourt", model.OtherCourt),
                    new SqlParameter("@OtherAgencyName", model.OtherAgencyName),
                    new SqlParameter("@CaseType", model.CaseType),
                    new SqlParameter("@YearId", model.YearId),
                    new SqlParameter("@Attachment", model.Attachment),
                    new SqlParameter("@ComputerNumber", model.ComputerNumber),
                    new SqlParameter("@Remark", model.Remark),
                    new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
                };
            DataTable dt = Db.ExecProcDataTable("proc_Addcourt", parm);
            return dt;
        }
        #endregion
        public DataTable ReminderRepresentation(ComplaintDetails model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Action", model.Action),
            new SqlParameter("@LetterNumber", model.LetterNumber),
            new SqlParameter("@LetterDate", model.LetterDate),
            new SqlParameter("@ComplaintNo", model.ComplaintNo),
            new SqlParameter("@ActionName", model.ActionName),
            new SqlParameter("@Attatchment", model.Attatchment), 
            new SqlParameter("@Flag", model.Flag),
            new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
        };
            DataTable dt = Db.ExecProcDataTable("proc_ReminderRepresentation", parm);
            return dt;
        }
        public DataTable AddHearing(ComplaintDetails model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Action", model.Action),
            new SqlParameter("@LetterNumber", model.LetterNumber),
            new SqlParameter("@LetterDate", model.LetterDate),
            new SqlParameter("@ComplaintNo", model.ComplaintNo),
            new SqlParameter("@ActionName", model.ActionName),
            new SqlParameter("@Attatchment", model.Attatchment),
            new SqlParameter("@Flag", model.Flag),
            new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
            new SqlParameter("@ForwadingOfficerUserNumber", model.ForwadingOfficerUserNumber),
            new SqlParameter("@ForwardingOfficerComment", model.ForwardingOfficerComment),
        };
            DataTable dt = Db.ExecProcDataTable("proc_Hiring", parm);
            return dt;
        }


        public DataSet GetDepartmentalComplaintReport(DepartmentalComplaint model)
        {
            SqlParameter[] parm = { 
            new SqlParameter("@ComplaintNo", model.ComplaintNO), 
            new SqlParameter("@Action", model.Action), 
            new SqlParameter("@Id", model.Id),
            new SqlParameter("@ZoneId", model.ZoneId),
            new SqlParameter("@CircleId", model.CircleId),
            new SqlParameter("@DivisionId", model.DivisionId),
             new SqlParameter("@OptionalPr", model.OptionalPr),
             new SqlParameter("@CourtId", model.CourtId),
             new SqlParameter("@InvestigationId", model.InvestigationId),
             new SqlParameter("@Fromdate", model.Fromdate),
           new SqlParameter("@todate", model.todate),
       new SqlParameter("@EnquiryType", string.IsNullOrEmpty(model.EnquiryType) ? "-1" : model.EnquiryType),
       new SqlParameter("@ComplaintRelatedTo", string.IsNullOrEmpty(model.ComplaintRelatedTo) ? "-1" : model.ComplaintRelatedTo),
        new SqlParameter("@PreviousComplaintDesignationId",model.PreviousComplaintDesignationId==0? -1 : model.PreviousComplaintDesignationId),
          new SqlParameter("@ComplainRelatedDesignationId",model.ComplainRelatedDesignationId==0? -1 : model.ComplainRelatedDesignationId),
         new SqlParameter("@ComplaintRelatedZoneId",model.ComplaintRelatedZoneId==0? -1 : model.ComplaintRelatedZoneId),
        new SqlParameter("@ComplaintRelatedCircleId",model.ComplaintRelatedCircleId==0? -1 : model.ComplaintRelatedCircleId),
        new SqlParameter("@ComplaintRelatedDivisionId",model.ComplaintRelatedDivisionId==0? -1 : model.ComplaintRelatedDivisionId),
         //new SqlParameter("@EmployeeId", model.EmployeeId),
    new SqlParameter("@EmployeeId", string.IsNullOrWhiteSpace(model.EmployeeId) ? (object)DBNull.Value : (object)model.EmployeeId),
     new SqlParameter("@ComplainantName", string.IsNullOrWhiteSpace(model.ComplainantName) ? (object)DBNull.Value : (object)model.ComplainantName),
        new SqlParameter("@R_U", string.IsNullOrEmpty(model.R_U) ? "-1" : model.R_U),

        };

            DataSet ds = Db.ExecuteDataSet("Proc_ReportDashboard", parm);
            return ds;
        }

        public DataSet GetReportPendingComplaintEnquiryOfficer(DepartmentalComplaint model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@action", model.Action), 
            new SqlParameter("@Id", model.Id),
            new SqlParameter("@OptionalPr", model.OptionalPr),
            new SqlParameter("@ComplaintNo", model.ComplaintNO), 
            new SqlParameter("@RoleId", model.RoleId),
            new SqlParameter("@ZoneId", model.ZoneId),
            new SqlParameter("@CircleId", model.CircleId),
            new SqlParameter("@DivisionId", model.DivisionId),
        };
            DataSet ds = Db.ExecuteDataSet("proc_DashboardReportCommonList", parm);
            return ds;
        }

        public DataTable UpdateUserProfile(UserLogin model)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = {
            new SqlParameter("@action", model.action), 
            new SqlParameter("@MobileNo", model.MobileNo),
            new SqlParameter("@EmailId", model.EmailId),
            new SqlParameter("@UserNumber", model.UserNumber) 
        };
            dt = Db.ExecProcDataTable("proc_UpdateUserEmailID", parm);
            return dt;
        }

        #region Om
        public DataTable InsertUpdateAppeal(ComplaintDetails model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Action", model.Action),
            new SqlParameter("@ComplaintNo", model.ComplaintNo),
            new SqlParameter("@FileNumber", model.FileNumber),
            new SqlParameter("@Attatchhment", model.Attatchhment),
            new SqlParameter("@Desctioption", model.Desctioption),
            new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
            new SqlParameter("@ForwadingOfficerUserNumber", model.ForwadingOfficerUserNumber), 
        };
            DataTable dt = Db.ExecProcDataTable("proc_AppealEnquiry", parm);
            return dt;
        }
       
        public DataTable InsertUpdateRevision(ComplaintDetails model)
        {
            SqlParameter[] parm = {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@ComplaintNo", model.ComplaintNo),
                new SqlParameter("@FileNumber", model.FileNumber),
                new SqlParameter("@Attatchhment", model.Attatchhment),
                new SqlParameter("@Desctioption", model.Desctioption),
                new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
                new SqlParameter("@ForwadingOfficerUserNumber", model.ForwadingOfficerUserNumber), 
         };
            DataTable dt = Db.ExecProcDataTable("proc_RevisionEnquiry", parm);
            return dt;
        }

        #endregion

        #region  Filter Section
        public DataSet GetDepartmentalComplaint(DepartmentalComplaint model)

        {
            SqlParameter[] parm = {
     new SqlParameter("@RoleType", model.RoleId),
     new SqlParameter("@ComplaintNo", model.ComplaintNO),
     new SqlParameter("@WorkingName", model.WorkingName),
     new SqlParameter("@ComplaintDescription", model.ComplaintDescription),
     new SqlParameter("@Attachment", model.Attachment),
     new SqlParameter("@CreatedBy", model.CreatedBy),
     new SqlParameter("@IPAddress", model.IPAddress),
     new SqlParameter("@Action", model.Action),
     new SqlParameter("@ComplaintDesignation", model.ComplaintDesignation),
     new SqlParameter("@ComplaintMarkedDesignation", model.ComplaintMarkedDesignation),
      new SqlParameter("@EnquiryType", string.IsNullOrEmpty(model.EnquiryType) ? "-1" : model.EnquiryType), 
     
      new SqlParameter("@DepartmentalRemarkSubject", model.DepartmentalRemarkSubject),
      new SqlParameter("@DepartmentalRemarkDescription", model.DepartmentalRemarkDescription),
   
    
       new SqlParameter("@ForwadingOfficer", model.ForwadingOfficer),
       new SqlParameter("@ForwardingOfficerDate", model.ForwardingOfficerDate),
       new SqlParameter("@ForwardingOfficerComment", model.ForwardingOfficerComment),
    
       new SqlParameter("@DepartmentalComplaintMarkToZC", model.DepartmentalComplaintMarkToZC),
       new SqlParameter("@DepartmentalComplaintMarkedZone", model.DepartmentalComplaintMarkedZone),
       new SqlParameter("@DepartmentalComplaintMarkedCircle", model.DepartmentalComplaintMarkedCircle),
       new SqlParameter("@DepartmentalComplaintMarkToOfficer", model.DepartmentalComplaintMarkToOfficer),
       new SqlParameter("@DepartmentalComplaintMarkedDate", model.DepartmentalComplaintMarkedDate),
       new SqlParameter("@DepartmentalComplaintMarkedNo", model.DepartmentalComplaintMarkedNo),
       new SqlParameter("@DepartmentalComplaintMarkedRemark", model.DepartmentalComplaintMarkedRemark),
       new SqlParameter("@CourtId", model.CourtId),
       new SqlParameter("@InvestigationId", model.InvestigationId),
       new SqlParameter("@Fromdate", model.Fromdate),
       new SqlParameter("@todate", model.todate),
       new SqlParameter("@ComplaintRelatedTo", string.IsNullOrEmpty(model.ComplaintRelatedTo) ? "-1" : model.ComplaintRelatedTo),
        new SqlParameter("@PreviousComplaintDesignationId",model.PreviousComplaintDesignationId==0? -1 : model.PreviousComplaintDesignationId),
         new SqlParameter("@ComplaintRelatedZoneId",model.ComplaintRelatedZoneId==0? -1 : model.ComplaintRelatedZoneId),
        new SqlParameter("@ComplaintRelatedCircleId",model.ComplaintRelatedCircleId==0? -1 : model.ComplaintRelatedCircleId),
        new SqlParameter("@ComplaintRelatedDivisionId",model.ComplaintRelatedDivisionId==0? -1 : model.ComplaintRelatedDivisionId),
         //new SqlParameter("@EmployeeId", model.EmployeeId),
    new SqlParameter("@EmployeeId", string.IsNullOrWhiteSpace(model.EmployeeId) ? (object)DBNull.Value : (object)model.EmployeeId),
     new SqlParameter("@ComplainantName", string.IsNullOrWhiteSpace(model.ComplainantName) ? (object)DBNull.Value : (object)model.ComplainantName),
     new SqlParameter("@msg", System.Data.SqlDbType.NVarChar, 250),
 };

            DataSet ds = Db.ExecuteDataSet("proc_DepartmentalComplaint", parm);
            return ds;
        }
        #endregion
        public DataTable GenerateValidateOTP(UserLogin model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Action", model.action),
            new SqlParameter("@OTP", model.OTP),
            new SqlParameter("@MobileNo", model.MobileNo),
            new SqlParameter("@UserRelatedTo", model.UserRelatedTo),
            new SqlParameter("@ZoneId", model.ZoneId==0?-1:model.ZoneId),
            new SqlParameter("@CircleId", model.CircleId==0?-1:model.CircleId),
            new SqlParameter("@DivisionId", model.DivisionId==0?-1:model.DivisionId),
        };
            DataTable dt = Db.ExecProcDataTable("User_Login_With_OTP", parm);
            return dt;
        }
        public DataSet ShowCauseInsertUpdateSelect(ShowCauseNoticCls model)
        {
            SqlParameter[] parm = {
        new SqlParameter("@Action", model.Action),
        new SqlParameter("@ActionName", model.ActionName),
        new SqlParameter("@ShowcauseNumber", model.ShowcauseNumber),
         new SqlParameter("@IssuingAuthorityName", string.IsNullOrEmpty(model.IssuingAuthorityName) ? "-1" : model.IssuingAuthorityName),
        new SqlParameter("@Fromdate", string.IsNullOrWhiteSpace(model.Fromdate) ? (object)DBNull.Value : (object)model.Fromdate),
         new SqlParameter("@todate", string.IsNullOrWhiteSpace(model.todate) ? (object)DBNull.Value : (object)model.todate),
         new SqlParameter("@NoticeRelatedTo", string.IsNullOrEmpty(model.NoticeRelatedTo) ? "-1" : model.NoticeRelatedTo),
        new SqlParameter("@NoticeRelatedZoneId",model.NoticeRelatedZoneId==0? -1 : model.NoticeRelatedZoneId),
        new SqlParameter("@NoticeRelatedCircleId",model.NoticeRelatedCircleId==0? -1 : model.NoticeRelatedCircleId),
        new SqlParameter("@NoticeRelatedDivisionId",model.NoticeRelatedDivisionId==0? -1 : model.NoticeRelatedDivisionId),
        new SqlParameter("@NoticeRelatedDesignationId",model.NoticeRelatedDesignationId==0? -1 : model.NoticeRelatedDesignationId),
        new SqlParameter("@ComplainantName", string.IsNullOrWhiteSpace(model.ComplainantName) ? (object)DBNull.Value : (object)model.ComplainantName),
        new SqlParameter("@Status", string.IsNullOrWhiteSpace(model.Status) ? (object)DBNull.Value : (object)model.Status),
        new SqlParameter("@IssueletterNumber", model.IssueletterNumber),
        new SqlParameter("@IssueletterDate", model.IssueletterDate),
        new SqlParameter("@LastdateforSubmission", model.LastdateforSubmission),
        new SqlParameter("@NoticeDetails", model.NoticeDetails),
        new SqlParameter("@Attachment", model.AttachmentPath),
        new SqlParameter("@AnushMarkAttachment", model.AttachmentPath),
        new SqlParameter("@AnushMarkDescription", model.AnushMarkDescription),
        new SqlParameter("@ShowCauseForwordingOfficer", model.ShowCauseForwordingOfficer),
        new SqlParameter("@ShowcauseForwordingDescription", model.ShowcauseForwordingDescription),
        new SqlParameter("@FinalOfficeMemorandumNo", model.FinalOfficeMemorandumNo),
        new SqlParameter("@FinalDate", model.FinalDate),
        new SqlParameter("@FinalStatus", model.FinalStatus),
        new SqlParameter("@FinalAttatchment", model.AttachmentPath),
        new SqlParameter("@FinalDescription", model.FinalDescription),
        new SqlParameter("@CreatedBy_User_Number", model.CreatedBy_User_Number),
        };
            DataSet dt = Db.ExecuteDataSet("SP_ShowCauseNotice", parm);
            return dt;
        }
        public DataTable ShowUserDetails(ComplaintDetail model)
        {
            SqlParameter[] parm = {
                 new SqlParameter("@Action", model.Action),
                 new SqlParameter("@ComplaintNo", model.ComplaintNo),
                };
            DataTable dt = Db.ExecProcDataTable("dbo.proc_UserDetails", parm);
            return dt;
        }


        public DataSet GetPendingComplaintEnquiryOfficer(DepartmentalComplaint model)
        {
            SqlParameter[] parm = {
            new SqlParameter("@action", model.Action),
            new SqlParameter("@UserNumber", model.CreatedBy),
            new SqlParameter("@Id", model.Id),
            new SqlParameter("@OptionalPr", model.OptionalPr),
            new SqlParameter("@ComplaintNo", model.ComplaintNO),
            new SqlParameter("@RoleId", model.RoleId),
            new SqlParameter("@ZoneId", model.ZoneId),
            new SqlParameter("@CircleId", model.CircleId),
            new SqlParameter("@DivisionId", model.DivisionId),
            new SqlParameter("@Fromdate", string.IsNullOrWhiteSpace(model.Fromdate) ? (object)DBNull.Value : (object)model.Fromdate),
            new SqlParameter("@todate", string.IsNullOrWhiteSpace(model.todate) ? (object)DBNull.Value : (object)model.todate),
            new SqlParameter("@ComplainantName", string.IsNullOrWhiteSpace(model.ComplainantName) ? (object)DBNull.Value : (object)model.ComplainantName),
            new SqlParameter("@PreviousComplaintDesignationId",model.PreviousComplaintDesignationId==0? -1 : model.PreviousComplaintDesignationId),
             new SqlParameter("@ComplainRelatedDesignationId",model.ComplainRelatedDesignationId==0? -1 : model.ComplainRelatedDesignationId),
               new SqlParameter("@R_U", string.IsNullOrEmpty(model.R_U) ? "-1" : model.R_U),
             new SqlParameter("@ComplaintRelatedTo", string.IsNullOrEmpty(model.ComplaintRelatedTo) ? "-1" : model.ComplaintRelatedTo), 
          new SqlParameter("@ComplaintRelatedCircleId",model.ComplaintRelatedCircleId==0? -1 : model.ComplaintRelatedCircleId),
          new SqlParameter("@ComplaintRelatedDivisionId",model.ComplaintRelatedDivisionId==0? -1 : model.ComplaintRelatedDivisionId), 
        };
            DataSet ds = Db.ExecuteDataSet("SP_SECommonList", parm);
            return ds;
        }
        public DataTable DesignationBind(ComplaintDetails model)
        {
            SqlParameter[] parm = { 
                 new SqlParameter("@ForwadingOfficerUserNumber", model.ForwadingOfficerUserNumber),
                };
            DataTable dt = Db.ExecProcDataTable("proc_get_designation_usernumber", parm);
            return dt;
        }
    }

}                                                                                                            