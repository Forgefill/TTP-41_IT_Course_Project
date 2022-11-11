using MongoDB.Bson.Serialization.Attributes;
using MongoWebApi.JsonHelpers;
using Newtonsoft.Json;

namespace MongoWebApi.Models
{
    
    [BsonDiscriminator(RootClass =true)]
    [BsonKnownTypes(typeof(CharColumn), typeof(EnumColumn), typeof(IntegerColumn), 
        typeof(RealColumn), typeof(EmailColumn), typeof(StringColumn))]
    [JsonConverter(typeof(ColumnConverter))]
    public abstract class Column
    {
        [BsonId]
        public string ColumnName { get; set; }

        public string ColumnType { get; set; }

        public string DefValue { get; set; }

        public List<string> elements { get; set; } = new List<string>();

        public abstract bool isCorrect(string value);

        public abstract string TypeRule();

        public Column()
        {

        }

        public Column(string ColumnName)
        {
            this.ColumnName = ColumnName;
        }
    }
}
