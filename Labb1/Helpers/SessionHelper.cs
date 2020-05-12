using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static async Task<T> GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return await Task.FromResult<T>(value == null ? default(T) : JsonConvert.DeserializeObject<T>(value));
        }

    }
}
