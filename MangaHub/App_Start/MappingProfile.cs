using AutoMapper;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Core.ViewModels;

namespace MangaHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Chapter, ChapterDto>().ReverseMap();
            Mapper.CreateMap<Reading, ReadingDto>().ReverseMap();
            Mapper.CreateMap<Chapter, ChapterFormViewModel>().ReverseMap();
        }
    }
}