using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Moshang.OA.IDAL;
using Moshang.OA.DALFactory;
using Moshang.OA.Model;

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

        //要求子类必须实现
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
        
        //添加
        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }

        //修改
        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }
        
        //删除
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }

        //批量逻辑删除
        public int DeleteListByLogical(List<int> ids)
        {
            CurrentDal.DeleteListByLogical(ids);
            return DbSession.SaveChanges();
        }

        //批量真实删除
        public int DeleteList(List<int> ids)
        {
            foreach (var id in ids)
            {
                CurrentDal.Delete(id);
            }

            return DbSession.SaveChanges();

        }

        //根据id删除
        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges() > 0;
        }

    }
}