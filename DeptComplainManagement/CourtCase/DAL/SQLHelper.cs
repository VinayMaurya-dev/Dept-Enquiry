using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CourtCase.DAL
{
 
    public class SQLHelper
    { 
		string consString = ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBLayer"].ConnectionString);
        public DataTable ExecProcDataTable(string ProName, SqlParameter[] Param)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(ProName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                if (Param != null)
                {
                    foreach (SqlParameter prm in Param)
                    {
                        cmd.Parameters.Add(prm);
                    }
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet ExecuteDataSet(string CommandName, SqlParameter[] param)
        {
            if (string.IsNullOrWhiteSpace(CommandName))
            {
                throw new ArgumentException("Cannot be empty", nameof(CommandName));
            }
            using (SqlConnection conn = new SqlConnection(consString))
            {
                SqlCommand cmd = new SqlCommand(CommandName, conn);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            cmd.Parameters.Add(param[i]);
                        }
                    }

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                    return ds;
                }
                catch (SqlException ex)
                {
                    throw;
                }
                finally
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    conn.Close();
                }
            }
        }
    }


}





    