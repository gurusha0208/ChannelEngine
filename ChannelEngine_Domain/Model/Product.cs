using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine_Domain.Model
{      
    public class Product
    {
        public string MerchantProductNo { get; set; }
        public List<StockLocation> StockLocations { get; set; }
    }

    public class StockLocation
    {
        public int Stock { get; set; }
        public int StockLocationId { get; set; }
    }



}
