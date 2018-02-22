using System.Collections.Generic;
using System.Linq;
using Moshang.OA.IBLL;
using Moshang.OA.Model;
using Moshang.OA.Model.Param;

namespace Moshang.OA.BLL
{
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        public IQueryable<ActionInfo> LoagPageData(Model.Param.ActionQueryParam actionQueryParam)
        {
            short normalFlag = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;

            var temp = DbSession.ActionInfoDal.GetEntities(a => a.DelFlag == normalFlag);

            //过滤
            if (!string.IsNullOrEmpty(actionQueryParam.SchName))
            {
                temp = temp.Where(a => a.ActionName.Contains(actionQueryParam.SchName)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(actionQueryParam.SchRemark))
            {
                temp = temp.Where(a => a.Remark.Contains(actionQueryParam.SchRemark)).AsQueryable();
            }

            actionQueryParam.Total = temp.Count();

            //分页
            return temp.OrderBy(u => u.ID)
                .Skip(actionQueryParam.PageSize * (actionQueryParam.PageIndex - 1))
                .Take(actionQueryParam.PageSize).AsQueryable();
        }


        public bool SetRole(int actionId, List<int> roleIds)
        {
            var actionInfo = DbSession.ActionInfoDal.GetEntities(a => a.ID == actionId).FirstOrDefault();
            actionInfo.RoleInfo.Clear();//全剁掉。

            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.ID));
            foreach (var roleInfo in allRoles)
            {
                actionInfo.RoleInfo.Add(roleInfo);//加最新的角色。
            }
            DbSession.SaveChanges();
            return true;
        }

    }
}