using System;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace Moshang.OA.Common.Cache
{
    public class CacheHelper
    {

        //Spring.Net注入Cache实现
        public static ICacheWriter CacheWriter { get; set; }

        static CacheHelper()
        {
            //通过容器创建一个对象
            IApplicationContext ctx = ContextRegistry.GetContext();
            //ctx.GetObject("CacheHelper");
            CacheHelper.CacheWriter = ctx.GetObject("CacheWriter") as ICacheWriter;
        }

        public static void AddCache(string key, object value, DateTime expDate)
        {
            //ICacheWriter cacheWriter;
            CacheWriter.AddCache(key, value, expDate);

        }

        public static void AddCache(string key, object value)
        {
            CacheWriter.AddCache(key, value);
        }

        public static object GetCache(string key)
        {
            return CacheWriter.GetCache(key);
        }

        public static void SetCache(string key, object value, DateTime exTime)
        {
            CacheWriter.SetCache(key, value, exTime);
        }

        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }
    }
}