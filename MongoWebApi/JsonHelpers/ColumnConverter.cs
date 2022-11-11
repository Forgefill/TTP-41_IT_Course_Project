using MongoWebApi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace MongoWebApi.JsonHelpers
{
    public class ColumnConverter:JsonConverter
    {
        public ColumnConverter():base()
        {

        }

        private static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings()
        {
            ContractResolver = new ColumnSpecifiedConcreteClassConverter()
        };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Column));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            switch (jo["ColumnType"].Value<string>())
            {
                case "Char":
                    return JsonConvert.DeserializeObject<CharColumn>(jo.ToString(), SpecifiedSubclassConversion);
                case "Int":
                    return JsonConvert.DeserializeObject<IntegerColumn>(jo.ToString(), SpecifiedSubclassConversion);
                case "Email":
                    return JsonConvert.DeserializeObject<EmailColumn>(jo.ToString(), SpecifiedSubclassConversion);
                case "Enum":
                    return JsonConvert.DeserializeObject<EnumColumn>(jo.ToString(), SpecifiedSubclassConversion);
                case "Real":
                    return JsonConvert.DeserializeObject<RealColumn>(jo.ToString(), SpecifiedSubclassConversion);
                case "String":
                    return JsonConvert.DeserializeObject<StringColumn>(jo.ToString(), SpecifiedSubclassConversion);
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
