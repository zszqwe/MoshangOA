using Moshang.OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.IDAL;

namespace Moshang.OA.EFDAL
{
    public class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {
        //crud
        public string Name { get; set; }
        //DataModelContainer db = new DataModelContainer();

        #region Pass查询

        //public UserInfo GetUserInfoById(int id)
        //{
        //    return db.UserInfo.Find(id);
        //}

        //public List<UserInfo> GetAllUserInfosd()
        //{
        //    return db.UserInfo.ToList();
        //}


        #endregion

        //#region 查询

        ////单元(方法)测试
        //public IQueryable<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> whereLamda)
        //{
        //    return db.UserInfo.Where(whereLamda).AsQueryable();
        //}

        //public IQueryable<UserInfo> GetPageUsers<S>(int pageSize, int pageIndex, out int total,
        //    Expression<Func<UserInfo, bool>> whereLambda,
        //    Expression<Func<UserInfo, S>> orderByLambda, bool isAsc)
        //{
        //    total = db.UserInfo.Where(whereLambda).Count();
        //    if (isAsc)
        //    {
        //        //降序
        //        var temp = db.UserInfo.Where(whereLambda)
        //            .OrderBy<UserInfo, S>(orderByLambda)
        //            .Skip(pageSize * (pageIndex - 1))
        //            .Take(pageSize).AsQueryable();
        //        return temp;
        //    }
        //    else
        //    {
        //        var temp = db.UserInfo.Where(whereLambda)
        //            .OrderByDescending<UserInfo, S>(orderByLambda)
        //            .Skip(pageSize * (pageIndex - 1))
        //            .Take(pageSize).AsQueryable();
        //        return temp;
        //    }
        //}

        //#endregion

        //#region 添加

        //public UserInfo Add(UserInfo userInfo)
        //{
        //    db.UserInfo.Add(userInfo);
        //    db.SaveChanges();
        //    return userInfo;
        //}

        //#endregion

        //#region 修改

        //public bool Update(UserInfo userInfo)
        //{
        //    db.Entry(userInfo).State = EntityState.Modified;
        //    return db.SaveChanges() > 0;
        //}


        //#endregion

        //#region 删除

        //public bool Delete(UserInfo userInfo)
        //{
        //    db.Entry(userInfo).State = EntityState.Deleted;
        //    return db.SaveChanges() > 0;
        //}


        //#endregion
    }
}
