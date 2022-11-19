namespace MyMovieDB.DTOS;

public sealed class GetMovieDTO : MovieDTO
{
    public string Category { get; set; } = "";
}