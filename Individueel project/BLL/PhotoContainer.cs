using System.Drawing;
using System.IO;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using BLL.Classes;
using Interfaces;
using Interfaces.DTOS;

namespace BLL
{
    public class PhotoContainer
    {
        readonly private IPhotoDal _photoDal;
        public PhotoContainer(IPhotoDal photodal)
        {
            _photoDal = photodal;
        }
        
        public int AddPhotoToUser(Photo photo)
        {
           
            char delimiter = ',';
            int index = photo.PhotoData.IndexOf(delimiter);
            string modifiedString = photo.PhotoData.Substring(index + 1);
            string base64String = photo.PhotoData.Substring(index + 1);
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);

                using (Image image = Image.Load(imageBytes))
                {
                    PhotoDTO photoDTO =ConvClassPhotoToDTO(photo);
                    _photoDal.AddPhotoByUserId(photoDTO);
                    return 200; ;
                }
            }
            catch (Exception ex)
            {
                return 415;
            }
        }
    

        public Photo GetPhotoByPhotoId(int photoId)
        {
            PhotoDTO photoDTO = _photoDal.GetPhotoByPhotoId(photoId);
            return ConvDTOUserToClass(photoDTO);
        }

        public int DeletePhoto(int photoId, int userId)
        {
            return _photoDal.DeletePhoto(photoId, userId);
        }

        public List<Photo> GetPhotosByUserId(int userId)
        {
            List<PhotoDTO> photosDTO = _photoDal.GetPhotosByUserId(userId);
            List<Photo> photos = new();
            if(photosDTO.Count == 0)
            {
                return photos; //empty list bc no photos
            }
            else
            {
                foreach(PhotoDTO photo in photosDTO)
                {
                    photos.Add(ConvDTOUserToClass(photo));
                }
                return photos;
            }
        }

        private Photo ConvDTOUserToClass(PhotoDTO photoDTO)
        {
            Photo newPhoto = new();
            newPhoto.PhotoId = photoDTO.PhotoId;
            newPhoto.UserId = photoDTO.UserId;
            newPhoto.PhotoData = photoDTO.PhotoData;
            return newPhoto;
        }
        private PhotoDTO ConvClassPhotoToDTO(Photo photo)
        {
            PhotoDTO newPhoto = new();
            newPhoto.UserId = photo.UserId;
            newPhoto.PhotoData = photo.PhotoData;
            return newPhoto;
        }
    }
}
