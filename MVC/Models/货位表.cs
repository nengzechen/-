using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class 货位表
    {
        public String 货位编号 { get; set; }
        public int 最大库存量 { get; set; }
        public int 已用库存量 { get; set; }
        public int 电子标签信息 { get; set; }
        public String 灯光状态 { get; set; }
        public String 所在库区 { get; set; }
        public String 存放产品 { get; set; }
        public int 绑定单号 { get; set; }

    }
}