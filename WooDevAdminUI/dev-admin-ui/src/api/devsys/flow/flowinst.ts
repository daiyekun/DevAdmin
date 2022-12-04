/**
 * 审批实例相关
 */
import { FlowCondition } from '../model/flow/flowInstModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  GetTemp = '/DevFlowInstance/GetTemp', //获取合同模板
}
export const flowTempSaveApi = (params: FlowCondition) =>
  defHttp.post<ResultData>({ url: Api.GetTemp, params });
