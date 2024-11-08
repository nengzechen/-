using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class 移库表
    {
        public int 移库单号 { get; set; }
        public int 电子标签移库数 { get; set; }
        public String 目标货位号 { get; set; }
        public String 当前货位号 { get; set; }
        public String 移库人 { get; set; }
        public String 移库时间 { get; set; }
        public String 移库状态{ get; set; }

    }
}