using Microsoft.EntityFrameworkCore;
using MyMovieDB.Models;

namespace MyMovieDB.Data;

public sealed class MovieRepository : Repository<Movie>
{
    public MovieRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task AddAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        _context.Entry(movie).Reference(movie => movie.Category).Load();

        await SaveChangesAsync();
    }

    public override async Task<Movie?> GetAsync(int id)
    {
        return await _context.Movies.Include(movie => movie.Category).FirstOrDefaultAsync(movie => movie.Id == id);
    }

    public async Task<(int, IEnumerable<Movie>)> GetAllAsync(int page, int size, string sort, string filter)
    {
        var movies = _context.Movies
            .Where(movie => movie.Title.ToLower().Contains(filter.ToLower()));

        return (
            movies.Count(),
            await (sort == "asc" ? movies.OrderBy(movie => movie.CreatedAt) : movies.OrderByDescending(movie => movie.CreatedAt))
            .Skip(page * size)
            .Take(size)
            .Include(movie => movie.Category)
            .ToListAsync()
        );
    }

    public async Task<(int, IEnumerable<Review>)> GetReviewsAsync(int id, int page, int size, string sort, string filter)
    {
        var reviews = _context.Reviews
            .Where(review => review.MovieId == id && review.User.Contains(filter));

        return (
            reviews.Count(),
            await (sort == "asc" ? reviews.OrderBy(review => review.CreatedAt) : reviews.OrderByDescending(review => review.CreatedAt))
            .Skip(page * size)
            .Take(size)
            .ToListAsync()
        );
    }

    public async Task<Review> AddReviewAsync(Review review)
    {
        Review newReview = (await _context.Reviews.AddAsync(review)).Entity;

        await SaveChangesAsync();

        return newReview;
    }
}