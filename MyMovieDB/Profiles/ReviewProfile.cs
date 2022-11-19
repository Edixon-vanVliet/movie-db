using AutoMapper;
using MyMovieDB.DTOS;
using MyMovieDB.Models;

namespace MyMovieDB.Profiles;

public sealed class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDTO>().ReverseMap();
    }
}