using System.Collections.Generic;
using System.Linq;
using Moshang.OA.IBLL;
using Moshang.OA.Model;

namespace Moshang.OA.BLL
{
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        public bool SetRole(int userId, List<int> roleIds)
        {
            var actionInfo = DbSession.ActionInfoDal.GetEntities(u => u.ID == userId).FirstOrDefault();
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