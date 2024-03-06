using EtudeManyToMany.API.Data;
using EtudeManyToMany.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EtudeManyToMany.API.Repository
{
    public class TrajetRepository : IRepository<Trajet>
    {
        private ApplicationDbContext _dbContext { get; }
        public TrajetRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(Trajet trajet)
        {
            var addedObj = await _dbContext.Trajets.AddAsync(trajet);
            await _dbContext.SaveChangesAsync();
            return addedObj.Entity.TrajetId;
        }

        public async Task<bool> Delete(int id)
        {
            var trajet = await GetById(id);
            if (trajet == null)
                return false;
            _dbContext.Trajets.Remove(trajet);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Trajet?> Get(Expression<Func<Trajet, bool>> predicate)
        {
            return await _dbContext.Trajets.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Trajet>> GetAll()
        {
            return await _dbContext.Trajets.ToListAsync();
        }

        public async Task<List<Trajet>> GetAll(Expression<Func<Trajet, bool>> predicate)
        {
            return await _dbContext.Trajets.Where(predicate).ToListAsync();
        }


        public async Task<Trajet?> GetById(int id)
        {
            return await _dbContext.Trajets.FindAsync(id);
        }

        public async Task<bool> Update(Trajet trajet)
        {
            var trajetFromDb = await GetById(trajet.TrajetId);

            if (trajetFromDb == null)
                return false;

            if (trajetFromDb.ConducteurId != trajet.ConducteurId)
                trajetFromDb.ConducteurId = trajet.ConducteurId;
            if (trajetFromDb.LieuDepart != trajet.LieuDepart)
                trajetFromDb.LieuDepart = trajet.LieuDepart;
            if (trajetFromDb.LieuArrivee != trajet.LieuArrivee)
                trajetFromDb.LieuArrivee = trajet.LieuArrivee;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
