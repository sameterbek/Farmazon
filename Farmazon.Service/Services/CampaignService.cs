using Farmazon.Model.Enum;
using Farmazon.Model.Model;
using Farmazon.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmazon.Service.Services
{
    public class CampaignService : ICampaignService
    {
        private Func<EDiscountType, IApplyDiscountProcessService> _ApplyDiscountProcessService { get; set; }
        public CampaignService(Func<EDiscountType, IApplyDiscountProcessService> ApplyDiscountProcessService)
        {
            _ApplyDiscountProcessService = ApplyDiscountProcessService;
        }

        public decimal CalculateCartDiscount(ShoppingCart shoppingCart, Campaign campaign)
        {
            var applicableCartProduct = shoppingCart.GetCartProducts().Where(x => x.Product.Category.Guid == campaign.Category.Guid).ToList();

            var discountProcessService = _ApplyDiscountProcessService(campaign.DiscountType);

            return discountProcessService.CalculateTotalDiscount(applicableCartProduct, campaign);

        }
    }
}
