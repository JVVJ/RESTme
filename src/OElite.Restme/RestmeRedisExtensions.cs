﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace OElite
{
    public static class RestmeRedisExtensions
    {

        public static async Task<T> RedisGetAsync<T>(this Restme restme, string redisKey)
        {
            MustBeRedisMode(restme);
            string stringValue = await restme.redisDatabase.StringGetAsync(redisKey);
            if (typeof(T).IsPrimitiveType())
            {
                return (T)Convert.ChangeType(stringValue, typeof(T));
            }
            else
            {
                return stringValue.JsonDeserialize<T>();
            }
        }
        public static async Task<T> RedisPostAsync<T>(this Restme restme, string redisKey, T dataObject)
        {
            MustBeRedisMode(restme);
            if (await restme.redisDatabase.StringSetAsync(redisKey, dataObject.JsonSerialize()))
                return dataObject;
            else
                return default(T);
        }
        public static async Task<T> RedisDeleteAsync<T>(this Restme restme, string redisKey)
        {
            MustBeRedisMode(restme);
            var result = await restme.redisDatabase.KeyDeleteAsync(redisKey);
            if (typeof(T) == typeof(bool))
                return (T)Convert.ChangeType(result, typeof(T));
            else
                return default(T);
        }

        #region Private Methods
        private static void MustBeRedisMode(Restme restme)
        {
            if (restme?.CurrentMode != RestMode.RedisCacheClient)
                throw new InvalidOperationException($"current request is not valid operation, you are under RestMode: {restme.CurrentMode.ToString()}");
        }
        #endregion

    }
}