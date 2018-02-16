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

        


        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        }
        
    }
    
}
