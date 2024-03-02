using EtudeManyToMany.API.Data;
using EtudeManyToMany.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EtudeManyToMany.API.Repository
{
    public class ConducteurRepository : IRepository<Conducteur>
    {
        private ApplicationDbContext _dbContext { get; }
        public ConducteurRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(Conducteur conducteur)
        {
            var addedObj = await _dbContext.Conducteurs.AddAsync(conducteur);
            await _dbContext.SaveChangesAsync();
            return addedObj.Entity.ConducteurId;
        }

        public async Task<bool> Delete(int id)
        {
            var conducteur = await GetById(id);
            if (conducteur == null)
                return false;
            _dbContext.Conducteurs.Remove(conducteur);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Conducteur?> Get(Expression<Func<Conducteur, bool>> predicate)
        {
            return await _dbContext.Conducteurs.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Conducteur>> GetAll()
        {
            return await _dbContext.Conducteurs.ToListAsync();
        }

        public async Task<List<Conducteur>> GetAll(Expression<Func<Conducteur, bool>> predicate)
        {
            return await _dbContext.Conducteurs.Where(predicate).ToListAsync();
        }

        public async Task<Conducteur?> GetById(int id)
        {
            return await _dbContext.Conducteurs.FindAsync(id);
        }

        public async Task<bool> Update(Conducteur conducteur)
        {
            var conducteurFromDb = await GetById(conducteur.ConducteurId);

            if (conducteurFromDb == null)
                return false;

            if (conducteurFromDb.UtilisateurId != conducteur.UtilisateurId)
                conducteurFromDb.UtilisateurId = conducteur.UtilisateurId;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
