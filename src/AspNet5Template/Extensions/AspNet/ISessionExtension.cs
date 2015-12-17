using Microsoft.AspNet.Http.Features;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public static class ISessionExtension{
        public static void Set(this ISession obj,string key,string value) {
            obj.Set(key, Encoding.UTF8.GetBytes(value));
        }

        public static bool TryGetStringValue(this ISession obj, string key,out string value) {
            byte[] result;
            value = null;
            if (!obj.TryGetValue(key, out result)) return false;
            value = Encoding.UTF8.GetString(result);
            return true;
        }
        
        public static void Set(this ISession obj,string key,JToken value) {
            obj.Set(key, value.ToString());
        }

        public static bool TryGetJTokenValue(this ISession obj, string key, out JToken value) {
            string result;
            value = null;
            if (!obj.TryGetStringValue(key, out result)) return false;
            value = JToken.Parse(result);
            return true;
        }
    }
}
