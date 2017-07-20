using Sitecore.Shell.Applications.ContentEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xConnectAPI
{
    public static class Configuration
    {
        //Read\Write cconfiguration
        public static string ReadConfiguration(string path)
        {
            if (System.IO.File.Exists(path))
            {
                string result = System.IO.File.ReadAllText(path);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
                return "";
            }
            return null;
        }


        private static void CreateConfigurationFile()
        {
            System.IO.StreamWriter textFile = new System.IO.StreamWriter(@"config.ini");
            textFile.Close();
        }

        public static void WriteXConnectUrlToFile(string path, string xconnectUrl)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(xconnectUrl);
                fileStream.Write(info, 0, info.Length);

                // writing data in bytes already
                byte[] data = new byte[] { 0x0 };
                fileStream.Write(data, 0, data.Length);
            }
        }
    }
}
