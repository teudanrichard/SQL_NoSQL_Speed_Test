using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Statistic
{
    public class Types
    {
        public enum SQLType
        {
            SQL, NoSQL
        }
        public enum SQLActions
        {
            Olvasás, Beszúrás, Frissítés, Törlés
        }
    }
}
