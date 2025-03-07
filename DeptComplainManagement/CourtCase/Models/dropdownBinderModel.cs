using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CourtCase.Models
{
	public class dropdownBinderModel
	{
		public string id { get; set; }
		public string value { get; set; }
		public string Action { get; set; }
		public string CircleId { get; set; }
		public string ZoneId { get; set; }
		public string DivisionId { get; set; }
		public string RoleTypeId { get; set; }
		public string CaseRelatedTo { get; set; }
		public string DesignationId { get; set; }
		public string UserNumber { get; set; }



        public DataSet getDataSet()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlDataAdapter da = new SqlDataAdapter("dropdownBinderSP_Select", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				
				da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
				da.SelectCommand.Parameters.AddWithValue("@RoleTypeId", this.RoleTypeId);
				da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
				da.SelectCommand.Parameters.AddWithValue("@Circleid", this.CircleId);
				da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.DivisionId);
				da.SelectCommand.Parameters.AddWithValue("@CaseRelatedTo", this.CaseRelatedTo);
				da.SelectCommand.Parameters.AddWithValue("@DesignationId", this.DesignationId); 
                DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
		}

        public DataSet getLoginDataSet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("dropdownBinderforLogin", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;


                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
                da.SelectCommand.Parameters.AddWithValue("@RoleTypeId", this.RoleTypeId);
                da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
                da.SelectCommand.Parameters.AddWithValue("@Circleid", this.CircleId);
                da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.DivisionId);
                da.SelectCommand.Parameters.AddWithValue("@CaseRelatedTo", this.CaseRelatedTo);
                da.SelectCommand.Parameters.AddWithValue("@DesignationId", this.DesignationId);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public List<dropdownBinderModel> getDataToBind()
		{
			DataSet ds = getDataSet();
			List<dropdownBinderModel> ls = new List<dropdownBinderModel>();
			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						dropdownBinderModel obj = new dropdownBinderModel();
						obj.id = dr["id"].ToString();
						obj.value = dr["value"].ToString(); 
                        ls.Add(obj);
					}
				}
			}
			return ls;
		}
        public List<dropdownBinderModel> getDataToBindData()
        {
            DataSet ds = getDataSet();
            List<dropdownBinderModel> ls = new List<dropdownBinderModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dropdownBinderModel obj = new dropdownBinderModel();
                        obj.id = dr["id"].ToString();
                        obj.value = dr["value"].ToString();
                        obj.UserNumber = dr["UserNumber"].ToString();
                        ls.Add(obj);
                    }
                }
            }
            return ls;
        }


        public List<dropdownBinderModel> BindLoginData()
        {
            DataSet ds = getLoginDataSet();
            List<dropdownBinderModel> ls = new List<dropdownBinderModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dropdownBinderModel obj = new dropdownBinderModel();
                        obj.id = dr["id"].ToString();
                        obj.value = dr["value"].ToString();
                        ls.Add(obj);
                    }
                }
            }
            return ls;
        }

    }
}