using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace AspNet5Template.Controllers{
    //[Route("api/[controller]")]
    public class TestController : Controller{
        

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get(){
            var t = 1 / (int)Math.Pow(0,2);
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value){
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value){
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id){
        }
    }
}
