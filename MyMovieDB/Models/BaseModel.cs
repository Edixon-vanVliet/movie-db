namespace MyMovieDB.Models;

public class BaseModel
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}