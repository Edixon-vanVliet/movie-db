using System.ComponentModel.DataAnnotations;

namespace MyMovieDB.DTOS;

public class MovieDTO
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string Synopsis { get; set; } = "";

    public DateTime ReleaseDate { get; set; }

    public int Rating { get; set; }
}