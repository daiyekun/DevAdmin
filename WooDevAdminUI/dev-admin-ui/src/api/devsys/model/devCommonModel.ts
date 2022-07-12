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
