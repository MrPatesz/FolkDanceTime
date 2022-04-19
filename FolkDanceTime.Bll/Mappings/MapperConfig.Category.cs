using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureCategory(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Category, CategoryDto>();

            return cfg;
        }
    }
}
