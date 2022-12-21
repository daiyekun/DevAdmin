import { FormSchema } from '/@/components/Form';
import { getdataListApi } from '/@/api/devsys/system/datadic';
// import SelectUserModel from '/@/views/devsys/contract/common/SelectUserModel.vue';
// import { useModal } from '/@/components/Modal';
// const [registerUserModel, { openModal: UserSelectModel }] = useModal();
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
export const schemas: FormSchema[] = [
  {
    field: 'C_NAME',
    component: 'Input',
    label: '名称',
    required: true,
  },
  {
    field: 'C_CODE',
    component: 'Input',
    label: '编号',
    required: true,
    colProps: {
      offset: 2,
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
    required: true,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'ANT_MONERY',
    component: 'InputNumber',
    required: true,
    label: '金额',
    componentProps: {
      style: { width: '100%' },
    },
  },
  {
    field: 'SOURCE_ID',
    component: 'ApiSelect',
    componentProps: {
      api: getdataListApi,
      labelField: 'NAME',
      valueField: 'ID',
      params: { LbId: 9 },
    },
    label: '合同来源',
    colProps: { offset: 2 },
  },
  {
    field: 'DEPART_ID',
    label: '经办机构',
    component: 'TreeSelect',
    colProps: { offset: 2 },
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
    field: 'SIGNING_DATE',
    component: 'DatePicker',
    label: '签订日期',
    componentProps: {
      style: { width: '100%' },
    },
  },
  {
    field: 'EFFECTIVE_DATE',
    component: 'DatePicker',
    label: '生效日期',
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'PLAN_DATE',
    component: 'DatePicker',
    label: '计划完成日期',
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'IS_SUBCONT',
    component: 'RadioGroup',
    label: '总包合同',

    // itemProps: {
    //   extra: '客户、邀评人默认被分享',
    // },
    defaultValue: '0',
    componentProps: {
      options: [
        {
          label: '是',
          value: '1',
        },
        {
          label: '否',
          value: '0',
        },
      ],
    },
  },
  {
    field: 'HEAD_USER_ID',
    component: 'Input',
    label: '负责人',
    componentProps: {
      onclick: () => {
        //console.log(e, '点击了。。。。。');
        openModal1();
      },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'CONT_SINGNO',
    component: 'Input',
    label: '签订人身份证号',
    colProps: {
      offset: 2,
    },
  },

  {
    field: 'IS_FRAMEWORK',
    component: 'Select',
    label: '合同属性',
    componentProps: {
      options: storeFrameworkOptions,
    },
    required: true,
  },
  {
    field: 'EST_MONERY',
    component: 'InputNumber',
    // required: true,
    label: '预估金额',
    show: ({ model }) => {
      return model.IS_FRAMEWORK === '1';
    },
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'ADVANCE_MONERY',
    component: 'InputNumber',
    // required: true,
    label: '预收金额',
    show: ({ model }) => {
      return model.IS_FRAMEWORK === '1';
    },
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
];
export const taskSchemas: FormSchema[] = [
  // {
  //   field: 'NAME',
  //   component: 'Input',
  //   label: '名称',
  //   required: true,
  // },
  // {
  //   field: 'CODE',
  //   component: 'Input',
  //   label: '编号',
  //   required: true,
  //   colProps: {
  //     offset: 2,
  //   },
  // },
  // {
  //   field: 't5',
  //   component: 'TimePicker',
  //   label: '生效日期',
  //   required: true,
  //   componentProps: {
  //     style: { width: '100%' },
  //   },
  //   colProps: {
  //     offset: 2,
  //   },
  // },
];
