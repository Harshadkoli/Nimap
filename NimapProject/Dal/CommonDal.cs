using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NimapProject.Dal
{
    public class CommonDal
    {
        string ConnectionString = string.Empty;
        SqlConnection SqlConn = new SqlConnection();

        public CommonDal()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConn = new SqlConnection(ConnectionString);
                SqlConn.Open();
            }
            catch (Exception ex)
            {

                throw;
            }
            return SqlConn;
        }
    }
}