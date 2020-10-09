using AutoMapper;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Matter;
using System.Collections.Generic;

namespace Nogupe.Web.Mappings
{
    public static class MatterMapper
    {
        private static readonly IMapper Mapper;

        static MatterMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Matter, MatterViewModel>();

                cfg.CreateMap<MatterViewModel, Matter>();

                cfg.CreateMap<IEnumerable<MatterViewModel>, IEnumerable<Matter>>();

                cfg.CreateMap(typeof(PagedResult<Matter>), typeof(PagedListResultViewModel<MatterViewModel>));

            });

            Mapper = config.CreateMapper();
        }

        public static Matter ToEntityModel(this MatterViewModel matterViewModel, Matter matter)
        {
            return Mapper.Map(matterViewModel, matter);
        }

        public static MatterViewModel ToViewModel(this Matter matter)
        {
            return Mapper.Map<MatterViewModel>(matter);
        }

        public static IEnumerable<MatterViewModel> ToViewModel(this IEnumerable<Matter> matter)
        {
            return Mapper.Map<IEnumerable<MatterViewModel>>(matter);
        }

        public static PagedListResultViewModel<MatterViewModel> ToViewModel(
            this PagedResult<Matter> matters)
        {
            return Mapper.Map<PagedListResultViewModel<MatterViewModel>>(matters);
        }

    }
}
