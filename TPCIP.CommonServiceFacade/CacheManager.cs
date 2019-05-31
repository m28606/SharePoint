using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TPCIP.CommonServiceFacade
{
    public static class CacheManager
    {
        public enum CacheCategory
        {
            Default = 0,
            GuideAndArticleSelector = 1,
            AddressSearch = 2,
            Subscription = 3,
            RetailCustomer = 4,
            NoteDetails = 5,
            SpecDetails = 6,
            ProvisionalChannelList = 7,
            Pulse = 8,
            TimelineTemplates = 9,
            LineStateResult=10,
            LineCheckDiagnoseLid=11,
            CustomerOverviewDetails = 12,
            LineDiagnoseResult=13,
            DLMStatus=14,
            LineStateParameter=15,
            ServiceStatus=16,
            ServiceType=17,
            Orders=18,
            HybridLineInfo=19
        }
        private static System.Web.Caching.Cache _cache = HttpRuntime.Cache;
        /// <summary>
        /// Retrieves the Cache with the given Key and Category. If Not found then inserts the Data using Source Function into the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="category">User Defined Category which will be attached to Cache Name.</param>
        /// <param name="key">Key name for Cache</param>
        /// <param name="dataGenerator">Source of data function</param>
        /// <param name="durationInMinutes">Set to Zero for No Absolute Expiration else Non-Zero for Absolute Expiration</param>
        /// <returns>Returns Cached Data</returns>
        public static T Get<T>(CacheCategory category, string key, Func<T> dataGenerator, int durationInMinutes)
        {
            T value = (T)_cache[GenerateKey(category, key)];

            if (value == null)
            {
                value = dataGenerator();
                if (value is IList && (value as IList).Count == 0)
                {
                    return value;//don't cache empty objects
                }
                else
                {
                    Insert(category, key, value, durationInMinutes);
                }
            }

            return value;
        }

        private static void Insert<T>(CacheCategory category,  string key, T value, int durationInMinutes)
        {
            //Service Dashboard BLR 71012: Ajinkya Korade: 14-01-2016: Set No Absolute expiration if durationInMinutes is 0 
            if (durationInMinutes == 0)
            {
                _cache.Insert(GenerateKey(category, key), value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
            else
            {
                _cache.Insert(GenerateKey(category, key), value, null, DateTime.Now.AddMinutes(durationInMinutes), TimeSpan.Zero);
            }
        }
        /// <summary>
        /// Ajinkya Korade: 14-01-2016:Updates the Cache with the given Key and Category. If Not found then inserts the Data using Source Function into the cache.
        /// </summary>
        /// <Project>Service Dashboard BLR 71012</Project>
        /// <typeparam name="T"></typeparam>
        /// <param name="category">User Defined Category which will be attached to Cache Name.</param>
        /// <param name="key">Key name for Cache</param>
        /// <param name="dataGenerator">Source of data function</param>
        /// <param name="durationInMinutes">Set to Zero for No Absolute Expiration else Non-Zero for Absolute Expiration</param>
        /// <returns>Returns Cached Data</returns>
        public static void Update<T>(CacheCategory category, string key, Func<T> dataGenerator, int durationInMinutes)
        {
            T value = dataGenerator();
            //Set No Absolute expiration if durationInMinutes is 0 - Ajinkya Korade - 14-01-2016
            if (durationInMinutes == 0)
            {
                 _cache.Insert(GenerateKey(category, key), value, null,System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
            else
            {
            _cache.Insert(GenerateKey(category, key), value, null, DateTime.Now.AddMinutes(durationInMinutes), TimeSpan.Zero);
        }
        }
        private static string GenerateKey(CacheCategory category, string key)
        {
            return category.ToString() + "_" + key;
        }

        public static void Remove(CacheCategory category, string key)
        {            
            _cache.Remove(GenerateKey(category, key));
        }
    }
}
