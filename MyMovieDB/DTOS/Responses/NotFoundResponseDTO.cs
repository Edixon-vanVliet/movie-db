namespace MyMovieDB.DTOS;

public sealed class NotFoundResponseDTO
{
    public bool Success { get; set; }
    public string Error { get; set; }

    public NotFoundResponseDTO(string entityName, int id)
    {
        Success = false;
        Error = $"{entityName} with ID: {id} not found.";
    }
}