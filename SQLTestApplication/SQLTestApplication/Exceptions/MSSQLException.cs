using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Exceptions
{
    class MSSQLException : Exception
    {
        public MSSQLException() { }
        public MSSQLException(string message) : base(message) { }
    }
}
