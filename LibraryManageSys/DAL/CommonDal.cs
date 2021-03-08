using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LibraryManageSys.DAL
{
    /// <summary>
    /// implement Common interface  method
    /// <Remarks>created:2014.8.27</Remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonDal<T>:ICommonDal<T> where T : class
    {
        protected LMSEntitys  nContext= ContextFactory.GetCurrentContext();
        public T Add(T entity)
        {
            nContext.Entry<T>(entity).State = EntityState.Added;
            nContext.SaveChanges();
            return entity;
        }

        public int Count(Expression<Func<T, bool>> Predicate)
        {
            return nContext.Set<T>().Count(Predicate);
        }

        public bool Delete(T entity)
        {
            nContext.Set<T>().Attach(entity);
            nContext.Entry<T>(entity).State = EntityState.Deleted;
            return nContext.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            nContext.Set<T>().Attach(entity);
            nContext.Entry<T>(entity).State = EntityState.Modified;
            return nContext.SaveChanges()>0;
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            T _entity = nContext.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        public bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> anyLambda)
        {
            return nContext.Set<T>().Any(anyLambda);
        }

        public IQueryable<T> FindList(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, string orderName, bool isAsc)
        {
            var _list = nContext.Set<T>().Where<T>(whereLambda);
            _list = OrderBy(_list, orderName, isAsc);
            return _list;
        }

        public IQueryable<T> FindePageList(int pageIndex, int pageSize, out int totalRecord, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, string orderName, bool isAsc)
        {
            var _list = nContext.Set<T>().Where<T>(whereLambda);
            totalRecord = _list.Count();
            _list = OrderBy(_list, orderName, isAsc).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">原IQueryable</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>排序后的IQueryable<T></returns>
        public IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if (source == null) throw new ArgumentNullException("source", "不能为空");
            if (string.IsNullOrEmpty(propertyName)) return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if (_property == null) throw new ArgumentNullException("propertyName", "属性不存在");
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable), _methodName, new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);

        }
    }
}