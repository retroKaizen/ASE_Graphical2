using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWithCommandline
{
    class NonDigitValueException : Exception
    {
        public NonDigitValueException()
        {

        }
        public NonDigitValueException(String message) : base(message)
        {

        }
        public NonDigitValueException(String message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
