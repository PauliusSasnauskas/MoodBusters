using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MoodBustersWebAPI.Database
{
    public class DataSetTest
    {
        public DataSetTest()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = WebConfigurationManager.ConnectionStrings["MoodBistroDBConnectionString"].ConnectionString;

                IDataAdapter dataAdapter = new SqlDataAdapter("SELECT Id, UserId, DateTime, ByteCount FROM LogRecord", connection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                    
                DataTable dataTable = new DataTable("LogRecord");




            }
        }
    }
}