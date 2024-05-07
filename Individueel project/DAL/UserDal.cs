using System.Data;
using System.Data.Common;
using Interfaces;
using Interfaces.DTOS;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class UserDal : DbContext, IUserDal
    {

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> users = new();
            string query = "SELECT UserId, UserName, FirstName, LastName, Email, Biograpy, Gender, PrefGender, DateOfBirth, Password, Landcode, PhoneNumber, Activated FROM Swipey.[User];";
            DataTable dataTable = GetTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                users.Add(ConvertRowToDTO(row));
            }
            return users;
        }

        public UserDTO GetUserById(int id)
        {
            UserDTO user = new();
            string query = "SELECT UserId, UserName, FirstName, LastName, Email, Biograpy, Gender, PrefGender, DateOfBirth, Password, Landcode, PhoneNumber, Activated FROM Swipey.[User] WHERE UserId = @UserId";
            SqlParameter[] sqlParameters = [new SqlParameter("@UserId", id)];
            DataTable dataTable = GetTable(query, sqlParameters);
            if (dataTable.Rows.Count != 0)
            {
                user = ConvertRowToDTO(dataTable.Rows[0]);
            }
            return user;
        }

        public UserDTO GetUserByEmail(string email)
        {
            
            UserDTO user = new();
            string query = "SELECT UserId, UserName, FirstName, LastName, Email, Biograpy, Gender, PrefGender, DateOfBirth, Password, PhoneNumber, Activated FROM Swipey.[User] WHERE Email = @Email";
            SqlParameter[] sqlParameters = [new SqlParameter("@Email", email)];
            DataTable dataTable = GetTable(query, sqlParameters);
            if (dataTable.Rows.Count != 0)
            {
                user = ConvertRowToDTO(dataTable.Rows[0]);
            }
            return user;
        }

        public int UpdateUser(UserDTO userDto)
        {
            SqlParameter[] sqlParameters = [new SqlParameter("@Biograpy", userDto.Biograpy), new SqlParameter("@UserName", userDto.UserName), new SqlParameter("@Id", userDto.UserId)];
            
            string query = "UPDATE Swipey.[User] SET Biograpy=@Biograpy, UserName=@UserName WHERE UserId=@Id";
            return SendTable(query, sqlParameters);
        }
     
        public int CreateUser(UserDTO userdto)
        {
            SqlParameter[] sqlParameters = [new SqlParameter("@Email", userdto.Email), new SqlParameter("@Password", userdto.Password), new SqlParameter("@UserName", userdto.UserName), new SqlParameter("@PhoneNumber", userdto.PhoneNumber), new SqlParameter("@DateOfBirth", userdto.DateOfBirth), new SqlParameter("@FirstName", userdto.FirstName), new SqlParameter("@LastName", userdto.LastName)];
            string query = "INSERT INTO Swipey.[User] (Email, Password, UserName, PhoneNumber, DateOfBirth, Firstname, LastName, Activated) VALUES (@Email, @Password, @UserName, @PhoneNumber, @DateOfBirth, @Firstname, @LastName, 0 );";
           return SendTable(query, sqlParameters);
        }

        public bool IsValueUnique(string Colomn, string Value, int? UserId)
        {
            string query;
            SqlParameter[] sqlParameters;
            if (UserId == null)
            {
                query = "SELECT count(*) FROM Swipey.[User] WHERE " + Colomn + " = @Value";
                sqlParameters = [new SqlParameter("@Table", Colomn), new SqlParameter("@Value", Value)];
            }
            else
            {
                query = "SELECT count(*) FROM Swipey.[User] WHERE " + Colomn + " = @Value AND NOT UserId = @UserId";
                sqlParameters = [new SqlParameter("@Table", Colomn), new SqlParameter("@Value", Value), new SqlParameter("@UserId", UserId)];
            }


            DataTable dataTable = GetTable(query, sqlParameters);
            return (int)dataTable.Rows[0][0] == 0;
        }

        private UserDTO ConvertRowToDTO(DataRow row)
        {
            UserDTO userDTO = new UserDTO();
            if (row != null)
            {
                userDTO = new UserDTO()
                {
                    UserId = (int)row["UserId"],
                    UserName = (string)row["UserName"],
                    FirstName = (string)row["FirstName"],
                    LastName = (string)row["LastName"],
                    Email = (string)row["Email"],
                    Biograpy = row["Biograpy"] != DBNull.Value ? (string?)row["Biograpy"] : null,
                    Gender = row["Gender"] != DBNull.Value ? (bool)row.Field<bool>("Gender") : null,
                    PrefGender = row["PrefGender"] != DBNull.Value ? (bool?)row.Field<bool>("PrefGender") : null,
                    DateOfBirth = (DateTime)row["DateOfBirth"],
                    Password = (string)row["Password"],
                    PhoneNumber = (string)row["PhoneNumber"],
                    Activated = (bool)row["Activated"]

                    
                };
            }
            return userDTO;
        }


    }
}
