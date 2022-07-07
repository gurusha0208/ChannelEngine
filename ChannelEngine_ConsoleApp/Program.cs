using ChannelEngine_Domain.Model;
using ChannelEngine_Repository;
using ChannelEngine_Repository.Implementation;
using ChannelEngine_Repository.Interface;
using ChannelEngine_Service.Implementation;
using ChannelEngine_Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChannelEngine_ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<IProductRepository, ProductRepository>()
                .AddSingleton<IRepository, Repository>()
                .BuildServiceProvider();

            // Get list of Top 5 products
            var product = serviceProvider.GetService<IProductService>();
            var orders = product.GetTopProducts(Config.Status);
            DisplayOfTopFive(orders);

            Console.WriteLine("Please type the product number to Update Stock");

            var line = Console.ReadLine().Replace(" ", "").Replace("-", "").ToLower();

            while (!orders.Select(x => x.MerchantProductNo.ToLower().Replace("-", "")).ToList().Contains(line))
            {
                Console.WriteLine("Please type a valid stock number");
                line = Console.ReadLine().Replace(" ", "").Replace("-", "").ToLower();
            }

            var item = orders.Where(x => x.MerchantProductNo.ToLower().Replace("-", "") == line).FirstOrDefault();

            var model = new List<Product>()
            {
                new Product(){
                    MerchantProductNo = item.MerchantProductNo,
                    StockLocations = new List<StockLocation>()
                    {
                        new StockLocation()
                        {
                        StockLocationId = item.StockLocation.Id,
                        Stock = Config.StockValue
                        }
                    }
                }
            };

            //update stock
            var result = product.UpdateStock(model);

            if (result)
            {
                Console.WriteLine("Updated successfully");
            }
            else
            {
                Console.WriteLine("Error while Updating");
            }

            Console.ReadLine();
        }

        public static void DisplayOfTopFive(List<Line> orders)
        {
            Console.WriteLine(String.Format("|{0,15}|{1,20}|{2,5}|", "Product No", "Gtin", "Qty"));
            foreach (var itemLine in orders)
            {
                Console.WriteLine(String.Format("|{0,15}|{1,20}|{2,5}|", itemLine.MerchantProductNo, itemLine.Gtin, itemLine.Quantity));
            }

        }
    }
}
