/**
 * 审批实例相关
 */
import {
  FlowCondition,
  FlowSubmitModel,
  flowInstListResultModel,
  FlowInstSearchParams,
  PersionApprovalInfo,
  ApprovalPerssionDto,
  FlowOptionDto,
  FlowNodeParams,
  FlowNodeInfoListGetResultModel,
  SaveFlowPdfReqData,
  ResultFlowPdfData,
} from '../model/flow/flowInstModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  GetTemp = '/DevFlowInstance/GetTemp', //获取合同模板
  CreateFlowInst = '/DevFlowInstance/createFlowInst', //创建审批实例
  GetFlowInstList = '/DevFlowInstance/getList', //审批实例
  IsAppExistInfo = '/DevFlowInstance/isAppExistInfo', //判断审批权限
  SubmitOption = '/DevFlowInstance/submitOption', //提交意见
  getFlowInstChart = '/DevFlowInstance/getFlowInstChartData', //审批实例流出图
  getNodeInfoByNodeId = '/DevFlowInstance/getNodeInfoByNodeId', //获取审批实例节点信息及意见
  SaveFlowPdf = '/FlowToPdf/flowInstToPdf', //生成pdf
}
export const getFlowTempApi = (params: FlowCondition) =>
  defHttp.post<ResultData>({ url: Api.GetTemp, params });
export const createFlowInstApi = (params: FlowSubmitModel) =>
  defHttp.post<ResultData>({ url: Api.CreateFlowInst, params });
export const GetFlowInstListApi = (params?: FlowInstSearchParams) =>
  defHttp.get<flowInstListResultModel>({ url: Api.GetFlowInstList, params });
export const IsAppExistInfoApi = (params: ApprovalPerssionDto) =>
  defHttp.post<PersionApprovalInfo>({ url: Api.IsAppExistInfo, params });

export const submitOptionApi = (params: FlowOptionDto) =>
  defHttp.post<ResultData>({ url: Api.SubmitOption, params });
export const flowInstChartViewApi = (instId: number) =>
  defHttp.get<string>({ url: Api.getFlowInstChart, params: { instId } });

export const getNodeInfoByNodeIdApi = (params: FlowNodeParams) =>
  defHttp.get<FlowNodeInfoListGetResultModel>({ url: Api.getNodeInfoByNodeId, params });

export const saveFlowPdfApi = (params: SaveFlowPdfReqData) => {
  return defHttp.get<ResultFlowPdfData>({ url: Api.SaveFlowPdf, params });
};
