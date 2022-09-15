// 数据字典api
import {
  datadicParams,
  datadicListGetResultModel,
  datadicSaveInfo,
  datadicAddInfo,
  datadicdeldata,
  datadictreeParams,
} from '../model/systemModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  datadicList = '/DevDataDic/getdatadicList', //查询字典列表
  datadicSave = '/DevDataDic/datadicSave', //保存字典列表
  datadicAdd = '/DevDataDic/datadicAdd', //初始新增字典列表
  datadicDelete = '/DevDataDic/deleteDic', //删除
  dataList = '/DevDataDic/getdiclist', //查询字典列表
  flowobjdataList = '/DevDataDic/getflowdic', //根据审批对象查询类别

  datatreeList = '/DevDataDic/getTreeList', //查询字典列表
}

export const getdatadicList = (params?: datadicParams) =>
  defHttp.get<datadicListGetResultModel>({ url: Api.datadicList, params });

export const datadicSaveApi = (params: datadicSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.datadicSave, params });
export const datadicAddApi = (params: datadicAddInfo) =>
  defHttp.get<ResultData>({ url: Api.datadicAdd, params });
export const datadicDelApi = (params: datadicdeldata) =>
  defHttp.get<ResultData>({ url: Api.datadicDelete, params });
export const getdataListApi = (params: datadicParams) =>
  defHttp.get<datadicListGetResultModel>({ url: Api.dataList, params });
export const getFlowdataListApi = (params: datadicParams) =>
  defHttp.get<datadicListGetResultModel>({ url: Api.flowobjdataList, params });

export const getdatadictreeList = (params?: datadictreeParams) =>
  defHttp.get<datadicListGetResultModel>({ url: Api.datadicList, params });
