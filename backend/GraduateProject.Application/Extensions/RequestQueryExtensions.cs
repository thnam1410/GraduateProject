using GraduateProject.Application.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace GraduateProject.Application.Extensions;

public static class RequestQueryExtensions
{
    public static PaginatedListQuery? GetPaginatedListQuery(this IQueryCollection query)
    {
        PaginatedListQuery? paginatedListQuery = new PaginatedListQuery();
        StringValues stringValues;
        query.TryGetValue("offset", out stringValues);
        int result1;
        if (int.TryParse(stringValues.ToString(), out result1))
            paginatedListQuery.Offset = Math.Max(result1, 0);
        query.TryGetValue("limit", out stringValues);
        int result2;
        if (int.TryParse(stringValues.ToString(), out result2))
            paginatedListQuery.Limit = Math.Max(result2, 0);
        query.TryGetValue("searchKey", out stringValues);
        paginatedListQuery.SearchKey = stringValues.ToString();
        query.TryGetValue("filter", out stringValues);
        paginatedListQuery.Filter = JsonConvert.DeserializeObject<Filter>(stringValues.ToString());
        query.TryGetValue("sort", out stringValues);
        paginatedListQuery.Sort = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringValues.ToString());
        return paginatedListQuery;
    }
}