/***流程模板***/
import {
  flowItemListResultModel,
  flowitemParams,
  flowObjectListResultModel,
} from '../model/flow/flowTempModel';
import { defHttp } from '/@/utils/http/axios';
// import { ResultData } from '/@/api/model/baseModel';
enum Api {
  getFlowItems = '/DevFlowTemp/getFlowItems', //审批事项
  getFlowObjects = '/DevFlowTemp/getFlowObjs', //审批对象
}

export const getFlowItemList = (params?: flowitemParams) =>
  defHttp.get<flowItemListResultModel>({ url: Api.getFlowItems, params });

export const getFlowObjectist = () =>
  defHttp.get<flowObjectListResultModel>({ url: Api.getFlowObjects });
