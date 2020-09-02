using AutoMapper;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Models.Career;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Mappings
{
    public static class CareerMapper
    {
        private static readonly IMapper Mapper;

        static CareerMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Career, CareerViewModel>();

                cfg.CreateMap<CareerViewModel, Career>();

            });

            Mapper = config.CreateMapper();
        }

        public static Career ToEntityModel (this CareerViewModel careerViewModel, Career career)
        {
            return Mapper.Map(careerViewModel, career);
        }

        public static CareerViewModel ToViewModel (this Career career)
        {
            return Mapper.Map<CareerViewModel>(career);
        }
    }
}
