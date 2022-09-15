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
