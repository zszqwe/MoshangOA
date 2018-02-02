using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace SpringNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //IUserInfoDal userInfoDal=new UserInfoDal();
            //userInfoDal.Show();

            //容器创建
            IApplicationContext ctx = ContextRegistry.GetContext();
            IUserInfoDal dal= ctx.GetObject("UserInfoDal") as IUserInfoDal;

            dal.Show();

            IUserInfoDal dal1 = ctx.GetObject("UserInfoDal1") as IUserInfoDal;

            dal1.Show();

            //UserInfoService userInfoService=((IObjectFactory) ctx).GetObject("UserInfoService") as UserInfoService;
            //userInfoService.Show();


            Console.ReadKey();
        }
    }
}
