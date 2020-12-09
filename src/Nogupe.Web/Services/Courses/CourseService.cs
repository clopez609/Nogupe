using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.PredicateExtentions;
using Nogupe.Web.Helpers.QueryableExtentions;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Courses.DTOs;
using Nogupe.Web.Services.Files.DTOs;
using Nogupe.Web.Services.Ratings.DTOs;
using Nogupe.Web.Services.Walls.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nogupe.Web.Services.Courses
{
    public class CourseService : Repository<Course>, ICourseService
    {
        private readonly DataContext _context;

        public static IMapper _mapper;

        public CourseService(DataContext context) : base(context)
        {
            _context = context; 

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>();

                cfg.CreateMap<Comment, CommentDTO>()
                    .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.User.FirstName));

                cfg.CreateMap<File, FileDTO>();

                cfg.CreateMap<Inscription, InscriptionDTO>()
                    .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.FirstName));

                cfg.CreateMap<Rating, RatingDTO>()
                    .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.FirstName));
            });

            _mapper = config.CreateMapper();
        }

        public CourseDTO GetByIdDTO(int id)
        {
            var course = _context.Set<Course>()
                   .Include(x => x.Assistances)
                   .Include(x => x.Inscriptions)
                   .Include(x => x.Ratings).ThenInclude(e => e.User)
                   .Include(x => x.Files)
                   .Include(x => x.Comments).ThenInclude(e => e.User)
                   .Include(x => x.Tokens)
                   .FirstOrDefault(x => x.Id == id);

            var courseDTO = _mapper.Map<CourseDTO>(course);

            return courseDTO;
        }

        public PagedListResult<CourseListDTO> GetListDTOPaged(
            int page,
            int pageSize,
            string search = null,
            IFilter customFilter = null)
        {
            var includeProperties = "Career,Matter,Weekday,User";
            Expression<Func<Course, bool>> allFilters = null;
            if (search != null) allFilters = GetSearchFilter(search);
            if (customFilter != null)
                allFilters = allFilters == null
                    ? GetCustomFilter(customFilter)
                    : allFilters.AndAlso(GetCustomFilter(customFilter));
            //if (filter != null) allFilters = allFilters == null ? filter : allFilters.AndAlso(filter);

            var courseQueryable = GetQueryable(allFilters, includeProperties);

            var query = from p in courseQueryable
                        select new CourseListDTO
                        {
                            Id = p.Id,
                            CommissionNumber = p.CommissionNumber,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            CareerName = p.Career.Name,
                            MatterName = p.Matter.Name,
                            WeekdayName = p.Weekday.Name,
                            UserName = p.User.FirstName,
                        };

            return query.GetPaged(page, pageSize);
        }

        public virtual IQueryable<Course> GetQueryable(
            Expression<Func<Course, bool>> filter = null,
            //Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<Course> query = _context.Set<Course>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public Expression<Func<Course, bool>> GetSearchFilter(string search)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Course, bool>> GetCustomFilter(IFilter customFilter)
        {
            var filter = (CourseFilter)customFilter;

            Expression<Func<Course, bool>> result = execution => true;

            if (filter.CareerId.HasValue) result = result.AndAlso(x => x.CareerId == filter.CareerId);

            if (filter.MatterId.HasValue) result = result.AndAlso(x => x.MatterId == filter.MatterId);

            return result;
        }

    }
}
