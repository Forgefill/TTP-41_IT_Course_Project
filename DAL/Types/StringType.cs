using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Types
{
    public class StringType:BaseType
    {
        public StringType(string name):base(name)
        {
            typeName = "String";
            defValue = string.Empty;
        }

        public StringType():base() { }

        public override string TypeRule()
        {
            return typeName;
        }
        public override bool isCorrect(string value)
        {
            return true;
        }
    }
}
