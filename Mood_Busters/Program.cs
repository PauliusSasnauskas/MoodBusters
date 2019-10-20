using System;
using System.Windows.Forms;

namespace Mood_Busters
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string url = System.Configuration.ConfigurationManager.AppSettings["webServiceUrl"];
            Application.Run(new MBWindow(new WebRequestRecognitionApi(url)));

        }
    }
}
