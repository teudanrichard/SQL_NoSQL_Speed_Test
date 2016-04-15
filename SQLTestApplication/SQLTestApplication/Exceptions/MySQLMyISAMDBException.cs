using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Exceptions
{
    class MySQLMyISAMDBException : Exception
    {
        public MySQLMyISAMDBException() { }
        public MySQLMyISAMDBException(string message) : base(message) { }
    }
}
