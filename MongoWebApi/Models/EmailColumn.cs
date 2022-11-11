using MongoWebApi.JsonHelpers;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MongoWebApi.Models
{
    [JsonConverter(typeof(ColumnConverter))]
    public class EmailColumn:Column
    {
        public EmailColumn(string name) : base(name)
        {
            DefValue = "example@mail.com";
            ColumnType = "Email";  
        }

        public EmailColumn() : base()
        {
            ColumnName = "Email";
            DefValue = "example@mail.com";
            ColumnType = "Email";
        }

        public override string TypeRule()
        {
            return "Email type must contain \"@\" symbol and domain name.\nIt mustn`t contain blank space";
        }
        public override bool isCorrect(string value)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            return validateEmailRegex.IsMatch(value);
        }
    }
}
