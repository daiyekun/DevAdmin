import { FormProps } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table/src/types/table';
import { h } from 'vue';
import { Tag } from 'ant-design-vue';
import { getdataListApi } from '/@/api/devsys/system/datadic';
export const contcolumns: BasicColumn[] = [
  {
    title: 'ID',
    dataIndex: 'ID',
    width: 80,
    defaultHidden: true,
  },
  {
    title: '名称',
    dataIndex: 'C_NAME',
    width: 220,
    fixed: 'left',
    align: 'left',
  },
  {
    title: '编号',
    dataIndex: 'C_CODE',
    width: 130,
    align: 'left',
  },
  {
    title: '合同对方',
    dataIndex: 'ComName',
    width: 180,
    align: 'left',
  },
  {
    title: '类别',
    dataIndex: 'CateName',
    width: 140,
    // align: 'left',
  },
  {
    title: '合同金额',
    dataIndex: 'ANT_MONERYThod',
    width: 140,
  },
  {
    title: '合同属性',
    dataIndex: 'ContPro',
    width: 140,
  },
  {
    title: '经办机构',
    dataIndex: 'DeptName',
    width: 130,
  },
  {
    title: '签约主体',
    dataIndex: 'MainDeptName',
    width: 130,
  },
  {
    title: '签订日期',
    dataIndex: 'SIGNING_DATE',
    width: 130,
  },
  {
    title: '生效日期',
    dataIndex: 'EFFECTIVE_DATE',
    width: 130,
  },
  {
    title: '计划完成日期',
    dataIndex: 'PLAN_DATE',
    width: 130,
  },

  {
    title: '状态',
    dataIndex: 'StateDic',
    width: 120,
    customRender: ({ record }) => {
      const status = record.CONT_STATE;
      let color = 'default';
      const text = record.StateDic;
      switch (~~status) {
        case 0: //未审核
          color = 'default';
          break;
        case 1: //执行中
          color = 'green';
          break;
        case 2: //已终止
          color = '#f50';
          break;
        case 3: //已作废
          color = '#FF6600';
          break;
        case 4: //审批中
          color = '#33FF33';
          break;
        case 5: //被打回
          color = '#FF9900';
          break;
        case 6: //已完成
          color = 'green';
          break;
        case 8: //审批通过
          color = '#0000FF';
          break;
        default:
          color = '#003399';
          break;
      }
      return h(Tag, { color: color }, () => text);
    },
  },

  {
    title: '流程状态',
    dataIndex: 'WfState',
    width: 130,
  },
  {
    title: '流程节点',
    dataIndex: 'WF_NODE',
    width: 140,
    align: 'left',
  },
  {
    title: '审批事项',
    dataIndex: 'WfFlowDic',
    width: 150,
  },
  {
    title: '创建人',
    dataIndex: 'CreateUserName',
    width: 130,
  },
  {
    title: '创建时间',
    dataIndex: 'CREATE_TIME',
    sorter: true,
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

const storeFrameworkOptions: LabelValueOptions = [
  {
    label: '标准合同',
    value: '0',
  },
  {
    label: '框架合同',
    value: '1',
  },
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
      {
        field: `ComName`,
        label: `合同对方`,
        component: 'Input',
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },
      {
        field: 'CATE_ID',
        component: 'ApiSelect',
        componentProps: {
          api: getdataListApi,
          labelField: 'NAME',
          valueField: 'ID',
          params: { LbId: 2 },
        },
        label: '合同类别',
        //required: true,
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },
      {
        field: 'SIGNING_DATE',
        component: 'RangePicker',
        label: '签订日期',
        // required: true,
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },
      {
        field: 'EFFECTIVE_DATE',
        component: 'RangePicker',
        label: '生效日期',
        // required: true,
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },
      {
        field: 'IS_FRAMEWORK',
        component: 'Select',
        label: '合同属性',
        componentProps: {
          options: storeFrameworkOptions,
        },
        //required: true,
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },
    ],
  };
}
