
namespace Sitecore.ContentTesting.Model.xConnect.Models
{
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Schema;

    public class CustomDataModel
    {
        public static XdbModel Model { get; } = BuildModel();

        private static XdbModel BuildModel()
        {
            var modelBuilder = new XdbModelBuilder("ContentTesting", new XdbModelVersion(1, 0));

            modelBuilder.ReferenceModel(CollectionModel.Model);
            modelBuilder.DefineEventType<MVTestTriggered>(false);
            modelBuilder.DefineEventType<PersonalizationEvent>(false);
            modelBuilder.DefineFacet<Contact, TestCombinationsData>("TestCombinations");

            return modelBuilder.BuildModel();
        }
    }
}
