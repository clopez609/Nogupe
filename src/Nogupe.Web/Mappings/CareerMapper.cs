using AutoMapper;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Career;
using System.Collections;
using System.Collections.Generic;

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

                cfg.CreateMap(typeof(PagedListResult<Career>), typeof(PagedListResultViewModel<CareerViewModel>));


            });

            Mapper = config.CreateMapper();
        }

        public static Career ToEntityModel(this CareerViewModel careerViewModel, Career career)
        {
            return Mapper.Map(careerViewModel, career);
        }

        public static CareerViewModel ToViewModel(this Career career)
        {
            return Mapper.Map<CareerViewModel>(career);
        }

        public static PagedListResultViewModel<CareerViewModel> ToViewModel(
            this PagedListResult<Career> carrers)
        {
            return Mapper.Map<PagedListResultViewModel<CareerViewModel>>(carrers);
        }

    }
}
