using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL.Types
{
    
    public abstract class BaseType
    {
        [XmlElement(ElementName ="ColumnName")]
        public string name;
        [XmlElement(ElementName ="TypeName")]
        public string typeName;
        [XmlElement(ElementName = "DefaultValue")]
        public string defValue;

        abstract public bool isCorrect(string value);

        abstract public string TypeRule();


        protected BaseType(string name)
        {
            this.name = name;
        }

        public BaseType() { }
    }
} 
