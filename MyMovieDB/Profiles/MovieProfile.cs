using AutoMapper;
using MyMovieDB.DTOS;
using MyMovieDB.Models;

namespace MyMovieDB.Profiles;

public sealed class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, GetMovieDTO>()
            .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.Category!.Name)
            );

        CreateMap<CreateMovieDTO, Movie>();
    }
}