import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { TreeItem } from '/@/components/Tree/index';

export const columns: BasicColumn[] = [
  {
    title: '名称',
    dataIndex: 'NAME',
    width: 160,
    //edit: true,
    editRow: true,
    editComponent: 'Input',
  },
  {
    title: '简称',
    dataIndex: 'SORT_NAME',
    width: 130,
    //edit: true,
    editRow: true,
    // editComponent: 'Input',
  },
  {
    title: '排序',
    width: 110,
    dataIndex: 'ORDER_NUM',
    //edit: true,
    editRow: true,
    // editComponent: 'InputNumber',
  },
  {
    title: '描述',
    dataIndex: 'REMARK',
    //edit: true,
    editRow: true,
    // editComponent: 'Input',
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '名称',
    component: 'Input',
    colProps: { span: 8 },
  },
  // {
  //   field: 'nickname',
  //   label: '昵称',
  //   component: 'Input',
  //   colProps: { span: 8 },
  // },
];

export const datadicFormSchema: FormSchema[] = [
  {
    field: 'NAME',
    label: '名称',
    component: 'Input',
    required: true,
  },
  {
    field: 'SORT_NAME',
    label: '简称',
    component: 'Input',
    required: true,
  },
  {
    label: '排序',
    field: 'ORDER_NUM',
    component: 'Input',
    required: true,
  },

  {
    label: '备注',
    field: 'REMARK',
    component: 'InputTextArea',
  },
];

export const datatreeData: TreeItem[] = [
  {
    title: '数据字典',
    key: '01',
    id: '0',
    icon: 'home|svg',
    children: [
      { title: '部门类别', key: 'bmlb', id: 1 },
      { title: '合同类别', key: 'htlb', id: 2 },
      { title: '合同附件', key: 'htfj', id: 3 },
      { title: '客户附件', key: 'khfj', id: 4 },
      { title: '供应商附件', key: 'gysfj', id: 5 },
    ],
  },
];
