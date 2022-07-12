// 客户 api
import {
  customerParams,
  customerListGetResultModel,
  customerSaveInfo,
  custFileListGetResultModel,
  custFileParams,
} from '../model/customerModel';
import { devUpdateField, deldataInfo } from '../model/devCommonModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  customerList = '/Customer/getCustomerList', //查询列表
  customerSave = '/Customer/customerSave', //新建修改保存用户
  customerFileSave = '/Customer/customerFileSave', //附件保存
  customerFileList = '/Customer/getCustFileList', //附件列表
  custUpdateField = '/Customer/custUpdateField', //修改附件字段
  custFileDel = '/Customer/custFileDel', //删除附件
}

export const getCusertomerListApi = (params: customerParams) =>
  defHttp.get<customerListGetResultModel>({ url: Api.customerList, params });

export const customerSaveApi = (params: customerSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.customerSave, params });
export const uploadfileSaveApi = (params: any) =>
  defHttp.post<ResultData>({ url: Api.customerFileSave, params });

export const getCustFileListApi = (params: custFileParams) =>
  defHttp.get<custFileListGetResultModel>({ url: Api.customerFileList, params });
export const custUpdateFieldApi = (params: devUpdateField) =>
  defHttp.post<ResultData>({ url: Api.custUpdateField, params });
export const custFileDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.custFileDel, params });
