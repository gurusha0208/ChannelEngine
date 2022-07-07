using ChannelEngine_Domain.Model;
using ChannelEngine_Repository.Implementation;
using ChannelEngine_Repository.Interface;
using ChannelEngine_Service.Implementation;
using ChannelEngine_Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ChannelEngine_Test
{    
    [TestClass]
    public class ProductServiceTest
    {
        public static Mock<IProductRepository> mock = new Mock<IProductRepository>();
            
        IProductService service = new ProductService(mock.Object);
        
        [TestMethod]
        public void CheckGetTopProductsReturnEmpty() 
        {
            var status = "TEST_STATUS";
            mock.Setup(p => p.GetTopProducts(status)).Returns(new List<Line>());
            var result = service.GetTopProducts(status);
            Assert.AreEqual(0, result.Count);
        }


        [TestMethod]
        public void CheckGetTopProductsReturnValue()
        {
            var status = "TEST_STATUS";
            var list = new List<Line>() { new Line { MerchantProductNo = "AA" , Gtin = "BB" , Quantity = 2 } };
            mock.Setup(p => p.GetTopProducts(status)).Returns(list);
            var result = service.GetTopProducts(status);
            Assert.AreEqual("AA", result.First().MerchantProductNo);
            Assert.AreEqual("BB", result.First().Gtin);
            Assert.AreEqual(2, result.First().Quantity);
        }

        [TestMethod]
        public void UpdateStockFailed()
        {
            var product = new List<Product>() { new Product() { MerchantProductNo= "NOTEXIST"} };
            mock.Setup(p => p.UpdateStock(product)).Returns(false);
            var result = service.UpdateStock(product);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateStockSuccess()
        {
            var product = new List<Product>() { new Product() { MerchantProductNo = "AS" } };
            mock.Setup(p => p.UpdateStock(product)).Returns(true);
            var result = service.UpdateStock(product);
            Assert.AreEqual(true, result);
        }
    }
}
