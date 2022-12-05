/**
 * 审批实例相关
 */
import { FlowCondition, FlowSubmitModel } from '../model/flow/flowInstModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  GetTemp = '/DevFlowInstance/GetTemp', //获取合同模板
  CreateFlowInst = '/DevFlowInstance/createFlowInst', //创建审批实例
}
export const getFlowTempApi = (params: FlowCondition) =>
  defHttp.post<ResultData>({ url: Api.GetTemp, params });
export const createFlowInstApi = (params: FlowSubmitModel) =>
  defHttp.post<ResultData>({ url: Api.CreateFlowInst, params });