import { BasicPageParams, BasicFetchResult } from '/@/api/model/baseModel';

/**
 * 列表
 */
export interface FlowGroupListItem {
  ID: number;
  NAME: string;
  REMARK: string;
  CODE: string;
  CREATE_TIME: string;
}
/**
 * 新建角色对象
 */
export interface flowGroupSaveInfo {
  ID: number;
  NAME: string;
  CODE: string;
  REMARK: number;
}
/**
 * @description: Request list return value
 */
export type flowGroupistGetResultModel = BasicFetchResult<FlowGroupListItem>;
export type rgroupParams = BasicPageParams & {
  KeyWord?: string;
};
