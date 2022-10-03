using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Types
{
    public class CharType:BaseType
    {
        public CharType(string name):base(name)
        {
            defValue = string.Empty;
            typeName = "Char";
        }

        public CharType() : base() { }

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
