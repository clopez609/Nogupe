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

                cfg.CreateMap<IEnumerable<CareerViewModel>, IEnumerable<Career>>();

                cfg.CreateMap(typeof(PagedResult<Career>), typeof(PagedListResultViewModel<CareerViewModel>));


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

        public static IEnumerable<CareerViewModel> ToViewModel(this IEnumerable<Career> career)
        {
            return Mapper.Map<IEnumerable<CareerViewModel>>(career);
        }

        public static PagedListResultViewModel<CareerViewModel> ToViewModel(
            this PagedResult<Career> carrers)
        {
            return Mapper.Map<PagedListResultViewModel<CareerViewModel>>(carrers);
        }

    }
}
