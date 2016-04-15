using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Exceptions
{
    class NoSQLException : Exception
    {
        public NoSQLException() { }
        public NoSQLException(string message) : base(message) { }
    }
}
