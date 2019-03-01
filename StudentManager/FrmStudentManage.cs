using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DAL;
using Models;




namespace StudentManager
{
    public partial class FrmStudentManage : Form
    {
        private StudentClassService objClassService = new StudentClassService();
        private StudentService objStuService = new StudentService();
        public FrmStudentManage()
        {
            InitializeComponent();
            //��ʼ���༶������
            this.cboClass.DataSource = objClassService.GetAllClasses();
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;//Ĭ�ϲ�ѡ��

        }
        //���հ༶��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��༶��", "��ʾ��Ϣ");
                return;
            }
            this.dgvStudentList.AutoGenerateColumns = false;//����ʾδ��װ������
            //ִ�в�ѯ
            this.dgvStudentList.DataSource = objStuService.GetStudentByClass(this.cboClass.Text);

        }
        //����ѧ�Ų�ѯ
        private void btnQueryById_Click(object sender, EventArgs e)
        {
            if (this.txtStudentId.Text.Trim().Length == 0)
            {
                MessageBox.Show("������ѧ�ţ�", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
                return;
            }
            //��һ����֤ѧ�ű��������֣���ʹ��������ʽ...��
            if ( !Common.DataValidate.IsInteger(this.txtStudentId.Text.ToString()))
            {
                MessageBox.Show("�����ѧ�ű���Ϊ���֣�", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
                return;
            }

            //ִ�в�ѯ
            StudentExt objStudent = objStuService.GetStudentById(this.txtStudentId.Text.Trim());
            if (objStudent == null)
            {
                MessageBox.Show("ѧԱ��Ϣ�����ڣ�", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
            }
            else
            {
                //��ѧԱ��ϸ��Ϣ������ʾ
                FrmStudentInfo objFrmStuInfo = new FrmStudentInfo(objStudent);
                objFrmStuInfo.Show();
            }

        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && this.txtStudentId.Text.Trim().Length != 0)
            {
                btnQueryById_Click(null, null);
            }
        }
        //˫��ѡ�е�ѧԱ������ʾ��ϸ��Ϣ
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvStudentList.CurrentRow != null)
            {
                string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
                this.txtStudentId.Text = studentId;
                btnQueryById_Click(null, null);
            }
        }
        private void tsmiModifyStu_Click(object sender, EventArgs e)
        {
            btnEidt_Click(null, null);
        }
        //�޸�ѧԱ����
        private void btnEidt_Click(object sender, EventArgs e)
        {
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("û���κ�Ҫ�޸ĵ�ѧ��Ϣ��", "��ʾ��Ϣ");
                return;
            }
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("��ѡ��Ҫ�޸ĵ�ѧԱ��Ϣ��", "��ʾ��Ϣ");
                return;
            }
            //��ȡѧ��
            //string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            //��ȡҪ�޸ĵ�ѧԱ��ϸ��Ϣ
            StudentExt objStudent = objStuService.GetStudentById(studentId);
            //��ʾҪ�޸ĵ�ѧԱ��Ϣ����
            FrmEditStudent objEditStudent = new FrmEditStudent(objStudent);
            DialogResult result = objEditStudent.ShowDialog();
            //�ж��޸��Ƿ��
            if (result == DialogResult.OK)
            {
                btnQuery_Click(null, null);//ͬ��ˢ���޸ĵ���Ϣ���ʺϲ�ѯ������С�������
            }

        }
        //ɾ��ѧԱ����
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("û���κ�Ҫɾ����ѧ��Ϣ��", "��ʾ��Ϣ");
                return;
            }
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("��ѡ��Ҫɾ����ѧԱ��Ϣ��", "��ʾ��Ϣ");
                return;
            }
            //ɾ��ȷ��
            string studentName = this.dgvStudentList.CurrentRow.Cells["StudentName"].Value.ToString();
            DialogResult result = MessageBox.Show("ȷ��Ҫɾ��ѧԱ  [" + studentName + "]  ��", "ɾ��ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel) return;
            //��ȡѧ�Ų�ɾ��
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            try
            {
                if (objStuService.DeleteStudentById(studentId) == 1)
                {
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ʾ��Ϣ");
            }
        }
        private void FrmSearchStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.objFrmStuManage = null;
        }
        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvStudentList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==46)
            {
                btnDel_Click(null,null);
            }
                    
                    //����0 - 9��48 - 57
                    //�����ͷ���������ҷֱ�Ϊ��37��38��39��40
                    //ESC����27
                    //�س�����13
                    //Backspace�˸�ɾ������8
                    //Deleteɾ������46
                    //С���㣺46
        }

        private void tsmidDeleteStu_Click(object sender, EventArgs e)
        {
            btnDel_Click(null, null);
        }
    }
}