using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class 入库表
    {
        public int 入库单号 { set; get; }
        public String 产品名称 { set; get; }
        public String 产品编号 { set; get; }
        public int 电子标签入库数 { set; get; }
        public String 目标货位号 { set; get; }
        public String 入库人 { set; get; }
        public String 入库时间 { set; get; }
        public String 入库状态 { set; get; }
    }
}