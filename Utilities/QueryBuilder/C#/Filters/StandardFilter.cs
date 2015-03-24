using Newtonsoft.Json;

namespace QueryBuilderLib.Filters
{
    public class StandardFilter : IFilter
    {
        [JsonProperty(PropertyName = "field")]
        public string ColumnName { get; set; }

        [JsonProperty(PropertyName = "op")]
        public FilterOperation FilterOperation { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object FilterData { get; set; }
    }
}