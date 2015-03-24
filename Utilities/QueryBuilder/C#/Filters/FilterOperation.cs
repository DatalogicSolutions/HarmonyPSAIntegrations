using System.ComponentModel;

namespace QueryBuilderLib.Filters
{
    public enum FilterOperation
    {
        [Description("Undefined")]
        undefined,
        [Description("Between")]
        bw,
        [Description("Wildcard")]
        ws,
        [Description("Equals")]
        eq,
        [Description("Does Not Equal")]
        ne,
        [Description("is Less Than")]
        lt,
        [Description("is Less Than or Equal To")]
        le,
        [Description("is Greater Than")]
        gt,
        [Description("is Greater or Equal To")]
        ge,
        [Description("Ends With")]
        ew,
        [Description("Starts With")]
        sw,
        [Description("Contains")]
        cn,
        [Description("Is Null")]
        isnull,
        [Description("All")]
        all,
        [Description("is Empty or is Null")]
        none,
        [Description("In")]
        inop,
        [Description("Not In")]
        notin
    }
}