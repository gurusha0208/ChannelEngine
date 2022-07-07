using ChannelEngine_Domain.Model;
using ChannelEngine_Repository.Interface;
using ChannelEngine_Service.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine_Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public List<Line> GetTopProducts(string status)
        {
            List<Line> listOrders = new List<Line>();
            try
            {

                listOrders = _repository.GetTopProducts(status);
                return listOrders;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateStock(Product product)
        {
            bool result = false;
            try
            {

                result = _repository.UpdateStock(product);
                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
