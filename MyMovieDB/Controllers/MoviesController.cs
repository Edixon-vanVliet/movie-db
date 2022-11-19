using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMovieDB.Data;
using MyMovieDB.DTOS;
using MyMovieDB.Models;

namespace MyMovieDB.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;
    private readonly IMapper _mapper;
    private readonly MovieRepository _movieRepository;

    public MoviesController(ILogger<MoviesController> logger, IMapper mapper, IRepository<Movie> movieRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _movieRepository = movieRepository as MovieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies(
        [FromQuery] int page = 0,
        [FromQuery] int size = 10,
        [FromQuery] string sort = "asc",
        [FromQuery] string filter = "")
    {
        try
        {
            _logger.LogInformation($"Get Movies with following arguments: \n\n Page: {page}, Size: {size}, Sort: {sort}, Filter: {filter}");
            (int count, IEnumerable<Movie> movies) = await _movieRepository.GetAllAsync(page, size, sort, filter);

            return Ok(new TableResponseDTO<GetMovieDTO>(_mapper.Map<IEnumerable<GetMovieDTO>>(movies), count, page, size, sort, filter));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO());
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation($"Get Movie with ID: {id}");
            Movie? movie = await _movieRepository.GetAsync(id);

            if (movie is null)
            {
                _logger.LogInformation($"Movie with ID: {id} not found.");
                return NotFound(new NotFoundResponseDTO(nameof(Movie), id));
            }

            return Ok(new ResponseDTO<GetMovieDTO>(_mapper.Map<GetMovieDTO>(movie)));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO());
        }

    }

    [HttpGet("{id}/reviews")]
    public async Task<IActionResult> GetReviews(
        [FromRoute] int id,
        [FromQuery] int page = 0,
        [FromQuery] int size = 10,
        [FromQuery] string sort = "asc",
        [FromQuery] string filter = "")
    {
        try
        {
            _logger.LogInformation($"Get review of movie with ID: {id}");
            (int count, IEnumerable<Review> reviews) = await _movieRepository.GetReviewsAsync(id, page, size, sort, filter);

            return Ok(new TableResponseDTO<ReviewDTO>(
                _mapper.Map<IEnumerable<ReviewDTO>>(reviews), count, page, size, sort, filter));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO());
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostMovie([FromBody] CreateMovieDTO movie)
    {
        try
        {
            _logger.LogInformation($"Add movie.");
            Movie newMovie = _mapper.Map<Movie>(movie);
            await _movieRepository.AddAsync(newMovie);

            return CreatedAtAction(
                nameof(GetMovie),
                new { newMovie.Id },
                new ResponseDTO<GetMovieDTO>(_mapper.Map<GetMovieDTO>(newMovie)));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO());
        }
    }

    [HttpPost("{id}/reviews")]
    public async Task<IActionResult> PostReview(int id, [FromBody] ReviewDTO review)
    {
        try
        {
            _logger.LogInformation($"Add review to movie with ID: {id}.");
            Review newReview = await _movieRepository.AddReviewAsync(_mapper.Map<Review>(review));

            return CreatedAtAction(
                nameof(GetReviews),
                new { id },
                new ResponseDTO<ReviewDTO>(_mapper.Map<ReviewDTO>(newReview)));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO());
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, [FromBody] CreateMovieDTO movie)
    {
        try
        {
            _logger.LogInformation($"Update Movie with ID: {id}");
            Movie? updatedMovie = await _movieRepository.UpdateAsync(_mapper.Map<Movie>(movie));

            if (updatedMovie is null)
            {
                _logger.LogInformation($"Movie with ID: {id} not found.");
                return NotFound(new NotFoundResponseDTO(nameof(Movie), id));
            }

            return Ok(new ResponseDTO<GetMovieDTO>(_mapper.Map<GetMovieDTO>(updatedMovie)));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO());
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        try
        {
            _logger.LogInformation($"Delete Movie with ID: {id}");
            Movie? movie = await _movieRepository.DeleteAsync(id);

            if (movie is null)
            {
                _logger.LogInformation($"Movie with ID: {id} not found.");
                return NotFound(new NotFoundResponseDTO(nameof(Movie), id));
            }

            return Ok(new ResponseDTO<GetMovieDTO>(_mapper.Map<GetMovieDTO>(movie)));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO());
        }
    }
}
