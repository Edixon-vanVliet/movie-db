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

    public Review(int movieId, string user, string comment)
    {
        MovieId = movieId;
        User = user;
        Comment = comment;
    }

    public Review(int id, int movieId, string user, string comment) : this(movieId, user, comment)
    {
        Id = id;
    }
}