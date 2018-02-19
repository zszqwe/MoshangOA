using Moshang.OA.EFDAL;
using Moshang.OA.IDAL;

namespace Moshang.OA.DALFactory
{
    public partial class DbSession:IDbSession
    {
       

        /// <summary>
        /// 获取当前EF上下文 进行实体修改整体提交
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}