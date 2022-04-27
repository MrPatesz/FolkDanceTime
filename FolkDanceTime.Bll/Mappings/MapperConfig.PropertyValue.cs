using AutoMapper;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        private static IMapperConfigurationExpression ConfigurePropertyValue(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PropertyValue, PropertyValueDto>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Property.Name))
                .ForMember(dto => dto.PropertyId, opt => opt.MapFrom(entity => entity.PropertyId))
                .ForMember(dto => dto.PropertyValueId, opt =>opt.MapFrom(entity => entity.Id));

            return cfg;
        }
    }
}
