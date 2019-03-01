using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Net;
using System.Windows.Forms;

namespace UpdatePro
{
  public class UpdateManager
    {
      public UpdateManager()
      {
         //给对象类型 的属性初始化
          this.LastUpDateInfo = new UpdateInfo();
          this.NewUpDateInfo = new UpdateInfo();

        //给属性 
          this.GetLastUpDateInfo();
          this.GetNewUpDateInfo();


      }

      //属性
      /// <summary>
      /// 上次的更新的信息
      /// </summary>
      public UpdateInfo LastUpDateInfo { get; set; }

      /// <summary>
      ///  最新更新信息
      /// </summary>
      public UpdateInfo NewUpDateInfo { get; set; }

      /// <summary>
      /// 是否需要更新
      /// </summary>
      public bool isUpdate       
      {
          get 
          {
              DateTime dt1 = Convert.ToDateTime(this.LastUpDateInfo.UpdateTime);
              DateTime dt2 =Convert.ToDateTime( this.NewUpDateInfo.UpdateTime);
              return dt2 > dt1;
              
          } 
      }
      public string TempFilePath 
      {
          get
          {
              string newTempPath = Environment.GetEnvironmentVariable("Temp") + "\\UpDateFiles";
              if ( !Directory.Exists(newTempPath))
              {
                  Directory.CreateDirectory(newTempPath); 
              }
              return newTempPath;
          
          }
          
           }

      //方法
      /// <summary>
      /// 从本地获取上次更新信息 并封装到LastUpDateInfo中
      /// </summary>
      private void GetLastUpDateInfo()
      {
          FileStream myFile = new FileStream("UpDateList.xml", FileMode.Open);
          XmlTextReader  xmlReader = new XmlTextReader(myFile);
          while (xmlReader.Read())
          {
              switch (xmlReader.Name)
              {
                  case "URLAddress":
                      this.LastUpDateInfo.UpdateFileUrl = xmlReader.GetAttribute("URL");
                      break;
                  case "Version":
                      this.LastUpDateInfo.Version = xmlReader.GetAttribute("Num");
                      break;
                  case "UpdateTime":
                      this.LastUpDateInfo.UpdateTime =Convert.ToDateTime(xmlReader.GetAttribute("Date"));
                      break;
                  default :
                      break;
              }
          }
          xmlReader.Close();
          myFile.Close();
      }

      /// <summary>
      ///  从远程服务器电脑获取上次更新信息 并封装到newUpDateInfo中
      /// </summary>

      private void GetNewUpDateInfo()
      {

          string newXmlTempPath = this.TempFilePath + "\\UpDateList.xml";
          WebClient objClient = new WebClient();
          objClient.DownloadFile(this.LastUpDateInfo.UpdateFileUrl + "UpDateList.xml", newXmlTempPath);

          this.NewUpDateInfo.FileList = new List<string[]>();

          FileStream myFile = new FileStream(newXmlTempPath, FileMode.Open);
          XmlTextReader xmlReader = new XmlTextReader(myFile);
          while (xmlReader.Read())
          {
              switch (xmlReader.Name)
              {
                  //case "URLAddress":UpdateFileList
                  //    this.NewUpDateInfo.UpdateFileUrl = xmlReader.GetAttribute("URL");
                  //    break;
                  case "Version":
                      this.NewUpDateInfo.Version = xmlReader.GetAttribute("Num");
                      break;
                  case "UpdateTime":
                      this.NewUpDateInfo.UpdateTime = Convert.ToDateTime(xmlReader.GetAttribute("Date"));
                      break;
                  case "UpdateFile":
                       string Ver=xmlReader.GetAttribute("Ver");;
                       string FileName=xmlReader.GetAttribute("FileName");
                       string ContentLength=xmlReader.GetAttribute("ContentLength");
                       this.NewUpDateInfo.FileList.Add
                           (new string[] { FileName, ContentLength, Ver,"0" });
                      break;
                  default:
                      break;
              }
          }
          xmlReader.Close();
          myFile.Close();
      }


      public delegate void ShowUpdateProgress(int fileIndex, int dowLoadPrcent);
      public ShowUpdateProgress   ShowProgressDelegate; 
      /// <summary>
      /// 下载所有需要更新的文件（根椐updatelist）并显示百分比
      /// </summary>
      public void DownloadFiles()
      {
          List<string[]> fileList= this.NewUpDateInfo.FileList;
          for (int i = 0; i < fileList.Count; i++)
          {
              string filename = fileList[i][0];//要下载的文件名
              string fileUrl = this.LastUpDateInfo.UpdateFileUrl + filename;//要下载的文件Url
              WebRequest objWebRequest = WebRequest.Create(fileUrl); //根据文件的URl 连结服务器， 创建请求对象
              WebResponse objWebResponse = objWebRequest.GetResponse();//根据请求对象 创建响应对象
              Stream objStream = objWebResponse.GetResponseStream();//通过响应对象，返回数据流对象（相当于石油）
              StreamReader objStreamReader = new StreamReader(objStream);//相当于管道

              long fileLenth = objWebResponse.ContentLength;
              byte[] bufferByte = new byte[fileLenth];
              int allByte = bufferByte.Length;
              int startByte = 0;
              while (fileLenth>0)
              {
                  Application.DoEvents();//表示在一线程中允许处理其它事件
                  int downloadByte = objStream.Read(bufferByte, startByte, allByte);
                  if (downloadByte==0)
                  {
                      break;
                  }
                  startByte += downloadByte;
                  allByte -= downloadByte;

                  float part =(float)startByte/1024;
                  float total = (float)bufferByte.Length/1024;
                  int percent = Convert.ToInt32(100 * part/total);
                  ShowProgressDelegate(i, percent);

              }
              //保存读取完的文件
              string newfileName = this.TempFilePath + "\\" + filename;
              FileStream fs = new FileStream(newfileName, FileMode.OpenOrCreate, FileAccess.Write);
              fs.Write(bufferByte, 0, allByte);

              objStream.Close();
              objStreamReader.Close();
              fs.Close();
          
          }

      
      }
    
      /// <summary>
      /// 将下载的文件从临时目录copy到应用程序下，
      /// </summary>
      /// <returns></returns>
      public bool copyfiles()
      {
          string[] files = Directory.GetFiles(TempFilePath);
          foreach (string name in files)
          {
              string currentfile = name.Substring(name.LastIndexOf(@"\")+1);
         
          //如果存在则先删除文件
          if (File.Exists(currentfile))
          {
              File.Delete(currentfile);
          }

          File.Copy(name, currentfile);
         }
          return true;
      }
    }
}
