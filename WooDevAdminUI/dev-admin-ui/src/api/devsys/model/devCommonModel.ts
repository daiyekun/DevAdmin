/**
 * 修改字段对象
 */
export interface devUpdateField {
  Id: number; //Id
  Field?: string; //字段名称
  Value?: string; //修改值
}
/**
 * 删除对象信息-列表操作删除公用
 */
export interface deldataInfo {
  Ids: string;
}

/***
 * 导出数据传参
 */
import { BasicColumn } from '/@/components/Table';
export type ExportExcelData = {
  //选择列表ID
  selkey: Array<string | number>;
  //当前列表列集合
  colums: BasicColumn[];
  //查询表单对象
  seardata: Object;
  //导出类型 客户,供应商...
  extype: string;
};

/****
 * 导出excel
 * 调用后台时参数
 *
 */

export type ExcelInfo = {
  Ids?: string; //选择ID
  Scell: number; //导出列类型
  Srow: number; //导出行类型
  CelKeys: string; //导出列key
  CellNames: string; //导出列名称
  extype: string; //导出表类型 客户，公司
};
/**
 * 导出最终对象
 */

export type ExcelReqData = { Seldata: ExcelInfo; SearData: Object };
/***
 * 导出excel 返回信息
 */
export type ExResultData = {
  FileName: string; //文件名称
  Memi: string; //类型
  FilePath: string; //路径
};
