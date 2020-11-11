using AutoMapper;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Services.Users.DTOs;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Auth;

namespace Nogupe.Web.Mappings
{
    public static class AuthMapper
    {
        private static readonly IMapper Mapper;

        static AuthMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();

                cfg.CreateMap<UserListDTO, UserListViewModel>();

                cfg.CreateMap<ProfileViewModel, User>();

                cfg.CreateMap(typeof(PagedListResult<UserListDTO>), typeof(PagedListResultViewModel<UserListViewModel>));
            });

            Mapper = config.CreateMapper();
        }

        public static PagedListResultViewModel<UserListViewModel> ToViewModel(
           this PagedListResult<UserListDTO> users)
        {
            return Mapper.Map<PagedListResultViewModel<UserListViewModel>>(users);
        }

        public static UserViewModel ToViewModel(this User user)
        {
            return Mapper.Map<UserViewModel>(user);
        }

        public static User ToEntityModel (this ProfileViewModel profileViewModel, User user)
        {
            return Mapper.Map(profileViewModel, user);
        }
    }
}
