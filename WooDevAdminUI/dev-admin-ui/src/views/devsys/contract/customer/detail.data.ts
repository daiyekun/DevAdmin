import { DescItem } from '/@/components/Description/index';
export const schema: DescItem[] = [
  {
    field: 'NAME',
    label: '用户名',
  },
  {
    field: 'CODE',
    label: '编号',
    // render: (curVal, data) => {
    //   return `${data.NAME}-${curVal}`;
    // },
  },
  {
    field: 'TRADE',
    label: '行业',
  },
  {
    field: 'CateName',
    label: '类别',
  },
  {
    field: 'LevelName',
    label: '客户级别',
  },
  {
    field: 'CrateName',
    label: '信用等级',
  },
  {
    field: 'ZIPCODE',
    label: '邮编',
  },
  {
    field: 'DEPUTY',
    label: '法人代表',
  },
  {
    field: 'EST_DATE',
    label: '成立日期',
  },
  {
    field: 'WEBSITE',
    label: '公司网址',
  },
  {
    field: 'LevelName',
    label: '客户级别',
  },
  {
    field: 'EXP_RANGE',
    label: '经营范围',
  },
  {
    field: 'REG_CAP',
    label: '注册资本',
  },
  {
    field: 'COMP_TYPE',
    label: '公司类型',
  },
  {
    field: 'FIELD1',
    label: '备用1',
  },
  {
    field: 'FIELD2',
    label: '备用2',
  },
  {
    field: '地址',
    label: 'ADDRESS',
  },
];
