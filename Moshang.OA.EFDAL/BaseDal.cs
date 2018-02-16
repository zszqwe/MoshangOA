using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.Model;

namespace Moshang.OA.EFDAL
{
    /// <summary>
    /// 封装所有Dal 公共crud方法
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class BaseDal<T> where T : class, new()
    {
        //DataModelContainer db = new DataModelContainer();
        public DbContext Db
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }

        #region 查询

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLamda)
        {
            return Db.Set<T>().Where(whereLamda).AsQueryable();
        }

        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            total = Db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                //降序
                var temp = Db.Set<T>().Where(whereLambda)
                    .OrderBy<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = Db.Set<T>().Where(whereLambda)
                    .OrderByDescending<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
                return temp;
            }
        }

        #endregion

        #region 添加

        public T Add(T entity)
        {
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;
        }

        #endregion

        #region 修改

        public bool Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            //return Db.SaveChanges() > 0;
            return true;
        }


        #endregion

        #region 删除

        public bool Delete(T entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
            //return Db.SaveChanges() > 0;
            return true;
        }

        public bool Delete(int id)
        {
            var entity = Db.Set<T>().Find(id);
            Db.Set<T>().Remove(entity);


            return true;
        }

        #endregion
    }
}
