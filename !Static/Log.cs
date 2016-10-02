using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR._Static
{
    public static class Log
    {
        public static List<string> log;

        public static void InitLog()
        {
            log = new List<string>();
            log.Add("LOG STARTED ON " + DateTime.Now.ToString(new CultureInfo("en-US")));
        }

        public static void SetEntry(string function, string action, string var, int val)
        {
            log.Add("Function: " + function + "--- " + action + ", variable: " + var + ", value: $" + val.ToString("X6"));
        }

        public static void SetEntry(string arrayName, int length)
        {
            log.Add("Array Name: " + arrayName + ", length: " + length.ToString("X6"));
        }

        public static void SetEntry(string function, int index, int val)
        {
            log.Add("Function: " + function + ", index: " + index + ", value: $" + val.ToString("X6"));
        }

        public static void SetEntry(string message)
        {
            log.Add("------ " + message + " ------");
        }

        public static void WriteLog()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", false))
                {
                    foreach (string s in log)
                    {
                        sr.WriteLine(s);
                    }

                    sr.Flush();
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not write log to " + AppDomain.CurrentDomain.BaseDirectory + "log.txt!\n\nError: " + e.Message);
            }
            
        }
    }
}
