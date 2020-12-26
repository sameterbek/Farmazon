using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Model.Model
{
    public class Category : BaseModel
    {
        public string Title { get; set; }
        public Category ParentCategory { get; set; }
        public List<Product> Products { get; set; }
        public Category(string title)
        {
            Title = title;
            Guid = Guid.NewGuid();
            Products = new List<Product>();
        }
        public Category(string title, Category parentCategory)
        {
            Title = title;
            ParentCategory = parentCategory;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
