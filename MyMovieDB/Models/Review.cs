namespace MyMovieDB.Models;

public sealed class Review : BaseModel
{
    public string User { get; private set; }
    public string Comment { get; private set; }
    public int MovieId { get; private set; }

    public Review()
    {
        User = "";
        Comment = "";
    }

    public Review(int movieId, string name, string comment)
    {
        MovieId = movieId;
        User = name;
        Comment = comment;
    }

    public Review(int id, int movieId, string name, string comment) : this(movieId, name, comment)
    {
        Id = id;
    }
}