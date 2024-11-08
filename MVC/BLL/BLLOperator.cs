using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WMS.BLL
{
    public class BLLOperator
    {
        //以下是入库表的bll
        public IList<Models.入库表> GetRKInfo(int page, int rows)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetRKInfo(page, rows);
        }
        public int GetRKInfoCount()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetRKInfoCount();
        }
        public void UpDateRKInfo(Models.入库表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateRKInfo(param);
        }
        public IList<Models.入库表> GetRKDataJson(int page, int rows, String id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetRKDataJson(page, rows, id);
        }
        public int GetRKInfoCount1(String id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetRKInfoCount1(id);
        }
        public IList<Models.货位表> 入库表DataJson1()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.入库表DataJson1();
        }
        // 以下是出库表的bll------------------------------------------------------------------------------------------------------------------------------------
        public IList<Models.出库表> GetCKInfo(int page, int rows)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCKInfo(page, rows);
        }
        public int GetCKInfoCount()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCKInfoCount();
        }
        public void UpDateCKInfo(Models.出库表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateCKInfo(param);
        }
        public IList<Models.出库表> GetCKDataJson(int page, int rows, String id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCKDataJson(page, rows, id);
        }
        public int GetCKInfoCount1(String id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCKInfoCount1(id);
        }
        public IList<Models.货位表> 出库表DataJson1()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.出库表DataJson1();
        }
        /* public IList<Models.仓库表> GetRKDataJson(String number)
         {
             DAL.DALOperator dal = new DAL.DALOperator();
             return dal.GetRKDataJson(number);
         }
         public IList<Models.货位表> GetRKDataJson1(String id)
         {
             DAL.DALOperator dal = new DAL.DALOperator();
             return dal.GetRKDataJson1(id);
         }*/
        //以下是采购内容的bll----------------------------------------------------------------------------------------------------------------------
        public IList<Models.采购表> GetCGInfo(int page,int rows,String id,String id1)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCGInfo(page,rows,id,id1);
        }
        public int GetCGInfoCount(String id, String id1)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCGInfoCount(id,id1);
        }
        public int InsertCGInfo(Models.采购表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.InsertCGInfo(param);
        }
        public void UpDateCGInfo(Models.采购表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateCGInfo(param);
        }
        public int DeleteCGInfo(int id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.DeleteCGInfo(id);
        }
        public int GetLastCGInfo()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetLastCGInfo();
        }
        // 这个方法用来返回产品表信息！！1
        public IList<Models.产品表> 采购表DataJson1(String number)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.采购表DataJson1(number);
        }
        public IList<Models.供应商表> 采购表DataJson2(String number)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.采购表DataJson2(number);
        }
        public IList<Models.产品表> 采购表DataJson3(String number)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.采购表DataJson3(number);
        }
        // 以上是采供内容bll----------------------------------------------------------------------
        // 以下是销售表bll-------------------------------------------------------------------------------------------------------
        public IList<Models.销售表> GetXSInfo(int page, int rows,String id, String id1)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetXSInfo(page, rows,id,id1);
        }
        public int GetXSInfoCount(String id, String id1)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetXSInfoCount(id,id1);
        }
        public int InsertXSInfo(Models.销售表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.InsertXSInfo(param);
        }
        public void UpDateXSInfo(Models.销售表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateXSInfo(param);
        }
        public int DeleteXSInfo(int id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.DeleteXSInfo(id);
        }
        public int GetLastXSInfo()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetLastXSInfo();
        }
        // 这个方法用来返回产品表信息！！1
        public IList<Models.产品表> 销售表DataJson1(String number)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.销售表DataJson1(number);
        }
        public IList<Models.客户表> 销售表DataJson2(String number)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.销售表DataJson2(number);
        }
        //以下是货位内容bll------------------------------------------------------------------------------------------------
        public IList<Models.货位表> GetHWInfo(int page, int rows,String number)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetHWInfo(page, rows,number);
        }
        public int GetHWInfoCount()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetHWInfoCount();
        }
        /*
        public void InsertHWInfo(Models.货位表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.InsertHWInfo(param);
        }
        public void UpDateHWInfo(Models.货位表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateHWInfo(param);
        }
        public int DeleteHWInfo(int id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.DeleteHWInfo(id); ;
        }
        public int GetLastHWInfo()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetLastHWInfo();
        }
        public IList<Models.货位表> GetHWDataJson(String number)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetHWDataJson(number);
        }
        */
        // 以上是货位内容bll-----------------------------------------------------------------------------------------------------

        //以下是移库表内容bll
        public IList<Models.移库表> GetYKInfo(int page, int rows)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetYKInfo(page, rows);
        }
        public int GetYKInfoCount()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetYKInfoCount();
        }
        public void InsertYKInfo(Models.移库表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.InsertYKInfo(param);
        }
        public int GetLastYKInfo()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetLastYKInfo();
        }
        public IList<Models.货位表> GetYKDataJson1()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetYKDataJson1();
        }
        public void UpDateYKInfo(Models.移库表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateYKInfo(param);
        }
        public IList<Models.移库表> GetYKDataJson(int page, int rows, String id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetYKDataJson(page, rows, id);
        }
        public int GetYKInfoCount1(String id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetYKInfoCount1(id);
        }
            // 以下是产品表bll-----------------------------------------------------------------------------------------------------
            public IList<Models.产品表> GetCPInfo(int page, int rows)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCPInfo(page, rows);
        }
        public int GetCPInfoCount()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetCPInfoCount();
        }
        public void InsertCPInfo(Models.产品表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.InsertCPInfo(param);
        }
        public void UpDateCPInfo(Models.产品表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateCPInfo(param);
        }
        public int DeleteCPInfo(int id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.DeleteCPInfo(id); ;
        }
        public IList<Models.产品表> 产品表DataJson()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.产品表DataJson();
        }
        // 以下是供应商编号的bll------------------------------------------------
        public IList<Models.供应商表> GetGYSInfo(int page, int rows)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetGYSInfo(page, rows);
        }
        public int GetGYSInfoCount()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetGYSInfoCount();
        }
        public void InsertGYSInfo(Models.供应商表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.InsertGYSInfo(param);
        }
        public void UpDateGYSInfo(Models.供应商表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateGYSInfo(param);
        }
        public int DeleteGYSInfo(int id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.DeleteGYSInfo(id); ;
        }

        // 以下是客户表bll-----------------------------------------------------------------------------------------
        public IList<Models.客户表> GetKHInfo(int page, int rows)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetKHInfo(page, rows);
        }
        public int GetKHInfoCount()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.GetKHInfoCount();
        }
        public void InsertKHInfo(Models.客户表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.InsertKHInfo(param);
        }
        public void UpDateKHInfo(Models.客户表 param)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            dal.UpDateKHInfo(param);
        }
        public int DeleteKHInfo(int id)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.DeleteKHInfo(id); ;
        }
        // 仓库1----------------------------
        public IList<Models.货位表> 仓库1DataJson()
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.仓库1DataJson();
        }
        public int 仓库1UpDate(String id,String status,String name)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.仓库1UpDate(id, status,name);
        }

        // 以下是登录bll-------------------------------------------------------------------------------------------------------------
        public IList<Models.用户表> Login(String user, String pass)
        {
            DAL.DALOperator dal = new DAL.DALOperator();
            return dal.Login(user, pass);
        }
    }
}