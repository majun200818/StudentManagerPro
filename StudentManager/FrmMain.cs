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

            //��ʾ��¼�û���
            this.lblCurrentUser.Text = Program.objCurrentAdmin.AdminName + "]";

        }

        public static FrmAddStudent objFrmAddStudent = null;
        //������֤�����Ψһ��
        private void tsmiAddStudent_Click(object sender, EventArgs e)
        {
            if (objFrmAddStudent == null)
            {
                objFrmAddStudent = new FrmAddStudent();
                objFrmAddStudent.Show();
             
            }
            else
            {
                objFrmAddStudent.Activate();//����ֻ������С����ʱ��������
                objFrmAddStudent.WindowState = FormWindowState.Normal;
            }
        }
        //ѧԱ����
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
        //��ʾ�ɼ���ѯ���������
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
        //�˳�ϵͳ
        private void tmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //�ɼ����ٲ�ѯ
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
                objFrmScore.Activate();//����ֻ������С����ʱ��������
                objFrmScore.WindowState = FormWindowState.Normal;
            }         
        }
        //�����޸�
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
            DialogResult result = MessageBox.Show("ȷ���˳���", "�˳�ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                //ȡ����ǰ�Ĺرղ���
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
                MessageBox.Show(" �޷��ӽ�������������������� "+ex.Message,"��ʾ��Ϣ");
                return;
            }
            if (!objUpdateManager.isUpdate)
            {
                MessageBox.Show(" ��ǰ�汾�Ѿ������°汾������Ҫ������", "��ʾ��Ϣ");
                return; 
            }

            if (MessageBox.Show("Ϊ�˸����ļ�,���˳���ǰ������ȷ�������Ѿ����棡ȷ���˳���", "ѯ��",
              MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
              DialogResult.Cancel) return;
            Application.Exit();
            //������������
            System.Diagnostics.Process.Start("UpdatePro.exe");
        }



    }
}