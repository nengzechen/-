using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult 登录()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        //入库表的控制器
        public ActionResult 入库表()
        {
            return View();
        }
        public ActionResult 入库表DataGrid(DataGridParam param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.入库表> info = bll.GetRKInfo(page, rows);
            int count = bll.GetRKInfoCount();
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult 入库表UpDate(Models.入库表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateRKInfo(param);
            return View();
        }
        public ActionResult 入库表DataJson(DataGridParam param, String id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.入库表> info = bll.GetRKDataJson(page, rows, id);
            int count = bll.GetRKInfoCount1(id);
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 入库表DataJson1()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.货位表> info = bll.入库表DataJson1();
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        /*  public ActionResult 入库表DataJson(String number)
          {
              BLL.BLLOperator bll = new BLL.BLLOperator();
              IList<Models.仓库表> info = bll.GetRKDataJson(number);
              return Json(info, JsonRequestBehavior.AllowGet);
          }
          public ActionResult 入库表DataJson1(String id)
          {
              BLL.BLLOperator bll = new BLL.BLLOperator();
              IList<Models.货位表> info = bll.GetRKDataJson1(id);
              return Json(info, JsonRequestBehavior.AllowGet);
          }*/
        // 出库控制器-----------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult 出库表()
        {
            return View();
        }
        public ActionResult 出库表DataGrid(DataGridParam param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.出库表> info = bll.GetCKInfo(page, rows);
            int count = bll.GetCKInfoCount();
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult 出库表UpDate(Models.出库表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateCKInfo(param);
            return View();
        }
        public ActionResult 出库表DataJson(DataGridParam param, String id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.出库表> info = bll.GetCKDataJson(page, rows, id);
            int count = bll.GetCKInfoCount1(id);
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 出库表DataJson1()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.货位表> info = bll.出库表DataJson1();
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        //采购表的控制器----------------------------------------------------------------------------------------------------------
        public ActionResult 采购表()
        {

            return View();
        }
        public ActionResult 采购表DataGrid(DataGridParam param,String id,String id1)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.采购表> info = bll.GetCGInfo(page, rows,id,id1);
            int count = bll.GetCGInfoCount(id,id1);
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int 采购表Insert(Models.采购表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.InsertCGInfo(param);

        }
        [HttpPost]
        public ActionResult 采购表UpDate(Models.采购表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateCGInfo(param);
            return View();

        }
        [HttpPost]
        public int 采购表Delete(int id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.DeleteCGInfo(id);

        }
        [HttpGet]
        public int 采购表Last()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.GetLastCGInfo();
        }
        public ActionResult 采购表DataJson1(String number)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.产品表> info = bll.采购表DataJson1(number);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 采购表DataJson2(String number)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.供应商表> info = bll.采购表DataJson2(number);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 采购表DataJson3(String number)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.产品表> info = bll.采购表DataJson3(number);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        // 以下是销售表控制器---------------------------------------------------------------------------------------------------------------
        public ActionResult 销售表()
        {

            return View();
        }
        public ActionResult 销售表DataGrid(DataGridParam param,String id,String id1)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.销售表> info = bll.GetXSInfo(page, rows,id,id1);
            int count = bll.GetXSInfoCount(id,id1);
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int 销售表Insert(Models.销售表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.InsertXSInfo(param);


        }
        [HttpPost]
        public ActionResult 销售表UpDate(Models.销售表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateXSInfo(param);
            return View();

        }
        [HttpPost]
        public int 销售表Delete(int id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.DeleteXSInfo(id);

        }
        [HttpGet]
        public int 销售表Last()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.GetLastXSInfo();
        }
        public ActionResult 销售表DataJson1(String number)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.产品表> info = bll.销售表DataJson1(number);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 销售表DataJson2(String number)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.客户表> info = bll.销售表DataJson2(number);
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        // 以下是货位表内容------------------------------------------------------------------------------------------------------
        public ActionResult 货位表()
        {
            return View();
        }
        public ActionResult 货位表DataGrid(DataGridParam param,String number)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.货位表> info = bll.GetHWInfo(page, rows,number);
            int count = bll.GetHWInfoCount();
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /*
        [HttpPost]
        public ActionResult 货位表Insert(Models.货位表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.InsertHWInfo(param);
            return View();

        }
        [HttpPost]
        public ActionResult 货位表UpDate(Models.货位表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateHWInfo(param);
            return View();

        }
        [HttpPost]
        public int 货位表Delete(int id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.DeleteHWInfo(id);

        }
        [HttpGet]
        public int 货位表Last()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.GetLastHWInfo();
        }
        // 把后台货位编号读出来 要求货位类型是等待出入
        public ActionResult 货位表DataJson(String number)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.货位表> info = bll.GetHWDataJson(number);
            return Json(info, JsonRequestBehavior.AllowGet);
        }*/
        // 货位表控制器截止到这-----------------------------------------------------------------------------------------------

        //移库表控制器开始-------------------------------------------------
        public ActionResult 移库表()
        {
            return View();
        }
        public ActionResult 移库表DataGrid(DataGridParam param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.移库表> info = bll.GetYKInfo(page, rows);
            int count = bll.GetYKInfoCount();
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult 移库表Insert(Models.移库表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.InsertYKInfo(param);
            return View();

        }
        public int 移库表Last()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.GetLastYKInfo();
        }
        public ActionResult 移库表DataJson1()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.货位表> info=bll.GetYKDataJson1();
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 移库表UpDate(Models.移库表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateYKInfo(param);
            return View();

        }
        public ActionResult 移库表DataJson(DataGridParam param, String id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.移库表> info = bll.GetYKDataJson(page, rows, id);
            int count = bll.GetYKInfoCount1(id);
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /*public ActionResult 移库表UpDate(Models.移库表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateYKInfo(param);
            return View();

        }

        [HttpGet]
        public int 移库表Last()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.GetLastYKInfo();
        }
        // 根据前台搜索框的状态值来返回对应数据
        public ActionResult 移库表DataJson(DataGridParam param, String id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.移库表> info = bll.GetYKDataJson(page, rows, id);
            int count = bll.GetYKInfoCount1(id);
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        */
        // 以下是产品表控制器内容------------------------------------------------------------------------------------------------------------------
        public ActionResult 产品表()
        {
            return View();
        }
        public ActionResult 产品表DataGrid(DataGridParam param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.产品表> info = bll.GetCPInfo(page, rows);
            int count = bll.GetCPInfoCount();
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 产品表Insert(Models.产品表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.InsertCPInfo(param);
            return View();
        }
        public ActionResult 产品表UpDate(Models.产品表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateCPInfo(param);
            return View();

        }
        [HttpPost]
        public int 产品表Delete(int id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.DeleteCPInfo(id);

        }
        public ActionResult 产品表DataJson()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.产品表> result = bll.产品表DataJson();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // 以下是供应商表控制器---------------------------------------------------------------------------
        public ActionResult 供应商表()
        {
            return View();
        }
        public ActionResult 供应商表DataGrid(DataGridParam param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.供应商表> info = bll.GetGYSInfo(page, rows);
            int count = bll.GetGYSInfoCount();
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 供应商表Insert(Models.供应商表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.InsertGYSInfo(param);
            return View();
        }
        public ActionResult 供应商表UpDate(Models.供应商表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateGYSInfo(param);
            return View();

        }
        [HttpPost]
        public int 供应商表Delete(int id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.DeleteGYSInfo(id);

        }

        // 以下是客户表控制器------------------------------------------------------------------------------
        public ActionResult 客户表()
        {
            return View();
        }
        public ActionResult 客户表DataGrid(DataGridParam param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            int page = param.page;
            int rows = param.rows;
            IList<Models.客户表> info = bll.GetKHInfo(page, rows);
            int count = bll.GetGYSInfoCount();
            var result = new
            {
                total = count,
                rows = info
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult 客户表Insert(Models.客户表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.InsertKHInfo(param);
            return View();
        }
        public ActionResult 客户表UpDate(Models.客户表 param)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            bll.UpDateKHInfo(param);
            return View();

        }
        [HttpPost]
        public int 客户表Delete(int id)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.DeleteKHInfo(id);

        }
        // --------------------------------
        public ActionResult store()
        {
            return View();
        }
        // ----------------------------------------------------------------------------------
        public ActionResult 仓库1()
        {
            return View();
        }
        public ActionResult 仓库1DataJson()
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.货位表> result = bll.仓库1DataJson();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public int 仓库1UpDate(String id,String status,String name)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            return bll.仓库1UpDate(id, status,name);
            

        }

        // 以下是登录控制器----------------------------------------------------------------------------------------------------------------------------
        public ActionResult Login(String user, String pass)
        {
            BLL.BLLOperator bll = new BLL.BLLOperator();
            IList<Models.用户表> result = bll.Login(user,pass);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}