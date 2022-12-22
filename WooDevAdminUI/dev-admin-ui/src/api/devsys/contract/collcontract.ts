import { defHttp } from '/@/utils/http/axios';
import {
  constractParams,
  constractListGetResultModel,
  contractSaveInfo,
  contractViewInfo,
  PlanFinceListGetResultModel,
  ContChidParams,
  contplanfinceSaveInfo,
  ContTextListGetResultModel,
  ContAttachmentListGetResultModel,
  ContSubmatterListGetResultModel,
  contSubmatterSaveInfo,
} from '../model/devcontractModel';
import { devUpdateField, deldataInfo, ExcelReqData, updateStateDto } from '../model/devCommonModel';
import { ResultData } from '/@/api/model/baseModel';
enum Api {
  contractList = '/CollContract/getContractList', //查询列表
  contractSave = '/CollContract/constractSave', //新建修改保存用户
  contractView = '/CollContract/constractView', //详情
  contractDel = '/CollContract/constractDel', //删除
  contractClearData = '/CollContract/constractClear', //清理客户数据
  contractExcel = '/CollContract/exportexcel', //导出excel
  updateState = '/CollContract/updateState', //修改状态
  planfinceList = '/PlanFince/getPlanFinceList', //计划资金列表
  planfinceSave = '/PlanFince/planFinceSave', //计划资金保存
  planfinceDel = '/PlanFince/planFinceDel', //删除计划资金
  contTextSave = '/ContText/contTextSave', //文本保存
  getContTextList = '/ContText/getContTextList', //文本列表
  contTextUpdateField = '/ContText/contTextUpdateField', //修改文本字段
  contTextDel = '/ContText/contTextDel', //删除文本
  contAttachmentSave = '/ContAttachment/contAttachmentSave', //合同附件保存
  getContAttachmentList = '/ContAttachment/getContAttachmentList', //合同附件列表
  contAttachmentUpdateField = '/ContAttachment/contAttachmentUpdateField', //修改合同附件字段
  contAttachmentDel = '/ContAttachment/contAttachmentDel', //删除合同附件

  contSubmatterSave = '/SubMatter/submatterSave', //标的保存
  getSubmatterList = '/SubMatter/getSubmatterList', //标的列表
  contSubmatterUpdateField = '/SubMatter/contSubmatterUpdateField', //修改合同标的字段
  submatterDel = '/SubMatter/submatterDel', //删除合同附件
}
export const getContractListApi = (params: constractParams) =>
  defHttp.get<constractListGetResultModel>({ url: Api.contractList, params });
export const constractSaveApi = (params: contractSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.contractSave, params });
export const constractViewApi = (id: number) =>
  defHttp.get<contractViewInfo>({ url: Api.contractView, params: { id } });
export const constractDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.contractDel, params });
export const updateStateApi = (params: updateStateDto) =>
  defHttp.post<ResultData>({ url: Api.updateState, params });
export const constractExcelApi = (params: ExcelReqData) =>
  defHttp.post<ResultData>({ url: Api.contractExcel, params });
export const contractClearDataApi = () => defHttp.get<ResultData>({ url: Api.contractClearData });

//计划资金
export const getPlanFinceListApi = (params: ContChidParams) =>
  defHttp.get<PlanFinceListGetResultModel>({ url: Api.planfinceList, params });
export const planFinceSaveApi = (params: contplanfinceSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.planfinceSave, params });
export const planFinceDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.planfinceDel, params });

//合同文本
export const contTextSaveApi = (params: any) =>
  defHttp.post<ResultData>({ url: Api.contTextSave, params });
export const getContTextListApi = (params: ContChidParams) =>
  defHttp.get<ContTextListGetResultModel>({ url: Api.getContTextList, params });
export const contTextUpdateFieldApi = (params: devUpdateField) =>
  defHttp.post<ResultData>({ url: Api.contTextUpdateField, params });
export const contTextDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.contTextDel, params });

//合同附件
export const contAttachmentSaveApi = (params: any) =>
  defHttp.post<ResultData>({ url: Api.contAttachmentSave, params });
export const getContAttachmentListApi = (params: ContChidParams) =>
  defHttp.get<ContAttachmentListGetResultModel>({ url: Api.getContAttachmentList, params });
export const contAttachmentUpdateFieldApi = (params: devUpdateField) =>
  defHttp.post<ResultData>({ url: Api.contAttachmentUpdateField, params });
export const contAttachmentDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.contAttachmentDel, params });

//标的
export const getSubmatterListApi = (params: ContChidParams) =>
  defHttp.get<ContSubmatterListGetResultModel>({ url: Api.getSubmatterList, params });
export const contSubmatterSaveApi = (params: contSubmatterSaveInfo) =>
  defHttp.post<ResultData>({ url: Api.contSubmatterSave, params });
export const submatterDelApi = (params: deldataInfo) =>
  defHttp.get<ResultData>({ url: Api.submatterDel, params });
