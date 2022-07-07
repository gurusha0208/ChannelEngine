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
    public class Repository : IRepository
    {       
        public Repository()
        {

        }

        [HttpGet]
        public async Task<List<Order>> GetOrders(string status) 
        {
            List<Order> listOrders = new List<Order>();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync(Config.ApiUrl+ "v2/orders?statuses=" + status.ToUpper() + "&apikey=" + Config.ApiKey);

                    if (response.IsSuccessStatusCode)
                    {

                        var objResponse = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<OrderResponse>(objResponse);
                        if (result != null)
                            listOrders = result.Content;

                    }

                    return listOrders;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        public async Task<bool> UpdateStock(List<Product> product) 
        { 
            bool result = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Add("X-CE-KEY", Config.ApiKey);

                    var myContent = JsonConvert.SerializeObject(product);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PutAsync(Config.ApiUrl + "v2/offer/stock", byteContent);

                    if (response.IsSuccessStatusCode)
                    {
                        result = true;
                    }
                                        
                    return result;
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
