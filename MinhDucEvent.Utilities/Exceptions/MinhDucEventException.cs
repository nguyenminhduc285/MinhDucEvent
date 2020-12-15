using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Utilities.Exceptions
{
    public class MinhDucEventException : Exception
    {
        public MinhDucEventException()
        {
        }

        public MinhDucEventException(string message) : base(message)
        {
        }

        public MinhDucEventException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}