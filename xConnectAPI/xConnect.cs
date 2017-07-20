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

namespace xConnectAPI
{
    public class xConnect
    {
        static void Main(string[] args)
        {
        }

            static xConnect()
        {
            
        }
        async static Task InitializeConnectionStrings()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var connectionStringXCoonectC = "StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=97B7D2CF30E986BB14B7655137771C2DF6B14EB0";
            var cssXCoonectCS = new ConnectionStringSettings("xconnect.collection.certificate", connectionStringXCoonectC);
            config.ConnectionStrings.ConnectionStrings.Add(cssXCoonectCS);

            var connectionStringXCoonect = @"https://SitecoreXConnectLocal";
            var cssXCoonect = new ConnectionStringSettings("xconnect.collection", connectionStringXCoonect);
            config.ConnectionStrings.ConnectionStrings.Add(cssXCoonect);

            ConfigurationManager.RefreshSection("connectionStrings");
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static void Initialize()
        {
            InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task InitializeAsync()
        {
            await InitializeConnectionStrings();
            var clientModel = new Sitecore.XConnect.Client.Configuration.RuntimeModelConfiguration();




            //var collectionmodel = new xConnectAPI.StaticModelConfiguration(Type.GetType("Sitecore.XConnect.Collection.Model.CollectionModel"));
            //var customDataModel = new xConnectAPI.StaticModelConfiguration(Type.GetType("Sitecore.ContentTesting.Model.xConnect.Models.CustomDataModel"));

            var collectionmodel = new Sitecore.XConnect.Client.Configuration.StaticModelConfiguration("Sitecore.XConnect.Collection.Model.CollectionModel, Sitecore.XConnect.Collection.Model");
            var customDataModel = new Sitecore.XConnect.Client.Configuration.StaticModelConfiguration("Sitecore.ContentTesting.Model.xConnect.Models.CustomDataModel, Sitecore.ContentTesting.Model");


            clientModel.AddModelConfiguration(collectionmodel);
            clientModel.AddModelConfiguration(customDataModel);

            var xConnectConfiguration = new Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration(clientModel, "xconnect.collection", "xconnect.collection", "xconnect.collection");


            var handlers = xConnect.GetConnectionRequestHandlers("97B7D2CF30E986BB14B7655137771C2DF6B14EB0", "xconnect.collection.certificate");
            var cfg = xConnect.GetXConnectClientConfiguration(clientModel, new Uri("https://SitecoreXConnectLocal"), new Uri("https://SitecoreXConnectLocal"), handlers, handlers);

            var a = "sda";
            //await cfg.InitializeAsync();
            var b = "sad";
            return;

            using (var client = new XConnectClient(cfg))
            {
                try
                {
                    // Identifier for a 'known' contact
                    var identifier = new ContactIdentifier[]
                    {
                            new ContactIdentifier("twitter", "myrtlemcmuffin" + Guid.NewGuid().ToString("N"), ContactIdentifierType.Known)
                    };

                    // Print out the identifier that is going to be used
                    Console.WriteLine("Identifier:" + identifier[0].Identifier);

                    // Create a new contact with the identifier
                    Contact knownContact = new Contact(identifier);

                    client.AddContact(knownContact);

                    // Submit contact and interaction - a total of two operations
                    await client.SubmitAsync();

                    // Get the last batch that was executed
                    var operations = client.LastBatch;

                    Console.WriteLine("RESULTS...");

                    // Loop through operations and check status
                    foreach (var operation in operations)
                    {
                        Console.WriteLine(operation.OperationType + operation.Target.GetType().ToString() + " Operation: " + operation.Status);
                    }
                }
                catch (XdbExecutionException ex)
                {
                    // Deal with exception
                }
            }

        }

        public static XConnectClientConfiguration GetXConnectClientConfiguration(IModelConfiguration clientModel, Uri collectionEndpointUri, Uri searchEndpointUri, Uri configurationEndpointUri, IWebRequestHandlerModifier[] collectionRequestHandlers, IWebRequestHandlerModifier[] searchRequestHandlers, IWebRequestHandlerModifier[] configurationRequestHandlers)
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
