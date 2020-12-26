using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Model.ExceptionModel
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(string message) : base(message)
        {

        }
    }
}
