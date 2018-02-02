using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using Moshang.OA.IDAL;
using Moshang.OA.DALFactory;

namespace Moshang.OA.BLL
{
    public abstract class BaseService<T> where T : class, new()
    {

        public IBaseDal<T> CurrentDal { get; set; }

        public IDbSession DbSession
        {
            get
            {
                return DbSessionFactory.GetCurrentDbSession();
            }
        }

        public BaseService()
        {
            SetCurrentDal();
        }

        //要求自类必须实现
        public abstract void SetCurrentDal();
        
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDal.GetEntities(whereLamda);
        }

        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageSize,pageIndex,out total,whereLambda,orderByLambda,isAsc);
        }
        
        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }
    }
}