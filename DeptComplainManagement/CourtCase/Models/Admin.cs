using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CourtCase.Models
{
    public class Admin
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string Action { get; set; }
        public DataTable Table { get; set; }
        public string IP_Address { get; set; }
        public int CreatedBy_User_Number { get; set; }
    }
    public class result
    {
        public int SR { get; internal set; }
        public string Msg { get; internal set; }
	}
    public class Circle
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public int CircleId { get; set; }
        public string CircleName { get; set; }
        public string Action { get; set; }
        public DataTable Table { get; set; }
        public string IP_Address { get; set; }
        public int CreatedBy_User_Number { get; set; }
    }
    public class Division
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int CircleId { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string CircleName { get; set; }
        public string Action { get; set; }
        public DataTable Table { get; set; }
        public string IP_Address { get; set; }
        public int CreatedBy_User_Number { get; set; }
    }
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string RoleTypeId { get; set; }
        public string Action { get; set; }
        public DataTable Table { get; set; }
        public string IP_Address { get; set; }
        public int CreatedBy_User_Number { get; set; }
    }
    public class SubjectMaster
    {
        public int subjectMatterId { get; set; }
        public string subjectMatter { get; set; }
        public string RelatedTo { get; set; }
        public string Action { get; set; }
        public DataTable Table { get; set; }
        public string IP_Address { get; set; }
        public int CreatedBy_User_Number { get; set; }
    }
    public class ComplaintDetail
    {
        public int Id { get; set; }
        public string ComplaintType { get; set; }
        public string ComplaintNo { get; set; }
        public string ComplaintName { get; set; }
        public string ComplaintMobile { get; set; }
        public string NameofPersonnel { get; set; }
        public int Designation { get; set; }
        public string DesignationName { get; set; }
        public string OfficeRelatedTo { get; set; }
        public int OfficeRelatedZoneId { get; set; }
        public int OfficeRelatedCircleId { get; set; }
        public int OfficeRelatedDivision { get; set; }
        public string ComplaintAddress { get; set; }
        public string ComplaintDescription { get; set; }
        public string Attachment { get; set; }
        public string ComplaintSubject { get; set; }
        public string ComplaintDate { get; set; }
        public string CreatedBy { get; set; }
        public DataTable Table { get; set; }
        public string IPAddress { get; set; }
        public string Action { get; set; }
        public string msg { get; set; }
        public string filepath { get; set; }
        public string filetype { get; set; }
        public string CompalintedWorkDes { get; set; }
        public string ComplaintMarkedOfficer { get; set; }
        public string InsptComplaintMarkedOfficer { get; set; }
        public string InvestigatingOfficerAppointedDate { get; set; }
        public string InvestigatingLastAppointedDate { get; set; }
        public HttpPostedFileBase AttatchmentFile { get; set; }
         // Farwarded Officers
        public string FarwardedDesignation { get; set; }
        public string FarwardedMarkedDate { get; set; }
        public string FarwardedMarkedLastDate { get; set; }
        public string FarwardedMarkedDiscription { get; set; }
        public string RoleTypeName { get; set; }
        public string ZoneName { get; set; }
        public string CircleName { get; set; }
        public string DivisionName { get; set; }
        public string UpdateComplaintInsptdate { get; set; }
        public string RemarkByDepartment { get; set; }
        public string ActiveStatus { get; set; }
        public string Remark { get; set; }
        public string OtherComplaintDocument { get; set; }
        public string FarwardedSataus { get; set; }
        public string ComplaintStatus { get; set; }




    }
    public class DepartmentalComplaint
    {
        public int Id { get; set; }
        public int ComplaintRelatedZoneId { get; set; }
        public int ComplaintRelatedDivisionId { get; set; }
        public int ComplaintRelatedCircleId { get; set; }
        public string OptionalPr { get; set; }
        public string RoleType { get; set; }
        public string EnquiryType { get; set; }
        public string WorkingName { get; set; }
        public string RoleId { get; set; }
        public string ComplaintDesignation { get; set; }
        public string ComplaintMarkedDesignation { get; set; }
        public string Attachment { get; set; }
        public string ComplaintSubject { get; set; }
        public string ComplaintDate { get; set; }
        public string CreatedBy { get; set; }
        public DataTable Table { get; set; }
        public string IPAddress { get; set; }
        public string Action { get; set; }
        public string msg { get; set; }
        public string filepath { get; set; }
        public string filetype { get; set; }
        public string ComplaintNO { get; set; }
        public string ComplaintDescription { get; set; }
        public int ZoneId { get; set; }
        public int CircleId { get; set; }
        public int DivisionId { get; set; }
        public int CourtId { get; set; }
        public int InvestigationId { get; set; }

        // Add Remarked
        public string DepartmentalRemarkSubject { get; set; } 
        public string DepartmentalRemarkDescription { get; set; }
        public string DepartmentalMarkToRoleType { get; set; }
       
   
     //forwording Officer 
        public string ForwardingOfficerDate { get; set; }
        public string ForwardingOfficerLetterNo { get; set; }
        public string ForwardingOfficerLetter { get; set; }
        public string ForwardingOfficerComment { get; set; }
        public string ForwardingOfficerZCDH { get; set; }
        public string ForwadingOfficer { get; set; }

        // marked Officer 

        public string DepartmentalComplaintMarkToZC { get; set; }
        public string DepartmentalComplaintMarkedZone { get; set; }
        public string DepartmentalComplaintMarkedCircle { get; set; }
        public string DepartmentalMarkedOfficerDesignations { get; set; }
        public string DepartmentalComplaintMarkedDate { get; set; }
        public string DepartmentalComplaintMarkedNo { get; set; }
        public string DepartmentalComplaintMarkedRemark { get; set; }
        public string DepartmentalComplaintMarkToOfficer { get; set; }

        public string Fromdate { get; set; }
        public string todate { get; set; }
        public string ComplainantName { get; set; }
        public string ComplaintRelatedTo { get; set; }
        public string EmployeeId { get; set; }
        public int PreviousComplaintDesignationId { get; set; }
        public int ComplainRelatedDesignationId { get; set; }
        public string R_U { get; set; }


    }

    public class AdverseEntry
    {
        public string Description { get; set; }

        public string KarmikName { get; set; }
        public string Designation { get; set; }
        public string NirgatKaryalaya { get; set; }
        public string Ordernumber { get; set; }
        public string DocumentNo { get; set; }
        public string Attachment { get; set; }
        public string filepath { get; set; }
        public string filetype { get; set; }
        public string Date { get; set; }
        public string Action { get; set; }
        public string IPAddress { get; set; }
        public string CreatedBy { get; set; }
        public string msg { get; set; }
        public int Id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }




        public bool BindData()
        {

            DataSet ds = new DataSet();
            ds = getDataAdverseEntry();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.Id = Convert.ToInt32(dr["Id"]);
                    this.Description = dr["Description"].ToString();
                    this.KarmikName = dr["KarmikName"].ToString();
                    this.Designation = dr["DesignationName"].ToString();
                    this.NirgatKaryalaya = dr["NirgatKaryalaya"].ToString();
                    this.Ordernumber = dr["Ordernumber"].ToString();
                    this.DocumentNo = dr["DocumentNo"].ToString();
                    this.Attachment = dr["Date"].ToString();

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





        public DataSet getDataAdverseEntry()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_SpecialAdverseEntry", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Description", this.Description);
                da.SelectCommand.Parameters.AddWithValue("@KarmikName", this.KarmikName);
                da.SelectCommand.Parameters.AddWithValue("@Designation", this.Designation);
                da.SelectCommand.Parameters.AddWithValue("@NirgatKaryalaya", this.NirgatKaryalaya);
                da.SelectCommand.Parameters.AddWithValue("@Ordernumber", this.Ordernumber);
                da.SelectCommand.Parameters.AddWithValue("@DocumentNo", this.DocumentNo);
                da.SelectCommand.Parameters.AddWithValue("@Date", this.Date);
                da.SelectCommand.Parameters.AddWithValue("@Attachment", this.Attachment);
                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
                da.SelectCommand.Parameters.AddWithValue("@Id", this.Id);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

      

    }
}