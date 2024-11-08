using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class 销售表
    {
        public int 订单号 { get; set; }
        public String 产品名称 { get; set; }
        public String 产品编号 { get; set; }
        public int 销售数量 { get; set; }
        public String 客户编号 { get; set; }
        public String 客户名称 { get; set; }
        public int 订单总额 { get; set; }
        public String 销售时间 { get; set; }
    }
}