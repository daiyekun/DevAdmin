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
