export interface BasicPageParams {
  page: number;
  pageSize: number;
}

export interface BasicFetchResult<T> {
  items: T[];
  total: number;
}

export interface ResultData {
  code: number;
  message: string;
  result: any;
}
export interface ResultDataT<T> {
  code: number;
  message: string;
  result: T;
}
export interface ResultviewData<T> {
  result: T;
  code: number;
  message: string;
}
export interface ResData {
  result: number;
  data: any;
}
