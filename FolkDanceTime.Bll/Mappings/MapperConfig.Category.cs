using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigureCategory(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Category, CategoryDto>()
                .ForMember(dto => dto.Properties, opt => opt.MapFrom(entity => entity.CategoryToProperties.Select(ctp => ctp.Property)));

            return cfg;
        }
    }
}
