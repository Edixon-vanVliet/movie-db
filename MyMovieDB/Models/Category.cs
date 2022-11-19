namespace MyMovieDB.Models;

public sealed class Category : BaseModel
{
    public string Name { get; private set; }

    public Category()
    {
        Name = "";
    }

    public Category(string name)
    {
        Name = name;
    }

    public Category(int id, string name) : this(name)
    {
        Id = id;
    }
}