using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using System.Configuration;//引入读取配置文件的命名空间

namespace DAL
{
    /// <summary>
    /// 通用数据访问类
    /// </summary>
    public class SQLHelper
    {
        // private static string connString = "Server=aaaa\\sqlexpress;DataBase=StudentManageDB;Uid=sa;Pwd=password01!";

       // private static string connString = "Server=.;DataBase=StudentManageDB;Uid=sa;Pwd=ijen@2252";
        public static readonly string connString = Common.StringSecurity.DESDecrypt(ConfigurationManager.ConnectionStrings["connString"].ToString());

        /// <summary>
        /// 执行增、删、改方法
        /// </summary>
        /// <param name="sql">组合的SQL</param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志...
                throw  ex;
            }
            finally// 无论是否发生异常，都要执行的代码。
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行带参数的SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public static int Update(string sql,SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.Add(param);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志...

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行带参数的存储过程
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int UpdateByProcedure(string ProcedureName, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.Open();
                cmd.CommandText = ProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(param);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志...

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行事务，保正数据一致性
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public static int UpdataByTran(List<string> sqlList)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.Transaction = con.BeginTransaction();
                int result = 0;
                foreach (var sql in sqlList)
                {
                    cmd.CommandText = sql;
                    result += cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();

                return result;


            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("调用事务更新方法时出现异常" + ex.Message);
            }
            finally
            {
                if (cmd.Transaction!=null)
                {
                    cmd.Transaction = null;
                    con.Close();
                }

            }            
        }

        /// <summary>
        /// 执行单一结果（select）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志...

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行结果集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                //将错误信息写入日志...

                throw ex;
            }
        }

        public static SqlDataReader GetReader(string ProcedureName, SqlParameter[] parm)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcedureName;
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                //将错误信息写入日志...

                throw ex;
            }
        }
        /// <summary>
        /// 执行查询返回一个DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);//创建数据适配器对象
            DataSet ds = new DataSet();//创建一个内存数据集
            try
            {
                conn.Open();
                da.Fill(ds);//使用数据适配器填充数据集
                return ds;
            }
            catch (Exception ex)
            {
                //将错误信息写入日志...

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
