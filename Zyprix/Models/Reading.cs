using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Zyprix.Models
{
    public class Reading
    {
        public int Id { get; set; }
        public DateTime TimeStampUTC { get; set; }
        public int CoinId { get; set; }
        public PredictClass PredictClass { get; set; }
        public double ProbSell { get; set; }
        public double ProbHold { get; set; }
        public double ProbBuy { get; set; }
        public decimal? Price { get; set; }
        public double? EMA { get; set; }
        public double? Volatility { get; set; }
        public bool PassedProbFilter { get; set; }
        public bool PassedTrendFilter { get; set; }
        public bool PassedVolFilter { get; set; }
        public string FinalSignal { get; set; }
        public int ModelId { get; set; }
        public int ConfigRowId { get; set; }
        public bool SentToAzure { get; set; }
        public DateTime CreateDate { get; set; }
    }


    public enum PredictClass {
        [Description("Sell")]
        SELL = 0,
        [Description("Hold")]
        HOLD = 1,
        [Description("Buy")]
        BUY = 2  
    }

}
