using Interfaces.DTOS;

namespace Interfaces
{
    public interface IUserDal
    {
        public List<UserDTO> GetAllUsers();
        public UserDTO GetUserById(int id);
        public UserDTO GetUserByEmail(string email);
        public int CreateUser(UserDTO userdto);
        public int UpdateUser(UserDTO userDTO);
        public bool IsValueUnique(string Table, string Value, int? UserId);
    }
}
