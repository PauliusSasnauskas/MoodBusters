using System;
using System.Windows.Forms;
using System.Configuration;

namespace Mood_Busters
{
    public sealed class Key
    {
        private static string[] instance = null;

        private Key() { }

        public static string[] GetKeys
        {
            get
            {
                if (instance == null)
                {
                    try
                    {
                        return new string[] { ConfigurationManager.AppSettings.Get("Key0"), ConfigurationManager.AppSettings.Get("Key1") };
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("License key not found.", "Error_license", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return instance;
            }
        }
    }
}
