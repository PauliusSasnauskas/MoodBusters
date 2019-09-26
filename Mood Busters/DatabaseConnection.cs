using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mood_Busters
{
    public static class DatabaseConnection
    {
        private static string save_dataURL = "http://ingvarasgalinskas.000webhostapp.com/mood_busters/save_data.php";
        private static string load_dataURL = "http://ingvarasgalinskas.000webhostapp.com/mood_busters/load_data.php";

        public static void SaveData(string mood, string date_time, string location)
        {
            string urlAddress = save_dataURL;

            using (WebClient client = new WebClient())
            {
                NameValueCollection postData = new NameValueCollection();
                postData["mood"] = mood;
                postData["date_time"] = date_time;
                postData["location"] = location;

                string data = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                Console.WriteLine(data);
            }
        }

        public static string LoadData(string id)
        {
            string urlAddress = load_dataURL;

            using (WebClient client = new WebClient())
            {
                NameValueCollection postData = new NameValueCollection();
                postData["id"] = id;

                string data = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                return data;
            }
        }
    }
}
