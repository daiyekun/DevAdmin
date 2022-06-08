// 数据字典api
import { datadicParams, datadicListGetResultModel, datadicSaveInfo } from '../model/systemModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  datadicList = '/DevDataDic/getdatadicList',
  datadicSave = '/DevDataDic/datadicSave',
}

export const getdatadicList = (params: datadicParams) =>
  defHttp.get<datadicListGetResultModel>({ url: Api.datadicList, params });

export const datadicSaveApi = (params: datadicSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.datadicSave, params });
