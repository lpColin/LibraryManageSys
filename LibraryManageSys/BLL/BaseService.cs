using LibraryManageSys.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageSys.BLL
{
    /// <summary>
    /// base servcice class
    /// <remarks>创建:2014.8.27</remarks>
    /// </summary>
    public abstract class BaseService<T>:IBaseService<T> where T :class
    {
        protected ICommonDal<T> CurrentRepository { get; set; }
        public BaseService(ICommonDal<T> currentRepository) { CurrentRepository = currentRepository; }

        public T Add(T Entity)
        {
            return CurrentRepository.Add(Entity);
        }

        public bool Update(T Entity)
        {
            return CurrentRepository.Update(Entity);
        }

        public bool Delete(T Entity)
        {
            return CurrentRepository.Delete(Entity);
        }

    } 
}