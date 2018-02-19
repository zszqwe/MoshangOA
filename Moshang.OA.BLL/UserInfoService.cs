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
using Moshang.OA.Model.Param;

namespace Moshang.OA.BLL
{
    public class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {




        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        }

        public IQueryable<UserInfo> LoagPageData(Model.Param.UserQueryParam userQueryParam)
        {
            short normalFlag = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;

            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFlag == normalFlag);

            //过滤
            if (!string.IsNullOrEmpty(userQueryParam.SchName))
            {
                temp = temp.Where(u => u.UName.Contains(userQueryParam.SchName)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(userQueryParam.SchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.SchRemark)).AsQueryable();
            }

            userQueryParam.Total = temp.Count();

            //分页
            return temp.OrderBy(u => u.ID)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();


        }
    }
    
}
