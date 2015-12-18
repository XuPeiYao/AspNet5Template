using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;


namespace AspNet5Template.Extensions.EntityFramework{
    public static class IQueryableExtension {
        //實作LazyLoad擴充
        public static IQueryable LazyLoad<TEntity>(this IQueryable<TEntity> source) where TEntity : class {
            Type GenericType = source.GetType().GetGenericArguments()?[0];

            PropertyInfo[] Properties = GenericType.GetProperties();

            IQueryable<TEntity> result = source;

            foreach (var Property in Properties) {
                if (Property.GetCustomAttribute<LazyLoadAttribute>() == null) continue;

                #region 建構Lambda
                var header = Expression.Parameter(GenericType, "item");
                Expression<Func<TEntity, object>> func = Expression.Lambda<Func<TEntity, object>>(
                    Expression.MakeMemberAccess(header, Property),
                    header
                );
                var header2 = Expression.Parameter(typeof(object), "item");
                Expression<Func<dynamic, object>> func2 = Expression.Lambda<Func<dynamic, object>>(
                    Expression.MakeMemberAccess(header, Property),
                    header2
                );
                #endregion

                List<Type> typeList = new List<Type>();
                result = result.Include(func).ThenLazyLoad<TEntity>(func2, typeList);//解決單層讀取問題
            }
            return result;
        }

        private static IIncludableQueryable<TEntity, dynamic> ThenLazyLoad<TEntity>(this IIncludableQueryable<TEntity, dynamic> source, Expression<Func<dynamic, object>> Func, List<Type> typeList) where TEntity : class {
            //var temp = Func.Compile().Invoke(source.First());

            MemberExpression e = (MemberExpression)Func.Body;
            Type type = ((PropertyInfo)e.Member).PropertyType;
            if (type.Namespace == "System.Collections.Generic") {
                type = type.GenericTypeArguments.First();
            }
            if (typeList.Contains(type)) return source;
            
            IIncludableQueryable<TEntity, dynamic> result = source;
            PropertyInfo[] Properties = type.GetProperties();

            foreach (var Property in Properties) {
                if (Property.GetCustomAttribute<LazyLoadAttribute>() == null) continue;
                if (Property.PropertyType == source.ElementType) continue;

                #region 建構Lambda
                var header = Expression.Parameter(typeof(object), "item");
                Expression<Func<dynamic, dynamic>> func = Expression.Lambda<Func<dynamic, dynamic>>(
                    Expression.MakeMemberAccess(Expression.Convert(header,type), Property),
                    header
                );
                #endregion

                List<Type> typeList2 = new List<Type>(typeList.ToArray());
                typeList2.Add(type);//Function Return Type

                result = result.ThenInclude(func).ThenLazyLoad(func,typeList2);//.ThenLazyLoad<TEntity>(func);//解決單層讀取問題
            }
            return result;
        }
    }
}
