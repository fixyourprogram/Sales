using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();

        void Save(T entity);

        void Save(IEnumerable<T> entity);

        void Update(T entity, string id);

        void Update(T entity, long id);

        void Delete(long id);

        void Delete(long keyId, long secondKeyId);

        T Get(Expression<Func<T, bool>> filter);

        T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> filter);

        IList<T> GetAll(Expression<Func<T, bool>> filter);

        IList<T> GetAll(Expression<Func<T, bool>> filter, int skip, int take, Expression<Func<T, long>> ordering, params Expression<Func<T, object>>[] includes);

        IList<T> GetAll(Expression<Func<T, bool>> filter, int skip, int take, Expression<Func<T, DateTime?>> ordering, params Expression<Func<T, object>>[] includes);

        IList<T> GetAll(Expression<Func<T, bool>> filter, int skip, int take, Expression<Func<T, long>> ordering, params string[] includes);

        IList<T> GetAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        IList<T> GetAll(Expression<Func<T, bool>> filter, params string[] includes);

        IList<T> GetAllNotLazy(Expression<Func<T, bool>> filter);
    }
}
