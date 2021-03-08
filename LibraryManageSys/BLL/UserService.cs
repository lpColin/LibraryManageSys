using LibraryManageSys.DAL;
using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.BLL
{
    /// <summary>
    /// user service
    /// </summary>
    public class UserService:BaseService<User>,IUserService
    {
        public UserService() : base(RepositoryFactory.UserRepository) { }

        public bool Exist(string userName)
        {
            return CurrentRepository.Exist(u => u.userName == userName);
        }

        public User Find(int userID)
        {
            return CurrentRepository.Find(u => u.userId== userID);
        }

        public User Find(string userName)
        {
            return CurrentRepository.Find(u => u.userName == userName);
        }

        public IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order)
        {
            bool _isAsc = true;
            string _orderName = string.Empty;
            switch (order)
            {
                case 0:
                    _isAsc = true;
                    _orderName = "UserID";
                    break;
                case 1:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
                case 2:
                    _isAsc = true;
                    _orderName = "RegistrationTime";
                    break;
                case 3:
                    _isAsc = false;
                    _orderName = "RegistrationTime";
                    break;
                case 4:
                    _isAsc = true;
                    _orderName = "OrderTime";
                    break;
                case 5:
                    _isAsc = false;
                    _orderName = "OrderTime";
                    break;
                default:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
            }
            return CurrentRepository.FindePageList(pageIndex, pageSize, out totalRecord, u => true, _orderName, _isAsc);

        }
    }
}