using Sitecore.Shell.Applications.ContentEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace xConnectAPI
{
    public class Configuration
    {
        public string xConnectUrl { get; set; }
        public string CertificateThumPrint { get; set; }

        //Read\Write cconfiguration
        public static Configuration ReadConfiguration(string path)
        {
            Configuration cfg = new Configuration();
           
            if (System.IO.File.Exists(path))
            {
                using (StreamReader sr = System.IO.File.OpenText(path))
                {
                    string s = String.Empty;
                    while ((s = sr.ReadLine()) != null)
                    {
                       if (s.StartsWith("xConnectUrl="))
                        {
                            cfg.xConnectUrl = s.Substring(12);
                        }
                       if(s.StartsWith("CertificateThumPrint="))
                        {
                            cfg.CertificateThumPrint = s.Substring(21);
                        }
                    }
                }
                return cfg;
             }
            return null;
        }


        public static void WriteConfigurationToFile(string path, string xConnectUrl, string CleintThumbprint)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("xConnectUrl="+ xConnectUrl);
                sw.WriteLine("CertificateThumPrint=" + CleintThumbprint);
            }
        }


    }


}
