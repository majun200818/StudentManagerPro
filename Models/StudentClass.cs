﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    [Serializable]
    /// <summary>
    /// 班级实体类
    /// </summary>
    public class StudentClass
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}
