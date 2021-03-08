using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LibraryManageSys.DAL
{
    /// <summary>
    /// base interface for Dal
    /// <remarks>create：2014.8.27</remarks>
    /// </summary>
    public interface ICommonDal<T>
    {
        ///<summary>
        ///add
        ///</summary>
        ///<param name="entity">data entity</param>
        ///<returns> entity after added</returns>  
        T Add(T entity);

        ///<summary>
        ///删除
        ///</summary>
        ///<param name="Entity">data entity</param>
        ///<returns>是否成功</returns>
        bool Delete(T entity);

        ///<summary>
        ///更新
        ///</summary>
        ///<param name="Entity">data entity</param>
        ///<returns>是否成功</returns>
        bool Update(T entity);

        ///<summary>
        ///查询数据
        ///</summary>
        ///<param name="WhereLambda">查询表达式</param>
        ///<returns>entity</returns>
        T Find(Expression<Func<T, bool>> whereLambda);

        ///<summary>
        ///查询记录数
        ///</summary>
        ///<param name="predicate">条件表达式</param>
        ///<returns>记录数</returns>
        int Count(Expression<Func<T, bool>> Predicate);

        ///<summary>
        ///是否存在
        ///</summary>
        ///<param name="anyLambda">查询表达式</param>
        ///<returns>布尔值</returns>
        bool Exist(Expression<Func<T, bool>> anyLambda);

        ///<summary>
        ///查询数据列表
        ///</summary>
        ///<param name="S">排序</param>
        ///<param name="whereLambda">查询表达式</param>
        ///<param name="isAsc">是否升序</param>
        ///<param name="orderLambda">排序表达式</param>
        ///<returns></returns>
        IQueryable<T> FindList(Expression<Func<T, bool>> whereLambda, string orderName,bool isAsc);

        ///<summary>
        ///查找分页数据列表
        ///</summary>
        ///<param name="S">排序</param>
        ///<param name=""pageIndex>当前页</param>
        ///<param name="pageSize">每页记录数</param>
        /// ///<param name="totalRecord">总记录数</param>
        ///<param name="whereLambda">查询表达式</param>
        ///<param name="isAsc">是否升序</param>
        ///<param name="orderLambda">排序表达式</param>
        ///<returns></returns>
        IQueryable<T> FindePageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLambda, string orderName,bool isAsc);


        IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc);
    }
}