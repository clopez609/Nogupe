using AutoMapper;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.ViewModels.Wall;
using System;

namespace Nogupe.Web.Mappings
{
    public static class CommentMapper
    {
        private static readonly IMapper Mapper;

        static CommentMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommentDetailViewModel, Comment>();
            });

            Mapper = config.CreateMapper();
        }

        public static Comment ToEntityModel(this CommentDetailViewModel commentDetailViewModel, Comment comment, int userId)
        {
            var entity = Mapper.Map(commentDetailViewModel, comment);
            entity.CreatedDate = DateTime.Now;
            entity.UserId = userId;

            return comment;
        }
    }
}
