using AutoMapper;

namespace Nogupe.Web.Mappings
{
    public static class AuthMapper
    {
        private static readonly IMapper Mapper;

        static AuthMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

            });

            Mapper = config.CreateMapper();
        }

    }
}
