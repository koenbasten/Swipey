using Interfaces.DTOS;

namespace Interfaces
{
    public interface IAttemptedLoginDal
    {//All toevoegen bij UML ook Id als para, succesSful"L", verander AttLog naar DTO
        public List<AttemptedLoginDTO> GetAllAttLogByUserId(int userId);
        public List<AttemptedLoginDTO> GetLastUnsuccessfullAttLogs(int userId);
        public int CreateAttLog(AttemptedLoginDTO attemptedLoginDto);


    }
}
