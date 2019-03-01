using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdatePro
{
    public partial class frmUpdate : Form
    {

        private UpdateManager objUpdateManager = new UpdateManager();

        public frmUpdate()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            this.btnFinish.Visible = false;
            objUpdateManager.ShowProgressDelegate = this.showUpdateProcess;

            List<string[]> filelist=objUpdateManager.NewUpDateInfo.FileList;
            foreach (string[] item in filelist)
            {
                this.lvUpdateList.Items.Add(new ListViewItem(item)); 
            }
            this.lblVersion.Text = objUpdateManager.NewUpDateInfo.Version;
            this.lblLastUpdateTime.Text = objUpdateManager.NewUpDateInfo.UpdateTime.ToString();

        }
        /// <summary>
        /// 根椐委托定义一个同步显示下载百分比的方法
        /// </summary>
        /// <param name="fileIndex">文件序号</param>
        /// <param name="finisedPercent">已经完成下载百分比</param>
        private void showUpdateProcess(int fileIndex, int finisedPercent)
        {
            this.lvUpdateList.Items[fileIndex].SubItems[3].Text = finisedPercent + "%";
            this.pbDownLoadFile.Maximum = 100;
            this.pbDownLoadFile.Value = finisedPercent;

        }

        //完成
        private void btnFinish_Click(object sender, EventArgs e)
        {           
            try
            {
                if(objUpdateManager.copyfiles())
                {
                    Application.ExitThread();
                    Application.Exit();
                    System.Diagnostics.Process.Start("StudentManager.exe");
                }
               
            }
            catch (Exception ex)
            {
                 this.btnClose.Enabled = true;
                 MessageBox.Show(ex.Message);
                 return;
            }

        }
        //下一步
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.btnNext.Enabled = false;
            try
            {
                this.lblUpdateStatus.Text = "正在下载更新文件，请稍后……";
                this.lblTrips.Text = "点击取消可以结束升级……";
                this.btnClose.Enabled = false;
                objUpdateManager.DownloadFiles();

                this.lblTrips.Text = "全部文件下载完成,点击完成结束升级……";
                this.lblUpdateStatus.Visible = false;
                this.btnNext.Visible = false;
                this.btnCancel.Visible = false;

                this.btnFinish.Visible = true;
                this.btnFinish.Location = this.btnCancel.Location;
             //   this.btnClose.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
