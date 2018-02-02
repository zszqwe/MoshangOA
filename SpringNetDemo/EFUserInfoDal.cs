using System;

namespace SpringNetDemo
{
    public class EFUserInfoDal : IUserInfoDal
    {
        public EFUserInfoDal(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public void Show()
        {
            Console.WriteLine("EFUserInfoDal Show "+Name);
        }
    }
}