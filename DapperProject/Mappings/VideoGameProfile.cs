using AutoMapper;
using DapperProject.Dtos;
using DapperProject.Models;

namespace DapperProject.Mappings
{
    public class VideoGameProfile : Profile
    {
        public VideoGameProfile() 
        {
            CreateMap<VideoGame, VideoGameDto>();

            CreateMap<CreateVideoGameDto, VideoGame>();

            CreateMap<UpdateVideoGameDto, VideoGame>();
        }
    }
}
