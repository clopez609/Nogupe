using AutoMapper;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Services.Walls.DTOs;
using Nogupe.Web.ViewModels.Wall;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Mappings
{
    public static class WallMapper
    {
        private static readonly IMapper Mapper;

        static WallMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Wall, WallViewModel>();

                cfg.CreateMap<WallViewModel, Wall>();

                cfg.CreateMap<WallDTO, WallViewModel>()
                    .ForMember(dest => dest.Documents, opt => opt.Ignore());

                cfg.CreateMap<WallViewModel, WallDTO>();

                cfg.CreateMap<Comment, CommentViewModel>();

                cfg.CreateMap<CommentViewModel, Comment>();

                cfg.CreateMap<CommentDTO, CommentViewModel>();

                cfg.CreateMap<CommentViewModel, CommentDTO>();
            });

            Mapper = config.CreateMapper();
        }

        public static WallViewModel ToViewModel(this Wall wall)
        {
            var viewModel = Mapper.Map<WallViewModel>(wall);
            return viewModel;
        }

        public static WallViewModel ToViewModel(this WallDTO wallDTO)
        {
            var viewModel = Mapper.Map<WallViewModel>(wallDTO);

            if(viewModel.Documents != null)
            {
                foreach( var wallFile in wallDTO.Documents)
                viewModel.Documents.Add(new WallFileViewModel
                {
                    Id = wallFile.Id,
                    FileId = wallFile.FileId,
                    WallId = wallFile.WallId,
                    FileName = wallFile.File.Name,
                    UIdFileName = wallFile.File.UIdFileName,
                    FileUrl = GetFilePath(wallFile.File.UIdFileName)
                });
            }

            return viewModel;
        }

        private static string GetFilePath(string fileId)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileId);

            return uploadPath;
        }

        public static Wall ToEntityModel(this WallViewModel wallViewModel, Wall wall)
        {
            Mapper.Map(wallViewModel, wall);

            return wall;
        }

        public static WallDTO ToDTO(this WallViewModel wallViewModel)
        {
            var wallDTO = Mapper.Map<WallDTO>(wallViewModel);
            return wallDTO;
        }
    }
}
