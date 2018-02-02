using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.DALFactory;
using Moshang.OA.EFDAL;
using Moshang.OA.IBLL;
using Moshang.OA.IDAL;
using Moshang.OA.Model;

namespace Moshang.OA.BLL
{
    public class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        //IUserInfoDal userInfoDal = new UserInfoDal();
        //IUserInfoDal userInfoDal = StaticDalFactory.GetUserInfoDal();
        //DbSession dbSession = new DbSession();
        //public UserInfo Add(UserInfo userInfo)
        //{
        //    // return userInfoDal.Add(userInfo);
        //    dbSession.UserInfoDal.Add(userInfo);
        //    //if (dbSession.SaveChenges() > 0)
        //    //{
        //    //    //单元工作模式UnitWork
        //    //}

        //    dbSession.SaveChenges();
        //}

        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        }
        
    }
    
}
