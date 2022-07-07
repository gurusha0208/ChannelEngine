using ChannelEngine_Domain.Model;
using ChannelEngine_Repository;
using ChannelEngine_Repository.Implementation;
using ChannelEngine_Repository.Interface;
using ChannelEngine_Service.Implementation;
using ChannelEngine_Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine_Test
{    
    [TestClass]
    public class ProductRepositoryTest
    { 
        public static Mock<IRepository> mock = new Mock<IRepository>();
            
        IProductRepository repo = new ProductRepository(mock.Object);
        
        [TestMethod]
        public void CheckGetTopProductsReturnEmpty() 
        {
            var status = "TEST_STATUS";
            Task<List<Order>> rt = Task.FromResult(new List<Order>());
            mock.Setup(p => p.GetOrders(status)).Returns(rt);
            var result = repo.GetTopProducts(status);
            Assert.AreEqual(0, result.Count);
        }


        [TestMethod]
        public void CheckGetTopProductsReturnValue()
        {
            var status = "TEST_STATUS";
            Task<List<Order>> list = Task.FromResult(new List<Order>() { new Order { Lines = new List<Line>() { new Line() { MerchantProductNo = "AA", Gtin = "BB", Quantity = 2 } } } });
            mock.Setup(p => p.GetOrders(status)).Returns(list);
            var result = repo.GetTopProducts(status);
            Assert.AreEqual("AA", result.First().MerchantProductNo);
            Assert.AreEqual("BB", result.First().Gtin);
            Assert.AreEqual(2, result.First().Quantity);
        }

        [TestMethod]
        public void UpdateStockFailed()
        {
            var product = new List<Product>() { new Product() { MerchantProductNo = "NOTEXIST" } };
            mock.Setup(p => p.UpdateStock(product)).Returns(Task.FromResult(false));
            var result = repo.UpdateStock(product);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UpdateStockSuccess()
        {
            var product = new List<Product>() { new Product() { MerchantProductNo = "AS" } };
            mock.Setup(p => p.UpdateStock(product)).Returns(Task.FromResult(true));
            var result = repo.UpdateStock(product);
            Assert.AreEqual(true, result);
        }
    }
}
