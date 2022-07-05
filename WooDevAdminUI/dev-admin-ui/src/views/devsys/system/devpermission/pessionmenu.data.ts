import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
// import { h } from 'vue';
// import { Tag } from 'ant-design-vue';

export const columns: BasicColumn[] = [
  {
    title: '菜单名称',
    dataIndex: 'menuName',
    width: 200,
    align: 'left',
  },
  // {
  //   title: '权限分配',
  //   dataIndex: 'dypession',
  //   width: 180,
  //   customRender: ({ record }) => {
  //     const status = record.dypession;
  //     const enable = ~~status === 1;
  //     const color = enable ? 'blue' : 'red';
  //     const text = enable ? '需要' : '不需要';
  //     return h(Tag, { color: color }, () => text);
  //   },
  // },
  {
    title: '权限类别',
    dataIndex: 'pssionlb',
    width: 180,
    edit: true,
    editComponent: 'Select',
    editComponentProps: {
      options: [
        { label: '个人', value: 0 },
        { label: '机构', value: 1 },
        { label: '全部', value: 2 },
        { label: '是', value: 4 },
        { label: '否', value: 5 },
        { label: '本机构', value: 3 },
        { label: '无权限', value: -1 },
      ],
    },
  },
  {
    title: '权限标识',
    dataIndex: 'permission',
    width: 180,
  },
  {
    title: '权限描述',
    dataIndex: 'permdisc',
    width: 180,
  },
];

// const isDir = (type: number) => type === 0;
// const isMenu = (type: number) => type === 1;
// const isButton = (type: number) => type === 2;

export const searchFormSchema: FormSchema[] = [
  {
    field: 'MenuName',
    label: '菜单名称',
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'Persiondic',
    label: '权限描述',
    component: 'Input',
    colProps: { span: 8 },
  },
];
/**
 * 权限类
 */
export type permssionInfo = {
  Id: number;
  Permission?: string;
  Pssionlb: number;
  RoleId: number;
};
