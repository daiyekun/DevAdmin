//系统管理
import {
  departParams,
  departListGetResultModel,
  departSaveInfo,
  deldataInfo,
} from '../model/systemModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  departList = '/DevDepart/getTreeList', //组织机构查询列表
  departSave = '/DevDepart/departSave', //新建修改保存组织机构
  departDelete = '/DevDepart/deleteDepart', //删除
}

export const getDepartList = (params?: departParams) =>
  defHttp.get<departListGetResultModel>({ url: Api.departList, params });

export const departSaveApi = (params: departSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.departSave, params });

export const departDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.departDelete, params });
