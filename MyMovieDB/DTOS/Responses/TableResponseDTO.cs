namespace MyMovieDB.DTOS;

public sealed class TableResponseDTO<T>
{
    public bool Success { get; set; }
    public int Count { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public string Sort { get; set; }
    public string Filter { get; set; }
    public IEnumerable<T> Data { get; set; }

    public TableResponseDTO(IEnumerable<T> data, int count, int page, int size, string sort, string filter)
    {
        Success = true;
        Count = count;
        Page = page;
        Size = size;
        Sort = sort;
        Filter = filter;
        Data = data;
    }
}