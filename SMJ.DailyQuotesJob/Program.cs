using SMJ.Model;
using SMJ.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SMJ.DailyQuotesJob
{
    class Program
    {
        private static DailyQuotesService services = new DailyQuotesService();
        static async Task Main(string[] args)
        {
            // 爬取和新增資料
            await CrawlerStockByDate(DateTime.Now);

            // 爬取過往資料
            //var start = new DateTime(2020, 1, 1);
            //var end = new DateTime(2020, 12, 13);
            //CrawlerStockByOldDate(start, end);

            Console.WriteLine("Done.");

            // 取得每日資料
            //GetData();

            Console.ReadLine();
        }

        /// <summary>
        /// 爬取每日收盤資料
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static async Task CrawlerStockByDate(DateTime date)
        {
            using (var client = new HttpClient())
            {
                //string json = await client.GetStringAsync($"https://www.twse.com.tw/exchangeReport/MI_INDEX?response=json&date=20211013&type=ALLBUT0999&_=1634182224496");
                string json = await client.GetStringAsync($"https://www.twse.com.tw/exchangeReport/MI_INDEX?response=json&date={date.ToString("yyyyMMdd")}&type=ALLBUT0999&_=1634182224496");

                var res = JsonSerializer.Deserialize<DataInfo>(json);
                List<DailyQuotes> list = new List<DailyQuotes>();

                if (res.stat.Equals("很抱歉，沒有符合條件的資料!"))
                {
                    Console.WriteLine($"{date.Date} is a holiday.");
                    return;
                }

                if (res.data9 != null)
                {
                    res.data9.ForEach(data =>
                    {
                        list.Add(new DailyQuotes()
                        {
                            StockCode = data[0],
                            StockName = data[1],
                            TradeDate = date.Date,
                            TradeVolumn = data[2],
                            TradeValue = data[3],
                            TradePrice = data[4],
                            OpeningPrice = data[5],
                            HighestPrice = data[6],
                            LowestPrice = data[7],
                            ClosingPrice = data[8],
                            Dir = data[9],
                            Change = data[10],
                            LastBestBidPrice = data[11],
                            LastBestBidVolume = data[12],
                            LastBestAskPrice = data[13],
                            LastBestAskVolume = data[14],
                            PER = data[15]

                        });
                    });

                    services.Insert(list);
                    Console.WriteLine($"{date.Date} Complete.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{date.Date} failed to obtain data.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// 取得每日資料
        /// </summary>
        public static void GetData()
        {
            var items = services.GetAllItems();

            if (items.Count == 0) { Console.WriteLine("No item."); return; }

            foreach (var item in items)
            {
                var sb = new StringBuilder();
                sb.Append($"{item.ID}, ");
                sb.Append($"{item.StockCode}, ");
                sb.Append($"{item.StockName}, ");
                sb.Append($"{item.TradeDate}, ");
                sb.Append($"{item.TradeVolumn}, ");
                sb.Append($"{item.TradeValue}, ");
                sb.Append($"{item.TradePrice}, ");
                sb.Append($"{item.OpeningPrice}, ");
                sb.Append($"{item.HighestPrice}, ");
                sb.Append($"{item.LowestPrice}, ");
                sb.Append($"{item.ClosingPrice}, ");
                sb.Append($"{item.Dir}, ");
                sb.Append($"{item.Change}, ");
                sb.Append($"{item.LastBestBidPrice}, ");
                sb.Append($"{item.LastBestBidVolume}, ");
                sb.Append($"{item.LastBestAskPrice}, ");
                sb.Append($"{item.LastBestAskVolume}, ");
                sb.Append($"{item.PER}, ");

                Console.WriteLine(sb.ToString());
                sb.Clear();
            }
        }

        /// <summary>
        /// 爬取過往日期區間資料
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static async void CrawlerStockByOldDate(DateTime start, DateTime end)
        {
            while (DateTime.Compare(start.Date, end.Date) <= 0)
            {
                if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine($"{start.Date} is a holiday.");
                    start = start.AddDays(1);
                    continue;
                }
                else
                {
                    // 爬取和新增資料
                    await CrawlerStockByDate(start);

                    start = start.AddDays(1);
                    Console.WriteLine($"next wait...");
                    Thread.Sleep(10000);
                }
            }
        }
    }

    public class DataInfo
    {
        public string stat { get; set; }
        public List<List<string>> data9 { get; set; }
        public List<string> fields9 { get; set; }
    }
}
