using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace AspNet5Template.Controllers{
    //[Route("api/[controller]")]可使用RouteAttribute方式設定該控制器路由，此方式優先於MapRoute
    public class TestController : Controller{
        // GET: api/values
        //[HttpGet] 此為REST設定，僅在RouteAttribute作用時作用
        public IEnumerable<string> Get(){
            int g = 2 / (100 - (int)Math.Pow(10,2));
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
    }
}
