using ChannelEngine_Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine_Repository
{
    public interface IRepository
    {
        Task<List<Order>> GetOrders(string status);
        Task<bool> UpdateStock(List<Product> product);

    }
}
