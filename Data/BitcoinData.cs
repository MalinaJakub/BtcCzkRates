using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcCzkRates.Data
{
    public class BitcoinData
    {
        public int Id { get; set; }
        public decimal PriceEUR { get; set; }
        public decimal PriceCZK { get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
