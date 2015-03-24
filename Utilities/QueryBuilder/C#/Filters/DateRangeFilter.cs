using Newtonsoft.Json;

namespace QueryBuilderLib.Filters
{
    public abstract class DateRangeFilter : IFilter
    {
        [JsonProperty(PropertyName = "field")]
        public string ColumnName { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object FilterData { get; protected set; }        
    }
}