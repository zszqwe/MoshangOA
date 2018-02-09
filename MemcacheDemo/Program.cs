using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.ClientLibrary;
using Moshang.OA.IBLL;

namespace MemcacheDemo
{
    class Program
    {
        public IUserInfoService UserInfoService { get; set; }
        static void Main(string[] args)
        {
            //分布Memcachedf服务IP 端口
            string[] servers =
            {
                "192.168.1.105:11211"
            };

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;

            string key1 = "key-1";
            string value1 = "value1";
            mc.Add(key1, value1);
            Console.WriteLine(key1 + "   " + mc.Get(key1));

            string key2 = "key5";
            string value2 = "value5";
            mc.Add(key2, value2);
            Console.WriteLine(key2 + "   " + mc.Get(key2));



            //var userInfos = UserInfoService.GetEntities(u => u.UName == "admin" && u.Pwd == "123456" && u.DelFlag == "0").FirstOrDefault();
            //var userinfo ={string username="Moshang",string wd="123456"}
            mc.Add("d4491076-3a6b-4f47-9515-a2bb64e7869c","11111" );
            Console.WriteLine(mc.Get("d4491076-3a6b-4f47-9515-a2bb64e7869c"));
        }
    }
}
