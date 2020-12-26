using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Model.ExceptionModel
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string message) : base(message)
        {

        }
    }
}
