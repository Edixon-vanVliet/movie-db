using System.ComponentModel.DataAnnotations;

namespace MyMovieDB.DTOS;

public sealed class CreateMovieDTO : MovieDTO
{
    [Required]
    public int CategoryId { get; set; }
}