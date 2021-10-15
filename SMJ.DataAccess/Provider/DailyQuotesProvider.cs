using Dapper;
using SMJ.Model;
using SMJ.Log;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace SMJ.DataAccess.Provider
{
    public class DailyQuotesProvider
    {
        /// <summary>
        /// 查詢當年度所有資料
        /// </summary>
        /// <returns></returns>
        public List<DailyQuotes> GetAllItems()
        {
            List<DailyQuotes> items = null;

            string sqlCommand = @"
                    SELECT [ID]
                          ,[StockCode]
                          ,[StockName]
                          ,[TradeDate]
                          ,[TradeVolumn]
                          ,[TradeValue]
                          ,[TradePrice]
                          ,[OpeningPrice]
                          ,[HighestPrice]
                          ,[LowestPrice]
                          ,[ClosingPrice]
                          ,[Dir]
                          ,[Change]
                          ,[LastBestBidPrice]
                          ,[LastBestBidVolume]
                          ,[LastBestAskPrice]
                          ,[LastBestAskVolume]
                          ,[PER]
                      FROM [DailyQuotes]";

            using (var conn = new SqlConnection(DataAccessService.connectionStr))
            {
                items = conn.Query<DailyQuotes>(sqlCommand).ToList();
            }

            return items;
        }

        /// <summary>
        /// 查詢特定日期資料
        /// </summary>
        /// <returns></returns>
        public List<DailyQuotes> GetDateItem(DateTime date)
        {
            List<DailyQuotes> items = null;

            string sqlCommand = $@"
                    SELECT [ID]
                          ,[StockCode]
                          ,[StockName]
                          ,[TradeDate]
                          ,[TradeVolumn]
                          ,[TradeValue]
                          ,[TradePrice]
                          ,[OpeningPrice]
                          ,[HighestPrice]
                          ,[LowestPrice]
                          ,[ClosingPrice]
                          ,[Dir]
                          ,[Change]
                          ,[LastBestBidPrice]
                          ,[LastBestBidVolume]
                          ,[LastBestAskPrice]
                          ,[LastBestAskVolume]
                          ,[PER]
                      FROM [DailyQuotes]
                      WHERE [TradeDate] = '{date.Date.ToString("yyyy-MM-dd")}'";

            using (var conn = new SqlConnection(DataAccessService.connectionStr))
            {
                items = conn.Query<DailyQuotes>(sqlCommand).ToList();
            }

            return items;
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="list"></param>
        public void Insert(List<DailyQuotes> list)
        {
            string code = "";
            string name = "";

            try
            {
                string sqlCommand = @"
                    INSERT INTO [DailyQuotes]
                                ([StockCode]
                                ,[StockName]
                                ,[TradeDate]
                                ,[TradeVolumn]
                                ,[TradeValue]
                                ,[TradePrice]
                                ,[OpeningPrice]
                                ,[HighestPrice]
                                ,[LowestPrice]
                                ,[ClosingPrice]
                                ,[Dir]
                                ,[Change]
                                ,[LastBestBidPrice]
                                ,[LastBestBidVolume]
                                ,[LastBestAskPrice]
                                ,[LastBestAskVolume]
                                ,[PER])
                            VALUES
                                (@StockCode
                                ,@StockName
                                ,@TradeDate
                                ,@TradeVolumn
                                ,@TradeValue
                                ,@TradePrice
                                ,@OpeningPrice
                                ,@HighestPrice
                                ,@LowestPrice
                                ,@ClosingPrice
                                ,@Dir
                                ,@Change
                                ,@LastBestBidPrice
                                ,@LastBestBidVolume
                                ,@LastBestAskPrice
                                ,@LastBestAskVolume
                                ,@PER)";

                using (var conn = new SqlConnection(DataAccessService.connectionStr))
                {
                    using (var scope = new TransactionScope())
                    {
                        foreach (var l in list)
                        {
                            code = l.StockCode;
                            name = l.StockName;
                            conn.Execute(sqlCommand, l);

                            LoggerService.WriteInfo($"Add {code} {name}");
                        }

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                LoggerService.WriteError($"[{code} {name}]新增時發生意外錯誤,交易關閉", ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
