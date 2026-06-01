using System;
using System.Collections.Generic;
using System.Text;

namespace Zyprix.Models
{
    public class Configuration
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public decimal BuyProbabilityThreshold {  get; set; }
        public decimal SellProbabilityThreshold { get; set; }
        public int TrendEMALenght { get; set; }
        public int VolFilterWindow { get; set; }
        public decimal VolMinThreshold { get; set; }
        public decimal GlobalThreshold { get; set; }
        public decimal PerSymbolFloor { get; set; }
        public decimal Margin { get; set; }
        public int CooldownHours { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
