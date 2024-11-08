using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class 采购表
    {
        public int 订单号 { get; set; }
        public String 产品名称 { get; set; }
        public String 产品编号 { get; set; }
        public int 采购数量 { get; set; }
        public String 供应商编号 { get; set; }
        public String 供应商名 { get; set; }
        public int 订单总额 { get; set; }
        public String 采购时间 { get; set; }
    }
}