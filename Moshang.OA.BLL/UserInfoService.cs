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
    public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        
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

        public bool SetRole(int userId, List<int> roleIds)
        {
            var user = DbSession.UserInfoDal.GetEntities(u => u.ID == userId).FirstOrDefault();
            user.RoleInfo.Clear();

            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.ID));
            foreach (var roleInfo in allRoles)
            {
                user.RoleInfo.Add(roleInfo);
            }
            DbSession.SaveChanges();
            return true;
        }
    }
    
}
