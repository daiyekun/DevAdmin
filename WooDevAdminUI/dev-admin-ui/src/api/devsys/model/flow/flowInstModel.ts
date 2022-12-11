/***
 * 实例相关模型
 ***/
import { BasicFetchResult, BasicPageParams } from '/@/api/model/baseModel';
/***
 * 查询模板条件
 ***/
export interface FlowCondition {
  FlowObj: number; //审批对象类别 --> 0:客户...
  CateId: number;
  DeptId: number;
  FlowItem: number;
  Monery?: number;
}
/**
 * 查询流程图 提交相关对象
 */
export type FlowShowData = {
  FlowType: number;
  TempId: number; //模板ID
  ObjId: number; //对象ID
  FlowItem: number; //对象ID
  Name: string; //提交对象名称
  // Condition: FlowCondition; //对象部分信息
  // TempId: number; //模板ID
  // Name: string;
};
/**
 * 提交流程实体
 **/
export interface FlowSubmitModel {
  FlowType: number;
  ObjId: number;
  FlowItem: number;
  TempId: number;
}

/**
 * 审批实例
 * 审批历史
 */
export interface FlowInstListItem {
  ID: number;
  NAME: string;
  CODE: string;
  FLOW_TYPE: number;
  APP_ID: number;
  APP_MONERY: number;
  START_USER_ID: number;
  CREATE_TIME: string;
  CREATE_USERID: number;
  CURR_NODE_NAME: string;
  FLOW_ITEM_ID: number;
  FLOW_END_TIME: string;
  FlowTypeDic: string;
  FlowItemDic: string;
  StartUserName: string;
  CreateUserName: string;
}
export type flowInstListResultModel = BasicFetchResult<FlowInstListItem>;

/**
 * 列表搜索
 */
export type flowSearchParams = BasicPageParams & {
  KeyWord?: string;
};

export type FlowInstSearchParams = flowSearchParams & {
  CustId?: number;
  FlowType?: number;
};
/***
 * 审批权限
 ***/
export interface PersionApprovalInfo {
  WaitId: number; //ID DEV_FLOW_INST_WAIT_USER 表
  InstId: number;
  NodeId: string;
  ReText: number;
}
/***
 * 获取审批权限是传递的参数对象
 */

export interface ApprovalPerssionDto {
  AppObjId: number;
  AppObjType: number;
  UserId: number;
}
/***
 * 审批权限
 */
export interface ApprovalQx {
  appqx: PersionApprovalInfo;
}

/***
 * 审批意见提交对象
 */
export interface FlowOptionDto {
  WaitId: number;
  InstId: number;
  NodeId: string;
  Sta: number;
  Msg: string;
}

/**
 *查询当前节点信息关键参数
 */
export type FlowNodeParams = {
  NodeStr?: string;
  InstId?: number;
};

/**
 * 节点信息表
 */
export interface FlowInstNodeInfoListItem {
  ID: number;
  OPT_ID: number;
  OPT_NAME: string;
  NODE_STRID: string;
  O_TYPE: number;
  INFO_STATE: number;
  OtypeDsc: string;
  ObjName: string;
  NodeMsg: Array<NodeMsg>;
}
/**
 * 消息
 */
export interface NodeMsg {
  Msg: string;
  UserName: string;
  StartTime?: Date;
  EndTime?: Date;
}
export type FlowNodeInfoListGetResultModel = BasicFetchResult<FlowInstNodeInfoListItem>;

export type SaveFlowPdfReqData = { instId: number };
/***
 * 导出返回pdf信息
 */
export type ResultFlowPdfData = {
  FileName: string; //文件名称
  Memi: string; //类型
  FilePath: string; //路径
};
