using ChannelEngine_Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine_Service.Interface
{
    public interface IProductService
    {
        List<Line> GetTopProducts(string status);
        bool UpdateStock(Product product);
    }
}
