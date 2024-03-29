/***流程模板***/
import {
  flowItemListResultModel,
  flowitemParams,
  flowObjectListResultModel,
  FlowTempSaveInfo,
  flowTempListGetResultModel,
  flowTempParams,
  deldataInfo,
  FlowTempViewInfo,
  FlowChartTempSaveInfo,
  FlowNodeParams,
  FlowNodeInfoListGetResultModel,
  FlowTempNodeInfo,
  FlowTempNodeUpdateInfo,
  ExistNodeInfo,
} from '../model/flow/flowTempModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  getFlowItems = '/DevFlowTemp/getFlowItems', //审批事项
  getFlowObjects = '/DevFlowTemp/getFlowObjs', //审批对象
  flowTempSave = '/DevFlowTemp/flowTempSave', //审批模板保存
  flowTempList = '/DevFlowTemp/getList', //审批模板列表
  flowTempDelete = '/DevFlowTemp/delFlowTemp', //删除
  flowTempView = '/Customer/flowTempView', //审批模板详情
  setFlowTempStatus = '/DevFlowTemp/setFlowTempStatus', //修改状态
  flowChartTempSave = '/DevFlowTemp/flowChartTempSave', //保持模板流程图
  getFlowTempChart = '/DevFlowTemp/getFlowChartData', //根据模板ID获取流出图
  getNodeInfoByNodeId = '/DevFlowTemp/getNodeInfoByNodeId', //根据节点ID查询节点信息
  flowNodeInfoSave = '/DevFlowTemp/flowNodeInfoSave', //节点信息修改
  flowNodeInfoAppObj = '/DevFlowTemp/delFlowNodeInfoObj', //删除节点审批人员
  flowNodeUpdate = '/DevFlowTemp/flowNodeUpdate', //节点修改
  IsExistNode = '/DevFlowTemp/IsExistNode', //根据节点ID与模板ID判断节点是否保存
}

export const getFlowItemList = (params?: flowitemParams) =>
  defHttp.get<flowItemListResultModel>({ url: Api.getFlowItems, params });

export const getFlowObjectist = () =>
  defHttp.get<flowObjectListResultModel>({ url: Api.getFlowObjects });

export const flowTempSaveApi = (params: FlowTempSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.flowTempSave, params });

export const getFlowTempList = (params?: flowTempParams) =>
  defHttp.get<flowTempListGetResultModel>({ url: Api.flowTempList, params });
export const flowTempDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.flowTempDelete, params });

export const flowTempomerViewApi = (id: number) =>
  defHttp.get<FlowTempViewInfo>({ url: Api.flowTempView, params: { id } });

export const setFlowTempStatus = (id: number, status: number) =>
  defHttp.post({ url: Api.setFlowTempStatus, params: { id, status } });

export const flowChartTempSaveApi = (params: FlowChartTempSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.flowChartTempSave, params });

export const flowTempChartViewApi = (tempId: number) =>
  defHttp.get<string>({ url: Api.getFlowTempChart, params: { tempId } });

export const getNodeInfoByNodeIdApi = (params: FlowNodeParams) =>
  defHttp.get<FlowNodeInfoListGetResultModel>({ url: Api.getNodeInfoByNodeId, params });

export const flowNodeInfoSaveApi = (params: FlowTempNodeInfo) =>
  defHttp.post<ResultData>({ url: Api.flowNodeInfoSave, params });

export const flowNodeInfoAppObjApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.flowNodeInfoAppObj, params });

export const flowNodeUpdateApi = (params: FlowTempNodeUpdateInfo) =>
  defHttp.post<ResultData>({ url: Api.flowNodeUpdate, params });

export const IsExistNodeApi = (params: ExistNodeInfo) =>
  defHttp.get<ResultData>({ url: Api.IsExistNode, params });
