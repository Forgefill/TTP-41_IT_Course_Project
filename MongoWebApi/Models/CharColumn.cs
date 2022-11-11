using MongoWebApi.JsonHelpers;
using Newtonsoft.Json;

namespace MongoWebApi.Models
{
    [JsonConverter(typeof(ColumnConverter))]
    public class CharColumn:Column
    {
        public CharColumn(string name) : base(name)
        {
            DefValue = string.Empty;
            ColumnType = "Char";
        }

        public CharColumn() : base()
        {
            ColumnName = "Char";
            DefValue = string.Empty;
            ColumnType = "Char";
        }

        public override string TypeRule()
        {
            return "Char type valid input must be unicode character";
        }

        public override bool isCorrect(string value)
        {
            return char.TryParse(value, out _);
        }
    }
}
