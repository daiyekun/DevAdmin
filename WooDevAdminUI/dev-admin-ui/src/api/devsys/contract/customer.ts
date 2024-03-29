// 客户 api
import {
  customerParams,
  customerListGetResultModel,
  customerSaveInfo,
  custFileListGetResultModel,
  custFileParams,
  custContactListGetResultModel,
  custContactSaveInfo,
  CustomerViewInfo,
} from '../model/customerModel';
import { devUpdateField, deldataInfo, ExcelReqData, updateStateDto } from '../model/devCommonModel';
import { defHttp } from '/@/utils/http/axios';
import { ResultData } from '/@/api/model/baseModel';
//import { ResponseType } from 'axios';

enum Api {
  customerList = '/Customer/getCustomerList', //查询列表
  customerSave = '/Customer/customerSave', //新建修改保存用户
  customerView = '/Customer/customerView', //客户详情
  customerDel = '/Customer/customerDel', //客户详情
  customerExcel = '/Customer/exportexcel', //导出excel
  updateState = '/Customer/updateState', //修改状态

  customerClearData = '/Customer/customerClear', //清理客户数据
  customerFileSave = '/CustFile/customerFileSave', //附件保存
  customerFileList = '/CustFile/getCustFileList', //附件列表
  custUpdateField = '/CustFile/custUpdateField', //修改附件字段
  custFileDel = '/CustFile/custFileDel', //删除附件
  custContactList = '/CustContact/getCustContactList', //联系人列表
  custContactSave = '/CustContact/custContactSave', //联系人保存
  custContactDel = '/CustContact/custContactDel', //删除联系人
}

export const getCusertomerListApi = (params: customerParams) =>
  defHttp.get<customerListGetResultModel>({ url: Api.customerList, params });

export const customerSaveApi = (params: customerSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.customerSave, params });
export const CustomerViewApi = (id: number) =>
  defHttp.get<CustomerViewInfo>({ url: Api.customerView, params: { id } });
export const customerDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.customerDel, params });

// export const customerExcelApi = (params: ExcelReqData) =>
//   defHttp.post<ResultData>({ responseType: 'blob', url: Api.customerExcel, params });
export const customerExcelApi = (params: ExcelReqData) =>
  defHttp.post<ResultData>({ url: Api.customerExcel, params });

export const uploadfileSaveApi = (params: any) =>
  defHttp.post<ResultData>({ url: Api.customerFileSave, params });

export const getCustFileListApi = (params: custFileParams) =>
  defHttp.get<custFileListGetResultModel>({ url: Api.customerFileList, params });
export const custUpdateFieldApi = (params: devUpdateField) =>
  defHttp.post<ResultData>({ url: Api.custUpdateField, params });
export const custFileDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.custFileDel, params });

export const getCustContactListApi = (params: custFileParams) =>
  defHttp.get<custContactListGetResultModel>({ url: Api.custContactList, params });
export const custContactSaveApi = (params: custContactSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.custContactSave, params });
export const custContactDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.custContactDel, params });
export const customerClearDataApi = () => defHttp.get<ResultData>({ url: Api.customerClearData });
export const updateStateApi = (params: updateStateDto) =>
  defHttp.post<ResultData>({ url: Api.updateState, params });
