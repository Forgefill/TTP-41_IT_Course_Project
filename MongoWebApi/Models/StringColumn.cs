using MongoWebApi.JsonHelpers;
using Newtonsoft.Json;
namespace MongoWebApi.Models
{
    [JsonConverter(typeof(ColumnConverter))]
    public class StringColumn:Column
    {
        public StringColumn(string name) : base(name)
        {
            ColumnType = "String";
            DefValue = string.Empty;
        }

        public StringColumn() : base()
        {
            ColumnName = "String";
            ColumnType = "String";
            DefValue = string.Empty;
        }

        public override string TypeRule()
        {
            return ColumnType;
        }
        public override bool isCorrect(string value)
        {
            return true;
        }
    }
}
