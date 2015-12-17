using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AspNet5Template.Extensions.AspNet;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.StaticFiles;
using AspNet5Template.Controllers;

namespace AspNet5Template{
    public sealed class Startup : StartupBase{
        public Startup(IHostingEnvironment env){
            //讀取應用程式設定檔
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development")){
                //這將快速的推送數據至Application Insights，讓你更快取得結果
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            //加入環境變數
            builder.AddEnvironmentVariables();

            //當設定檔變更時重新讀取
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }
        
        //此方法在執行階段被呼叫，使用此方法在容器中加入服務
        public void ConfigureServices(IServiceCollection services){
            //加入Application Insights框架服務
            services.AddApplicationInsightsTelemetry(Configuration);

            //加入快取
            services.AddCaching();

            //加入Session
            services.AddSession();

            //加入MVC服務
            services.AddMvc();
            
            //加入Exception過濾器
            services.AddTransient<AppExceptionFilterAttribute>();
        }

        //此方法在執行階段被呼叫，使用此方法設定HTTP Request流程
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory){
            //使用IIS平台處理器
            app.UseIISPlatformHandler();

            #region Development Configure
            //加入紀錄主控台設定
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            //除錯模式
            loggerFactory.AddDebug();

            //使用Application Insights Request數據傳遞
            app.UseApplicationInsightsRequestTelemetry();

            //使用Application Insights例外數據傳遞
            app.UseApplicationInsightsExceptionTelemetry();
            #endregion

            #region StaticFiles Configure
            //設定預設檔案
            ConfigureDefaultFiles(app);

            //設定錯誤頁面對應
            ConfigureErrorPages(app, env);

            //使用靜態檔案
            app.UseStaticFiles();
            #endregion

            //使用Session
            app.UseSession();

            //使用MVC服務，並且載入預設路由設定
            app.UseMvc(ConfigureMvcRoute);
            
            //WebSocket設定
            app.UseWebSockets<TestChatHanlder>();
        }
                        
        //Web Application程式進入點
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);//以本類別作為啟動類別
    }
}
