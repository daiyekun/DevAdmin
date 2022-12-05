/***
 * 实例相关模型
 ***/
//import { BasicFetchResult, BasicPageParams } from '/@/api/model/baseModel';
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
