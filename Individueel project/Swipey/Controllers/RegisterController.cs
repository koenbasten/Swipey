using BLL;
using BLL.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swipey.Models;

namespace Swipey.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        UserContainer _userContainer;
        LoginController _loginController;
        public RegisterController(UserContainer userContainer)
        {
            _loginController = new LoginController(userContainer);
            _userContainer = userContainer;
        }
        [Authorize]
        public IActionResult Index(RegisterViewModel viewModel)
        {
            return View("Index", viewModel);
        }

        public IActionResult Register(RegisterViewModel viewModel, string phone, string dateofbirth)
        {
            string format = "yyyy-MM-dd";
            DateTime parsedDateTime;


            // Parse the string to a DateTime object
            if (DateTime.TryParseExact(dateofbirth, format, null, System.Globalization.DateTimeStyles.None, out parsedDateTime))
            {
                
                parsedDateTime.ToString();
                if (viewModel.Password != viewModel.DupPassword)
                {
                    viewModel.Message = "Password's don't match";
                }
                User user = new User()
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    UserName = viewModel.UserName,
                    PhoneNumber = phone,
                    DateOfBirth = parsedDateTime,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Gender = viewModel.Gender

                };
                viewModel.Message = _userContainer.CreateUser(user);
            }
            else
            {
                viewModel.Message = "Datum is incorrect";
            }
            if (viewModel.Message == null)
            {
                LoginViewModel loginViewModel = new();
                loginViewModel.NewUser = true;
                loginViewModel.Email = viewModel.Email;
                return RedirectToAction("Index", "Login", loginViewModel);
            }
            else
            {
                return Index(viewModel);
            }
        }
    }
}
