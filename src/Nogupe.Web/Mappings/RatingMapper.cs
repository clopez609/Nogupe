using AutoMapper;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Services.Ratings.DTOs;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Mappings
{
    public static class RatingMapper
    {
        private static readonly IMapper Mapper;

        static RatingMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RatingListViewModel, Rating>();

                cfg.CreateMap<RatingListDTO, RatingListViewModel>();

                cfg.CreateMap(typeof(PagedListResult<RatingListDTO>), typeof(PagedListResultViewModel<RatingListViewModel>));
            });

            Mapper = config.CreateMapper();
        }

        public static Rating ToEntityModel(this RatingListViewModel ratingListViewModel, Rating rating)
        {
            return Mapper.Map(ratingListViewModel, rating);
        }


        public static PagedListResultViewModel<RatingListViewModel> ToViewModel(
            this PagedListResult<RatingListDTO> ratings)
        {
            return Mapper.Map<PagedListResultViewModel<RatingListViewModel>>(ratings);
        }
    }
}
