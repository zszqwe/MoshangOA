using System;

namespace SpringNetDemo
{
    public class UserInfoService
    {
        public IUserInfoDal UserInfoDal { get; set; }

        public void Show()
        {
            UserInfoDal.Show();
            Console.WriteLine("UserInfoService Show");
        }
    }
}