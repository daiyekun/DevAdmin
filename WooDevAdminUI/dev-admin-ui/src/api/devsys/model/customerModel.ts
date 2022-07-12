import { BasicPageParams, BasicFetchResult, ResultviewData } from '/@/api/model/baseModel';
export interface customerListItem {
  ID: number;
  NAME: string;
  CODE: string;
  WF_NODE?: string;
  WF_ITEM?: string;
  FIELD1?: string;
  FIELD2?: string;
  CreateUserName?: string;
  StateDic?: string;
  WfState?: string;
  LeadUserName?: string;
  CateName?: string;
  LevelName?: string;
  CrateName?: string;
  CREATE_TIME: Date;
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
export type customerListGetResultModel = BasicFetchResult<customerListItem>;
export type customerParams = BasicPageParams & {
  Name?: string;
  Code?: string;
};

/**
 * 新建对象
 */
export interface customerSaveInfo {
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
export type customerViewInfo = customerSaveInfo & {
  StateDic: string;
};
export type ViewUserModel = ResultviewData<customerViewInfo>;

/**
 * 客户附件列表
 */
export interface custFileListItem {
  ID: number;
  NAME: string;
  PATH: string;
  CATE_ID?: number;
  REMARK?: string;
  CreateUserName?: string;
  CREATE_TIME: Date;
}
export type custFileListGetResultModel = BasicFetchResult<custFileListItem>;
export type custFileParams = {
  CustId?: number;
};
