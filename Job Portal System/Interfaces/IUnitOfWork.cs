using Job_Portal_System.Models;

namespace Job_Portal_System.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IRepository<Job> JobRepository { get;}
        IRepository<Application> ApplicationRepository { get;}

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        Task<int> SaveAsync();

    }
}
