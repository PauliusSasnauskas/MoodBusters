using System;
using System.IO;
using System.Windows.Forms;

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
                    DirectoryInfo dir = Directory.GetParent(
                                    Directory.GetParent(
                                        Directory.GetCurrentDirectory()).ToString());

                    try
                    {
                        //TODO: Probably decrypt keys.txt which would be encrypted??

                        return (File.ReadAllLines(String.Concat(dir.ToString(), "\\keys.txt")));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(StringConst.ErrLicenceNotFound, StringConst.ErrLicense, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return instance;
            }
        }
    }
}
