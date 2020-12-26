using Farmazon.Model.Model;
using Farmazon.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmazon.Service.DiscountService
{
    public class AmountDiscountProcessService : IApplyDiscountProcessService
    {
        public decimal CalculateTotalDiscount(List<CartProduct> cartProducts, Campaign campaign)
        {
            var validCartProducts = cartProducts.Where(x => x.Product.Category != null && x.Product.Category.Guid == campaign.Category.Guid).ToList();

            if(validCartProducts.Sum(x=>x.Quantity) < campaign.MinItem)
            {
                return 0;
            }

            return campaign.DiscountValue;

        }
    }
}
