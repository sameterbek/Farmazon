using Farmazon.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Model.Model
{
    public class Coupon : BaseModel
    {
        public decimal MinPurchaseAmount { get; set; }
        public decimal DiscountValue { get; set; }
        public EDiscountType DiscountType { get; set; }

        public Coupon(decimal minPurchaseAmount, decimal discountValue, EDiscountType discountType)
        {
            MinPurchaseAmount = minPurchaseAmount;
            DiscountValue = discountValue;
            DiscountType = discountType;
        }

        public decimal CalculateCouponDiscount(decimal purchaseAmount)
        {
            if (purchaseAmount >= MinPurchaseAmount)
            {
                if (DiscountType == EDiscountType.Amount)
                {
                    return DiscountValue >= purchaseAmount ? purchaseAmount : DiscountValue;
                }
                else if (DiscountType == EDiscountType.Rate)
                {
                    var discountTotal = (purchaseAmount * DiscountValue) / 100;
                    return discountTotal >= purchaseAmount ? purchaseAmount : discountTotal;
                }
            }
            return 0;
        }
    }
}
