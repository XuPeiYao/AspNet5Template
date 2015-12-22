using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.EntityFramework{
    /// <summary>
    /// 標註EntityFramework中模型類別成員在呼叫<see cref="IQueryableExtension.FullInclude{TEntity}(IQueryable{TEntity}, int)"/>時自動引入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IncludeAttribute : Attribute{
    }
}
