using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Models
{
    public class 仓库表
    {
        public String 仓库 { get; set; }
        public int 最大货位数 { get; set; }
        public int 可用货位数 { get; set; }
        public int 总库存容量 { get; set; }
        public int 当前库存容量 { get; set; }
    }
}