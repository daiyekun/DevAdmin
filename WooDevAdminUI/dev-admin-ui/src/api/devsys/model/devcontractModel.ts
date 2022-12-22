import { BasicPageParams, BasicFetchResult, ResultviewData } from '/@/api/model/baseModel';
export interface contractListItem {
  ID: number;
  C_NAME: string;
  C_CODE: string;
  CATE_ID: number;
  ANT_MONERY: number;
  CONT_STATE: number;
  CREATE_USERID: number;
  CREATE_TIME: Date;
  HEAD_USER_ID: number;
  WF_NODE?: string;
  WF_ITEM?: string;
  DEPART_ID: number;
  COMP_ID: number;
  SIGNING_DATE: Date;
  EFFECTIVE_DATE: Date;
  PLAN_DATE: Date;
  ACT_DATE: Date;
  IS_FRAMEWORK?: number;
  IS_SUBCONT?: number;
  WfFlowDic: string;
  StateDic: string;
  WfState: string;
  CreateUserName: string;
  LeadUserName: string;
  CateName: string;
  ComName: string;
  ANT_MONERYThod: string;
  DeptName: string;
  MainDeptName: string;
  ContPro: string;
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
export type constractListGetResultModel = BasicFetchResult<contractListItem>;
export type constractParams = BasicPageParams & {
  Name?: string;
  Code?: string;
  ComName?: string;
  CATE_ID?: number;
  SIGNING_DATE?: Date;
  EFFECTIVE_DATE?: Date;
  IS_FRAMEWORK?: number;
};
/**
 * 新建对象
 */
export interface contractSaveInfo {
  ID: number;
  NAME: string;
  CODE: string;
  CATE_ID: number;
  TRADE: string;
  LEVEL_ID: number;
  CREATE_ID: number;
  ZIPCODE: string;
  DEPUTY: string;
  EST_DATE: Date;
  WEBSITE: string;
  EXP_RANGE: string;
  REG_CAP: string;
  COMP_TYPE: string;
  FIELD1: string;
  FIELD2: string;
  ADDRESS: string;
  LEAD_USERID: number;
}

/*
 *修改查看信息载体
 */
export type contractViewInfo = contractSaveInfo & {
  StateDic: string;
  SourceName: string;
  IS_SUBCONT_DSC: string;
  EST_MONERY_Thod: string;
  ADVANCE_MONERY_Thod: string;
};
export type ViewUserModel = ResultviewData<contractViewInfo>;
export type ContChidParams = {
  CustId?: number;
};
/**
 * 计划资金新建载体
 */
export interface contplanfinceSaveInfo {
  ID: number;
  NAME: string;
  F_TYPE: number;
  PLAN_DATE: Date;
  REMARK: string;
  CONT_ID: number;
}

/**
 * 计划资金
 */
export interface planFinceListItem {
  ID: number;
  NAME: string;
  F_TYPE: number;
  PLAN_DATE: Date;
  REMARK: string;
  CONT_ID: number;
  CreateUserName: string;
  AMOUNT_Thod: string;
  SETT_YPE_DSC: string;
}
export type PlanFinceListGetResultModel = BasicFetchResult<planFinceListItem>;

/**
 * 合同文本
 */
export interface ContTextListItem {
  ID: number;
  NAME: string;
  CATE_ID: number;
  FILE_NAME: string;
  EXTEND: string;
  TEXT_PATH: string;
  DOWN_TIMES?: number;
  REMARK?: string;
  CREATE_USERID: number;
  CreateUserName?: string;
  CREATE_TIME: Date;
  CateName: string;
  STAGE: number;
  CONT_ID: number;
}
export type ContTextListGetResultModel = BasicFetchResult<ContTextListItem>;

/**
 * 合同附件
 */
export interface ContAttachmentListItem {
  ID: number;
  F_NAME: string;
  CATE_ID: number;
  FILE_NAME: string;
  EXTEND: string;
  FILE_PATH: string;
  DOWN_TIME?: number;
  REMARK?: string;
  CREATE_USERID: number;
  CreateUserName?: string;
  CREATE_TIME: Date;
  CateName: string;
  CONT_ID: number;
}
export type ContAttachmentListGetResultModel = BasicFetchResult<ContAttachmentListItem>;

/**
 * 标的保存对象
 */
export interface contSubmatterSaveInfo {
  ID: number;
  S_NAME: string;
  SPEC: string;
  S_TYPE: string;
  UNIT: string;
  AMOUNT: number;
  PRICE?: number;
  SUBTOTAL?: number;
  PLAN_DATE: Date;
  SALE_PRICE: number;
  REMARK: string;
  CONT_ID: number;
}

/**
 * 合同标的
 */
export interface ContSubMatterListItem {
  ID: number;
  S_NAME: string;
  SPEC: string;
  S_TYPE: string;
  UNIT: string;
  AMOUNT: number;
  PRICE?: number;
  SUBTOTAL?: number;
  CREATE_USERID: number;
  CreateUserName?: string;
  CREATE_TIME: Date;
  PLAN_DATE: Date;
  SALE_PRICE: number;
  REMARK: string;
  SUBTOTAL_Thod: string;
  CONT_ID: number;
}
export type ContSubmatterListGetResultModel = BasicFetchResult<ContSubMatterListItem>;
