//系统管理
import {
  departParams,
  departListGetResultModel,
  departSaveInfo,
  deldataInfo,
  roleListGetResultModel,
  roleParams,
  roleSaveInfo,
  UserParams,
  UserListGetResultModel,
  userSaveInfo,
  UserViewInfo,
  loginlogSearchParams,
  LoginLogResultModel,
  optionlogSearchParams,
  OptionLogResultModel,
  MenuParams,
  MenuListGetResultModel,
  menuSaveInfo,
  MenuSearchParams,
} from '../model/systemModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  departList = '/DevDepart/getTreeList', //组织机构查询列表
  departSave = '/DevDepart/departSave', //新建修改保存组织机构
  departDelete = '/DevDepart/deleteDepart', //删除
  departMainList = '/DevDepart/getList', //组织机构查询列表

  roletList = '/DevRole/getList', //角色查询列表
  roleSave = '/DevRole/roleSave', //新建修改保存角色
  roleDelete = '/DevRole/delRole', //删除
  getAllRoleList = '/DevRole/getAllRoleList', //查询所有角色
  setRoleStatus = '/DevRole/setRoleStatus', //修改角色状态

  userList = '/DevUser/getUserList', //用户列表
  isAccountExist = '/DevUser/isAccountExist', //账号是否存在
  userSave = '/DevUser/userSave', //新建修改保存用户
  userDelete = '/DevUser/delUser', //删除
  userview = '/DevUser/userview', //详情
  setUserStatus = '/DevUser/setUserStatus', //禁用
  userUpdate = '/DevUser/userUpdate', //个人信息设置
  updatePwd = '/DevUser/updatePwd', //修改密码

  loginlogList = '/DevLoginLog/getList', //登录日志
  optionlogList = '/DevOptionLog/getList', //登录日志
  //菜单
  MenuList = '/DevMenu/getMenuListTable',
  MenuSave = '/DevMenu/menuSave',
  MenuDelete = '/DevMenu/delmenu', //删除
  RoleMenuList = '/DevMenu/getMenuPersion', //角色权限分配
  PermssionSave = '/DevMenu/permssionSave', //保存权限
  SetSystemCache = '/DevSystemSet/setCache', //设置系统缓存
}

export const getDepartList = (params?: departParams) =>
  defHttp.get<departListGetResultModel>({ url: Api.departList, params });

export const getdepartMainList = (params?: departParams) =>
  defHttp.get<departListGetResultModel>({ url: Api.departMainList, params });

export const departSaveApi = (params: departSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.departSave, params });

export const departDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.departDelete, params });
export const getRoleList = (params?: roleParams) =>
  defHttp.get<roleListGetResultModel>({ url: Api.roletList, params });
export const getAllRoleList = (params?: roleParams) =>
  defHttp.get<roleListGetResultModel>({ url: Api.getAllRoleList, params });
export const setRoleStatus = (id: number, status: number) =>
  defHttp.post({ url: Api.setRoleStatus, params: { id, status } });

export const roleSaveApi = (params: roleSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.roleSave, params });

export const roleDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.roleDelete, params });

export const getUserList = (params: UserParams) =>
  defHttp.get<UserListGetResultModel>({ url: Api.userList, params });
export const isAccountExistApi = (account: string, Id: number) =>
  defHttp.post<ResultData>(
    { url: Api.isAccountExist, params: { account, Id } },
    { errorMessageMode: 'none' },
  );
export const userSaveApi = (params: userSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.userSave, params });
export const userDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.userDelete, params });
export const userViewApi = (id: number) =>
  defHttp.get<UserViewInfo>({ url: Api.userview, params: { id } });
export const userStatusApi = (id: number, status: number) =>
  defHttp.post<ResultData>({ url: Api.setUserStatus, params: { id, status } });
export const userUpdateApi = (params: userSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.userUpdate, params });
export const userUpdatePwdApi = (userid: number, newpwd: string, oldpwd: string) =>
  defHttp.post<ResultData>({ url: Api.updatePwd, params: { userid, newpwd, oldpwd } });

export const getLoginLogList = (params: loginlogSearchParams) =>
  defHttp.get<LoginLogResultModel>({ url: Api.loginlogList, params });

export const getOptionLogList = (params: optionlogSearchParams) =>
  defHttp.get<OptionLogResultModel>({ url: Api.optionlogList, params });

export const getMenuList = (params?: MenuParams) =>
  defHttp.get<MenuListGetResultModel>({ url: Api.MenuList, params });

export const menuSaveApi = (params: menuSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.MenuSave, params });

export const menuDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.MenuDelete, params });

export const getRoleMenuList = (params?: MenuSearchParams) =>
  defHttp.get<MenuListGetResultModel>({ url: Api.RoleMenuList, params });

export const permssionSaveApi = (params: any) =>
  defHttp.post<ResultData>({ url: Api.PermssionSave, params });

export const setSystemCacheApi = (sta: number) =>
  defHttp.get<ResultData>({ url: Api.SetSystemCache, params: { sta } });
