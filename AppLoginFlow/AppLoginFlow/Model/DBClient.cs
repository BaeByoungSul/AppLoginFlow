using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AppLoginFlow.Model
{
  
    public class DBClient
    {


        private SqlConnection pDbConn;

        public DBClient(string sConnName)
        {
            if (sConnName == "BSBAE_HOME")
            {
                pDbConn = new SqlConnection(GetConnString_BSBAE_HOME());
            }
            else
                throw new Exception("Unknown Connection name!!");
            
            pDbConn.Open();
        }

        public DataSet GetDataSet(SqlCommand dbcmd)
        {
            try
            {
                DataSet ds = new DataSet();

                dbcmd.Connection = pDbConn;
                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbcmd);
                dbAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
        private string GetConnString_BSBAE_HOME()
        {
            string srvrdbname = "TestDB";
            string srvrname = "bsbae.ddns.net,51433";
            string srvrusername = "sa";
            string srvrpassword = "wkehd124!@$";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            
            return sqlconn;
        }
    }

}
