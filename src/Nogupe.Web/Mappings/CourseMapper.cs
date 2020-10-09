using AutoMapper;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Course;

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

                cfg.CreateMap(typeof(PagedResult<Course>), typeof(PagedListResultViewModel<CourseViewModel>));

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

        public static PagedListResultViewModel<CourseViewModel> ToViewModel(
            this PagedResult<Course> courses)
        {
            return Mapper.Map<PagedListResultViewModel<CourseViewModel>>(courses);
        }
    }
}
