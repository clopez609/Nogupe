using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Services.Walls.DTOs;
using System;
using System.Linq;

namespace Nogupe.Web.Services.Walls
{
    public class WallService : Repository<Wall>, IWallService
    {
        private readonly DataContext _context;

        public static IMapper _mapper;

        public WallService(DataContext context) : base(context)
        {
            _context = context;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Wall, WallDTO>();

                cfg.CreateMap<WallDTO, Wall>();

                cfg.CreateMap<Comment, CommentDTO>()
                    .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.User.FirstName));

                cfg.CreateMap<WallFile, WallFileDTO>();

            });

            _mapper = config.CreateMapper();
        }

        public WallDTO GetbyIdDTO(int id)
        {
            var wall = _context.Set<Wall>()
                .Include(x => x.Documents).ThenInclude(f => f.File)
                .Include(x => x.Comments).ThenInclude(x => x.User)
                .SingleOrDefault(x => x.CourseId == id);

            var wallDTO = _mapper.Map<WallDTO>(wall);

            return wallDTO;
        }

        public void CreateWall(Course course)
        {
            var walls = _context.Set<Wall>().AsNoTracking();
            if (walls.Any(x => x.Course.Equals(course)))
                throw new Exception("el muro ya tiene a una clase asociada");

            var wall = new Wall
            {
                CourseId = course.Id,
                Course = course,
            };
            Create(wall);
        }

        public void DeleteWall(Course course)
        {
            var wall = _context.Set<Wall>().Where(x => x.CourseId == course.Id).SingleOrDefault();
            if (wall == null) throw new Exception("no se encontro muro");

            Delete(wall);
        }
    }
}
