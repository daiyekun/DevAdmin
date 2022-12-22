/***
 * 导出文件使用
 */
import { ExcelReqData, ExResultData } from '../model/devCommonModel';
import { defHttp } from '/@/utils/http/axios';
// import { UploadApiResult } from '/@/api/devsys/model/uploadModel';
export const customerExcelApi = (params: ExcelReqData) => {
  let rapi = '';
  const extype = params.Seldata.extype;
  switch (extype) {
    case 'customer': {
      //客户
      rapi = '/Customer/exportexcel'; //导出excel
      break;
    }
    case 'collcontract': {
      //收款合同
      rapi = '/CollContract/exportexcel'; //导出excel
      break;
    }
  }
  return defHttp.post<ExResultData>({ url: rapi, params });
};
