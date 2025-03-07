using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CourtCase.Models
{
	public class Reports
	{
		public string ZoneId { get; set; }
		public string CircleId { get; set; }
		public string DivisionId { get; set; }
		public string CaseType { get; set; }
		public string Action { get; set; }


		public DataSet getDataSet()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlDataAdapter da = new SqlDataAdapter("SP_Reports", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
				da.SelectCommand.Parameters.AddWithValue("@CircleId", this.CircleId);
				da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.DivisionId);
				da.SelectCommand.Parameters.AddWithValue("@CaseType", this.CaseType);
				da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);



				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
		}
        public DataSet getCaseDataSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_AdminReports", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
                da.SelectCommand.Parameters.AddWithValue("@CircleId", this.CircleId);
                da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.DivisionId);
                da.SelectCommand.Parameters.AddWithValue("@CaseType", this.CaseType);
                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);



                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
    }
}