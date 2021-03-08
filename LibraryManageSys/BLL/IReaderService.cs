using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManageSys.BLL
{
    public interface IReaderService:IBaseService<Reader>
    {
        // <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <returns>布尔值</returns>
        bool Exist(string userName);

        /// <summary>
        /// 查找用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户</returns>
        User Find(int userID);

        /// <summary>
        /// 查找用户信息
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <returns>用户</returns>
        User Find(string userName);

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pageIndex">页码数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="order">排序：0-ID升序（默认），1ID降序，2注册时间升序，3注册时间降序，4登录时间升序，5登录时间降序</param>
        /// <returns></returns>
        IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order);
    }
}
