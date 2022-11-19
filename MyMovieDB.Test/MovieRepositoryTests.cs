using MyMovieDB.Data;
using MyMovieDB.Models;

namespace MyMovieDB.Test;

public class MovieRepositoryTests
{
    private readonly Config _config = new Config();

    [Fact]
    public async Task GetAllAsync_Return_50Movies()
    {
        var repo = new MovieRepository(_config.Context);
        int expectedMovies = 50;

        IEnumerable<Movie> movies = await repo.GetAllAsync();

        Assert.NotEmpty(movies);
        Assert.Equal(expectedMovies, movies.Count());
    }

    [Fact]
    public async Task GetAllAsync_Return_10_Movies_And_Count_50()
    {
        var repo = new MovieRepository(_config.Context);
        int page = 0;
        int size = 10;
        string sort = "asc";
        string filter = "";
        int expectedCount = 50;
        int expectedMovies = 10;

        (int count, IEnumerable<Movie> movies) = await repo.GetAllAsync(page, size, sort, filter);

        Assert.NotEmpty(movies);
        Assert.Equal(expectedMovies, movies.Count());
        Assert.Equal(expectedCount, count);
    }

    [Fact]
    public async Task GetAllAsync_Return_7_Movies_With_Dr_In_Title()
    {
        var repo = new MovieRepository(_config.Context);
        int page = 0;
        int size = 10;
        string sort = "asc";
        string filter = "Dr.";
        int expectedCount = 7;

        (int count, IEnumerable<Movie> movies) = await repo.GetAllAsync(page, size, sort, filter);

        Assert.NotEmpty(movies);
        Assert.Equal(expectedCount, count);
    }

    [Fact]
    public async Task GetAsync_Return_1_Movie()
    {
        var repo = new MovieRepository(_config.Context);
        Movie expectedMovie = MoviesSeed.Get().First(movie => movie.Id == 1);

        var movie = await repo.GetAsync(expectedMovie.Id);

        Assert.NotNull(movie);
        Assert.Equal(expectedMovie.Id, movie.Id);
        Assert.Equal(expectedMovie.Title, movie.Title);
        Assert.Equal(expectedMovie.Description, movie.Description);
        Assert.Equal(expectedMovie.Synopsis, movie.Synopsis);
        Assert.Equal(expectedMovie.ReleaseDate, movie.ReleaseDate);
        Assert.Equal(expectedMovie.Rating, movie.Rating);
        Assert.Equal(expectedMovie.CategoryId, movie.CategoryId);
        Assert.Equal(expectedMovie.CreatedAt, movie.CreatedAt);
        Assert.Equal(expectedMovie.ModifiedAt, movie.ModifiedAt);
        Assert.Equal(expectedMovie.DeletedAt, movie.DeletedAt);
    }

    [Fact]
    public async Task GetAsync_Return_Null_If_No_Movie()
    {
        var repo = new MovieRepository(_config.Context);
        int id = 150;

        var movie = await repo.GetAsync(id);

        Assert.Null(movie);
    }

    [Fact]
    public async Task AddAsync_Create_Movie()
    {
        var repo = new MovieRepository(_config.Context);
        var newMovie = new Movie("Title", "Description", "Synopsis", new DateTime(2022, 1, 1), 4, 1);

        await repo.AddAsync(newMovie);

        Assert.NotNull(newMovie);
        Assert.NotEqual(0, newMovie.Id);
        Assert.Equal("Title", newMovie.Title);
        Assert.Equal("Description", newMovie.Description);
        Assert.Equal("Synopsis", newMovie.Synopsis);
        Assert.Equal(new DateTime(2022, 1, 1), newMovie.ReleaseDate);
        Assert.Equal(4, newMovie.Rating);
        Assert.Equal(1, newMovie.CategoryId);
        Assert.NotNull(newMovie.Category);
        Assert.NotEqual(new DateTime(), newMovie.CreatedAt);
        Assert.Null(newMovie.ModifiedAt);
        Assert.Null(newMovie.DeletedAt);
    }

