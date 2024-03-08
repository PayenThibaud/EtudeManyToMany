using System.Linq.Expressions;

namespace EtudeManyToMany.Blazor.Services
{
    public interface IService<TEntity>
    {
        //CREATE
        Task<bool> Add(TEntity contact);
        // READ
        Task<TEntity?> GetById(int id);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        // UPDATE
        Task<bool> Update(TEntity contact);
        // DELETE
        Task<bool> Delete(int id);
    }
}
