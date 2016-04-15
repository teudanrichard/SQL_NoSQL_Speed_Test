using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestApplication.Exceptions
{
    class MissingParameterException : Exception
    {
        public MissingParameterException() { }
        public MissingParameterException(string message) : base(message) { }
    }
}
