using BLL;
using BLL.Classes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swipey.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace Swipey.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        UserContainer _userContainer;
        public LoginController(UserContainer userContainer)
        {
            _userContainer = userContainer;
        }
        public IActionResult Index(LoginViewModel viewModel)
        {
            Console.WriteLine(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier));
            Console.WriteLine(HttpContext.User.Identity.IsAuthenticated);
            return View("Index", viewModel);
        }
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (viewModel.Email == null || viewModel.Password == null)
            {
                viewModel.Message = "Please enter your credentials";
            }
            else
            {
                User user = _userContainer.Login(viewModel.Email, viewModel.Password);
                if (user.UserId != null)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    Thread.CurrentPrincipal = principal;
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);




                    viewModel.Message = "Welcome " + principal.FindFirstValue(ClaimTypes.Name);
                }
                else
                {
                    viewModel.Message = "Credentials didn't add up!";
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
