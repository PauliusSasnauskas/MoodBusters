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
            Application.Run(new Form1());
            //DatabaseConnection.SaveData("CALM", "2019-09-26 03:28:09", "Lithuania");            TESTING DATABASE
            //Console.WriteLine(DatabaseConnection.LoadData("1"));
        }
    }
}
