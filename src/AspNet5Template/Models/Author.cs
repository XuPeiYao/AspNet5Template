using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Models{
    public class Author{
        public int AuthorId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Post { get; set; }
    }
}
