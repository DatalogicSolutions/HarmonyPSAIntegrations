using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace QueryBuilderLib
{
    /// <summary>
    /// This class is used to generate urls to query data from harmony grids
    /// </summary>
    public class QueryBuilder
    {
        public QueryBuilder()
        {
            FilterInfo = new GridSearchData();
        }

        /// <summary>
        /// The column index with which to sort
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// The order of the sort
        /// </summary>
        public GridSortOrder SortOrder { get; set; }

        /// <summary>
        /// Which page of results to get back
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// The results page size
        /// </summary>        
        public int PageSize { get; set; }

        /// <summary>
        /// If specified, only return the columns named here. The names of these columns can be found in your developer F12 tools in your browser
        /// when refreshing grids
        /// </summary>
        public List<string> OnlyTheseColumns { get; set; }

        /// <summary>
        /// The filters you require
        /// </summary>
        public GridSearchData FilterInfo { get; set; }

        /// <summary>
        /// Do you want the results as CSV instead of JSON?
        /// </summary>
        public bool ResultsAsCsv { get; set; }

        /// <summary>
        /// Returns the generated url based on the configured properties
        /// </summary>
        /// <param name="endpointUrl">The url to the grid data. You can get this from your F12 developer tools in your browser</param>
        /// <returns>The url which executes the query and retreives the data</returns>
        public string ToUrl(string endpointUrl)
        {
            if (!endpointUrl.Contains("/api/"))
                throw new Exception("Your url needs to contain /api/ after the domain");

            var filters = JsonConvert.SerializeObject(FilterInfo, Formatting.None);
            var qs = "page=" + PageNo + "&rows=" + PageSize + "&sidx=" + SortIndex + "&sord=" + SortOrder.ToString().ToLower() + "&dynamicfilters=" + HttpUtility.UrlEncode(filters);

            if (OnlyTheseColumns != null && OnlyTheseColumns.Count > 0)
            {
                qs += "&onlySpecifiedColumns=true&gridColumns=" + OnlyTheseColumns.Aggregate((i, j) => i + "," + j);
            }

            if (ResultsAsCsv)
                qs += "&csvresult=true";

            if (!endpointUrl.Contains("?"))
                endpointUrl += "?";

            return endpointUrl + qs;
        }

        /// <summary>
        /// Returns the generated url based on the configured properties
        /// </summary>
        /// <param name="endpointUrl">The url to the grid data. You can get this from your F12 developer tools in your browser. Make sure you add /api/ after the domain</param>
        /// <param name="userName">The username you want to authenticate against in Harmony</param>
        /// <param name="password">The password you want to authenticate against in Harmony</param>
        /// <returns>The data returned from harmony as a json string</returns>
        public string Execute(string endpointUrl, string userName, string password)
        {
            using (WebClient client = new WebClient())
            {
                // The following 3 lines add the authentication information to your request
                string authInfo = userName + ":" + password;
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                client.Headers["Authorization"] = "Basic " + authInfo;

                endpointUrl = ToUrl(endpointUrl);

                // Post the transaction to Harmony
                byte[] responsebytes = client.DownloadData(endpointUrl);
                var resp = Encoding.UTF8.GetString(responsebytes);

                return resp;
            }            
        }

    }
}
