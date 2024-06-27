using Microsoft.EntityFrameworkCore;
using MSE.User.Domain;
using MSE.User.Infrastructure.Data;

namespace MSE.User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public UserRepository(UserDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Domain.Entities.User> AddAsync(Domain.Entities.User entity)
        {
            SetCreatedTime(entity);
            var result = await _context.Set<Domain.Entities.User>().AddAsync(entity);
            return result.Entity;
        }

        public async Task DeleteAsync(Domain.Entities.User entity)
        {
            _context.Set<Domain.Entities.User>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<Domain.Entities.User> GetByIdAsync(int id)
        {
            return await _context.Set<Domain.Entities.User>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Domain.Entities.User entity)
        {
            SetModifiedTime(entity);
            _context.Set<Domain.Entities.User>().Update(entity);
            await Task.CompletedTask;
        }

        private void SetCreatedTime(Domain.Entities.User entity)
        {
            entity.CreatedAt = DateTime.Now;
        }

        private void SetModifiedTime(Domain.Entities.User entity)
        {
            entity.LastModifiedAt = DateTime.Now;
        }
    }
}
