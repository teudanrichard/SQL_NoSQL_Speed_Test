using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Exceptions
{
    class MySQLInnoDBException : Exception
    {
        public MySQLInnoDBException() { }
        public MySQLInnoDBException(string message) : base(message) { }
    }
}
