using ChannelEngine_Domain.Model;
using ChannelEngine_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine_WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IConfiguration _config;
        public ProductController(IProductService productService, IConfiguration config)
        {
            _service = productService;
            _config = config;
        }

        public IActionResult Index()
        {
            List<Line> model = new List<Line>();
            try
            {
                var status = _config["AppSettings:Status"];
                model = _service.GetTopProducts(status);

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error :" + ex.Message;
            }

            return View(model);
        }

        public IActionResult UpdateStock(int id, string no)
        {

            try
            {
                var product = new List<Product>()
                {
                    new Product{
                        MerchantProductNo = no,
                        StockLocations = new List<StockLocation>()
                        {
                          new StockLocation(){  Stock = Convert.ToInt32(_config["AppSettings:StockValue"]),
                            StockLocationId = id }
                        }
                    }
                };
                bool result = _service.UpdateStock(product);
                TempData["Message"] = "Successfully updated ";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error :" + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
