using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieDB.Models;

public sealed class Movie : BaseModel
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Synopsis { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public int Rating { get; private set; }
    // Todo: Add image

    public int CategoryId { get; private set; }

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; private set; }

    public Movie()
    {
        Title = "";
        Description = "";
        Synopsis = "";
        ReleaseDate = new DateTime();
        Rating = 0;
    }

    public Movie(string title, string description, string synopsis, DateTime releaseDate, int rating, int categoryId)
    {
        Title = title;
        Description = description;
        Synopsis = synopsis;
        ReleaseDate = releaseDate;
        Rating = rating;

        CategoryId = categoryId;
    }

    public Movie(int id, string title, string description, string synopsis, DateTime releaseDate, int rating, int categoryId) : this(title, description, synopsis, releaseDate, rating, categoryId)
    {
        Id = id;
    }
}