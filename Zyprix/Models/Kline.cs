using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Zyprix.Models
{
    public class Kline
    {
        public int? Id { get; set; }
        public int? CoinId { get; set; }
        public KlineInterval? Interval { get; set; }
        public long? KlineOpenTime { get; set; }
        public decimal? OpenPrice { get; set; }
        public decimal? HighPrice { get; set; }
        public decimal? LowPrice { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? Volume { get; set; }
        public int? NumberOfTrades { get; set; }
        public DateTime? CreateDate { get; set; }
    }

    public enum KlineInterval
    {
        [Description("1s")]
        OneSecond = 0,

        [Description("1m")]
        OneMinute = 1,

        [Description("3m")]
        ThreeMinutes = 2,

        [Description("5m")]
        FiveMinutes = 3,

        [Description("15m")]
        FifteenMinutes = 4,

        [Description("30m")]
        ThirtyMinutes = 5,

        [Description("1h")]
        OneHour = 6,

        [Description("2h")]
        TwoHours = 7,

        [Description("4h")]
        FourHours = 8,

        [Description("6h")]
        SixHours = 9,

        [Description("8h")]
        EightHours = 10,

        [Description("12h")]
        TwelveHours = 11,

        [Description("1d")]
        OneDay = 12,

        [Description("3d")]
        ThreeDays = 13,

        [Description("1w")]
        OneWeek = 14,

        [Description("1mo")]
        OneMonth = 15
    }

}
