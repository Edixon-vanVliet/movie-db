using Microsoft.EntityFrameworkCore;
using MyMovieDB.Data;
using MyMovieDB.Models;

namespace MyMovieDB.Test;

public class Config
{
    public ApplicationDbContext Context { get; }

    public Config()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase(databaseName: "LibraryDbInMemory");

        var dbContextOptions = builder.Options;
        Context = new ApplicationDbContext(dbContextOptions);

        // Delete existing db before creating a new one
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }
}