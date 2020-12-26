using Farmazon.Model.Model;
using Farmazon.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private ICampaignService _CampaignService { get; set; }
        public ShoppingCartService(ICampaignService CampaignService)
        {
            _CampaignService = CampaignService;
        }

        public void ApplyCampaign(ShoppingCart shoppingCart, params Campaign[] campaigns)
        {
            Campaign bestCampaign = null;
            decimal bestDiscount = 0;
            foreach (var campaign in campaigns)
            {
               var discount = _CampaignService.CalculateCartDiscount(shoppingCart, campaign);
                if(discount > bestDiscount)
                {
                    bestCampaign = campaign;
                    bestDiscount = discount;

                }
            }

            shoppingCart.ApplyCampaign(bestCampaign, bestDiscount);
        }

    }
}
