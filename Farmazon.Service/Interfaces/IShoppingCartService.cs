using Farmazon.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Interfaces
{
    public interface IShoppingCartService
    {
        void ApplyCampaign(ShoppingCart shoppingCart, params Campaign[] campaigns);
    }
}
