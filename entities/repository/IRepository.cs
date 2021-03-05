using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace entities.repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        T GetById(Guid id);
        (IQueryable<T> Itens, int TotalPaginas, int TotalItens) GetAll(Pagination pagination);
        (IQueryable<T> Itens, int TotalPaginas, int TotalItens) GetAll(Expression<Func<T, bool>> expression, Pagination pagination);
        Task<T> Create(T entity);
        Task CreateRangeAsync(ICollection<T> entity);
        void DeleteRange(Expression<Func<T, bool>> predicate);
        T Update(T entity);
        T Delete(T entity);
        Task SaveChanges();
    }
}
