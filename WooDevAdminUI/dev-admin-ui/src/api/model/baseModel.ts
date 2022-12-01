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
  //resdata: any;
}
export interface ResultviewData<T> {
  result: T;
  code: number;
  message: string;
}
