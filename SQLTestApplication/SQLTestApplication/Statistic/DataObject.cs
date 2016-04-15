using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Statistic
{
    public class DataObject
    {
        [BsonId]
        public int ID { get; set; }
        public string Name { get; set; }
        public string NeptunCode { get; set; }
    }
}
