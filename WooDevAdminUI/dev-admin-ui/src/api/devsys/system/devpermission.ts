//权限
import {
  CreatePermission,
  DeletePermission,
  UpdatePermission,
  detailPermission,
} from '../model/permissionModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  createPermission = '/DevPermission/getCreatePermission', //新建权限
  deletePermission = '/DevPermission/deletePermission', //删除权限
  updatePermission = '/DevPermission/updatePermission', //修改权限
  detailPermission = '/DevPermission/detailPermission', //查看
}

export const GetCreatePermissionApi = (params: CreatePermission) =>
  defHttp.get<ResultData>({ url: Api.createPermission, params });

export const GetDeletePermissionApi = (params: DeletePermission) =>
  defHttp.get<ResultData>({ url: Api.deletePermission, params });

export const GetUpdatePermissionApi = (params: UpdatePermission) =>
  defHttp.get<ResultData>({ url: Api.updatePermission, params });
export const GetDetailPermissionApi = (params: detailPermission) =>
  defHttp.get<ResultData>({ url: Api.detailPermission, params });
