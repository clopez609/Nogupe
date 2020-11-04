using AutoMapper;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.ViewModels.Wall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Mappings
{
    public static class WallFileMapper
    {
        private static readonly IMapper Mapper;

        static WallFileMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<WallFileViewModel, WallFile>()
                    .ForMember(dest => dest.WallId, opt => opt.MapFrom(src => src.WallId))
                    .ForMember(dest => dest.FileId, opt => opt.MapFrom(src => src.FileId));

            });

            Mapper = config.CreateMapper();
        }

        public static WallFile ToEntityModel(this WallFileViewModel wallFileViewModel, WallFile wallFile)
        {
            return Mapper.Map(wallFileViewModel, wallFile);
        }

    }
}
