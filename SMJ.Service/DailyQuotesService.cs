using SMJ.DataAccess.Provider;
using SMJ.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMJ.Service
{
    public class DailyQuotesService
    {
        private DailyQuotesProvider dailyQuotesProvider = null;
        public DailyQuotesService()
        {
            dailyQuotesProvider = new DailyQuotesProvider();
        }

        public List<DailyQuotes> GetAllItems()
        {
            return dailyQuotesProvider.GetAllItems();
        }

        public void Insert(List<DailyQuotes> list)
        {
            dailyQuotesProvider.Insert(list);
        }
    }
}
