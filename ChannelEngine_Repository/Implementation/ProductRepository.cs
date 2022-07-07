using ChannelEngine_Domain.Model;
using ChannelEngine_Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChannelEngine_Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly Repository _repository;

        public ProductRepository(Repository repository)
        {
            _repository = repository;
        }

        public List<Line> GetTopProducts(string status)
        {
            List<Line> list = new List<Line>();
            try
            {

                var model = _repository.GetOrders(status).Result;
                // Get top 5 products
                if (model != null && model.Count > 0)
                {
                    model.ForEach(x => list.AddRange(x.Lines));
                    list = list.OrderByDescending(x => x.Quantity).Take(5).ToList();
                }
                
                return list;

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

                result = _repository.UpdateStock(product).Result;
                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
