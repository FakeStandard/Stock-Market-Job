using System;
using System.Collections.Generic;
using System.Text;

namespace SMJ.DataAccess
{
    public class DataAccessService
    {
        /// <summary>
        /// 連線字串 
        /// </summary>
        private static string _connectionStr = @"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=StockDataCollectionDB;Integrated Security=True";

        /// <summary>
        /// 公共連線字串
        /// </summary>
        public static string connectionStr { get { return _connectionStr; } }
    }
}
