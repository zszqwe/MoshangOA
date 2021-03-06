﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.IBLL;
using Moshang.OA.IDAL;
using Moshang.OA.Model;

namespace Moshang.OA.BLL
{
	
	public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.ActionInfoDal;
        } 
	}
	
	public partial class OrderInfoService:BaseService<OrderInfo>,IOrderInfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.OrderInfoDal;
        } 
	}
	
	public partial class R_UserInfo_ActionInfoService:BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.R_UserInfo_ActionInfoDal;
        } 
	}
	
	public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.RoleInfoDal;
        } 
	}
	
	public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        } 
	}
	
	public partial class UserInfoExtService:BaseService<UserInfoExt>,IUserInfoExtService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoExtDal;
        } 
	}
	
}