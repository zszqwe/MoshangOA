using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moshang.OA.Model;

namespace Moshang.OA.IBLL
{
    public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
        bool SetRole(int userId, List<int> roleIds);
    }
}