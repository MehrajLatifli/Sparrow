using System;
using System.Collections.Generic;
using System.Text;

namespace Sparrow.Application.Exception
{
    public class ConflictException : ApplicationException
    {
        public ConflictException(string message) : base(message) { }

    }
}
