using AutoMapper;
using Nogupe.Web.Services.Walls.DTOs;
using Nogupe.Web.ViewModels.Wall;

namespace Nogupe.Web.Mappings
{
    public static class CommentMapper
    {
        private static readonly IMapper Mapper;

        static CommentMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommentDetailViewModel, CommentDetailDTO>();

                cfg.CreateMap<CommentDetailDTO, CommentDetailViewModel>();
            });

            Mapper = config.CreateMapper();
        }

        public static CommentDetailDTO ToDTO(this CommentDetailViewModel commentViewModel)
        {
            var commentDTO = Mapper.Map<CommentDetailDTO>(commentViewModel);
            return commentDTO;
        }
    }
}
