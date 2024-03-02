using EtudeManyToMany.API.Data;
using EtudeManyToMany.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EtudeManyToMany.API.Repository
{
    public class PassagerRepository : IRepository<Passager>
    {
        private ApplicationDbContext _dbContext { get; }
        public PassagerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(Passager passager)
        {
            var addedObj = await _dbContext.Passagers.AddAsync(passager);
            await _dbContext.SaveChangesAsync();
            return addedObj.Entity.PassagerId;
        }

        public async Task<bool> Delete(int id)
        {
            var passager = await GetById(id);
            if (passager == null)
                return false;
            _dbContext.Passagers.Remove(passager);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Passager?> Get(Expression<Func<Passager, bool>> predicate)
        {
            return await _dbContext.Passagers.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Passager>> GetAll()
        {
            return await _dbContext.Passagers.ToListAsync();
        }

        public async Task<List<Passager>> GetAll(Expression<Func<Passager, bool>> predicate)
        {
            return await _dbContext.Passagers.Where(predicate).ToListAsync();
        }

        public async Task<Passager?> GetById(int id)
        {
            return await _dbContext.Passagers.FindAsync(id);
        }

        public async Task<bool> Update(Passager passager)
        {
            var passagerFromDb = await GetById(passager.PassagerId);

            if (passagerFromDb == null)
                return false;

            if (passagerFromDb.UtilisateurId != passager.UtilisateurId)
                passagerFromDb.UtilisateurId = passager.UtilisateurId;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
