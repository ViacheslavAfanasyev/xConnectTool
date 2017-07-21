using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace xConnectAPI
{
    public class Certificate
    {
        public string Name { get; set; }
        public string ThumPrint { get; set; }
    }
    public class Certificates
    {
        public static List<Certificate> CertificatesFromMyStore { get; set; }
        public static List<string> CertificatesName { get; set; }
       static Certificates()
        {
            X509Store store = new X509Store(StoreName.My);
            store.Open(OpenFlags.ReadOnly);
            CertificatesFromMyStore = new List<Certificate>();
            CertificatesName = new List<string>();
            foreach (var cert in store.Certificates)
            {
                CertificatesName.Add(cert.GetNameInfo(X509NameType.SimpleName, false));
                CertificatesFromMyStore.Add(new Certificate() { Name = cert.GetNameInfo(X509NameType.SimpleName, false), ThumPrint = cert.Thumbprint });
            }

            //Console.WriteLine(store.Certificates.Count);
            //foreach (var cvvvv in store.Certificates)
            //{
            //    if (!string.IsNullOrEmpty(cvvvv.FriendlyName))
            //        Console.WriteLine(cvvvv.GetNameInfo(X509NameType.SimpleName, false));
            //}
            store.Close();



        }

        public static string GetThumPrint(X509Certificate2 certificate)
        {
            return certificate.Thumbprint;
        }

        public static string GetCertificateName(X509Certificate2 certificate)
        {
            return certificate.GetNameInfo(X509NameType.SimpleName, false);
        }

    }
}
