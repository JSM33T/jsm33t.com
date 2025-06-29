using JassWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace JassWebApi.Data
{

    public interface IChangeLogRepository
    {
        IEnumerable<ChangeLog> GetAll();
        ChangeLog GetByVersion(string version);
        ChangeLog Insert(ChangeLog changeLog);
        bool DeleteByVersion(string version);
    }

     public class ChangeLogRepository : IChangeLogRepository
    {
        private readonly AppDbContext _context;

        public ChangeLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ChangeLog> GetAll()
        {
            return _context.ChangeLogs.ToList();
        }

        public ChangeLog GetByVersion(string version)
        {
            return _context.ChangeLogs.FirstOrDefault(c => c.Version == version);
        }

        public ChangeLog Insert(ChangeLog changeLog)
        {
            _context.ChangeLogs.Add(changeLog);
            _context.SaveChanges();
            return changeLog;
        }

        public bool DeleteByVersion(string version)
        {
            var entity = _context.ChangeLogs.FirstOrDefault(c => c.Version == version);
            if (entity == null)
                return false;

            _context.ChangeLogs.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }

}
