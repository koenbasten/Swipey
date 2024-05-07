using Microsoft.AspNetCore.Mvc;
using BLL;
using Swipey.Models;
using BLL.Classes;

namespace Swipey.Controllers
{
    public class UserController : Controller
    {
        readonly private UserContainer _userContainer;
        readonly private PhotoContainer _photoContainer;
        static User currentUser;


        public UserController(UserContainer userContainer, PhotoContainer photoContainer)
        {
            _userContainer =userContainer;
            _photoContainer =photoContainer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUserByUserName(string UserName, string? errorMessage)
        {
            currentUser = _userContainer.GetUserByEmail(UserName);
            EditUserViewModel _editUserViewModel = new();
           

            if (currentUser.UserId != null)
            {
                List<Photo> photos = _photoContainer.GetPhotosByUserId((int)currentUser.UserId);
               
                   _editUserViewModel.Photos =  photos.Select(photo => new PhotoViewModel
                    {
                        PhotoId = (int)photo.PhotoId,
                        PhotoData = photo.PhotoData
                    }).ToList();
                    
                HttpContext.Session.SetString("UserName", currentUser.UserName);
                HttpContext.Session.SetString("UserId", Convert.ToString(currentUser.UserId));
            }
            _editUserViewModel.Biograpy = currentUser.Biograpy;
            _editUserViewModel.UserName = currentUser.UserName;
            _editUserViewModel.FirstName = currentUser.FirstName;
            _editUserViewModel.LastName = currentUser.LastName;
            _editUserViewModel.UserErrorMessage = errorMessage;
          

            return View("EditUser", _editUserViewModel);
        }


        [HttpGet]
        public IActionResult EditUser()
        {
            EditUserViewModel _editUserViewModel = new EditUserViewModel();

            return View("EditUser", _editUserViewModel);
        }


        [HttpGet]
        public void DeletePhoto(int ID)
        {
            _photoContainer.DeletePhoto(ID, (int)currentUser.UserId);
            
        }


        [HttpPost]
        public IActionResult UpdateUser(EditUserViewModel model)
        {

            User newUser = new User()
            {
                UserId = currentUser.UserId,
                UserName = model.UserName,
                Biograpy = model.Biograpy,

            };
            int HttpCode = _userContainer.UpdateUser(newUser);
            switch (HttpCode)
            {
                case 200:
                    return GetUserByUserName(model.UserName, null);

                case 416:
                    return GetUserByUserName(currentUser.UserName, "Biograpy didn't match the required length!");

                case 417:
                    return GetUserByUserName(currentUser.UserName, "Username is too short!");

                case 418:
                    return GetUserByUserName(currentUser.UserName, "Username is too long! Please pick one with less than 25 characters");

                case 419:
                    return GetUserByUserName(currentUser.UserName, "Username can't contain special characters!");

                case 400:
                    return GetUserByUserName(currentUser.UserName, "Username already existed! Please pick a new one!");

                default:
                    return null;
            }
        }

        [HttpPost]
        public IActionResult AddPhoto(EditUserViewModel model)
        {
            string? errormessage = null;
            EditUserViewModel _newModel = new();
            if(model == null)
            {
                errormessage = "Please upload a supported format!";
            }
            else
            {
                _newModel.UserName = currentUser.UserName;
                if (model.Photo != null)
                {
                    string photoData = GetBase64(model.Photo).Result;
                    Photo photo = new Photo()
                    {
                        UserId = (int)currentUser.UserId,
                        PhotoData = photoData
                    };
                    int returnCode = _photoContainer.AddPhotoToUser(photo);
                    if (returnCode == 415) errormessage = "Please upload a supported format!";
                }
                else { errormessage = "Please upload a photo!"; }
            }
            return GetUserByUserName(currentUser.UserName, errormessage);
        }



        public async static Task<string> GetBase64(IFormFile file)
        {
           
                string filetype = file.ContentType;
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }
                return "data:" + filetype
                + ";base64," + Convert.ToBase64String(fileBytes);
          
         
        }
        

    }

  
}
