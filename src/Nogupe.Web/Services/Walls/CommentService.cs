using AutoMapper;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Services.Walls.DTOs;
using Nogupe.Web.ViewModels.Auth;
using Nogupe.Web.ViewModels.Wall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Walls
{
    public class CommentService : Repository<Comment>, ICommentService
    {
        private readonly DataContext _context;

        public static IMapper _mapper;

        public CommentService(DataContext context) : base(context)
        {
            _context = context;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentDetailDTO>();

                cfg.CreateMap<CommentDetailDTO, Comment>();
            });

            _mapper = config.CreateMapper();
        }

        public Comment Create(CommentDetailDTO commentDetailDTO, UserViewModel userViewModel)
        {
            var comment = _mapper.Map<Comment>(commentDetailDTO);
            comment.Date = DateTime.Now;
            comment.UserId = userViewModel.Id;
            comment.UserName = $"{userViewModel.Firstname} {userViewModel.Lastname}";

            Create(comment);

            return comment;
        }
    }
}
