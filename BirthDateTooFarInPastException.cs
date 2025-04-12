using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDisplay
{
    internal class BirthDateTooFarInPastException: Exception
    {
        public BirthDateTooFarInPastException() { }
        public BirthDateTooFarInPastException(string message) : base(message) { }
        public BirthDateTooFarInPastException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
