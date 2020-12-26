using Farmazon.Model.Model;
using Farmazon.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService()
        {

        }

        public Category CreateCategory(string title, Category parentCategory)
        {
            return new Category(title, parentCategory);
        }

        public Category CreateCategory(string title)
        {
            return new Category(title);
        }
    }

    
}
