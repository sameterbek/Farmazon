using Farmazon.Model.Model;
using Farmazon.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmazon.Service.Services
{
    public class DeliveryService : IDeliveryService
    {
        private const decimal FixedCost = 2.99m;
        private const decimal CostPerProduct = 0.2m;
        private const decimal CostPerDelivery = 0.1m;
        public DeliveryService()
        {

        }

        public decimal CalculateDeliveryCost(ShoppingCart shoppingCart)
        {
            var numberOfDelivery = NumberOfDelivery(shoppingCart);
            var numberOfProduct = NumberOfProduct(shoppingCart);

            var deliveryCost = (CostPerDelivery * numberOfDelivery) + (CostPerProduct * numberOfProduct) + FixedCost;
            shoppingCart.SetDeliveryCost(deliveryCost);
            return deliveryCost;
        }

        private int NumberOfDelivery(ShoppingCart shoppingCart)
        {
            return shoppingCart.GetCartProducts().GroupBy(x => x.Product.Category.Guid).Count();
        }

        private int NumberOfProduct(ShoppingCart shoppingCart)
        {
            return shoppingCart.GetCartProducts().GroupBy(x => x.Product.Guid).Count();
        }
    }
}
