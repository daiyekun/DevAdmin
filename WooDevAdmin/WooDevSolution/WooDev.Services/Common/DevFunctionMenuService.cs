using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace WooDev.Services
{

    /// <summary>
    /// 菜单
    /// </summary>
    public partial class DevFunctionMenuService
    {

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="whereLambda">where 条件</param>
        /// <returns></returns>
        public IList<DEV_FUNCTION_MENU> GetListByWhere(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda)
        {
            var tempquery = DbClient.Queryable<DEV_FUNCTION_MENU>().Where(whereLambda).Includes(a=>a.RouteMate);
            return tempquery.ToList();

         }

        #region 登录以后左侧树查询
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns></returns>
        public List<MenuItem> GetMenus(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda)
        {
            
            var list = GetListByWhere(whereLambda)
                .OrderBy(a=>a.RouteMate.ORDERNO).ToList();//查询到集合
            var routeItems = GetTreeMenus(list, 0);
            return routeItems;
        }

        /// <summary>
        /// 递归查询 菜单
        /// </summary>
        /// <param name="list">菜单集合</param>
        /// <param name="Pid">父ID</param>
        /// <returns></returns>
        public List<MenuItem> GetTreeMenus(List<DEV_FUNCTION_MENU> list,int Pid)
        {
            List<MenuItem> routeItems = new List<MenuItem>();
            var oneprents = list.Where(a => a.PID == Pid).ToList();
            foreach (var item in oneprents)
            {
                MenuItem routeItem = new MenuItem();
                routeItem.Id = item.ID;
                routeItem.Name = item.NAME;
                routeItem.Path = item.PATH;
                routeItem.Component = item.COMPONENT;
                routeItem.Redirect = item.REDIRECT;
                routeItem.Meta = GetRouteMate(item.RouteMate);
                ////查询子集
                //var crouteItems = GetChirdMenus(list, item);
                //routeItem.Children = crouteItems;
                var childs = list.Where(a => a.PID == item.ID).ToList();
                if (childs.Count > 0)
                {//如果还有就递归-无限菜单
                    var btnItems = GetTreeMenus(list, item.ID);
                    routeItem.Children = btnItems;
                }
                routeItems.Add(routeItem);

            }

            return routeItems;
        }




        /// <summary>
        /// 查询子集
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="item">父类</param>
        /// <returns></returns>
        private List<MenuItem> GetChirdMenus(IList<DEV_FUNCTION_MENU> list,DEV_FUNCTION_MENU item )
        {
            //查询子集
            var listchirds = list.Where(a => a.PID == item.ID).ToList();
            List<MenuItem> crouteItems = new List<MenuItem>();
            foreach (var citem in listchirds)
            {

                MenuItem crouteItem = new MenuItem();
                crouteItem.Id = citem.ID;
                crouteItem.Name = citem.NAME;
                crouteItem.Path = citem.PATH;
                crouteItem.Component = citem.COMPONENT;
                crouteItem.Meta = GetRouteMate(citem.RouteMate);
                var btnchilds= list.Where(a => a.PID == citem.ID).ToList();
                if (btnchilds.Count>0)
                {//如果还有就递归-无限菜单
                    var btnItems= GetChirdMenus(list, citem);
                    crouteItem.Children = btnItems;
                }
               
                crouteItems.Add(crouteItem);
                
                
            }
            return crouteItems;
            
        }
        /// <summary>
        /// 子 mate
        /// </summary>
        /// <param name="RouteMate"></param>
        /// <returns></returns>
        public RouteMenuMeta GetRouteMate(DEV_ROUTEMETA RouteMate)
        {
            var info = new RouteMenuMeta();
            info.Title = RouteMate.TITLE;
            info.HideMenu = (RouteMate.HIDEMENU ?? 0) == 1;
            info.HideBreadcrumb= (RouteMate.HIDE_BREADCRUMB ?? 0) == 1;
            info.HideChildrenInMenu= (RouteMate.HIDE_CHILDRENINMENU ?? 0) == 1;
            info.Icon = RouteMate.ICON;
            return info;

        }
        #endregion


        #region 树形表格


        /// <summary>
        /// 查询菜单大列表
        /// </summary>
        /// <param name="whereLambda">where 条件</param>
        /// <returns></returns>
        public List<MenuList> GetList(Expression<Func<DEV_FUNCTION_MENU, bool>> whereLambda)
        {
            IList<DEV_FUNCTION_MENU> listAll = GetListByWhere(whereLambda).OrderBy(a=>a.RouteMate.ORDERNO).ToList();
            var menuLists = GetMenus(listAll,0);
            return menuLists;

        }
        /// <summary>
        /// 递归查询菜单
        /// </summary>
        /// <param name="listAll">所需遍历的集合</param>
        /// <param name="Pid">父节点ID</param>
        /// <returns></returns>
        public List<MenuList> GetMenus(IList<DEV_FUNCTION_MENU> listAll,int Pid)
        {
            List<MenuList> menuLists = new List<MenuList>();
            var listmenus = listAll.Where(a => a.PID == Pid).ToList();
            foreach (var item0 in listmenus)
            {
                MenuList menu0 = GetMenuObj(item0);
                var chidmenus = listAll.Where(a=>a.PID== item0.ID).ToList();
                if (chidmenus.Count>0)
                {
                    var chidlist = GetMenus(listAll, item0.ID);
                    menu0.Children = chidlist;
                }
                menuLists.Add(menu0);

            }

                return menuLists;

        }



        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="item0"></param>
        /// <returns></returns>
        private MenuList GetMenuObj(DEV_FUNCTION_MENU item0)
        {
            MenuList menuList0 = new MenuList();
            menuList0.Id = item0.ID;
            if (item0.RouteMate!=null)
            {
                menuList0.MenuName = item0.RouteMate.TITLE;
                menuList0.OrderNo = item0.RouteMate.ORDERNO ?? 0;
                menuList0.Icon = item0.RouteMate.ICON;
                menuList0.IsExt= item0.RouteMate.ISLINK??0;
                //是否缓存
                menuList0.Keepalive= item0.RouteMate.IGNORE_KEEPALIVE ?? 0;
                menuList0.Show=(item0.RouteMate.HIDEMENU ?? 0);
            }
            menuList0.Name = item0.NAME;
            menuList0.Pid = item0.PID;
            menuList0.ParentMenu= item0.PID;
            menuList0.Type = item0.M_TYPE ?? -1;
            menuList0.Permission = item0.PERMISSION;
            menuList0.Component = item0.COMPONENT;
            menuList0.Status = item0.IS_SHOW;
            menuList0.CreateTime = item0.CREATE_TIME;
            menuList0.RoutePath = item0.PATH;
            return menuList0;
        }


        #endregion 



    }



}
