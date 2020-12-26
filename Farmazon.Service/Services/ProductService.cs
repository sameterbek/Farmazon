using Farmazon.Model.ExceptionModel;
using Farmazon.Model.Model;
using Farmazon.Service.Interfaces;
using Farmazon.Util.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Farmazon.Service.Services
{
    public class ProductService : IProductService
    {
        private ICategoryService _categoryService { get; set; }
        public ProductService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Product CreateProduct(string title, decimal price, Category category)
        {
            ICollection<ValidationResult> lstvalidationResult;

            if (category == null)
                throw new CategoryNotFoundException($"{title} ürünü için kategori bilgisi bulunamadı.");

            var product = new Product(title, price, category);

            bool valid = GenericValidator.TryValidate(product, out lstvalidationResult);
            if (!valid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult res in lstvalidationResult)
                {
                    sb.AppendLine(res.MemberNames.First() + ":" + res.ErrorMessage);
                }
                throw new CustomValidationException(sb.ToString());
            }

            category.AddProduct(product);

            return product;
        }
    }
}
