using System;
using System.Linq;
using System.Linq.Expressions;

namespace Moshang.OA.IBLL
{
    public interface IBaseService<T> where T : class, new()

    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLamda);

        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda, bool isAsc);

        T Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}