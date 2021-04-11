using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nogupe.Web.Common;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Mappings;
using Nogupe.Web.Models;
using Nogupe.Web.Services.Email;
using Nogupe.Web.Services.RoleTypes;
using Nogupe.Web.Services.Users;
using Nogupe.Web.ViewModels;
using Nogupe.Web.ViewModels.Auth;

namespace Nogupe.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleTypeService _roleTypeService;
        private readonly IEmailSenderService _emailSenderService;
        public AccountController(IUserService userService, IRoleTypeService roleTypeService, IEmailSenderService emailSenderService)
        {
            _userService = userService;
            _roleTypeService = roleTypeService;
            _emailSenderService = emailSenderService;
        }

        [HttpGet]
        public IActionResult Index(PagedListResultViewModel<UserListViewModel> parameters, string search)
        {
            Services.Users.DTOs.UserFilter filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.Users.DTOs.UserFilter>(search);
            }
            var pagination = new PaginationOptions();
            if (parameters.Page > 0)
            {
                pagination.Page = parameters.Page;
                pagination.PageSize = 10;
            }
            else
            {
                pagination.Page = 1;
                pagination.PageSize = 10;
            }

            var result = _userService.GetListDTOPaged(pagination.Page, pagination.PageSize, null, filter).ToViewModel();

            return View(result);
        }

        [HttpGet]
        public IActionResult Login()
        {
            // si el usuario accede directamente a Login y ya está logueado 
            // lo redirecciono a Home
            if (HttpContext.Session.GetString("_User") != null && HttpContext.Session.GetInt32("_Id") > 0)
            {
                return Redirect("/Home/Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.ValidateUser(model.UserName, model.Password);
                if (result.Result)
                {
                    var user = result.User;

                    HttpContext.Session.SetInt32("_Id", user.Id);
                    HttpContext.Session.SetString("_User", $"{user.FirstName} {user.LastName}"); 
                    HttpContext.Session.SetInt32("_Role", user.RoleId);

                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorCode);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/");
        }

        [HttpGet]
        public IActionResult PreInscription()
        {
            var viewModel = new PreRegisterViewModel();
            viewModel.Roles = new SelectList(_roleTypeService.GetAll(), "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PreInscription(PreRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.ValidateRegister(model.UserName, model.RoleId);
                if (result.Result)
                {
                    var user = new User()
                    {
                        UserName = model.UserName,
                        RoleId = model.RoleId
                    };
                    _userService.Create(user);

                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorCode);
                    model.Roles = new SelectList(_roleTypeService.GetAll(), "Id", "Name");
                    return View(nameof(PreInscription), model);
                }
            }
            model.Roles = new SelectList(_roleTypeService.GetAll(), "Id", "Name");
            return View(nameof(PreInscription), model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.ValidationUserName(model.UserName);
                if (result.Result)
                {
                    var user = new User()
                    {
                        UserName = result.User.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Phone = model.Phone,
                        CellPhone = model.CellPhone,
                        Address = model.Address,
                        AdressNumber = model.AdressNumber,
                        Password = model.Password
                    };
                    _userService.UpdateUser(user);

                    var userResult = result.User;

                    HttpContext.Session.SetInt32("_Id", userResult.Id);
                    HttpContext.Session.SetString("_User", $"{userResult.FirstName} {userResult.LastName}");
                    HttpContext.Session.SetInt32("_Role", userResult.RoleId);

                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorCode);
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var currentUserId = HttpContext.Session.GetInt32("_Id").Value;
            var user = _userService.GetById(currentUserId);

            if (user == null) return BadRequest();

            var profile = new ProfileViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                CellPhone = user.CellPhone,
                Address = user.Address,
                AdressNumber = user.AdressNumber
            };
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(int id, ProfileViewModel model)
        {
            var user = _userService.GetById(id);

            if (user == null) BadRequest();

            if (ModelState.IsValid)
            {
                model.ToEntityModel(user);
                _userService.Update(user);
                return Redirect("/Home/Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult StartRecovery()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StartRecovery(RecoveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var token = _userService.GenerateTokenRecovery(model.Email);

            if (!string.IsNullOrEmpty(token))
            {
                var email = new MailRequest()
                {
                    Email = model.Email,
                    Subject = "Recuperar Contraseña",
                    Body = "<p>Correo para recuperación de contraseña</p><br>" + "<a href='" + "https://nogupeweb.azurewebsites.net/Account/Recovery/?token=" + token + "'>Click para recuperar</a>"
                };

                _emailSenderService.SendEmailAsync(email);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Recovery(string token)
        {
            var model = new RecoveryPasswordViewModel();
            if (_userService.ValidateToken(token))
            {
                model.Token = token;
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Tu Token ha Expirado");
            return View(nameof(Login));
        }

        [HttpPost]
        public IActionResult Recovery(RecoveryPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _userService.ChangePassword(model.Token, model.Password);

            return View(nameof(Login));
        }
    }
}