using Dapper;
using SMJ.Model;
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
        /// 查詢所有資料
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
        /// 新增資料
        /// </summary>
        /// <param name="list"></param>
        public void Insert(List<DailyQuotes> list)
        {
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
                        //int i = 0; 

                        foreach (var l in list)
                        {
                            //conn.ExecuteScalarAsync(sqlCommand, l);
                            conn.Execute(sqlCommand, l);
                            //Console.WriteLine($"{++i}. Complete {l.StockName}");
                        }

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
