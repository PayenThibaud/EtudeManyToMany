using EtudeManyToMany.API.Data;
using EtudeManyToMany.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EtudeManyToMany.API.Repository
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private ApplicationDbContext _dbContext { get; }
        public ReservationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(Reservation reservation)
        {
            var addedObj = await _dbContext.Reservations.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();
            return addedObj.Entity.ReservationId;
        }

        public async Task<bool> Delete(int id)
        {
            var reservation = await GetById(id);
            if (reservation == null)
                return false;
            _dbContext.Reservations.Remove(reservation);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Reservation?> Get(Expression<Func<Reservation, bool>> predicate)
        {
            return await _dbContext.Reservations.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Reservation>> GetAll()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<List<Reservation>> GetAll(Expression<Func<Reservation, bool>> predicate)
        {
            return await _dbContext.Reservations.Where(predicate).ToListAsync();
        }

        public async Task<Reservation?> GetById(int id)
        {
            return await _dbContext.Reservations.FindAsync(id);
        }

        public Task<bool> Update(Reservation contact)
        {
            throw new NotImplementedException();
        }
    }
}