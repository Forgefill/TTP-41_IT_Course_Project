using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Types
{
    public class EnumType:BaseType
    {
        public List<string> EnumTypes;

        public EnumType(string name, List<string> enumValues):base(name)
        {
            EnumTypes = enumValues;
            defValue = enumValues[0];
            typeName = "Enum";
        }

        public EnumType() : base() { }

        public override string TypeRule()
        {
            return "Pls Choose correct enum item";
        }
        public override bool isCorrect(string value)
        {
            return EnumTypes.Any(x => x == value);
        }
    }
}
