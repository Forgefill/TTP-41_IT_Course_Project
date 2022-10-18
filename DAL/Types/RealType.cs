using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Types
{
    public class RealType:BaseType
    {
        public RealType(string name):base(name)
        {
            typeName = "Real";
            defValue = "0.0";
        }

        public RealType() : base() {
            name = "Real";
            typeName = "Real";
            defValue = "0.0";
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
