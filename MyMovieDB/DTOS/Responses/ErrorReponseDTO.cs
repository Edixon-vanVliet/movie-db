namespace MyMovieDB.DTOS;

public sealed class ErrorResponseDTO
{
    public bool Success { get; set; }
    public string Error { get; set; }

    public ErrorResponseDTO()
    {
        Success = false;
        Error = "Something went wrong, try again later.";
    }
}