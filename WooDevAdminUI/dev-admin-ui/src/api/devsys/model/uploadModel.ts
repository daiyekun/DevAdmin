export interface UploadApiResult {
  message: string;
  code: number;
  url: string;
}
//自定义导出数据
export interface devUploadApiResult {
  message: string;
  code: number;
  result: {};
}
/**
 * 权限类
 */
export type uploadInfo = {
  Extension?: string;
  FolderIndex?: number;
  FolderName?: string;
  FolderPath?: string;
  GuidFileName?: string;
  RemGuidName?: Boolean;
  SourceFileName?: string;
  TempId: number;
};
