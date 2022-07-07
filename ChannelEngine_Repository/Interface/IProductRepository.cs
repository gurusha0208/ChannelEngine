using ChannelEngine_Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine_Repository.Interface
{
    public interface IProductRepository
    {
        List<Line> GetTopProducts(string status);
        bool UpdateStock(Product product);
    }
}
