using Farmazon.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Interfaces
{
    public interface ICategoryService
    {
        Category CreateCategory(string title, Category parentCategory);
        Category CreateCategory(string title);
    }
}
