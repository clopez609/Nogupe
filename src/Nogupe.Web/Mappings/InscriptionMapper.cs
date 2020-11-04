using AutoMapper;
using Nogupe.Web.Common;
using Nogupe.Web.Services.Courses.DTOs;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Inscription;

namespace Nogupe.Web.Mappings
{
    public static class InscriptionMapper
    {
        private static readonly IMapper Mapper;

        static InscriptionMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InscriptionListDTO, InscriptionListViewModel>();

                cfg.CreateMap(typeof(PagedListResult<InscriptionListDTO>), typeof(PagedListResultViewModel<InscriptionListViewModel>));
            });

            Mapper = config.CreateMapper();
        }

        public static PagedListResultViewModel<InscriptionListViewModel> ToViewModel(
            this PagedListResult<InscriptionListDTO> inscriptions)
        {
            return Mapper.Map<PagedListResultViewModel<InscriptionListViewModel>>(inscriptions);
        }
    }
}
