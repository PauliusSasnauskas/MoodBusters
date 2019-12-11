using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MoodBustersWebAPI
{
    public class DataSetTest
    {
        public string PrintTables()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["MoodBistroDBConnectionString"].ConnectionString;

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                connection.Open();

                dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Ip, Name FROM User", connection);
                DataTable userTable = new DataTable("User");
                dataAdapter.Fill(userTable);

                dataAdapter.SelectCommand = new SqlCommand("SELECT Id, UserId, DateTime, ByteCount FROM dbo.LogRecord", connection);
                DataTable logTable = new DataTable("LogRecord");
                dataAdapter.Fill(logTable);

                DataSet bistroSet = new DataSet("MoodBistro");
                bistroSet.Tables.Add(userTable);
                bistroSet.Tables.Add(logTable);

                string a = "Tables:\n" + bistroSet.Tables[0].TableName + " " + bistroSet.Tables[1];
                return a;
            }
        }
    }
}