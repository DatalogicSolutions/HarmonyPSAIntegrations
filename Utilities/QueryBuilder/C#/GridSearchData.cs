using System.Collections.Generic;
using Newtonsoft.Json;
using QueryBuilderLib.Filters;

namespace QueryBuilderLib
{
    public class GridSearchData
    {
        public GridSearchData()
        {
            Rules = new List<IFilter>();
        }

        /// <summary>
        /// Filter operand; AND or OR
        /// </summary>
        [JsonProperty(PropertyName = "groupOp")]
        public GridSearchOperand GroupOp { get; set; }

        /// <summary>
        /// The filter rules
        /// </summary>
        [JsonProperty(PropertyName = "rules")]
        public List<IFilter> Rules { get; set; }
    }
}