using System;
using System.Collections.Generic;
using System.Text;

namespace Zyprix.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? ChainId { get; set; }
        public bool? Active { get; set; }
        public long? BinanceListingDate { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
