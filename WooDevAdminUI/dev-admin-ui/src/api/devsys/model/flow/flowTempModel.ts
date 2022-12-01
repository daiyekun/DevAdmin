/***流程模板相关模型***/
import { BasicFetchResult, BasicPageParams } from '/@/api/model/baseModel';
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
/**
 * 保存模板对象
 */
export interface FlowTempSaveInfo {
  ID: number;
  NAME: string;
  CODE: string;
  OBJ_TYPE: number;
  F_STATE: number;
  FLOW_ITEMS: string;
  DEPART_IDS: string;
  CATE_IDS: string;
  MIN_MONERY: number;
  MAX_MONERY: number;
}

/**
 * 模板列表
 */
export interface flowTempListItem {
  ID: number;
  NAME: string;
  REMARK: string;
  CODE: string;
  CREATE_TIME: string;
}
/**
 * 列表搜索
 */
export type flowTempParams = BasicPageParams & {
  KeyWord?: string;
};

/**
 * @description: 模板列表
 */
export type flowTempListGetResultModel = BasicFetchResult<flowTempListItem>;
/**
 * 删除对象信息-列表操作删除公用
 */
export interface deldataInfo {
  Ids: string;
}

/*
 *修改查看信息载体
 */
export type FlowTempViewInfo = FlowTempSaveInfo & {};
/***
 * 流程图
 */
export interface FlowChartTempSaveInfo {
  TempId: number;
  FlowData: string;
}
/**
 *节点
 */
export type FlowNodeParams = {
  NodeStr?: string;
};

/**
 * 节点信息表
 */
export interface FlowNodeInfoListItem {
  ID: number;
  OPT_ID: number;
  OPT_NAME: string;
  NODE_ID: number;
  NODE_STRID: string;
  O_TYPE: number;
  RE_TEXT: number;
  INFO_STATE: number;
  NRULE: number;
  OtypeDsc: string;
  ObjName: string;
}
export type FlowNodeInfoListGetResultModel = BasicFetchResult<FlowNodeInfoListItem>;

/***
 * 节点信息保存
 */
export interface FlowTempNodeInfo {
  TEMP_ID: number;
  /***
   * 节点ID
   */
  NODE_STRID: string;
  /***
   * 审批对象类型
   */
  O_TYPE: number;
  /***
   * 审批对象
   */
  SpObjIds: Array<number>;
}

/***
 * 节点修改对象
 */
export interface FlowTempNodeUpdateInfo {
  NodeId: string;
  TempId: number;
  Name: string;
  SpRules: number;
}

/***
 *判断节点是否保存
 */
export interface ExistNodeInfo {
  TempId: number;
  StrId: string;
}
