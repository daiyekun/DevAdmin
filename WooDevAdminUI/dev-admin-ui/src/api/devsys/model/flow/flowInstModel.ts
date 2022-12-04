/***
 * 实例相关模型
 ***/
//import { BasicFetchResult, BasicPageParams } from '/@/api/model/baseModel';
/***
 * 查询模板条件
 ***/
export interface FlowCondition {
  FlowObj: number;
  CateId: number;
  DeptId: number;
  FlowItem: number;
  Monery?: number;
}
/**
 * 查询流程图 提交相关对象
 */
export type FlowShowData = {
  Condition: FlowCondition; //对象部分信息
  TempId: number; //模板ID
  Name: string;
};
