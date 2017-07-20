using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Collection.Model;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Configuration;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace xConnectAPI
{
    public class xConnect
    {
        static xConnect()
        {
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(xConnect.WorkerUpdateStatus);
            System.Threading.Thread th = new System.Threading.Thread(ts);
            th.Start();
            configFilePath = "config.ini";
        }

        static void WorkerUpdateStatus()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                if (string.IsNullOrEmpty(xConnectUrl))
                {
                    Status = "xConnectUrl hasn't been set.";
                    StatusColor = "Red";
                    continue;
                }

                if (string.IsNullOrEmpty(cleintThumbprint))
                {
                    Status = "Client certificate hasn't been resolved yet.";
                    StatusColor = "Red";
                    continue;
                }

                if (Configuration == null)
                {
                    Status = string.Format("[{0}]:Configuration hasn't initialized yet.", xConnectUrl);
                    StatusColor = "Red";
                    continue;
                }
                else
                {
                    Status = string.Format("[{0}]:Configuration is initialized.", xConnectUrl);
                    StatusColor = "Lime";
                    continue;
                }

            }

        }

        private static XConnectClientConfiguration configuration;
        private static string xconnectUrl;
        private static string cleintThumbprint;
        private static string status;
        private static string statusColor;
        private static string configFilePath;

        public static string xConnectUrl
        {
            get
            {
                return xconnectUrl;
            }
            set
            {
                xconnectUrl = value;
            }
        }

        public static string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                //NotifyStaticPropertyChanged("Status");
                NotifyStaticPropertyChanged();

            }
        }
        public static string StatusColor
        {
            get
            {
                return statusColor;
            }
            set
            {
                statusColor = value;
                //NotifyStaticPropertyChanged("Status");
                NotifyStaticPropertyChanged();

            }
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }




        public static XConnectClientConfiguration Configuration
        {
            get
            {
                return configuration;
            }
            set
            {
                configuration = value;
            }
        }

        private static void InitializeConnectionStrings(string xConnectUrl, string clientThumPrint)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var connectionStringXCoonectC = "StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=" + clientThumPrint;
            var cssXCoonectCS = new ConnectionStringSettings("xconnect.collection.certificate", connectionStringXCoonectC);
            config.ConnectionStrings.ConnectionStrings.Add(cssXCoonectCS);

            var connectionStringXCoonect = xConnectUrl;
            var cssXCoonect = new ConnectionStringSettings("xconnect.collection", connectionStringXCoonect);
            config.ConnectionStrings.ConnectionStrings.Add(cssXCoonect);

            ConfigurationManager.RefreshSection("connectionStrings");
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static Result Initialize()
        {
            //await InitializeXConnect().ConfigureAwait(true).GetAwaiter().GetResult();

            return InitializeXConnect();
        }

        private static Result InitializeXConnect()
        {
            //Zaglushka
            //System.Threading.Thread.Sleep(5000);
            //xConnectUrl = @"https://xConnect.local";
            //var clientModel1 = new Sitecore.XConnect.Client.Configuration.RuntimeModelConfiguration();
            //Configuration = xConnect.GetXConnectClientConfiguration(clientModel1, new Uri("https://SitecoreXConnectLocal"), new Uri("https://SitecoreXConnectLocal"), null, null);

            //System.Threading.Thread.Sleep(5000);
            //cleintThumbprint = "asdasd";

            //System.Threading.Thread.Sleep(5000);


            
            
            //

            xConnectUrl = ResolveXConnectUrl();

            if (xConnectUrl==null)
            {
                return Result.NoConfigFile;
            }
            if (xConnectUrl == string.Empty)
            {
                return Result.ConfigFileIsEmpty;
            }
                return Result.Good;

            cleintThumbprint = ResolveClientThumPrint();
            InitializeConnectionStrings(xConnectUrl, cleintThumbprint);

            var clientModel = new Sitecore.XConnect.Client.Configuration.RuntimeModelConfiguration();

            //var collectionmodel = new xConnectAPI.StaticModelConfiguration(Type.GetType("Sitecore.XConnect.Collection.Model.CollectionModel"));
            //var customDataModel = new xConnectAPI.StaticModelConfiguration(Type.GetType("Sitecore.ContentTesting.Model.xConnect.Models.CustomDataModel"));

            var collectionmodel = new Sitecore.XConnect.Client.Configuration.StaticModelConfiguration("Sitecore.XConnect.Collection.Model.CollectionModel, Sitecore.XConnect.Collection.Model");
            var customDataModel = new Sitecore.XConnect.Client.Configuration.StaticModelConfiguration("Sitecore.ContentTesting.Model.xConnect.Models.CustomDataModel, Sitecore.ContentTesting.Model");


            clientModel.AddModelConfiguration(collectionmodel);
            clientModel.AddModelConfiguration(customDataModel);

            var xConnectConfiguration = new Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration(clientModel, "xconnect.collection", "xconnect.collection", "xconnect.collection");


            var handlers = xConnect.GetConnectionRequestHandlers("97B7D2CF30E986BB14B7655137771C2DF6B14EB0", "xconnect.collection.certificate");
            Configuration = xConnect.GetXConnectClientConfiguration(clientModel, new Uri("https://SitecoreXConnectLocal"), new Uri("https://SitecoreXConnectLocal"), handlers, handlers);

            InitalizeConfiguration().ConfigureAwait(false).GetAwaiter().GetResult();

            //using (var client = new XConnectClient(cfg))
            //{
            //    try
            //    {
            //        // Identifier for a 'known' contact
            //        var identifier = new ContactIdentifier[]
            //        {
            //                new ContactIdentifier("twitter", "myrtlemcmuffin" + Guid.NewGuid().ToString("N"), ContactIdentifierType.Known)
            //        };

            //        // Print out the identifier that is going to be used
            //        Console.WriteLine("Identifier:" + identifier[0].Identifier);

            //        // Create a new contact with the identifier
            //        Contact knownContact = new Contact(identifier);

            //        client.AddContact(knownContact);

            //        // Submit contact and interaction - a total of two operations
            //        await client.SubmitAsync();

            //        // Get the last batch that was executed
            //        var operations = client.LastBatch;

            //        Console.WriteLine("RESULTS...");

            //        // Loop through operations and check status
            //        foreach (var operation in operations)
            //        {
            //            Console.WriteLine(operation.OperationType + operation.Target.GetType().ToString() + " Operation: " + operation.Status);
            //        }
            //    }
            //    catch (XdbExecutionException ex)
            //    {
            //        // Deal with exception
            //    }
            //}

        }

        private static async Task InitalizeConfiguration()
        {
            await Configuration.InitializeAsync();
        }

        private static string ResolveXConnectUrl()
        {
            string result = xConnectAPI.Configuration.ReadConfiguration(configFilePath);
            return result;
        }
        private static string ResolveClientThumPrint()
        {
            throw new NotImplementedException();
            return cleintThumbprint;
        }






        //Methods are taken from Sitecore assemblies
        private static XConnectClientConfiguration GetXConnectClientConfiguration(IModelConfiguration clientModel, Uri collectionEndpointUri, Uri searchEndpointUri, Uri configurationEndpointUri, IWebRequestHandlerModifier[] collectionRequestHandlers, IWebRequestHandlerModifier[] searchRequestHandlers, IWebRequestHandlerModifier[] configurationRequestHandlers)
        {
            Uri baseUri = new Uri(!(collectionEndpointUri).ToString().EndsWith("/") ? new Uri(collectionEndpointUri + "/") : collectionEndpointUri, "odata/");
            Uri baseAddress = new Uri(!(searchEndpointUri).ToString().EndsWith("/") ? new Uri(searchEndpointUri + "/") : searchEndpointUri, "odata/");
            Uri uri3 = new Uri(!(configurationEndpointUri).ToString().EndsWith("/") ? new Uri(configurationEndpointUri + "/") : configurationEndpointUri, "configuration/");
            return new XConnectClientConfiguration(clientModel.Model, new CollectionWebApiClient(baseUri, null, collectionRequestHandlers), new SearchWebApiClient(baseAddress, null, searchRequestHandlers), new ConfigurationWebApiClient(uri3, null, configurationRequestHandlers));
        }

        private static XConnectClientConfiguration GetXConnectClientConfiguration(IModelConfiguration clientModel, Uri collectionEndpointUri, Uri configurationEndpointUri, IWebRequestHandlerModifier[] collectionRequestHandlers, IWebRequestHandlerModifier[] configurationRequestHandlers)
        {
            Uri baseUri = new Uri(!(collectionEndpointUri).ToString().EndsWith("/") ? new Uri(collectionEndpointUri + "/") : collectionEndpointUri, "odata/");
            Uri uri2 = new Uri(!(configurationEndpointUri).ToString().EndsWith("/") ? new Uri(configurationEndpointUri + "/") : configurationEndpointUri, "configuration/");
            return new XConnectClientConfiguration(clientModel.Model, new CollectionWebApiClient(baseUri, null, collectionRequestHandlers), new ConfigurationWebApiClient(uri2, null, configurationRequestHandlers));
        }

        private static IWebRequestHandlerModifier[] GetConnectionRequestHandlers(string thumbprint, string certificateConnectionStringName)
        {
            if (!string.IsNullOrWhiteSpace(thumbprint))
            {
                return new IWebRequestHandlerModifier[] { new CertificateWebRequestHandlerModifier(StoreName.My, StoreLocation.CurrentUser, X509FindType.FindByThumbprint, thumbprint) };
            }
            if (!string.IsNullOrWhiteSpace(certificateConnectionStringName))
            {
                return new IWebRequestHandlerModifier[] { new CertificateWebRequestHandlerModifier(certificateConnectionStringName) };
            }
            return new IWebRequestHandlerModifier[0];
        }
    }
}
