using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.DALFactory;
using Moshang.OA.IBLL;
using Moshang.OA.IDAL;
using Moshang.OA.Model;

namespace Moshang.OA.BLL
{
    public partial class OrderInfoService:BaseService<OrderInfo>, IOrderInfoService
    {
        //IOrderInfoDal orderInfoDal = StaticDalFactory.GetOrderInfoDal();

        //public OrderInfo Add(OrderInfo orderInfo)
        //{
        //    return orderInfoDal.Add(orderInfo);
        //}
    }
}
