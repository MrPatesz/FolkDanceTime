using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureItemSet(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ItemSet, ItemSetDto>();

            return cfg;
        }
    }
}
