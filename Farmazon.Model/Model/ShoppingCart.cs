using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmazon.Model.Model
{
    public class ShoppingCart : BaseModel
    {
        private List<CartProduct> CartProducts { get; set; }
        private Campaign CampaignDiscount { get; set; }
        private Coupon CouponDiscount { get; set; }
        private decimal CampaignDiscountTotal { get; set; }
        private decimal CouponDiscountTotal { get; set; }
        private decimal DeliveryCost { get; set; }
        private decimal NetTotalPrice
        {
            get
            {
                return CartProducts.Sum(x => x.Quantity * x.Product.Price) - CampaignDiscountTotal - CouponDiscountTotal;
            }
        }

        private decimal GrossTotalPrice
        {
            get
            {
                return CartProducts.Sum(x => x.Quantity * x.Product.Price);
            }
        }

        private decimal TotalPriceWithCampaign
        {
            get
            {
                return CartProducts.Sum(x => x.Quantity * x.Product.Price) - CampaignDiscountTotal;
            }
        }

        public ShoppingCart()
        {
            CartProducts = new List<CartProduct>();
        }

        public List<CartProduct> GetCartProducts()
        {
            return CartProducts;
        }

        public void AddItem(Product product, decimal Quantity)
        {
            CartProducts.Add(new CartProduct { Product = product, Quantity = Quantity });
        }

        public void ApplyCampaign(Campaign campaign, decimal campaignDiscountTotal)
        {
            CampaignDiscount = campaign;
            CampaignDiscountTotal = campaignDiscountTotal;
            


            if (CouponDiscount != null)
            {
                var tempCoupon = CouponDiscount;
                RemoveCoupon();
                ApplyCoupon(tempCoupon);
            }
        }

        public void ApplyCoupon(Coupon coupon)
        {
            var couponDiscountTotal = coupon.CalculateCouponDiscount(TotalPriceWithCampaign);
            if(couponDiscountTotal > 0)
            {
                CouponDiscount = coupon;
                CouponDiscountTotal = couponDiscountTotal;
            }
        }

        public void RemoveCoupon()
        {
            CouponDiscount = null;
            CouponDiscountTotal = 0;
        }

        public decimal GetTotalAmountAfterDiscounts()
        {
            return NetTotalPrice;
        }

        public decimal GetCouponDiscount()
        {
            return CouponDiscountTotal;
        }

        public decimal GetCampaignDiscount()
        {
            return CampaignDiscountTotal;
        }

        public decimal GetDeliveryCost()
        {
            return DeliveryCost;
        }

        public void SetDeliveryCost(decimal deliveryCost)
        {
            DeliveryCost = deliveryCost;
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Category").Append("\t").Append("Product").Append("\t").Append("\t").Append("Quantity").Append("\t").Append("Unit Price").Append("\t").Append("Total Price").Append("\t").Append("Total Dicount").Append("\t");
            sb.AppendLine();
            var groupedProducts = CartProducts.GroupBy(x => x.Product.Category).Select(group => new { Category = group.Key, CartProduct = group.ToList() });
            foreach (var groupedProduct in groupedProducts)
            {
                
                foreach (var cartProduct in groupedProduct.CartProduct)
                {
                    var campaignDiscountPerItem = 0m;
                    var couponDiscountPerItem = CouponDiscountTotal / CartProducts.Sum(x => x.Quantity) * cartProduct.Quantity;
                    if (CampaignDiscount != null && groupedProduct.Category.Guid == CampaignDiscount.Category.Guid)
                    {
                        campaignDiscountPerItem = CampaignDiscountTotal / groupedProduct.CartProduct.Sum(x => x.Quantity) * cartProduct.Quantity;
                    }
                    var totalDiscount = campaignDiscountPerItem + couponDiscountPerItem;

                    sb.Append(groupedProduct.Category.Title);
                    sb.Append("\t");
                    sb.Append(cartProduct.Product.Title);
                    sb.Append("\t");
                    sb.Append(cartProduct.Quantity);
                    sb.Append("\t");
                    sb.Append(cartProduct.Product.Price);
                    sb.Append("\t");
                    sb.Append(cartProduct.Product.Price * cartProduct.Quantity);
                    sb.Append("\t");
                    sb.Append(totalDiscount);
                    sb.AppendLine();
                    //sb.AppendLine($"{groupedProduct.Category.Title} || {cartProduct.Product.Title} || {cartProduct.Quantity} || {cartProduct.Product.Price} || {cartProduct.Product.Price * cartProduct.Quantity} || {totalDiscount}");
                }
            }

            sb.AppendLine($"Total Amount : {NetTotalPrice}");
            sb.AppendLine($"Total Delivery Cost : {DeliveryCost}");

            return sb.ToString();
        }

    }
}
