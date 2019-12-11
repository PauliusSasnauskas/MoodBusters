﻿using System;
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

                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Id, Ip, Name FROM [dbo].[User]", connection);
                

                DataSet bistroSet = new DataSet();
                dataAdapter.Fill(bistroSet);

                DataTable userTable = bistroSet.Tables[0];
                dataAdapter.Fill(userTable);
                
                string a = "Tables:\n" + bistroSet.Tables[0].TableName;
                return a;
            }
        }
    }
}