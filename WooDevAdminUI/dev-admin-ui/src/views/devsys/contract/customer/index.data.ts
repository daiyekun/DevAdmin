import { FormProps } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table/src/types/table';
export const customercolumns: BasicColumn[] = [
  {
    title: 'ID',
    dataIndex: 'ID',
    width: 80,
    defaultHidden: true,
  },
  {
    title: '名称',
    dataIndex: 'NAME',
    width: 220,
    fixed: 'left',
  },
  {
    title: '编号',
    dataIndex: 'CODE',
    width: 120,
  },
  {
    title: '类别',
    dataIndex: 'CateName',
    width: 140,
  },
  // {
  //   title: '负责人',
  //   dataIndex: 'LeadUserName',
  //   width: 120,
  // },

  {
    title: '状态',
    dataIndex: 'StateDic',
    width: 120,
  },
  {
    title: '客户级别',
    dataIndex: 'LevelName',
    width: 130,
  },
  {
    title: '信用等级',
    dataIndex: 'CrateName',
    width: 130,
  },
  {
    title: '备用1',
    dataIndex: 'FIELD1',
    width: 140,
  },
  {
    title: '备用2',
    dataIndex: 'FIELD2',
    width: 140,
  },
  {
    title: '流程状态',
    dataIndex: 'WfState',
    width: 130,
  },
  {
    title: '流程节点',
    dataIndex: 'WF_NODE',
    width: 130,
  },
  {
    title: '审批事项',
    dataIndex: 'WF_ITEM',
    width: 130,
  },

  {
    title: '创建时间',
    dataIndex: 'CREATE_TIME',
    sorter: true,
    // width: 130,//最后一列不设置宽度，否则错乱
  },
  // {
  //   title: '创建人',
  //   dataIndex: 'CreateUserName',
  //   width: 130,
  // },
  // {
  //   title: '操作',
  //   key: 'operation',
  //   fixed: 'right',
  //   width: 130,
  // },
];

export function getFormConfig(): Partial<FormProps> {
  return {
    labelWidth: 100,
    schemas: [
      {
        field: `Name`,
        label: `名称`,
        component: 'Input',
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },
      {
        field: `Code`,
        label: `编号`,
        component: 'Input',
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },
      // {
      //   field: `CateId`,
      //   label: `客户类别`,
      //   component: 'Input',
      //   colProps: {
      //     xl: 8,
      //     xxl: 8,
      //   },
      // }
    ],
  };
}
