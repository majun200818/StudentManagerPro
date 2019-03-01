using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class AttendanceService
    {
        /// <summary>
        /// 添加打卡记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string AddRecord(string cardNo)
        {
            string sql = "insert into Attendance (CardNo) values('{0}')";
            sql = string.Format(sql, cardNo);
            try
            {
                SQLHelper.Update(sql);
                return "success";
            }
            catch (Exception ex)
            {
                return "打卡失败！系统出现问题，请联系管理员！" + ex.Message;
            }
        }
    }
}
