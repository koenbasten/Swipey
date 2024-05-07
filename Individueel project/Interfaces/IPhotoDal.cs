using Interfaces.DTOS;

namespace Interfaces
{
    public interface IPhotoDal
    {
        public PhotoDTO GetPhotoByPhotoId(int photoId);
        public List<PhotoDTO> GetPhotosByUserId(int userId);
        public int AddPhotoByUserId(PhotoDTO photoDTO);
        public int DeletePhoto(int photoId, int userId);
    }
}
