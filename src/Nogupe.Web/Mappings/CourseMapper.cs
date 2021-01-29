using AutoMapper;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Services.Assistances.DTOs;
using Nogupe.Web.Services.Courses.DTOs;
using Nogupe.Web.Services.Ratings.DTOs;
using Nogupe.Web.Services.Walls.DTOs;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Assistance;
using Nogupe.Web.ViewModels.Comment;
using Nogupe.Web.ViewModels.Course;
using Nogupe.Web.ViewModels.File;
using Nogupe.Web.ViewModels.Inscription;
using Nogupe.Web.ViewModels.Rating;
using System.IO;

namespace Nogupe.Web.Mappings
{
    public static class CourseMapper
    {
        private static readonly IMapper Mapper;

        static CourseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();

                cfg.CreateMap<CourseViewModel, Course>();

                cfg.CreateMap<CourseListDTO, CourseListViewModel>();

                cfg.CreateMap<CourseDTO, CourseDetailViewModel>()
                    .ForMember(dest => dest.Files, opt => opt.Ignore());

                cfg.CreateMap<CommentDTO, CommentViewModel>();

                cfg.CreateMap<InscriptionDTO, InscriptionViewModel>();

                cfg.CreateMap<RatingDTO, RatingViewModel>();

                cfg.CreateMap<AssistanceDTO, AssistanceViewModel>();

                cfg.CreateMap(typeof(PagedListResult<CourseListDTO>), typeof(PagedListResultViewModel<CourseListViewModel>));
            });

            Mapper = config.CreateMapper();
        }

        public static Course ToEntityModel(this CourseViewModel courseViewModel, Course course)
        {
            return Mapper.Map(courseViewModel, course);
        }

        public static CourseViewModel ToViewModel(this Course course)
        {
            return Mapper.Map<CourseViewModel>(course);
        }

        public static CourseDetailViewModel ToViewModel(this CourseDTO courseDTO)
        {
            var viewModel = Mapper.Map<CourseDetailViewModel>(courseDTO);

            if (viewModel.Files != null)
            {
                foreach (var file in courseDTO.Files)
                viewModel.Files.Add(new FileViewModel
                {
                    Id = file.Id,
                    FileName = file.Name,
                    UIdFileName = file.UIdFileName,
                    FileUrl = GetFilePath(file.UIdFileName)
                });
            }

            return viewModel;
        }

        public static PagedListResultViewModel<CourseListViewModel> ToViewModel(
            this PagedListResult<CourseListDTO> courses)
        {
            return Mapper.Map<PagedListResultViewModel<CourseListViewModel>>(courses);
        }

        private static string GetFilePath(string UIdFileName)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", UIdFileName);

            return uploadPath;
        }
    }
}
