using AutoMapper;

namespace FolkDanceTime.Bll.Mappings
{
    public static partial class MapperConfig
    {
        public static IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConfigureCategory();
                cfg.ConfigureProperty();
                cfg.ConfigurePropertyValue();
                cfg.ConfigureItem();
                cfg.ConfigureItemTransaction();
                cfg.ConfigureUser();
                cfg.ConfigureDetailedItemTransaction();
                cfg.ConfigureItemSet();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
