using MongoWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MongoWebApi.JsonHelpers
{
    public class ColumnSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        public ColumnSpecifiedConcreteClassConverter():base()
        {

        }

        protected override JsonConverter? ResolveContractConverter(Type objectType)
        {
            if (typeof(Column).IsAssignableFrom(objectType) && !objectType.IsAbstract)
            {
                return null;
            }
            return base.ResolveContractConverter(objectType);
        }
    }
}