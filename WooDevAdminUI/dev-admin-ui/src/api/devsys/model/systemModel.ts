import { BasicPageParams, BasicFetchResult } from '/@/api/model/baseModel';

export type datadicParams = BasicPageParams & {
  name?: string;
};

export interface datadicListItem {
  ID: number;
  NAME: string;
  SORT_NAME: string;
  REMARK: string;
  ORDER_NUM: number;
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
  keyword?: string;
};
