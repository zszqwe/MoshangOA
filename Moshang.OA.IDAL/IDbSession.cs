namespace Moshang.OA.IDAL
{
    public interface IDbSession
    {
        IUserInfoDal UserInfoDal { get; }
        IOrderInfoDal OrderInfoDal { get; }

        int SaveChanges();
    }
}