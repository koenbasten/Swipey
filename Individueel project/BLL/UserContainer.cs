using BLL.Classes;
using BLL.Validators;
using Interfaces;
using Interfaces.DTOS;


namespace BLL
{
    public class UserContainer
    {
        private readonly PasswordContainer _passwordContainer;
        private readonly IUserDal _userDal;
        public UserContainer(IUserDal userdal)
        {
            _userDal = userdal;
            _passwordContainer = new();
        }
        public List<User> GetAllUsers()
        {
            List<UserDTO> list = new List<UserDTO>();
            List<User> list2 = new List<User>();
            list = _userDal.GetAllUsers();
            foreach (UserDTO userDTO in list)
            {
                list2.Add(ConvDTOUserToClass(userDTO));
            }
            return list2;
        }

        public User Login(string email, string password)
        {
            User user = new();
            UserDTO userDTO = _userDal.GetUserByEmail(email);
            if(userDTO.UserId != null)
            {
                if(_passwordContainer.Compare(password, userDTO.Password))
                {
                    user = ConvDTOUserToClass(userDTO);
                }
            }
            return user;
        }

        public string? CreateUser(User user)
        {
            var validator = new CreateUserValidator(_userDal);
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                return validationResult.ToString("\n");
            }
            else 
            {
                user.Password = _passwordContainer.HashPassword(user.Password);
                UserDTO userDTO = ConvUserClassToDTO(user);
                _userDal.CreateUser(userDTO);
                return null;
            } 
        }


        public User GetUserByEmail(string? email)
        {
            User user = new();
            if (email != null) {
                UserDTO userDTO = _userDal.GetUserByEmail(email);
                user = ConvDTOUserToClass(userDTO);
            }
            return user;
        }

        public User GetUserById(int id)
        {
            User user = new();
                UserDTO userDto = _userDal.GetUserById(id);
                user = ConvDTOUserToClass(userDto);
            return user;
        }

        public int UpdateUser(User newUser)
        {
            
            if (newUser.Biograpy.Length is < 10 or > 1000)
            {
                return 416;
            }
            else if (newUser.UserName.Length is < 4)
            {
                return 417;
            }
            else if (newUser.UserName.Length is > 25)
            {
                return 418;
            }
            else if (newUser.UserName.Any(ch => !char.IsLetterOrDigit(ch) && ch != '.'))
            {
                return 419;
            }
            else
            {
                bool newUsername = true;
                List<User> users = GetAllUsers();
                foreach(User user in users)
                {
                    if (user.UserId != newUser.UserId)
                    {
                        if (user.UserName == newUser.UserName)
                        {
                            newUsername = false;
                            return 400;
                        }
                    }
                }
                UserDTO userDTO = ConvUserClassToDTO(newUser);
                _userDal.UpdateUser(userDTO);
            }

            return 200;
        }

     
        private User ConvDTOUserToClass(UserDTO userDTO)
        {
            User user = new User();
            user.UserId = userDTO.UserId;
            user.UserName = userDTO.UserName;
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.Biograpy = userDTO.Biograpy;
            user.Gender = userDTO.Gender;
            user.PrefGender = userDTO.PrefGender;
            user.DateOfBirth = userDTO.DateOfBirth;
            user.Password = userDTO.Password;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.Activated = userDTO.Activated;
            return user;

        }

        private UserDTO ConvUserClassToDTO(User user)
        {
            UserDTO userdto = new();
            userdto.UserId = user.UserId;
            userdto.UserName = user.UserName;
            userdto.FirstName = user.FirstName;
            userdto.LastName = user.LastName;
            userdto.Email = user.Email;
            userdto.Biograpy = user.Biograpy;
            userdto.Gender = user.Gender;
            userdto.PrefGender = user.PrefGender;
            userdto.DateOfBirth = user.DateOfBirth;
            userdto.Password = user.Password;
            userdto.PhoneNumber = user.PhoneNumber;
            userdto.Activated = user.Activated;
            return userdto;
        }

    }
}
