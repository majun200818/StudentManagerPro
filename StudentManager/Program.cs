using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



namespace StudentManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //创建登录窗体
            FrmUserLogin objFrmLogin = new FrmUserLogin();
            DialogResult result = objFrmLogin.ShowDialog();


            //判断登录是否成功
            if (result == DialogResult.OK)
                Application.Run(new FrmMain());
            else
                Application.Exit();//退出整个应用程序

            //List<string> strList = new List<string>()
            //{
            //    "delete FROM Students WHERE StudentId=100010",
            //    "delete FROM Students WHERE StudentId=100013",
            //    "delete FROM Students WHERE StudentId=100014",
            //    "delete FROM Students WHERE StudentId=100011"
            //};
            //string sql = "select  COUNT(*)  from  dbo.Students";
            //Console.WriteLine("删除前学生总数为：{0}",SQLHelper.GetSingleResult(sql).ToString());
            //Console.WriteLine("-------------------------------");
            //int result = 0;
            //try
            //{
            //    result = SQLHelper.UpdataByTran(strList);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("*****************************************");
            //    throw new Exception(ex.Message);
            //}
            //if (result>0)
            //{
            //    Console.WriteLine("success!");

            //}
            //else
            //{
            //    Console.WriteLine("删除失败!");
            //    Console.WriteLine("-------------------------------");
            //    Console.WriteLine("删除后学生总数为：{0}", SQLHelper.GetSingleResult(sql).ToString());

            //}

        }

        //定义一个全局变量保存用户对象
         public static Models.SysAdmin objCurrentAdmin = null;

    }
}
