using AspNet5Template.Extensions.EntityFramework;
using Microsoft.Data.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Models {
    public class Blog{
        public int BlogId { get; set; }
        public string Url { get; set; }

        [Include]//ICollection
        public virtual ICollection<Post> Post { get; set; }
    }
}