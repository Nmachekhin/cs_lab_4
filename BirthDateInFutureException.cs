using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDisplay
{
    internal class BirthDateInFutureException : Exception
    {
        public BirthDateInFutureException() { }
        public BirthDateInFutureException(string message) : base(message) { }
        public BirthDateInFutureException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}
