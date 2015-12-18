using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNet5Template.Extensions.AspNet;

namespace AspNet5Template.Controllers{
    public class AppExceptionFilterAttribute : ExceptionFilterAttribute{
        public override void OnException(ExceptionContext context) {
            //取得
            Type ControllerType = context.GetControllerType();
            context.Result = new JsonResult(new {
                Title = "Error",
                Controller = ControllerType.Name,
                Method = context.ActionDescriptor.Name,
                Message = context.Exception.ToString()
            });
            
            base.OnException(context);
        }
    }
}
