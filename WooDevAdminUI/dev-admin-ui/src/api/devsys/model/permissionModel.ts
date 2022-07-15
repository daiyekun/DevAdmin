/**
 * 新建权限传参
 */
export interface CreatePermission {
  PerCode: string; //权限标识
}
/***
 *删除权限
 */
export interface DeletePermission {
  PerCode: string; //权限标识
  Ids: string; //要删除的ID
}
/***
 * 修改2权限
 */
export interface UpdatePermission {
  PerCode: string; //权限标识
  Id: number; //修改ID
}

/***
 * 查看权限
 */
export interface detailPermission {
  PerCode: string; //权限标识
  Id: number; //修改ID
}
