using System.Web;
using System.Web.Mvc;

namespace LibraryManageSys
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(System.Data.DataException),
                View = "DataBaseError"
            },1);

            filters.Add(new HandleErrorAttribute(),2);
           
        }
    }
}
