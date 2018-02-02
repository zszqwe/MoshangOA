using System;

namespace SpringNetDemo
{
    public class UserInfoDal : IUserInfoDal
    {
       

        public string Name { get; set; }

        public void Show()
        {
            Console.WriteLine("UserInfoDal Show");
        }
    }
}