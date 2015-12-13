using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public class StartupBase{
        /// <summary>
        /// 應用程式配置
        /// </summary>
        public IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// 錯誤頁面與錯誤代碼對應
        /// </summary>
        public Dictionary<int, string> ErrorPages { get; set; } = new Dictionary<int, string>();

        //此方法在執行階段被呼叫，使用此方法設定預設的檔案
        public void ConfigureDefaultFiles(IApplicationBuilder app) {
            //讀取設定檔中預設檔案設定
            var defaultFiles = Configuration.GetSection("DefaultFiles")?.GetChildren();

            //未設定則跳脫
            if (defaultFiles == null) return;

            foreach (var defaultFileItem in defaultFiles) {
                dynamic defaultFileObj = defaultFileItem.ToDynamicObject();
                app.UseDefaultFiles(new DefaultFilesOptions() {
                    RequestPath = defaultFileObj.RequestPath,
                    DefaultFileNames = ((object[])defaultFileObj.DefaultFileNames).Select(item => (string)item).ToList()
                });
            }
        }

        //此方法在執行階段被呼叫，使用此方法設定MVC路由規則
        public void ConfigureMvcRoute(IRouteBuilder routes) {
            //取得所有路由規則
            var rules = Configuration.GetSection("MvcRoutingRules")?.GetChildren();

            //未設定則跳脫
            if (rules == null) return;

            //註冊所有路由規則
            foreach (var rule in rules) {
                //取得子屬性集合
                var attributes = rule.GetChildren();

                //註冊路由
                routes.MapRoute(
                    name: attributes.Where(item => item.Key == "Name").FirstOrDefault()?.Value,
                    template: attributes.Where(item => item.Key == "Template").FirstOrDefault()?.Value,
                    defaults: attributes.Where(item => item.Key == "Defaults").FirstOrDefault()?.ToDynamicObject(),
                    constraints: attributes.Where(item => item.Key == "Constraints").FirstOrDefault()?.ToDynamicObject(),
                    dataTokens: attributes.Where(item => item.Key == "DataTokens").FirstOrDefault()?.ToDynamicObject()
                );
            }
        }

        //此方法在執行階段被呼叫，使用此方法設定錯誤頁面的檔案
        public void ConfigureErrorPages(IApplicationBuilder app, IHostingEnvironment env) {
            //取得所有錯誤頁面設定
            var pages = Configuration.GetSection("ErrorPages")?.GetChildren();

            //未設定則跳脫
            if (pages == null) return;

            //讀取所有錯誤頁面對應
            foreach (var page in pages) {
                //取得子屬性集合
                dynamic obj = page.ToDynamicObject();

                //存入狀態對應
                ErrorPages[int.Parse(obj.StatusCode)] = obj.FilePath;
            }

            //狀態對應
            app.UseStatusCodePages(builder => {
                builder.Run(handler => {
                    return Task.Run(() => {
                        int code = handler.Response.StatusCode;
                        if (ErrorPages.ContainsKey(code)) {//檢查是否存在指定的對應
                            handler.Response.Redirect($"{handler.Request.Scheme}://{handler.Request.Host}/{ErrorPages[handler.Response.StatusCode]}");
                            return;
                        }
                    });
                });
            });
        }
    }
}