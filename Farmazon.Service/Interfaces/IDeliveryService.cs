using Farmazon.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Interfaces
{
    public interface IDeliveryService
    {
        decimal CalculateDeliveryCost(ShoppingCart shoppingCart);
    }
}
