//系统管理
import { departParams, departListGetResultModel } from '../model/systemModel';
import { defHttp } from '/@/utils/http/axios';
//import { ResultData } from '/@/api/model/baseModel';
enum Api {
  departList = '/DevDepart/getTreeList', //组织机构查询列表
  datadicSave = '/DevDataDic/datadicSave', //保存字典列表
  datadicAdd = '/DevDataDic/datadicAdd', //初始新增字典列表
  datadicDelete = '/DevDataDic/deleteDic', //删除
}

export const getDepartList = (params: departParams) =>
  defHttp.get<departListGetResultModel>({ url: Api.departList, params });

// export const datadicSaveApi = (params: datadicSaveInfo) =>
//   defHttp.post<ResultData>({ url: Api.datadicSave, params });
// export const datadicAddApi = (params: datadicAddInfo) =>
//   defHttp.get<ResultData>({ url: Api.datadicAdd, params });
// export const datadicDelApi = (params: datadicdeldata) =>
//   defHttp.get<ResultData>({ url: Api.datadicDelete, params });
