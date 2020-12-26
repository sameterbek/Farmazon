using Farmazon.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmazon.Service.Interfaces
{
    public interface IApplyDiscountProcessService
    {
        decimal CalculateTotalDiscount(List<CartProduct> cartProducts, Campaign campaign);
    }
}
