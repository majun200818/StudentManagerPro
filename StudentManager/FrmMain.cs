using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UpdatePro;

namespace StudentManager
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            //显示登录用户名
            this.lblCurrentUser.Text = Program.objCurrentAdmin.AdminName + "]";

        }

        public static FrmAddStudent objFrmAddStudent = null;
        //怎样保证对象的唯一性
        private void tsmiAddStudent_Click(object sender, EventArgs e)
        {
            if (objFrmAddStudent == null)
            {
                objFrmAddStudent = new FrmAddStudent();
                objFrmAddStudent.Show();
             
            }
            else
            {
                objFrmAddStudent.Activate();//激活只能在最小化的时候起作用
                objFrmAddStudent.WindowState = FormWindowState.Normal;
            }
        }
        //学员管理
        public static FrmStudentManage objFrmStuManage = null;
        private void tsmiManageStudent_Click(object sender, EventArgs e)
        {
            if (objFrmStuManage == null)
            {
                objFrmStuManage = new FrmStudentManage();
                objFrmStuManage.Show();
            }
            else
            {
                objFrmStuManage.Activate();
                objFrmStuManage.WindowState = FormWindowState.Normal;
            }
        }
        //显示成绩查询与分析窗口
        public static FrmScoreManage objFrmScoreManage = null;
        private void tsmiQueryAndAnalysis_Click(object sender, EventArgs e)
        {
            if (objFrmScoreManage == null)
            {
                objFrmScoreManage = new FrmScoreManage();
                objFrmScoreManage.Show();
            }
            else
            {
                objFrmScoreManage.Activate();
                objFrmScoreManage.WindowState = FormWindowState.Normal;
            }
        }
        //退出系统
        private void tmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //成绩快速查询
        public static FrmScoreQuery objFrmScore = null;
        private void tsmiQuery_Click(object sender, EventArgs e)
        {
            if (objFrmScore == null)
            {
                objFrmScore = new FrmScoreQuery();
                objFrmScore.Show();
            }
            else
            {
                objFrmScore.Activate();//激活只能在最小化的时候起作用
                objFrmScore.WindowState = FormWindowState.Normal;
            }         
        }
        //密码修改
        public static FrmModifyPwd objFrmModifyPwd = null;
        private void tmiModifyPwd_Click(object sender, EventArgs e)
        {
            if (objFrmModifyPwd == null)
            {
                objFrmModifyPwd = new FrmModifyPwd();
                objFrmModifyPwd.Show();
            }
            else
            {
                objFrmModifyPwd.Activate();
                objFrmModifyPwd.WindowState = FormWindowState.Normal;
            }
        }

        private void tsbAddStudent_Click(object sender, EventArgs e)
        {
            tsmiAddStudent_Click(null, null);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tsmiManageStudent_Click(null, null);
        }
        private void tsbScoreAnalysis_Click(object sender, EventArgs e)
        {
            tsmiQueryAndAnalysis_Click(null, null);
        }
        private void tsbModifyPwd_Click(object sender, EventArgs e)
        {
            tmiModifyPwd_Click(null, null);
        }

        private void tsbQuery_Click(object sender, EventArgs e)
        {
            tsmiQuery_Click(null, null);
        }

        public static FrmAttendance objFrmAttendance = null;
        private void tsmi_Card_Click(object sender, EventArgs e)
        {
            if (objFrmAttendance == null)
            {
                objFrmAttendance = new FrmAttendance();
                objFrmAttendance.Show();
            }
            else
            {
                objFrmAttendance.Activate();
                objFrmAttendance.WindowState = FormWindowState.Normal;
            }
        }
        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出吗？", "退出询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                //取消当前的关闭操作
                e.Cancel = true;
            }
        }

        private void tmiUpdate_Click(object sender, EventArgs e)
        {
            UpdateManager objUpdateManager = null;            
            try
            {
                objUpdateManager = new UpdateManager();

            }
            catch (Exception ex)
            {
                MessageBox.Show(" 无法接结服务器，请检查网络设置 "+ex.Message,"提示信息");
                return;
            }
            if (!objUpdateManager.isUpdate)
            {
                MessageBox.Show(" 当前版本已经是最新版本，不需要升级！", "提示信息");
                return; 
            }

            if (MessageBox.Show("为了更新文件,将退出当前程序，请确保数据已经保存！确认退出吗？", "询问",
              MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
              DialogResult.Cancel) return;
            Application.Exit();
            //启动升级程序
            System.Diagnostics.Process.Start("UpdatePro.exe");
        }



    }
}