using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Common
{
   public class DataBase:IDisposable
    {
       public DataBase() {      
       }


       public SqlConnection con;

       #region 打开数据库连接
       public void open(){
           if (con == null) {
               con = new SqlConnection("Data Source=.;Initial Catalog=libraryManage;Persist Security Info=True;User ID=sa;Password=_password1");
           }
           if (con.State == ConnectionState.Closed)
               con.Open();
       }
       #endregion


       #region 关闭数据库连接
       public void close() {
           if (con != null) {
               con.Close();
           }
       }
       #endregion

       #region 释放数据库连接资源
       public void Dispose()
        {
            if (con != null) {
                con.Dispose();
                con = null;
            }
        }
       #endregion

       public SqlParameter MakeInParam(string ParaName, SqlDbType dbType, int size, object Value) {
           return MakeInParam(ParaName, dbType, size,ParameterDirection.Input, Value);
       }

       public SqlParameter MakeInParam(string ParaName, SqlDbType dbType, int size, ParameterDirection parameterDirection, object Value)
       {
           SqlParameter param;
           if (size > 0)
               param = new SqlParameter(ParaName, dbType, size);
           else
               param = new SqlParameter(ParaName,dbType);
            param.Direction = parameterDirection;
               if(!(parameterDirection==ParameterDirection.Output && Value == null))
                    param.Value= Value;
                   return param;       
           }

       public DataSet runProcReturn(string procName,SqlParameter[] prams,string tbName){
             SqlDataAdapter dap = createDataAdaper(procName,prams);
             DataSet ds = new DataSet();
             dap.Fill(ds, tbName);
             this.close();
             return ds;
        }

       public DataSet runProcReturn(string procName, string tbName) {
           SqlDataAdapter dap = createDataAdaper(procName, null);
           DataSet ds = new DataSet();
           dap.Fill(ds, tbName);
           this.close();
           return ds; 
       }


       public SqlDataAdapter createDataAdaper(string procName,SqlParameter[] prams)
       {
        	this.open();
            SqlDataAdapter dap = new SqlDataAdapter(procName,con);
            dap.SelectCommand.CommandType = CommandType.Text;
            if (prams != null) {
                foreach (SqlParameter parameter in prams)
                    dap.SelectCommand.Parameters.Add(parameter);
            }
            dap.SelectCommand.Parameters.Add(new SqlParameter("returnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
               return dap;           
       }


       public SqlCommand CreateCommand(string procName, SqlParameter[] prams) {
           this.open();
           SqlCommand cmd = new SqlCommand(procName, con);
           cmd.CommandType = CommandType.Text;
           if (prams != null) {
               foreach (SqlParameter parameter in prams)
                   cmd.Parameters.Add(parameter);
           }
           cmd.Parameters.Add(new SqlParameter("returnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
           return cmd;
       }
      
    }
       
}
