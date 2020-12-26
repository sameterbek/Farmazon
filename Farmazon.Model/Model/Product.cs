using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Farmazon.Model.Model
{
    public class Product : BaseModel
    {
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }
        [Range(0,double.MaxValue,ErrorMessage ="Price Cannot Be Less than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public Category Category { get; set; }
        public Product(string title, decimal price, Category category)
        {
            Title = title;
            Price = price;
            Category = category;
            Guid = Guid.NewGuid();
        }
    }
}
