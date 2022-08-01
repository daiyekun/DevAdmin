//审批组管理
import { deldataInfo, UserListGetResultModel, UserParams } from '../model/systemModel';
import {
  flowGroupistGetResultModel,
  rgroupParams,
  flowGroupSaveInfo,
} from '../model/flow/flowGroupModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  flowGroupList = '/DevFlowTempGroup/getList', //列表
  flowgroupSave = '/DevFlowTempGroup/flowGroupSave', //新建修改保存
  flowgroupDelete = '/DevFlowTempGroup/delFlowGroup', //删除
  getAllFlowGroupList = '/DevFlowTempGroup/getAllFlowGroupList', //查询所有
  setFlowGroupStatus = '/DevFlowTempGroup/setFlowGroupStatus', //修改状态
  getGroupUserList = '/DevFlowTempGroup/getUserList', //查询组用户
}

export const getFlowGroupList = (params?: rgroupParams) =>
  defHttp.get<flowGroupistGetResultModel>({ url: Api.flowGroupList, params });
export const getAllFlowGroupList = (params?: rgroupParams) =>
  defHttp.get<flowGroupistGetResultModel>({ url: Api.getAllFlowGroupList, params });
export const setFlowGroupStatus = (id: number, status: number) =>
  defHttp.post({ url: Api.setFlowGroupStatus, params: { id, status } });

export const flowGroupSaveApi = (params: flowGroupSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.flowgroupSave, params });

export const flowGroupDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.flowgroupDelete, params });

export const getGroupUserList = (params: UserParams) =>
  defHttp.get<UserListGetResultModel>({ url: Api.getGroupUserList, params });
