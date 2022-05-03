using System.Linq.Expressions;

namespace GraduateProject.Application.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }

    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source,
        bool condition,
        Func<TSource, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}