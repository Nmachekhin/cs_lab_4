using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDisplay
{
    internal class InvalidEmailFormattingException : Exception
    {
        public InvalidEmailFormattingException() { }

        public InvalidEmailFormattingException(string message) : base(message) { }

        public InvalidEmailFormattingException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}
