using Microsoft.EntityFrameworkCore;
using MSE.User.Domain;
using MSE.User.Domain.Entities;
using MSE.User.Infrastructure.Data;
using MSE.User.Infrastructure.Repositories.Interfaces;

namespace MSE.User.Infrastructure.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserDbContext _context;
        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public UserAccountRepository(UserDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserAccount> AddAsync(UserAccount entity)
        {
            SetCreatedTime(entity);
            var result = await _context.Set<UserAccount>().AddAsync(entity);
            return result.Entity;
        }

        public async Task DeleteAsync(UserAccount entity)
        {
            _context.Set<UserAccount>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<UserAccount> GetByIdAsync(int id)
        {
            return await _context.Set<UserAccount>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(UserAccount entity)
        {
            SetModifiedTime(entity);
            _context.Set<UserAccount>().Update(entity);
            await Task.CompletedTask;
        }

        private void SetCreatedTime(UserAccount entity)
        {
            entity.CreatedAt = DateTime.Now;
        }

        private void SetModifiedTime(UserAccount entity)
        {
            entity.LastModifiedAt = DateTime.Now;
        }
    }
}
