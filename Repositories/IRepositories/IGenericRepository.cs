using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetMany(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Update(T entity);
        Task Insert(T entity);
        Task Delete(T entity);

    }
}
