using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMJ.Model
{
    /// <summary>
    /// 每日收盤行情
    /// </summary>
    [Table("DailyQuotes")]
    public class DailyQuotes
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 證券代號
        /// </summary>
        public string StockCode { get; set; }

        /// <summary>
        /// 證券名稱
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime TradeDate { get; set; }

        /// <summary>
        /// 成交股數
        /// </summary>
        public string TradeVolumn { get; set; }

        /// <summary>
        /// 成交筆數
        /// </summary>
        public string TradeValue { get; set; }

        /// <summary>
        /// 成交金額
        /// </summary>
        public string TradePrice { get; set; }

        /// <summary>
        /// 開盤價
        /// </summary>
        public string OpeningPrice { get; set; }

        /// <summary>
        /// 最高價
        /// </summary>
        public string HighestPrice { get; set; }

        /// <summary>
        /// 最低價
        /// </summary>
        public string LowestPrice { get; set; }

        /// <summary>
        /// 收盤價
        /// </summary>
        public string ClosingPrice { get; set; }

        /// <summary>
        /// 漲跌(+/-)
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// 漲跌價差
        /// </summary>
        public string Change { get; set; }

        /// <summary>
        /// 最後揭示買價
        /// </summary>
        public string LastBestBidPrice { get; set; }

        /// <summary>
        /// 最後揭示買量
        /// </summary>
        public string LastBestBidVolume { get; set; }

        /// <summary>
        /// 最後揭示賣價
        /// </summary>
        public string LastBestAskPrice { get; set; }

        /// <summary>
        /// 最後揭示賣量
        /// </summary>
        public string LastBestAskVolume { get; set; }

        /// <summary>
        /// 本益比
        /// </summary>
        public string PER { get; set; }
    }
}
