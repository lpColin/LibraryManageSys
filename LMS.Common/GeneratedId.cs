using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Common
{
    /// <summary>
    /// generate UserId readerId bookId 
    /// <remarks>created:2014.8.25</remarks>
    /// </summary>
    public class GeneratedId
    {
        public string getUserId() {
            DataSet ds = GetAllUser("user");
            string strUserId = "";
            if (ds.Tables[0].Rows.Count == 0) {
                strUserId = "GLY10001";
            }else{
                strUserId = "GLY" + (Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count-1][0].ToString().Substring(3,5))+1);
            }
            return strUserId;        
        }

        private DataSet GetAllUser(string tbName)
        {
            return (new DataBase().runProcReturn("select * from user order by userId",tbName));         
        }

    }
}
