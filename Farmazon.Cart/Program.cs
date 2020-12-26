using Farmazon.Model.Enum;
using Farmazon.Model.Model;
using Farmazon.Service.DiscountService;
using Farmazon.Service.Interfaces;
using Farmazon.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Farmazon.Cart
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var serviceProvider = SetupDependencyContainer();

                var categoryService = serviceProvider.GetService<ICategoryService>();
                var productService = serviceProvider.GetService<IProductService>();
                var shoppingCartService = serviceProvider.GetService<IShoppingCartService>();
                var deliveryService = serviceProvider.GetService<IDeliveryService>();

                var category = categoryService.CreateCategory("Kategori-1");
                var category2 = categoryService.CreateCategory("Kategori-2");
                var product = productService.CreateProduct("Test1", 5, category);
                var product2 = productService.CreateProduct("Test2", 15, category2);

                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.AddItem(product, 4);
                shoppingCart.AddItem(product2, 2);

                Campaign campaign1 = new Campaign(category, 20.0m, 3, EDiscountType.Rate);

                Campaign campaign2 = new Campaign(category, 50.0m, 5, EDiscountType.Rate);

                Campaign campaign3 = new Campaign(category, 5.0m, 5, EDiscountType.Amount);

                shoppingCartService.ApplyCampaign(shoppingCart, campaign1, campaign2, campaign3);

                Coupon coupon = new Coupon(100, 10, EDiscountType.Rate);
                shoppingCart.ApplyCoupon(coupon);

                deliveryService.CalculateDeliveryCost(shoppingCart);

                Console.WriteLine(shoppingCart.Print());

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }



        static ServiceProvider SetupDependencyContainer()
        {
            //setup our DI
            return new ServiceCollection()
                .AddLogging(loggerFactory =>
                {
                    loggerFactory.ClearProviders().AddConsole()
                        .AddFilter(logLevel => logLevel >= LogLevel.Debug);
                })
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICampaignService, CampaignService>()
                .AddScoped<IShoppingCartService, ShoppingCartService>()
                .AddScoped<IDeliveryService, DeliveryService>()
                .AddTransient<AmountDiscountProcessService>()
                .AddTransient<RateDiscountProcessService>()
                .AddTransient<Func<EDiscountType, IApplyDiscountProcessService>>(serviceProvider => key =>
                {
                    switch (key)
                    {
                        case EDiscountType.Amount:
                            return serviceProvider.GetService<AmountDiscountProcessService>();
                        case EDiscountType.Rate:
                            return serviceProvider.GetService<RateDiscountProcessService>();
                        default:
                            return serviceProvider.GetService<AmountDiscountProcessService>();
                    }
                })
                .BuildServiceProvider();
        }
    }
}
