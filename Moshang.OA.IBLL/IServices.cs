﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.IBLL;
using Moshang.OA.Model;

namespace Moshang.OA.IBLL
{
	
	public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
    }
	
	public partial interface IOrderInfoService:IBaseService<OrderInfo>
    {
    }
	
	public partial interface IR_UserInfo_ActionInfoService:IBaseService<R_UserInfo_ActionInfo>
    {
    }
	
	public partial interface IRoleInfoService:IBaseService<RoleInfo>
    {
    }
	
	public partial interface IUserInfoService:IBaseService<UserInfo>
    {
    }
	
	public partial interface IUserInfoExtService:IBaseService<UserInfoExt>
    {
    }
	
}