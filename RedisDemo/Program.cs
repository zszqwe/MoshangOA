using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Redis测试实例
            var client = new RedisClient("127.0.0.1", 6379);

            #region Redis排序集合
            //最后一个参数为我们排序的依据
            //var s = client.AddItemToSortedSet("12", "百度4", 4);

            //client.AddItemToSortedSet("12", "谷歌4", 3);
            //client.AddItemToSortedSet("12", "阿里3", 2);
            //client.AddItemToSortedSet("12", "新浪1", 1);
            //client.AddItemToSortedSet("12", "腾讯5", 5);

            ////升序获取最一个值:"新浪"
            //var list = client.GetRangeFromSortedSet("12", 0, 0);
            //Console.WriteLine("升序:");
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            ////降序获取最一个值:"腾讯"
            //list = client.GetRangeFromSortedSetDesc("12", 0, 0);
            //Console.WriteLine("降序:");
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //} 
            #endregion

            #region 基本功能 分布式缓存
            //添加
            //client.Add("kayssss", "valuessss", DateTime.Now.AddMinutes(20));
            #endregion

            #region 数据结构:队列+栈
            ////队列
            //client.EnqueueItemOnList("LogQueue","Error1...");
            //client.EnqueueItemOnList("LogQueue", "Error2...");
            //string str=client.DequeueItemFromList("LogQueue");
            //Console.WriteLine(str);

            //栈
            //client.PushItemToList("yz","yz1");
            //client.PushItemToList("yz", "yz2");
            //Console.WriteLine(client.PopItemFromList("yz"));
            #endregion



            Console.Read();
        }
    }
}
