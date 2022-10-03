using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Types
{
    public class IntegerType:BaseType
    {

        public IntegerType(string name):base(name)
        {
            typeName = "Int";
            defValue = "0";
        }

        public IntegerType() : base() { }

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
