using System.ComponentModel.DataAnnotations;

namespace MyMovieDB.DTOS;

public sealed class ReviewDTO
{
    public int Id { get; set; }

    [Required]
    public string User { get; set; } = "";

    [Required]
    public string Comment { get; set; } = "";

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Movie ID is required")]
    public int MovieId { get; set; }
}