/**
 * dev 系统对些特殊场景axios封装
 * 改原有的能力有限
 */
 import type { AxiosRequestConfig} from 'axios';
 import axios from 'axios';
 import { devconfig } from '/@/api/devsys/model/devConfig.data';

//  export function exportExcel(form:any) {
//   return axios({ // 用axios发送post请求
//       method: 'post',
//       url: devconfig, // 请求地址
//       data: form, // 参数
//       responseType: 'blob', // 表明返回服务器返回的数据类型
//       headers: {
//           'Content-Type': 'application/json'
//       }
//   })
// }

// exportExcel(params:any).then(res => { // 处理返回的文件流
//   const blob = new Blob([res]);
//   const fileName = '统计.xlsx';
//   const elink = document.createElement('a');
//   elink.download = fileName;
//   elink.style.display = 'none';
//   elink.href = URL.createObjectURL(blob);
//   document.body.appendChild(elink);
//   elink.click();
//   URL.revokeObjectURL(elink.href); // 释放URL 对象
//   document.body.removeChild(elink);
// })

export interface ExportExcelparam {
  api: string;
  filename:string;
  params: {};
}

/***
 * 导出excel
 */
export const devExportExceApi=(param:ExportExcelparam)=> {
let config:AxiosRequestConfig = {
  headers: {
  "Content-Type": "multipart/form-data"
  },
  'responseType':'blob'
  };
  let reqapi=devconfig.devupload.excelrul+param.api;
  axios.post(reqapi,param.params,config).then((res)=>{
  let blob = new Blob([res.data], {type: "application/vnd.ms-excel"});
  const fileName = param.filename;
  const elink = document.createElement('a');
  elink.download = fileName;
  elink.style.display = 'none';
  elink.href = URL.createObjectURL(blob);
  document.body.appendChild(elink);
  elink.click();
  URL.revokeObjectURL(elink.href); // 释放URL 对象
  document.body.removeChild(elink);
 
}
};
