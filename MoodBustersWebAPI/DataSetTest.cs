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

                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Id, Ip, Name FROM [User]", connection);

                DataSet bistroSet = new DataSet();
                dataAdapter.Fill(bistroSet, "User");

                dataAdapter.SelectCommand = new SqlCommand("SELECT Id, UserId, DateTime, ByteCount FROM [LogRecord]", connection);
                dataAdapter.Fill(bistroSet, "LogRecord");

                string a = "Log:\n";
                foreach (DataRow row in bistroSet.Tables["LogRecord"].Rows)
                {
                    a += "UserId: " + row.Field<int>("UserId") + ", ";
                    a += "Time: " + row.Field<DateTime>("DateTime").ToString() + ", ";
                    a += "Bytes sent: " + row.Field<decimal>("ByteCount").ToString() + '\n';
                }
                return a;
            }
        }
    }
}