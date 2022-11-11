using DAL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.DatabaseEntities
{
    public class Table
    {
        [XmlElement(ElementName ="TableName")]
        public string Name { get; set; }


        public List<Row> Rows;

        [XmlElement(ElementName ="ColumnType")]
        public List<BaseType> columnTypes;

        public Table(string name)
        {
            Name = name;
            Rows = new List<Row>();
            columnTypes = new List<BaseType>();
        }

        public Table() 
        {
            Rows = new List<Row>();
            columnTypes = new List<BaseType>();
        }
    }
}
