/***流程模板相关模型***/
import { BasicFetchResult } from '/@/api/model/baseModel';
/**
 * 审批事项列表
 */
export interface FlowItemListItem {
  Id?: number;
  Name?: string;
  Selected?: string;
  Disabled?: string;
}
export type flowItemListResultModel = BasicFetchResult<FlowItemListItem>;
export type flowitemParams = {
  objEnum?: number;
};

/**
 * 审批对象
 */
export interface FlowObjectListItem {
  Value?: number;
  Desc?: string;
}
export type flowObjectListResultModel = BasicFetchResult<FlowObjectListItem>;
