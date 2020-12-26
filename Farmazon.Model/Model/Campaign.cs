using Farmazon.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Model.Model
{
    public class Campaign : BaseModel
    {
        public Category Category { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal MinItem { get; set; }
        public EDiscountType DiscountType { get; set; }

        public Campaign(Category category, decimal discountvalue, decimal minItem, EDiscountType discountType)
        {
            Category = category;
            DiscountValue = discountvalue;
            MinItem = minItem;
            DiscountType = discountType;
        }

    }
}
