using Microsoft.EntityFrameworkCore;
using MyMovieDB.Models;

namespace MyMovieDB.Data;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; }
    public DbSet<Movie> Movies { get; }
    public DbSet<Review> Reviews { get; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        Categories = this.Set<Category>();
        Movies = this.Set<Movie>();
        Reviews = this.Set<Review>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(CategoriesSeed.Get());
        modelBuilder.Entity<Movie>().HasData(MoviesSeed.Get());
        modelBuilder.Entity<Review>().HasData(ReviewsSeed.Get());

        modelBuilder.Entity<Movie>().HasMany<Review>().WithOne().HasForeignKey(review => review.MovieId);

        modelBuilder.Entity<Movie>().HasQueryFilter(movie => movie.DeletedAt == null);
        modelBuilder.Entity<Review>().HasQueryFilter(movie => movie.DeletedAt == null);
        modelBuilder.Entity<Category>().HasQueryFilter(movie => movie.DeletedAt == null);

        base.OnModelCreating(modelBuilder);
    }
}