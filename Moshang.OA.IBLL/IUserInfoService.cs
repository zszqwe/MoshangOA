﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.Model;

namespace Moshang.OA.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        IQueryable<UserInfo> LoagPageData(Model.Param.UserQueryParam userQueryParam);
        bool SetRole(int userId, List<int> roleIds);
    }
}
