using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WMS.DAL
{
    public class DALOperator
    {
        //以下是入库表的dal
        public IList<Models.入库表> GetRKInfo(int page, int rows)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 入库表 limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.入库表>(sql);
        }
        public int GetRKInfoCount()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 入库表";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public void UpDateRKInfo(Models.入库表 param)
        {
            int rk_id = param.入库单号;
            String rk_loction_id = param.目标货位号;
            String rk_person = param.入库人;
            String rk_time = param.入库时间;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 入库表 set 目标货位号='" + rk_loction_id + "',入库人='" + rk_person + "',入库时间='" + rk_time + "',入库状态= '入库成功' where 入库单号=" + rk_id + "";
            sqlHelper.Query(sql);

            int rk_count = param.电子标签入库数;// 把入库数和数据库里当前容量相加 且不能超过总量 
            String sql1 = "update 货位表 set 已用库存量=已用库存量+'" + rk_count + "'where 货位编号='" + rk_loction_id + "'";
            sqlHelper.Query(sql1);

            // 给产品表对应产品名称增加数量
            String product_id = param.产品编号;
            String sql0 = "update 产品表 set 库存数量=库存数量+"+rk_count+" where 产品编号='" + product_id + "'";
            sqlHelper.Query(sql0);

            // 手动更改待入货位号才会产生移库表
            String sql2 = "update 货位表 set 电子标签信息='0',灯光状态 = case when(已用库存量 > 最大库存量) then '移库'  else '默认' end where 货位编号 ='" + rk_loction_id + "'";
            sqlHelper.Query(sql2);

        }
        public IList<Models.入库表> GetRKDataJson(int page, int rows, String id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 入库表  where 入库状态='" + id + "' limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.入库表>(sql);
        }
        public int GetRKInfoCount1(String id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 入库表 where 入库状态='" + id + "'";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public IList<Models.货位表> 入库表DataJson1()
        {
            
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 货位表 where 灯光状态='默认'";
            return sqlHelper.ExecuteQuery<Models.货位表>(sql);
            
        }
        // 以下是出库表的dal-------------------------------------------------------------------------------------------------------------------------------------
        public IList<Models.出库表> GetCKInfo(int page, int rows)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 出库表 limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.出库表>(sql);
        }
        public int GetCKInfoCount()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 出库表";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public void UpDateCKInfo(Models.出库表 param)
        {
            int rk_id = param.出库单号;
            String rk_loction_id = param.目标货位号;
            String rk_person = param.出库人;
            String rk_time = param.出库时间;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 出库表 set 目标货位号='" + rk_loction_id + "',出库人='" + rk_person + "',出库时间='" + rk_time + "',出库状态= '出库成功' where 出库单号=" + rk_id + "";
            sqlHelper.Query(sql);

            int rk_count = param.电子标签出库数;// 把入库数和数据库里当前容量相加 且不能超过总量 
            String sql1 = "update 货位表 set 已用库存量=已用库存量-'" + rk_count + "'where 货位编号='" + rk_loction_id + "'";
            sqlHelper.Query(sql1);

            // 手动出库完成之后货位表灯光状态更改，电子标签信息更改
            String sql2 = "update 货位表 set 电子标签信息='0',灯光状态='默认' where 货位编号='" + rk_loction_id + "'";
            sqlHelper.Query(sql2);
            /* 出库表不应该产生一个移库单，应该产生的是补货单也就是采购单！！！！！！！！！！！
            String sql2 = "update 货位表 set 电子标签信息=" + rk_count + " ,灯光状态 = case when(已用库存量 > 最大库存量) then '移库'  else '默认' end where 货位编号 ='" + rk_loction_id + "'";
            sqlHelper.Query(sql2);*/

            // 出库完了之后要给产品表 对应的产品编号数量减少
            String product_id = param.产品编号;
            String sql0 = "update 产品表 set 库存数量=库存数量-"+rk_count+" where 产品编号='" + product_id + "'";
            sqlHelper.Query(sql0);

            // 出完库之后要就行一个判断，来自动增加入库单！！！！！预警最低到45
            String ck_product_name = param.产品名称;
            String ck_prodcut_id = param.产品编号;
            String sql3 = "select 已用库存量 from 货位表 where 货位编号='"+ rk_loction_id + "'";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql3));
            int amount = 50 - count;
            if (count < 45)
            {
                String sql4 = "insert 入库表 set 产品名称='" + ck_product_name + "',产品编号= '" + ck_prodcut_id + "',目标货位号='" + rk_loction_id + "',电子标签入库数="+amount+",入库状态='等待入库'";
                sqlHelper.Query(sql4);
            }

        }
        public IList<Models.出库表> GetCKDataJson(int page, int rows, String id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 出库表  where 出库状态='" + id + "' limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.出库表>(sql);
        }
        public int GetCKInfoCount1(String id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 出库表 where 出库状态='" + id + "'";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public IList<Models.货位表> 出库表DataJson1()
        {

            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 货位表 where 货位编号='默认'";
            return sqlHelper.ExecuteQuery<Models.货位表>(sql);

        }
        /* public IList<Models.仓库表> GetRKDataJson(String number)
         {
             DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
             String sql = "select * from 仓库表 order by 仓库";
             return sqlHelper.ExecuteQuery<Models.仓库表>(sql);
         }*/
        /*   public IList<Models.货位表> GetRKDataJson1(String id)
           {
               DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
               String sql = "select * from 货位表  where 仓库='" + id + "'";
               return sqlHelper.ExecuteQuery<Models.货位表>(sql);
           }*/
        //以下是采购表的dal page是页数  rows是条数  选取0-10  10-20  (page-1)*10-rows--------------------------------------------------------------------
        public IList<Models.采购表> GetCGInfo(int page, int rows,String id,String id1)
        {
            if (id == null && id1 == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 采购单 limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.采购表>(sql);
            }
            else if (id != "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 采购单 where 产品名称='" + id + "' and 供应商名='" + id1 + "' limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.采购表>(sql);
            }
            else if (id == "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 采购单 where 供应商名='" + id1 + "' limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.采购表>(sql);
            }
            else if (id == "" && id1 == "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 采购单 limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.采购表>(sql);
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 采购单 where 产品名称='" + id + "' limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.采购表>(sql);
            }

        }
        public int GetCGInfoCount(String id, String id1)
        {
            if (id == null && id1 == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                String sql = "select count(*) from 采购单";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else if (id != "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                String sql = "select count(*) from 采购单 where 产品名称='" + id + "' and 供应商名='" + id1 + "'";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else if (id == "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select count(*) from 采购单 where 供应商名='" + id1 + "'";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else if (id == "" && id1 == "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                String sql = "select count(*) from 采购单";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select count(*) from 采购单 where 产品名称='" + id + "'";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
        }
        public int InsertCGInfo(Models.采购表 param)
        {
            int cg_id = param.订单号;
            String cg_product_name = param.产品名称;
            String cg_product_id = param.产品编号;
            int cg_count = param.采购数量;
            String cg_supply_id = param.供应商编号;
            String cg_supply_name = param.供应商名;
            int cg_amount = param.订单总额;
            String cg_time = param.采购时间;

            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            // number1就是给货位表一个采购数量，货位表来返回一个货位编号 满足采购数量+已用库存量<最大库存量！！！！！！ 补充：还得满足货位表中存放产品字段值等于这个产品编号！！不过不等于的话 开辟新的货位号存放该产品！！
            String sql1 = "select 货位编号 from 货位表 where 最大库存量>已用库存量+" + cg_count + " and 存放产品='" + cg_product_id + "' and 灯光状态='默认' limit 1";
            DataSet ds = sqlHelper.Query(sql1);

            if (ds.Tables[0].Rows.Count != 0) // 不能分配一个货位架！
            {
                // 生成采购单!!
                String sql = "insert into 采购单 (订单号,产品名称,产品编号,采购数量,供应商编号,供应商名,订单总额,采购时间) values (" + cg_id + ",'" + cg_product_name + "','" + cg_product_id + "'," + cg_count + ",'" + cg_supply_id + "','" + cg_supply_name + "'," + cg_amount + ",'" + cg_time + "')";
                sqlHelper.Query(sql);

                String hw_id = (string)ds.Tables[0].Rows[0][0];
                //当我采购表生成一单时，入库表也应该自动生成一单
                String sql2 = "insert into 入库表(产品名称,产品编号,电子标签入库数,目标货位号,入库状态) values ('" + cg_product_name + "','" + cg_product_id + "'," + cg_count + ",'" + hw_id + "','等待入库')";
                sqlHelper.Query(sql2);

                // 获取入库单号给对应货位号进行绑定
                String sql3 = "select 入库单号 from 入库表 ORDER BY 入库单号 DESC limit 1";
                DataSet ds1 = sqlHelper.Query(sql3);
                int rk_id = (int)ds1.Tables[0].Rows[0][0];

                // 采购表生成一单时，给入库单自动分配了一个货位号，所以货位号的灯光状态和电子标签信息要显示对应的信息！！！！！！
                String sql4 = "update 货位表 set 存放产品='" + cg_product_id + "',电子标签信息=" + cg_count + ",灯光状态='入库',绑定单号=" + rk_id + " where 货位编号='" + hw_id + "'";
                sqlHelper.Query(sql4);

                return 1;
            }
            else 
            {
                return 2;
            }

        }
        public void UpDateCGInfo(Models.采购表 param)
        {
            int cg_id = param.订单号;
            String cg_product_name = param.产品名称;
            String cg_product_id = param.产品编号;
            int cg_count = param.采购数量;
            String cg_supply_id = param.供应商编号;
            String cg_supply_name = param.供应商名;
            int cg_amount = param.订单总额;
            String cg_time = param.采购时间;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 采购单 set 产品名称='" + cg_product_name + "',产品编号='" + cg_product_id + "',采购数量=" + cg_count + ",供应商编号='" + cg_supply_id + "',供应商名='" + cg_supply_name + "',订单总额=" + cg_amount + ",采购时间='" + cg_time + "' where 订单号=" + cg_id + "";
            sqlHelper.Query(sql);
        }
        public int DeleteCGInfo(int id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "delete from 采购单 where 订单号=" + id + "";
            sqlHelper.Query(sql);
            return 1;
        }
        public int GetLastCGInfo()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select 订单号 from 采购单 ORDER BY 订单号 DESC limit 1";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        // 以下是返回产品表信息到控件里面
        public IList<Models.产品表> 采购表DataJson1(String number)
        {
            if (number == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 产品表";
                return sqlHelper.ExecuteQuery<Models.产品表>(sql);
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 产品表 where 产品名称='"+number+"'";
                return sqlHelper.ExecuteQuery<Models.产品表>(sql);
            }
        }
        public IList<Models.供应商表> 采购表DataJson2(String number)
        {
            if (number == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 供应商表";
                return sqlHelper.ExecuteQuery<Models.供应商表>(sql);
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 供应商表 where 供应商名称='" + number + "'";
                return sqlHelper.ExecuteQuery<Models.供应商表>(sql);
            }
        }
        public IList<Models.产品表> 采购表DataJson3(String number)
        {

                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 产品表 where 产品编号='"+number+"'";
                return sqlHelper.ExecuteQuery<Models.产品表>(sql);
        }
        public IList<Models.产品表> 产品表DataJson()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 产品表";
            return sqlHelper.ExecuteQuery<Models.产品表>(sql);
        }
        // 以上是采购表内容dal-----------------------------------------------------------------------------------------
        // 以下是销售表内容dal----------------------------------------------------------------------------------------------
        public IList<Models.销售表> GetXSInfo(int page, int rows, String id, String id1)
        {
            if (id == null && id1 == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 销售单 limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.销售表>(sql);
            }
            else if (id != "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 销售单 where 产品名称='" + id + "' and 客户名称='" + id1 + "' limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.销售表>(sql);
            }
            else if (id == "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 销售单 where 客户名称='" + id1 + "' limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.销售表>(sql);
            }
            else if (id == "" && id1 == "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 销售单 limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.销售表>(sql);
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 销售单 where 产品名称='" + id + "' limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.销售表>(sql);
            }
        }
        public int GetXSInfoCount(String id, String id1)
        {
            if (id == null && id1 == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                String sql = "select count(*) from 销售单";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else if (id != "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                String sql = "select count(*) from 销售单 where 产品名称='" + id + "' and 客户名称='" + id1 + "'";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else if (id == "" && id1 != "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select count(*) from 销售单 where 客户名称='" + id1 + "'";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else if (id == "" && id1 == "")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                String sql = "select count(*) from 销售单";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select count(*) from 销售单 where 产品名称='" + id + "'";
                int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
                return count;
            }
        }
        public int InsertXSInfo(Models.销售表 param)
        {
            int cg_id = param.订单号;
            String cg_product_name = param.产品名称;
            String cg_product_id = param.产品编号;
            int cg_count = param.销售数量;
            String cg_supply_id = param.客户编号;
            String cg_supply_name = param.客户名称;
            int cg_amount = param.订单总额;
            String cg_time = param.销售时间;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            

            // 当我生成销售表应该自动生成出库表！！！！如果自动出库，就需要一个出库货位号，这个货位号要根据销售的产品名称和数量来确定！ 补充：当然了！也存在一种情况是库存量不够了，不能生成一个货位编号满足条件！
            String sql1 = "select 货位编号 from 货位表 where 已用库存量>" + cg_count + " and 存放产品='" + cg_product_id + "' and 灯光状态='默认' limit 1";
            DataSet ds = sqlHelper.Query(sql1);
            if (ds.Tables[0].Rows.Count != 0)
            {
                String sql = "insert into 销售单 (订单号,产品名称,产品编号,销售数量,客户编号,客户名称,订单总额,销售时间) values (" + cg_id + ",'" + cg_product_name + "','" + cg_product_id + "'," + cg_count + ",'" + cg_supply_id + "','" + cg_supply_name + "'," + cg_amount + ",'" + cg_time + "')";
                sqlHelper.Query(sql);

                String hw_id = (string)ds.Tables[0].Rows[0][0]; // 现在要出库的货位编号已经拿到了！

                //当我销售表生成一单时，出库表也应该自动生成一单
                String sql2 = "insert into 出库表(产品名称,产品编号,电子标签出库数,目标货位号,出库状态) values ('" + cg_product_name + "','" + cg_product_id + "'," + cg_count + ",'" + hw_id + "','等待出库')";
                sqlHelper.Query(sql2);

                String sql3 = "select 出库单号 from 出库表 ORDER BY 出库单号 DESC limit 1";
                DataSet ds1 = sqlHelper.Query(sql3);
                int ck_id = (int)ds1.Tables[0].Rows[0][0];

                // 采购表生成一单时，给入库单自动分配了一个货位号，所以货位号的灯光状态和电子标签信息要显示对应的信息！！！！！！
                String sql4 = "update 货位表 set 灯光状态='出库',电子标签信息=" + cg_count + ",灯光状态='出库',绑定单号=" + ck_id + " where 货位编号='" + hw_id + "'";
                sqlHelper.Query(sql4);

                return 1;
            }
            else 
            {
                return 2;
            }
            
        }
        public void UpDateXSInfo(Models.销售表 param)
        {
            int cg_id = param.订单号;
            String cg_product_name = param.产品名称;
            String cg_product_id = param.产品编号;
            int cg_count = param.销售数量;
            String cg_supply_id = param.客户编号;
            String cg_supply_name = param.客户名称;
            int cg_amount = param.订单总额;
            String cg_time = param.销售时间;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 销售单 set 产品名称='" + cg_product_name + "',产品编号='" + cg_product_id + "',销售数量=" + cg_count + ",客户编号='" + cg_supply_id + "',客户名称='" + cg_supply_name + "',订单总额=" + cg_amount + ",销售时间='" + cg_time + "' where 订单号=" + cg_id + "";
            sqlHelper.Query(sql);

            


        }
        public int DeleteXSInfo(int id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "delete from 销售单 where 订单号=" + id + "";
            sqlHelper.Query(sql);
            return 1;
        }
        public int GetLastXSInfo()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select 订单号 from 销售单 ORDER BY 订单号 DESC limit 1";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        // 以下是返回产品表信息到控件里面-------------------------------------------------------
        public IList<Models.产品表> 销售表DataJson1(String number)
        {
            if (number == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 产品表";
                return sqlHelper.ExecuteQuery<Models.产品表>(sql);
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 产品表 where 产品名称='" + number + "'";
                return sqlHelper.ExecuteQuery<Models.产品表>(sql);
            }
        }
        public IList<Models.客户表> 销售表DataJson2(String number)
        {
            if (number == null)
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 客户表";
                return sqlHelper.ExecuteQuery<Models.客户表>(sql);
            }
            else
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 客户表 where 客户名称='" + number + "'";
                return sqlHelper.ExecuteQuery<Models.客户表>(sql);
            }
        }
        // 以下是货位表内容dal-----------------------------------------------------------------------------------------
        public IList<Models.货位表> GetHWInfo(int page, int rows,String number)
        {
            if (number == null||number=="ALL")
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 货位表 limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.货位表>(sql);
            }
            else 
            {
                DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
                string sql = "select * from 货位表  where 所在库区='" + number + "' limit " + (page - 1) * 20 + "," + rows + "";
                return sqlHelper.ExecuteQuery<Models.货位表>(sql);
            }
        }
        public int GetHWInfoCount()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 货位表";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        /*
        public void InsertHWInfo(Models.货位表 param)
        {
            String hw_number = param.货位编号;
            int hw_max = param.最大库存量;
            int hw_used = param.已用库存量;
            //   String hw_store = param.仓库;
            // String hw_type = param.货位类型;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "insert into 货位表 (货位编号,最大库存量,已用库存量,货位类型) values ('" + hw_number + "'," + hw_max + ",'" + hw_used + "','等待出入')";
            sqlHelper.Query(sql);
        }
        public void UpDateHWInfo(Models.货位表 param)
        {
            String hw_number = param.货位编号;
            int hw_max = param.最大库存量;
            int hw_used = param.已用库存量;
            //   String hw_store = param.仓库;
           // String hw_type = param.货位类型;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
         //   String sql = "update 货位表 set 货位编号='" + hw_number + "',最大库存量=" + hw_max + ",已用库存量=" + hw_used + ",货位类型='" + hw_type + "'where id=" + hw_id + "";
           // sqlHelper.Query(sql);
        }
        public int DeleteHWInfo(int id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "delete from 货位表 where id=" + id + "";
            sqlHelper.Query(sql);
            return 1;
        }
        public int GetLastHWInfo()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select id from 货位表 ORDER BY id DESC limit 1";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public IList<Models.货位表> GetHWDataJson(String number)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select * from 货位表  where 货位类型='等待出入'";
            return sqlHelper.ExecuteQuery<Models.货位表>(sql);
        }
        */
        // 以上是货位表相关dal--------------------------------------------------------------------------------------------------------------

        //以下是移库表相关dal
        public IList<Models.移库表> GetYKInfo(int page, int rows)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 移库表 limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.移库表>(sql);
        }
        public int GetYKInfoCount()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 移库表";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public void InsertYKInfo(Models.移库表 param)
        {
            int yk_id = param.移库单号;
            int yk_amount = param.电子标签移库数;
            String yk_target_number = param.目标货位号;
            String yk_now_number = param.当前货位号;
           // String yk_pserson = param.移库人;
           // String yk_time = param.移库时间;
            // int yk_now_number = param.移库状态;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "insert into 移库表 (移库单号,电子标签移库数,目标货位号,当前货位号,移库状态) values (" + yk_id + "," + yk_amount + ",'" + yk_target_number + "','" + yk_now_number + "','等待移库')";
            sqlHelper.Query(sql);
            // 

            // 当我新建一个移库单的时候 目标货位号灯光状态要变成入库状态，当前货位号灯光状态要变成移库状态
            String sql1 = "update 货位表 set 灯光状态='移入',电子标签信息="+ yk_amount + ",绑定单号="+yk_id+" where 货位编号='" + yk_target_number + "'";
            sqlHelper.Query(sql1);
            String sql2 = "update 货位表 set 灯光状态='移库',电子标签信息="+ yk_amount + ",绑定单号="+yk_id+" where 货位编号='" + yk_now_number + "'";
            sqlHelper.Query(sql2);
        }
        public int GetLastYKInfo()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select 移库单号 from 移库表 ORDER BY 移库单号 DESC limit 1";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public IList<Models.货位表> GetYKDataJson1()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select * from 货位表 where 灯光状态='默认'";
            return sqlHelper.ExecuteQuery<Models.货位表>(sql);
        }
        public void UpDateYKInfo(Models.移库表 param)
        {
            int yk_id = param.移库单号;
            int yk_light_count = param.电子标签移库数;
            String yk_light_number = param.目标货位号;
            String yk_now_number = param.当前货位号;
            String yk_person = param.移库人;
            String yk_time = param.移库时间;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 移库表 set 电子标签移库数=" + yk_light_count + ",目标货位号='" + yk_light_number + "',当前货位号='" + yk_now_number + "',移库人='" + yk_person + "',移库时间='" + yk_time + "',移库状态='移库完成' where 移库单号=" + yk_id + "";
            sqlHelper.Query(sql);

            // 当我移库表确认的的时候，货位表的已用库存量已改减少，而且要根据已用库存量的多少来判断货位类型是否为等待出入！！！！！！
            String sql1 = "update 货位表 set 已用库存量=已用库存量-" + yk_light_count + " where 货位编号='" + yk_now_number + "'";
            sqlHelper.Query(sql1);

            String sql2 = "update 货位表 set 已用库存量=已用库存量+" + yk_light_count + " where 货位编号='" + yk_light_number + "'";
            sqlHelper.Query(sql2);

            String sql3 = "update 货位表 set 电子信息标签='0',灯光状态=case  when (已用库存量<最大库存量) then '默认'  else '移库' end";
            sqlHelper.Query(sql3);

            // 移库完之后要把产品表里面的库存数量更改----
            String sql4 = "select 存放产品 from 货位表 where 货位编号='" + yk_now_number + "'";
            DataSet ds = sqlHelper.Query(sql4);
            String now_product_id = (string)ds.Tables[0].Rows[0][0]; 

            String sql5 = "select 存放产品 from 货位表 where 货位编号='" + yk_light_number + "'";
            DataSet ds1 = sqlHelper.Query(sql5);
            String target_product_id = (string)ds1.Tables[0].Rows[0][0];

            String sql6 = "update 产品表 set 库存数量=库存数量-" + yk_light_count + " where 产品编号='" + now_product_id + "'";
            String sql7 = "update 产品表 set 库存数量=库存数量+" + yk_light_count + " where 产品编号='" + target_product_id + "'";
            sqlHelper.Query(sql6);
            sqlHelper.Query(sql7);
        }
        public IList<Models.移库表> GetYKDataJson(int page, int rows, String id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 移库表  where 移库状态='" + id + "' limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.移库表>(sql);
        }
        public int GetYKInfoCount1(String id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 移库表 where 移库状态='" + id + "'";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        /*  public void UpDateYKInfo(Models.移库表 param)
          {
              int yk_id = param.移库单号;
              int yk_light_count = param.电子标签移库数;
              int yk_light_number = param.电子标签货位号;
              int yk_now_number = param.当前货位号;
              String yk_person = param.移库人;
              String yk_time = param.移库时间;
              DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
              String sql = "update 移库表 set 电子标签移库数=" + yk_light_count + ",电子标签货位号=" + yk_light_number + ",当前货位号=" + yk_now_number + ",移库人='" + yk_person + "',移库时间='" + yk_time + "',移库状态='已完成' where 移库单号=" + yk_id + "";
              sqlHelper.Query(sql);

              // 当我移库表确认的的时候，货位表的已用库存量已改减少，而且要根据已用库存量的多少来判断货位类型是否为等待出入！！！！！！
              String sql1 = "update 货位表 set 已用库存量=已用库存量-" + yk_light_count + " where 货位编号=" + yk_now_number + "";
              sqlHelper.Query(sql1);

              String sql2 = "update 货位表 set 已用库存量=已用库存量+" + yk_light_count + " where 货位编号=" + yk_light_number + "";
              sqlHelper.Query(sql2);

              String sql3 = "update 货位表 set 货位类型=case  when (已用库存量<最大库存量) then '等待出入'  else '等待移库' end";
              sqlHelper.Query(sql3);
          }
          public int GetLastYKInfo()
          {
              DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
              String sql = "select 移库单号 from 移库表 ORDER BY 移库单号 DESC limit 1";
              int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
              return count;
          }
          */

        // 以下是产品表dal-------------------------------------------------------------------------------------------
        public IList<Models.产品表> GetCPInfo(int page, int rows)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 产品表 limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.产品表>(sql);
        }
        public int GetCPInfoCount()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 产品表";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public void InsertCPInfo(Models.产品表 param)
        {
            String cp_number = param.产品编号;
            String cp_name = param.产品名称;
            // int cp_count = param.库存数量;
            String cp_unit = param.存储单位;
            String cp_type = param.类型;
            int cp_price = param.价格;

            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "insert into 产品表 (产品编号,产品名称,库存数量,存储单位,类型,价格) values ('" + cp_number + "','" + cp_name + "','0','" + cp_unit + "','" + cp_type + "',"+cp_price+")";
            sqlHelper.Query(sql);

            // 新建一个产品的时候自动分配一个货位号！方便以后出入库使用
            String sql1 = "update 货位表 set 存放产品='" + cp_number + "' where isnull(存放产品) limit 1";
            sqlHelper.Query(sql1);
        }
        public void UpDateCPInfo(Models.产品表 param)
        {
            String cp_number = param.产品编号;
            String cp_name = param.产品名称;
            // int cp_count = param.库存数量;
            String cp_unit = param.存储单位;
            String cp_type = param.类型;
            int cp_price = param.价格;
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 产品表 set 产品名称='" + cp_name + "',存储单位='" + cp_unit + "',类型='" + cp_type + "',价格="+cp_price+" where 产品编号='" + cp_number + "'";
            sqlHelper.Query(sql);
        }
        public int DeleteCPInfo(int id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "delete from 产品表 where 产品编号=" + id + "";
            sqlHelper.Query(sql);
            String sql0 = "update 货位表 set 存放产品=null where 存放产品=" + id + "";
            sqlHelper.Query(sql0);
            return 1;
        }
        // 以下是供应商表dal的内容---------------------------------------------------------------------------
        public IList<Models.供应商表> GetGYSInfo(int page, int rows)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 供应商表 limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.供应商表>(sql);
        }
        public int GetGYSInfoCount()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 供应商表";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public void InsertGYSInfo(Models.供应商表 param)
        {
            String gys_number = param.供应商编号;
            String gys_name = param.供应商名称;
            String gys_tel = param.电话;
            String gys_fax = param.传真;
            String gys_email = param.邮箱;
            String gys_contact = param.联系人;
            String gys_location = param.地址;
            String gys_time = param.创建时间;

            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "insert into 供应商表 (供应商编号,供应商名称,电话,传真,邮箱,联系人,地址,创建时间) values ('" + gys_number + "','" + gys_name + "','" + gys_tel + "','" + gys_fax + "','" + gys_email + "','" + gys_contact + "','" + gys_location + "','" + gys_time + "')";
            sqlHelper.Query(sql);
        }
        public void UpDateGYSInfo(Models.供应商表 param)
        {
            String gys_number = param.供应商编号;
            String gys_name = param.供应商名称;
            String gys_tel = param.电话;
            String gys_fax = param.传真;
            String gys_email = param.邮箱;
            String gys_contact = param.联系人;
            String gys_location = param.地址;
            String gys_time = param.创建时间;

            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 供应商表 set 供应商名称='" + gys_name + "',电话='" + gys_tel + "',传真='" + gys_fax + "',邮箱='" + gys_email + "',联系人='" + gys_contact + "',地址='" + gys_location + "',创建时间='" + gys_time + "' where 供应商编号='" + gys_number + "'";
            sqlHelper.Query(sql);
        }
        public int DeleteGYSInfo(int id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "delete from 供应商表 where 供应商编号=" + id + "";
            sqlHelper.Query(sql);
            return 1;
        }

        // 以下是客户表dal------------------------------------------------------------------------------
        public IList<Models.客户表> GetKHInfo(int page, int rows)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 客户表 limit " + (page - 1) * 20 + "," + rows + "";
            return sqlHelper.ExecuteQuery<Models.客户表>(sql);
        }
        public int GetKHInfoCount()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select count(*) from 客户表";
            int count = Convert.ToInt32(sqlHelper.GetSingle(sql));
            return count;
        }
        public void InsertKHInfo(Models.客户表 param)
        {
            String kh_number = param.客户编号;
            String kh_name = param.客户名称;
            String kh_tel = param.电话;
            String kh_fax = param.传真;
            String kh_email = param.邮箱;
            String kh_contact = param.联系人;
            String kh_location = param.地址;
            String kh_time = param.创建时间;

            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "insert into 客户表 (客户编号,客户名称,电话,传真,邮箱,联系人,地址,创建时间) values ('" + kh_number + "','" + kh_name + "','" + kh_tel + "','" + kh_fax + "','" + kh_email + "','" + kh_contact + "','" + kh_location + "','" + kh_time + "')";
            sqlHelper.Query(sql);
        }
        public void UpDateKHInfo(Models.客户表 param)
        {
            String kh_number = param.客户编号;
            String kh_name = param.客户名称;
            String kh_tel = param.电话;
            String kh_fax = param.传真;
            String kh_email = param.邮箱;
            String kh_contact = param.联系人;
            String kh_location = param.地址;
            String kh_time = param.创建时间;

            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "update 客户表 set 客户名称='" + kh_name + "',电话='" + kh_tel + "',传真='" + kh_fax + "',邮箱='" + kh_email + "',联系人='" + kh_contact + "',地址='" + kh_location + "',创建时间='" + kh_time + "' where 客户编号='" + kh_number + "'";
            sqlHelper.Query(sql);
        }
        public int DeleteKHInfo(int id)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "delete from 客户表 where 客户编号=" + id + "";
            sqlHelper.Query(sql);
            return 1;
        }
        // 仓库1-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public IList<Models.货位表> 仓库1DataJson()
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            string sql = "select * from 货位表";
            return sqlHelper.ExecuteQuery<Models.货位表>(sql);
        }
        public int 仓库1UpDate(String id, String status,String name)
        {
            // 现在存在一个什么问题呢？ 灯光传过来的值是货位号，我一更改入库表改的就是货位号的！而没有改对应入库单号的
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select * from 货位表 where 货位编号='" + id + "'";
            DataSet ds = sqlHelper.Query(sql);
            int huodan = (int)ds.Tables[0].Rows[0][7]; // 把货位表绑定的货单拿到
            int cp_amount = (int)ds.Tables[0].Rows[0][3]; //拿到货位表上的电子标签信息所显示的数量，之后存放给产品
            String cp_id=(String)ds.Tables[0].Rows[0][6];
            String time = DateTime.Now.ToString();

            if (status == "入库")
            {
                string sql2 = "update 货位表 set 已用库存量=已用库存量+电子标签信息 where 货位编号='" + id + "' and 灯光状态='" + status + "'";
                sqlHelper.Query(sql2);
                string sql3 = "update 入库表 set 入库人='" + name + "',入库时间='" + time + "',入库状态='入库成功' where 入库单号=" + huodan + "";
                sqlHelper.Query(sql3);

                // 这里吧库存数量放到产品信息的数量中
                String sql0 = "update 产品表 set 库存数量=库存数量+" + cp_amount + " where 产品编号='" + cp_id + "'";
                sqlHelper.Query(sql0);
            }
            else if (status == "出库")
            {
                string sql4 = "update 货位表 set 已用库存量=已用库存量-电子标签信息 where 货位编号='" + id + "' and 灯光状态='" + status + "'";
                sqlHelper.Query(sql4);
                string sql5 = "update 出库表 set 出库人='" + name + "',出库时间='" + time + "',出库状态='出库成功' where 出库单号=" + huodan + "";
                sqlHelper.Query(sql5);

                // 这里吧库存数量放到产品信息的数量中
                String sql0 = "update 产品表 set 库存数量=库存数量-" + cp_amount + " where 产品编号='" + cp_id + "'";
                sqlHelper.Query(sql0);

                // 这样出库之后也需要进行一种库存判断！少了之后自动补货，
                String sql6 = "select * from 货位表 where 货位编号='" + id + "'";
                DataSet ds1 = sqlHelper.Query(sql6);
                int count = (int)ds1.Tables[0].Rows[0][2];
                String ck_product_name = (string)ds1.Tables[0].Rows[0][6];
                String sql7 = "select 产品名称 from 产品表 where 产品编号='" + ck_product_name + "'";
                DataSet ds3 = sqlHelper.Query(sql7);
                String ck_prodcut_id = (String)ds3.Tables[0].Rows[0][0];
                int amount = 50 - count;
                if (count < 45)
                {
                    String sql8 = "insert 入库表 set 产品名称='" + ck_product_name + "',产品编号= '" + ck_prodcut_id + "',目标货位号='" + id + "',电子标签入库数=" + amount + ",入库状态='等待入库'";
                    sqlHelper.Query(sql8);
                }

            }
            else if (status == "移库")
            {
                string sql9 = "update 货位表 set 已用库存量=已用库存量-电子标签信息 where 货位编号='" + id + "' and 灯光状态='" + status + "'";
                sqlHelper.Query(sql9);

                // 这里吧库存数量放到产品信息的数量中
                String sql0 = "update 产品表 set 库存数量=库存数量-" + cp_amount + " where 产品编号='" + cp_id + "'";
                sqlHelper.Query(sql0);
            }
            else if (status == "移入") 
            {
                string sql10 = "update 货位表 set 已用库存量=已用库存量+电子标签信息 where 货位编号='" + id + "' and 灯光状态='" + status + "'";
                sqlHelper.Query(sql10);
                string sql11 = "update 移库表 set 移库人='" + name + "',移库时间='" + time + "',移库状态='移库成功' where 移库单号=" + huodan + "";
                sqlHelper.Query(sql11);

                // 这里吧库存数量放到产品信息的数量中
                String sql0 = "update 产品表 set 库存数量=库存数量+" + cp_amount + " where 产品编号='" + cp_id + "'";
                sqlHelper.Query(sql0);
            }
            
            string sql12 = "update 货位表 set 灯光状态='默认',电子标签信息='0',绑定单号=null where 货位编号='"+id+"'";
            sqlHelper.Query(sql12);

            return 1;
        }

        // 以下是Login DAL-------------------------------------------------------------------------------------------------------------------------------------------
        public IList<Models.用户表> Login(String user, String pass)
        {
            DBTools.SqlHelper sqlHelper = new DBTools.SqlHelper();
            String sql = "select * from 用户表 where 账号='"+user+"' and 密码='"+pass+"'";
            return sqlHelper.ExecuteQuery<Models.用户表>(sql);
        }
    }
}