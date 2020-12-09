using AutoMapper;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.ViewModels.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Mappings
{
    public static class TokenMapper
    {
        private static readonly IMapper Mapper;

        static TokenMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TokenViewModel, Token>();

            });

            Mapper = config.CreateMapper();
        }

        public static Token ToEntityModel(this TokenViewModel tokenViewModel, Token token)
        {
            var entity = Mapper.Map(tokenViewModel, token);
            var timeAdd = Convert.ToInt32(tokenViewModel.Duration);
            entity.CreatedDate = DateTime.Now.AddSeconds(timeAdd);

            return entity;
        }
    }
}