    [Fact]
    public async Task UpdateAsync_Return_Updated_Movie()
    {
        var repo = new MovieRepository(_config.Context);
        var movie = MoviesSeed.Get().First();
        var expectedMovie = new Movie(movie.Id, "Title", movie.Description, movie.Synopsis, movie.ReleaseDate, movie.Rating, movie.CategoryId);

        var updatedMovie = await repo.UpdateAsync(expectedMovie);

        Assert.NotNull(updatedMovie);
        Assert.Equal(expectedMovie.Id, updatedMovie.Id);
        Assert.Equal(expectedMovie.Title, updatedMovie.Title);
        Assert.Equal(expectedMovie.Description, updatedMovie.Description);
        Assert.Equal(expectedMovie.Synopsis, updatedMovie.Synopsis);
        Assert.Equal(expectedMovie.ReleaseDate, updatedMovie.ReleaseDate);
        Assert.Equal(expectedMovie.Rating, updatedMovie.Rating);
        Assert.Equal(expectedMovie.CategoryId, updatedMovie.CategoryId);
        Assert.NotNull(updatedMovie.ModifiedAt);
    }

    [Fact]
    public async Task UpdateAsync_Return_Null()
    {
        var repo = new MovieRepository(_config.Context);
        var expectedMovie = new Movie(150, "Title", "Description", "Synopsis", DateTime.UtcNow, 1, 1);

        var updatedMovie = await repo.UpdateAsync(expectedMovie);

        Assert.Null(updatedMovie);
    }

    [Fact]
    public async Task DeleteAsync_Return_Deleted_Movie()
    {
        var repo = new MovieRepository(_config.Context);
        Movie expectedMovie = MoviesSeed.Get().First();

        var deletedMovie = await repo.DeleteAsync(expectedMovie.Id);

        Assert.NotNull(deletedMovie);
        Assert.Equal(expectedMovie.Id, deletedMovie.Id);
        Assert.Equal(expectedMovie.Title, deletedMovie.Title);
        Assert.Equal(expectedMovie.Description, deletedMovie.Description);
        Assert.Equal(expectedMovie.Synopsis, deletedMovie.Synopsis);
        Assert.Equal(expectedMovie.ReleaseDate, deletedMovie.ReleaseDate);
        Assert.Equal(expectedMovie.Rating, deletedMovie.Rating);
        Assert.Equal(expectedMovie.CategoryId, deletedMovie.CategoryId);
        Assert.NotNull(deletedMovie.DeletedAt);
    }

    [Fact]
    public async Task DeleteAsync_Return_Null()
    {
        var repo = new MovieRepository(_config.Context);
        int id = 150;

        var updatedMovie = await repo.DeleteAsync(id);

        Assert.Null(updatedMovie);
    }

    [Fact]
    public async Task GetReviewsAsync_Return_10_Movies_And_Count_25()
    {
        var repo = new MovieRepository(_config.Context);
        int id = 1;
        int page = 0;
        int size = 10;
        string sort = "asc";
        string filter = "";
        int expectedCount = 25;
        int expectedReviews = 10;

        (int count, IEnumerable<Review> reviews) = await repo.GetReviewsAsync(id, page, size, sort, filter);

        Assert.NotEmpty(reviews);
        Assert.Equal(expectedReviews, reviews.Count());
        Assert.Equal(expectedCount, count);
    }

    [Fact]
    public async Task GetReviewsAsync_Return_2_Movies_With_User_Bob()
    {
        var repo = new MovieRepository(_config.Context);
        int id = 1;
        int page = 0;
        int size = 10;
        string sort = "asc";
        string filter = "Bob";
        int expectedCount = 2;

        (int count, IEnumerable<Review> reviews) = await repo.GetReviewsAsync(id, page, size, sort, filter);

        Assert.NotEmpty(reviews);
        Assert.Equal(expectedCount, count);
    }

    [Fact]
    public async Task AddReviewAsync_Create_Movie()
    {
        var repo = new MovieRepository(_config.Context);
        var newReview = new Review(1, "User", "Comment");

        await repo.AddReviewAsync(newReview);

        Assert.NotNull(newReview);
        Assert.NotEqual(0, newReview.Id);
        Assert.Equal("User", newReview.User);
        Assert.Equal("Comment", newReview.Comment);
        Assert.NotEqual(new DateTime(), newReview.CreatedAt);
        Assert.Null(newReview.ModifiedAt);
        Assert.Null(newReview.DeletedAt);
    }
}