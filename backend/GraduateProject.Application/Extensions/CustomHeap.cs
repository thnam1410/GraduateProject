namespace GraduateProject.Application.Extensions;

public class CustomHeap<T> : Heap<T> where T : IHeapItem<T>
{
    public CustomHeap(int maxHeapSize) : base(maxHeapSize)
    {
    }

    public bool ContainsId(int heapIndex, Guid itemId)
    {
        var currentItem = items[heapIndex];
        var ids = items.Where(x => x is not null).Select(x =>
        {
            return x.GetType().GetProperty("Id")?.GetValue(x, null);
        });
        return ids.Contains(itemId);
    }
    
    public bool ContainsId(int heapIndex, int itemId)
    {
        var currentItem = items[heapIndex];
        var ids = items.Where(x => x is not null).Select(x =>
        {
            return x.GetType().GetProperty("Id")?.GetValue(x, null);
        });
        return ids.Contains(itemId);
    }
}