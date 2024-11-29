using Job_Portal_System.Data;
using Job_Portal_System.Interfaces;
using Job_Portal_System.Models;

namespace Job_Portal_System.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobPortalContext _context;

        public UnitOfWork(JobPortalContext context)
        {
            _context = context;
            JobRepository = new Repository<Job>(context);
            ApplicationRepository=new Repository<Application>(context);
        }
        public IRepository<Job> JobRepository { get; }

        public IRepository<Application> ApplicationRepository { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
