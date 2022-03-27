namespace GraduateProject.Application.Core;

public class PaginatedListQuery
{
    public int Offset { get; set; } = 0;

    public int Limit { get; set; } = 20;

    public string SearchKey { get; set; }

    public Dictionary<string, string>? Sort { get; set; }

    public Filter? Filter { get; set; }
}

public class Filter
{
    public string Field { get; set; }

    public FilterOperator? Operator { get; set; }

    public object Value { get; set; }

    public FilterLogic? Logic { get; set; }

    public List<Filter> Filters { get; set; }
}

public enum FilterOperator
{
    Contains,
    StartsWith,
    EndsWith,
    NotContains,
    NotStartsWith,
    NotEndsWith,
    Equal,
    Equals,
    Eq,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Range,
    DateRange,
    Any,
    NotAny,
}

public enum FilterLogic
{
    And,
    Or,
}