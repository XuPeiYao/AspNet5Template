using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public static class ExceptionContextExtension{
        /// <summary>
        /// 取得例外發生的控制器型別
        /// </summary>
        /// <param name="obj">擴充對象</param>
        /// <returns>控制器型別</returns>
        public static Type GetControllerType(this ExceptionContext obj) {
            return ((dynamic)obj).ActionDescriptor.ControllerTypeInfo;
        }

        /// <summary>
        /// 取得發生例外的方法資訊
        /// </summary>
        /// <param name="obj">擴充對象</param>
        /// <returns>方法資訊</returns>
        public static MethodInfo GetActionMethodInfo(this ExceptionContext obj) {
            return obj.GetControllerType().GetMethod(obj.ActionDescriptor.Name,obj.ActionDescriptor.Parameters.Select(item=>item.ParameterType).ToArray());            
        }
    }
}
