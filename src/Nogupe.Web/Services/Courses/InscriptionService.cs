using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Enums;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.PredicateExtentions;
using Nogupe.Web.Helpers.QueryableExtentions;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Courses.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nogupe.Web.Services.Courses
{
    public class InscriptionService : Repository<Inscription>, IInscriptionService
    {
        private readonly DataContext _context;

        public InscriptionService(DataContext context) : base(context)
        {
            _context = context;
        }

        public bool ValidateSubscribe(int courseId, int? userId)
        {
            var incription = _context.Set<Inscription>().SingleOrDefault(x => x.CourseId == courseId && x.UserId == userId);

            if (incription == null)
            {
                return true;
            }

            return false;
        }

        public void CreateSubcribe(Course course, int? userId)
        {
            var curso = _context.Set<Course>().Where(x => x.Id == course.Id).FirstOrDefault();
            var usuario = _context.Set<User>().Where(x => x.Id == userId).FirstOrDefault();

            var inscription = new Inscription()
            {
                CourseId = curso.Id,
                UserId = usuario.Id,
                Status = EnrollmentStatus.Pending,
            };

            Create(inscription);
        }

        public PagedListResult<InscriptionListDTO> GetListDTOPaged(
            int page,
            int pageSize,
            string search = null,
            IFilter customFilter = null)
        {
            var includeProperties = "Course,User";
            Expression<Func<Inscription, bool>> allFilters = null;
            if (search != null) allFilters = GetSearchFilter(search);
            if (customFilter != null)
                allFilters = allFilters == null
                    ? GetCustomFilter(customFilter)
                    : allFilters.AndAlso(GetCustomFilter(customFilter));

            var inscriptionQueryable = GetQueryable(allFilters, includeProperties);

            var query = from i in inscriptionQueryable
                        select new InscriptionListDTO
                        {
                            Id = i.CourseId,
                            CommissionNumber = i.Course.CommissionNumber,
                            CareerName = i.Course.Career.Name,
                            MatterName = i.Course.Matter.Name,
                            WeekdayName = i.Course.Weekday.Name,
                            Status = i.Status.ToString()
                        };

            return query.GetPaged(page, pageSize);
        }

        public PagedListResult<InscriptionUserListDTO> GetListUser(
            int page,
            int pageSize,
            string search = null,
            IFilter customFilter = null)
        {
            var includeProperties = "Course,User";
            Expression<Func<Inscription, bool>> allFilters = null;
            if (search != null) allFilters = GetSearchFilter(search);
            if (customFilter != null)
                allFilters = allFilters == null
                    ? GetCustomFilter(customFilter)
                    : allFilters.AndAlso(GetCustomFilter(customFilter));

            var inscriptionQueryable = GetQueryable(allFilters, includeProperties);

            var query = from i in inscriptionQueryable
                        select new InscriptionUserListDTO
                        {
                            Id = i.Id,
                            Username = $"{i.User.FirstName} {i.User.LastName}",
                            Status = i.Status.ToString()
                        };

            return query.GetPaged(page, pageSize);
        }

        public virtual IQueryable<Inscription> GetQueryable(
           Expression<Func<Inscription, bool>> filter = null,
           //Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
           string includeProperties = null,
           int? skip = null,
           int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<Inscription> query = _context.Set<Inscription>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public Expression<Func<Inscription, bool>> GetSearchFilter(string search)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Inscription, bool>> GetCustomFilter(IFilter customFilter)
        {
            var filter = (InscriptionFilter)customFilter;

            Expression<Func<Inscription, bool>> result = execution => true;

            if (filter.UserId.HasValue) result = result.AndAlso(x => x.UserId == filter.UserId);

            if (filter.CourseId.HasValue) result = result.AndAlso(x => x.CourseId == filter.CourseId);

            return result;
        }

    }
}
