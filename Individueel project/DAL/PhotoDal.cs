using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.DTOS;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class PhotoDal : DbContext, IPhotoDal
    {
        public PhotoDTO GetPhotoByPhotoId(int photoId)
        {
            PhotoDTO photo = new PhotoDTO();
            SqlParameter sqlParameter = new SqlParameter("@PhotoId", photoId);
            string query = "SELECT PhotoId, PhotoData, UserId FROM Swipey.Photo WHERE PhotoId = @PhotoId";
            DataTable dataTable = GetTable(query, sqlParameter);
            photo = ConvertRowToDTO(dataTable.Rows[0]);
            return photo;
        }

        public List<PhotoDTO> GetPhotosByUserId(int userId)
        {
            List<PhotoDTO> photos = new List<PhotoDTO>();
            SqlParameter sqlParameter = new SqlParameter("@UserId", userId);
            string query = "SELECT PhotoId, PhotoData, UserId FROM Swipey.Photo WHERE UserId = @UserId";
            DataTable dataTable = GetTable(query, sqlParameter);
            if (dataTable.Rows.Count != 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    photos.Add(ConvertRowToDTO(row));
                }
            }

            return photos;
        }
        public int AddPhotoByUserId(PhotoDTO photoDTO)
        {
            SqlParameter[] sqlParameters = [new SqlParameter("@PhotoData", photoDTO.PhotoData), new SqlParameter("@UserId", photoDTO.UserId)];
            string query = "INSERT INTO Swipey.Photo (UserId, PhotoData) VALUES (@UserId, @PhotoData);";
            Console.WriteLine(SendTable(query, sqlParameters));


            return 1;
        }
        public int DeletePhoto(int photoId, int userId)
        {
            SqlParameter[] sqlParameter = [new SqlParameter("@PhotoId", photoId), new SqlParameter("@UserId", userId)];
            string query = "DELETE FROM Swipey.Photo WHERE PhotoId=@PhotoId and UserId=@UserId";
            return SendTable(query, sqlParameter);

        }


        private PhotoDTO ConvertRowToDTO(DataRow row)
        {
            PhotoDTO photoDTO;
            if (row == null)
            {
                photoDTO = new PhotoDTO();
            }
            else
            {
                photoDTO = new PhotoDTO()
                {
                    UserId = (int)row["UserId"],
                    PhotoId = (int)row["PhotoId"],
                    PhotoData = (string)row["PhotoData"],
                };
            }
            return photoDTO;
        }

    }



}
