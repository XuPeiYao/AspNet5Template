using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using AspNet5Template.Extensions.AspNet;
using AspNet5Template.Models;
using Microsoft.Data.Entity;
using AspNet5Template.Extensions.EntityFramework;
using Newtonsoft.Json;
using AspNet5Template.Extensions.Reflection;
<<<<<<< HEAD
using System.Collections;
using System.Linq.Expressions;
=======
>>>>>>> origin/master

namespace AspNet5Template.Controllers{
    //[Route("api/[controller]")]可使用RouteAttribute方式設定該控制器路由，此方式優先於MapRoute
    [ServiceFilter(typeof(AppExceptionFilterAttribute))]//例外過濾器
    public class TestController : Controller {
        private BloggingContext db;

        public TestController(BloggingContext database) {
            db = database;
        }


        // GET: api/values
        //[HttpGet] 此為REST設定，僅在RouteAttribute作用時作用
        public IEnumerable<string> Get(){
            int g = 2 / (100 - (int)Math.Pow(10,2));//故意產生錯誤
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        //[HttpPost] 此為REST設定，僅在RouteAttribute作用時作用
        public void Post([FromBody]string value){
        }

        // PUT api/values/5
        //[HttpPut("{id}")] 此為REST設定，僅在RouteAttribute作用時作用
        public void Put(int id, [FromBody]string value){
        }

        // DELETE api/values/5
        //[HttpDelete("{id}")] 此為REST設定，僅在RouteAttribute作用時作用
        public void Delete(int id){
        }

        public async Task<JsonResult> Test() {
<<<<<<< HEAD
            
=======
            var v = this.GetMemberInfo(obj => obj.db.Post);

>>>>>>> origin/master
            //寫入Session
            this.HttpContext.Session.Set("test", "Hello World!");

            var g = from t in db.Blog
                    select t;
            
            //查詢資料庫內容
            var result1 = (from t in db.Blog select t).FullInclude(1);//EF7目前只支援預先加載不支援延遲加載以至於如果要存取關聯的實體必須使用Include與ThenInclude

<<<<<<< HEAD
            var result2 = (from t in db.Post select t).FullInclude();//使用這個方法時必須注意參考迴圈以防無窮迴圈
=======
            var result2 = (from t in db.Post select t).FullInclude();//使用這個方法時必須注意參考迴圈
            
            var result = new {
                a = result1,
                b = result2
            };
>>>>>>> origin/master
            
            var result = new {
                a = result1,
                b = result2
            };

            return await Task.FromResult(new JsonResult(result) { StatusCode  = 200 });
        }

        
    }
}
