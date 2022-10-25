using AutoMapper;
using MyBackendApp.DTO;
using MyBackendApp.Models;

namespace MyBackendApp.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            CreateMap<QuoteAddDTO, Quote>();
            CreateMap<Quote, QuoteGetDTO>();
            CreateMap<Quote, QuotesWithSamuraiDto>();
            CreateMap<Samurai, SamuraiGetDTO>();
            CreateMap<SamuraiAddDTO, Samurai>();
            CreateMap<QuoteAddTextDto, Quote>();
            CreateMap<Horse, HorseGetDto>();

            CreateMap<Samurai, SamuraiWithQuoteDTO>();
            CreateMap<SamuraiAddWithQuotesDto, Samurai>();
            CreateMap<Samurai, SamuraiWithHorseDto>();
            CreateMap<Battle, BattleGetDto>();
            CreateMap<Samurai, SamuraiWithBattleDto>();
        }
    }
}