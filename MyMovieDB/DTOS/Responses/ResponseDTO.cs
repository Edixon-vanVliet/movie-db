namespace MyMovieDB.DTOS;

public sealed class ResponseDTO<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }

    public ResponseDTO(T data)
    {
        Success = true;
        Data = data;
    }
}