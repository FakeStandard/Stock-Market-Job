using System;
using System.Collections.Generic;
using System.Text;

namespace Daily_Quotes_Job
{
    public class DailyQuotesService
    {
        private DailyQuotesProvider dailyQuotesProvider = null;
        public DailyQuotesService()
        {
            dailyQuotesProvider = new DailyQuotesProvider();
        }

        public List<DailyQuotesModel> GetAllItems()
        {
            return dailyQuotesProvider.GetAllItems();
        }

        public void Insert(List<DailyQuotesModel> list)
        {
            dailyQuotesProvider.Insert(list);
        }
    }
}
