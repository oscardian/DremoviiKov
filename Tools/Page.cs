namespace Tools;

public class Page<T>
{
    public Int32 TotalCount { get; }
    public T[] Values { get; }

    public Page(Int32 totalCount, T[] values)
    {
        TotalCount = totalCount;
        Values = values;
    }
}
