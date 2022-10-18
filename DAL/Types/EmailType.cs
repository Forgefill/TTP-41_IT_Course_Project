using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;

namespace DAL.Types
{
    public class EmailType:BaseType
    {
        public EmailType(string name):base(name)
        {
            defValue = "example@mail.com";
            typeName = "Email";
        }

        public EmailType() : base() {
            name = "Email";
            defValue = "example@mail.com";
            typeName = "Email";
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
