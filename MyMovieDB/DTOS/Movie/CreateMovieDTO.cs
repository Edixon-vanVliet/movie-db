using System.ComponentModel.DataAnnotations;

namespace MyMovieDB.DTOS;

public sealed class CreateMovieDTO : MovieDTO
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Category ID is required")]
    public int CategoryId { get; set; }
}