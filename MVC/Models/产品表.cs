using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class 产品表
    {
        public String 产品编号 { get; set; }
        public String 产品名称 { get; set; }
        public int 库存数量 { get; set; }
        public String 存储单位 { get; set; }
        public String 类型 { get; set; }
        public int 价格 { get; set; }
    }
}