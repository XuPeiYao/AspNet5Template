using AspNet5Template.Extensions.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Models {
    public class Post{
        
        public int PostId { get; set; }
        public int BlogId { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public Blog Blog { get; set; }

        [Include]
        public Author Author { get; set; }
    }
}