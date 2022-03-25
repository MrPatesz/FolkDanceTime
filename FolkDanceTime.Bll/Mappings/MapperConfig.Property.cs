using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureProperty(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Property, PropertyDto>();

            return cfg;
        }
    }
}
