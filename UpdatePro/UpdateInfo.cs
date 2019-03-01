using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatePro
{
    /// <summary>
    /// 更新信息实体类
    /// </summary>
    public class UpdateInfo
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 上次更新时间 
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 最新文件存放的服务器url
        /// </summary>
        public string UpdateFileUrl { get; set; }
        /// <summary>
        ///要更新的文件列表[和listView对应]
        /// </summary>
        public List<string[]> FileList { get; set; }



    }
}
