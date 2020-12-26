using Farmazon.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Interfaces
{
    public interface IProductService
    {
        Product CreateProduct(string title, decimal price, Category category);
    }
}
