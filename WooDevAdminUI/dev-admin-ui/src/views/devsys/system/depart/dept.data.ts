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
    field: 'deptName',
    label: '部门名称',
    component: 'Input',
    required: true,
  },
  {
    field: 'parentDept',
    label: '上级部门',
    component: 'TreeSelect',

    componentProps: {
      fieldNames: {
        label: 'deptName',
        key: 'id',
        value: 'id',
      },
      getPopupContainer: () => document.body,
    },
    required: true,
  },
  {
    field: 'orderNo',
    label: '排序',
    component: 'InputNumber',
    required: true,
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: '0',
    componentProps: {
      options: [
        { label: '启用', value: '0' },
        { label: '停用', value: '1' },
      ],
    },
    required: true,
  },
  {
    label: '备注',
    field: 'remark',
    component: 'InputTextArea',
  },
];
