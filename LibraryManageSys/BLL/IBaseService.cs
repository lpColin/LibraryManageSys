using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.BLL
{
    /// <summary>
    /// service接口基类
    /// <remarks>创建：2014.8.27</remarks>
    /// </summary>
    public interface IBaseService<T> where T :class
    {
            /// <summary>
            /// 添加方法
            /// </summary>
            /// <param name="Entity">数据实体</param>
            /// <returns>添加后的数据实体</returns>
            T Add(T Entity);

            /// <summary>
            /// 更新方法
            /// </summary>
            /// <param name="Entity">数据实体</param>
            /// <returns>是否成功</returns>
            bool Update(T Entity);

            /// <summary>
            /// 删除方法
            /// </summary>
            /// <param name="Entity">数据实体</param>
            /// <returns>是否成功</returns>
            bool Delete(T Entity);
        
    }
}