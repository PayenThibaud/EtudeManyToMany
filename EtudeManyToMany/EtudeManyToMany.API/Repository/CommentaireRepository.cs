using EtudeManyToMany.API.Data;
using EtudeManyToMany.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EtudeManyToMany.API.Repository
{
    public class CommentaireRepository : IRepository<Commentaire>
    {
        private ApplicationDbContext _dbContext { get; }
        public CommentaireRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(Commentaire commentaire)
        {
            var addedObj = await _dbContext.Commentaires.AddAsync(commentaire);
            await _dbContext.SaveChangesAsync();
            return addedObj.Entity.ConducteurId;
        }

        public async Task<bool> Delete(int id)
        {
            var commentaire = await GetById(id);
            if (commentaire == null)
                return false;
            _dbContext.Commentaires.Remove(commentaire);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Commentaire?> Get(Expression<Func<Commentaire, bool>> predicate)
        {
            return await _dbContext.Commentaires.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Commentaire>> GetAll()
        {
            return await _dbContext.Commentaires.ToListAsync();
        }

        public async Task<List<Commentaire>> GetAll(Expression<Func<Commentaire, bool>> predicate)
        {
            return await _dbContext.Commentaires.Where(predicate).ToListAsync();
        }

        public async Task<Commentaire?> GetById(int id)
        {
            return await _dbContext.Commentaires.FindAsync(id);
        }

        public async Task<bool> Update(Commentaire contact)
        {
            throw new NotImplementedException();
        }
    }
}
