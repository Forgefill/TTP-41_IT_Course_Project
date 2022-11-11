using MongoWebApi.JsonHelpers;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MongoWebApi.Models
{
    public class EnumColumn:Column
    {
        [DataMember]
        [JsonProperty]
        [JsonInclude]
        public List<string> enumValues;

        public EnumColumn(string name, List<string> enumValues) : base(name)
        {
            this.enumValues = enumValues;
            DefValue = enumValues[0];
            ColumnType = "Enum";
        }


        public EnumColumn() : base() { }

        public override string TypeRule()
        {
            return "Pls Choose correct enum item";
        }
        public override bool isCorrect(string value)
        {
            return enumValues.Any(x => x == value);
        }
    }
}
