using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureSearchResult(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Item, SearchResultDto>()
                .ForMember(dto => dto.ItemName, opt => opt.MapFrom(entity => entity.Name));

            return cfg;
        }
    }
}
