import { defHttp } from '/@/utils/http/axios';
import { getMenuListResultModel } from './model/menuModel';

enum Api {
  GetMenuList = '/DevMenu/getMenuList',
  GetRoleMenuList = '/DevMenu/getRoleMenuList',
}

/**
 * @description: Get user menu based on id
 */

export const getMenuList = () => {
  return defHttp.get<getMenuListResultModel>({ url: Api.GetMenuList });
};
/**
 * @description: 给角色分配菜单的时候使用，因为分配菜单只到菜单一级，按钮一级忽略
 */
export const getRoleMenuList = () => {
  return defHttp.get<getMenuListResultModel>({ url: Api.GetRoleMenuList });
};
