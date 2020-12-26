using Farmazon.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Interfaces
{
    public interface ICampaignService
    {
        decimal CalculateCartDiscount(ShoppingCart shoppingCart, Campaign campaign);
    }
}
