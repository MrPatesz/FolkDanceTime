using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureItem(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Item, ItemDto>()
                .ForMember(dto => dto.Properties, opt => opt.MapFrom(entity => entity.PropertyValues));

            return cfg;
        }
    }
}
