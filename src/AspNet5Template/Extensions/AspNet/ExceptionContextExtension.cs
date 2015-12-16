using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public static class ExceptionContextExtension{
        public static Type GetControllerType(this ExceptionContext obj) {
            return ((dynamic)obj).ActionDescriptor.ControllerTypeInfo;
        }

        public static MethodInfo GetActionMethodInfo(this ExceptionContext obj) {
            return obj.GetControllerType().GetMethod(obj.ActionDescriptor.Name,obj.ActionDescriptor.Parameters.Select(item=>item.ParameterType).ToArray());            
        }
    }
}
