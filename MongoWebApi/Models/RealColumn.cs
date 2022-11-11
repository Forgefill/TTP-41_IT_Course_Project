using MongoWebApi.JsonHelpers;
using Newtonsoft.Json;

namespace MongoWebApi.Models
{
    [JsonConverter(typeof(ColumnConverter))]
    public class RealColumn:Column
    {
        public RealColumn(string name) : base(name)
        {
            ColumnType = "Real";
            DefValue = "0.0";
        }

        public RealColumn() : base()
        {
            ColumnName = "Real";
            ColumnType = "Real";
            DefValue = "0.0";
        }

        public override string TypeRule()
        {
            return "Real type must contain real numbers with . separetor";
        }
        public override bool isCorrect(string value)
        {
            return double.TryParse(value, out _);
        }
    }
}
