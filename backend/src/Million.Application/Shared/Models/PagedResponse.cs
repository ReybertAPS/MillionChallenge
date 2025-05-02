namespace Million.Application.Shared.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long TotalRecords { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    public bool HasNextPage => PageIndex < TotalPages;
    public bool HasPreviousPage => PageIndex > 1;

    public PagedResponse() { }

    public PagedResponse(IEnumerable<T> data, int pageIndex, int pageSize, long totalRecords)
    {
        Data = data;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }
}
