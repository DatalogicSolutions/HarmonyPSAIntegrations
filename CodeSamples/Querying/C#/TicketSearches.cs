using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using NUnit.Framework;
using QueryBuilderLib;
using QueryBuilderLib.Filters;

namespace QueryingSamples
{
    [TestFixture]
    public class TicketSearches
    {
        [Test]
        public void find_ticket_which_match_name()
        {
            var query = new QueryBuilder()
            {
                PageNo = 1,
                PageSize = 20,
                SortIndex = 0,
                SortOrder = GridSortOrder.Asc,
                FilterInfo = new GridSearchData()
                {
                    GroupOp = GridSearchOperand.AND,
                    Rules = new List<IFilter>()
                    {
                        new StandardFilter()
                        {
                            ColumnName = "Name",
                            FilterOperation = FilterOperation.cn,
                            FilterData = "this phrase"                            
                        }
                    }
                },
                OnlyTheseColumns = new List<string>() { "Name", "Status", "StatusType" },
                ResultsAsCsv = true
            };

            var results = query.Execute("http://demo.harmonydev.com/api/TicketsData/Search", "api.user@harmonyerp.com", "password");

            Debug.Print(results);
        }
    }
}
