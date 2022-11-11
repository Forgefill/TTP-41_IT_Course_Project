using DAL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.DatabaseEntities
{
    public class Row
    {
        public List<string> elements;

        public Row() { }

        public Row(List<string> elements)
        {
            this.elements = elements;
        }

        public string this[int i]
        {
            get => elements[i];
            set => elements[i] = value;
        }

        public static bool operator ==(Row first, Row second)
        {
            bool result = true;
            for(int i = 0; i < first.elements.Count; i++)
            {
                result = result && (first[i] == second[i]);
            }
            return result;
        }

        public static bool operator !=(Row first, Row second)
        {
            return !(first == second);
        }
    }
}
