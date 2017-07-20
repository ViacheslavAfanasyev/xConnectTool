//using Sitecore.XConnect.Client.Configuration;
//using Sitecore.XConnect.Schema;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace xConnectAPI
//{
//    public class StaticModelConfiguration : IModelConfiguration
//    {
//        // Fields
//        private readonly XdbModel _schema;
//        private const string ModelDefaultPropertyName = "Model";

//        // Methods
//        public StaticModelConfiguration(string modeltype) : this(modeltype, "Model")
//        {
//        }

//        public StaticModelConfiguration(string modeltype, string staticproperty)
//        {
//            Type type = Type.GetType(modeltype);
//            if (type == null)
//            {
//                throw new Exception("Invalid Model Type for Static Model Configuration");
//            }
//            PropertyInfo property = type.GetProperty(staticproperty);
//            if (property == null)
//            {
//                throw new Exception("Invalid property for Static Model Configuration");
//            }
//            object obj2 = property.GetValue(null);
//            if (((XdbModel)obj2) == null)
//            {
//                throw new Exception("Invalid schema object return type for Static Model Configuration");
//            }
//            this._schema = (XdbModel)obj2;
//        }

//        public StaticModelConfiguration(Type type)
//        {
//            var value = type.GetProperty("Model", BindingFlags.Static | BindingFlags.Public).GetValue(null);
//            this._schema = (XdbModel)value;
//        }

//        // Properties
//        public XdbModel Model =>
//            this._schema;
//    }


//}
