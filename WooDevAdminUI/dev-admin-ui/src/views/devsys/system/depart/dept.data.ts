import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { h } from 'vue';
import { Tag } from 'ant-design-vue';

export const columns: BasicColumn[] = [
  {
    title: '部门名称',
    dataIndex: 'NAME',
    width: 160,
    align: 'left',
  },
  {
    title: '编号',
    dataIndex: 'CODE',
    width: 130,
    align: 'left',
  },
  {
    title: '排序',
    dataIndex: 'ORDER_NUM',
    width: 50,
  },
  {
    title: '签约主体',
    dataIndex: 'IS_MAIN',
    width: 100,
    customRender: ({ record }) => {
      const ismain = record.IS_MAIN;
      const enable = ~~ismain === 0;
      const color = enable ? 'green' : 'red';
      const text = enable ? '否' : '是';
      return h(Tag, { color: color }, () => text);
    },
  },
  {
    title: '状态',
    dataIndex: 'DSTATE',
    width: 100,
    customRender: ({ record }) => {
      const DSTATE = record.DSTATE;
      const enable = ~~DSTATE === 0;
      const color = enable ? 'red' : 'green';
      const text = enable ? '停用' : '启用';
      return h(Tag, { color: color }, () => text);
    },
  },

  {
    title: '创建时间',
    dataIndex: 'CREATE_TIME',
    width: 160,
  },
  // {
  //   title: '备注',
  //   dataIndex: 'REMARK',
  // },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'depName',
    label: '部门名称',
    component: 'Input',
    colProps: { span: 8 },
  },
  // {
  //   field: 'status',
  //   label: '状态',
  //   component: 'Select',
  //   componentProps: {
  //     options: [
  //       { label: '启用', value: '0' },
  //       { label: '停用', value: '1' },
  //     ],
  //   },
  //   colProps: { span: 8 },
  // },
];

export const formSchema: FormSchema[] = [
  {
    field: 'NAME',
    label: '部门名称',
    component: 'Input',
    required: true,
    colProps: { span: 24 },
  },
  {
    field: 'ID',
    label: 'ID',
    component: 'Input',
    show: false,
    colProps: { span: 24 },
  },
  {
    field: 'PID',
    label: '上级部门',
    component: 'TreeSelect',
    colProps: { span: 24 },
    componentProps: {
      fieldNames: {
        label: 'NAME',
        key: 'ID',
        value: 'ID',
      },
      getPopupContainer: () => document.body,
    },
    required: true,
  },
  {
    field: 'CODE',
    label: '编号',
    component: 'Input',
    required: true,
    colProps: { span: 24 },
  },
  {
    field: 'ORDER_NUM',
    label: '排序',
    component: 'InputNumber',
    required: true,
    colProps: { span: 24 },
  },
  {
    field: 'IS_MAIN',
    label: '签约主体',
    component: 'RadioButtonGroup',
    defaultValue: 0,
    colProps: { span: 24 },
    componentProps: {
      options: [
        { label: '是', value: 1 },
        { label: '否', value: 0 },
      ],
    },
    required: true,
  },
  {
    field: 'DSTATE',
    label: '状态',
    colProps: { span: 24 },
    component: 'RadioButtonGroup',
    defaultValue: 1,
    componentProps: {
      options: [
        { label: '启用', value: 1 },
        { label: '停用', value: 0 },
      ],
    },
    required: true,
  },
];
