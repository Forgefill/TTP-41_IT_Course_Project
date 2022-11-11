using MongoWebApi.JsonHelpers;
using Newtonsoft.Json;

namespace MongoWebApi.Models
{
    [JsonConverter(typeof(ColumnConverter))]
    public class IntegerColumn:Column
    {

        public IntegerColumn(string name) : base(name)
        {
            ColumnType = "Int";
            DefValue = "0";
        }

        public IntegerColumn() : base()
        {
            ColumnName = "Int";
            ColumnType = "Int";
            DefValue = "0";
        }

        public override string TypeRule()
        {
            return "Int type must contain only integers number";
        }
        public override bool isCorrect(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
