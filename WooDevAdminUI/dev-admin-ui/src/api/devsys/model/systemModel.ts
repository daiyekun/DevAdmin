import { BasicPageParams, BasicFetchResult, ResultviewData } from '/@/api/model/baseModel';

export type datadicParams = BasicPageParams & {
  name?: string;
  dtype?: number;
};

export type datadictreeParams = {
  dtype?: number;
};

export interface datadicListItem {
  ID: number;
  NAME: string;
  SORT_NAME: string;
  REMARK: string;
  ORDER_NUM: number;
}
/**
 * 删除对象信息-列表操作删除公用
 */
export interface deldataInfo {
  Ids: string;
}

/**
 * @description: Request list return value
 */
export type datadicListGetResultModel = BasicFetchResult<datadicListItem>;

/**
 * 新建字典对象
 */
export interface datadicSaveInfo {
  ID: number;
  NAME: string;
  SORT_NAME: string;
  REMARK: string;
  ORDER_NUM: number;
}
/**
 * 字典初始化新建参数
 */
export interface datadicAddInfo {
  TypeInt: number;
}
/**
 * 删除时数据对象ids
 */
export interface datadicdeldata {
  Ids: string;
}
/**
 * 部门列表
 */
export interface departListItem {
  ID: number;
  NAME: string;
  SORT_NAME: string;
  CODE: string;
  ORDER_NUM: number;
}

/**
 * @description: Request list return value
 */
export type departListGetResultModel = BasicFetchResult<departListItem>;
export type departParams = BasicPageParams & {
  depName?: string;
};

/**
 * 新建部门对象
 */
export interface departSaveInfo {
  ID: number;
  NAME: string;
  CODE: string;
  IS_MAIN: number;
  DSTATE: number;
  ORDER_NUM: number;
}

/**
 * 角色列表
 */
export interface roleListItem {
  ID: number;
  NAME: string;
  REMARK: string;
  CODE: string;
  CREATE_TIME: string;
}
/**
 * 新建角色对象
 */
export interface roleSaveInfo {
  ID: number;
  NAME: string;
  CODE: string;
  REMARK: number;
}
/**
 * @description: Request list return value
 */
export type roleListGetResultModel = BasicFetchResult<roleListItem>;
export type roleParams = BasicPageParams & {
  KeyWord?: string;
};
/**
 * 用户搜索
 */
export type UserParams = BasicPageParams & {
  NAME?: string;
  LOGIN_NAME?: string;
  GroupId?: number;
};
export type UserListGetResultModel = BasicFetchResult<UserListItem>;
export interface UserListItem {
  ID: number;
  LOGIN_NAME: string;
  EMAIL: string;
  NAME: string;
  ROLE_ID: number;
  CREATE_TIME: string;
  USTATE: number;
}
/**
 * 新建用户对象
 */
export interface userSaveInfo {
  ID: number;
  LOGIN_NAME: string;
  EMAIL: string;
  NAME: string;
  ROLE_ID: number;
  CREATE_TIME: string;
  USTATE: number;
  ID_NO: string;
  PWD: string;
  SEX: number;
  WX_CODE: string;
  TEL: string;
  REMARK: string;
  ADDRESS: string;
}
/*
 *修改查看信息载体
 */
export type UserViewInfo = userSaveInfo & {
  SexDic: string;
  DepName: string;
  StateDic: string;
};
export type ViewUserModel = ResultviewData<UserViewInfo>;

/**
 * 登录日志搜索
 */
export type loginlogSearchParams = BasicPageParams & {
  Name?: string;
  StartDate?: Date;
  EndtDate?: Date;
};
export type LoginLogResultModel = BasicFetchResult<LoginLogListItem>;
export interface LoginLogListItem {
  ID: number;
  NAME: string;
  IP: string;
  LOGIN_RES: number;
  CREATE_TIME: string;
}

/**
 * 操作日志搜索
 */
export type optionlogSearchParams = BasicPageParams & {
  Name?: string;
  StartDate?: Date;
  EndtDate?: Date;
};
export type OptionLogResultModel = BasicFetchResult<OptionLogListItem>;
export interface OptionLogListItem {
  ID: number;
  NAME: string;
  IP: string;
  REQ_URL: string;
  REQ_PARAMETER: string;
  ACTION_NAME: string;
  REQ_METHOD: string;
  OPTION_TYPE: number;
  REMARK: string;
}
//菜单

export type MenuParams = {
  menuName?: string;
  status?: number;
};

export type MenuListGetResultModel = BasicFetchResult<MenuListItem>;
export interface MenuListItem {
  id: string;
  orderNo: string;
  createTime: string;
  status: number;
  icon: string;
  component: string;
  permission: string;
}

/**
 * 新建菜单对象
 */
export interface menuSaveInfo {
  id: number;
  type: number;
  menuName: string;
  parentMenu: number;
  orderNo: number;
  icon: string;
  routePath: string;
  component: string;
  permission: string;
  status: number;
  isExt: number;
  keepalive: number;
  show: number;
  dypession: number;
  permdisc: string;
}
export type MenuSearchParams = {
  MenuName?: string;
  Persiondic?: string;
  RoleId: number;
};
