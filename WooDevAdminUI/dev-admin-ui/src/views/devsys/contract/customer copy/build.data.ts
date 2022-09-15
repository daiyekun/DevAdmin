import { FormSchema } from '/@/components/Form';
import { getdataListApi } from '/@/api/devsys/system/datadic';
import SelectUserModel from '/@/views/devsys/contract/common/SelectUserModel.vue';
// import { useModal } from '/@/components/Modal';
// const [registerUserModel, { openModal: UserSelectModel }] = useModal();

export const schemas: FormSchema[] = [
  {
    field: 'NAME',
    component: 'Input',
    label: '名称',
    required: true,
  },
  {
    field: 'CODE',
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
      params: { LbId: 6 },
    },
    label: '客户类别',
    required: true,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'TRADE',
    component: 'Input',
    label: '行业',
  },
  {
    field: 'LEVEL_ID',
    component: 'ApiSelect',
    componentProps: {
      api: getdataListApi,
      labelField: 'NAME',
      valueField: 'ID',
      params: { LbId: 7 },
    },
    label: '客户级别',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'CREATE_ID',
    component: 'ApiSelect',
    componentProps: {
      api: getdataListApi,
      labelField: 'NAME',
      valueField: 'ID',
      params: { LbId: 8 },
    },
    label: '信用等级',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'ZIPCODE',
    component: 'Input',
    label: '邮编',
  },
  {
    field: 'DEPUTY',
    component: 'Input',
    label: '法人代表',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'EST_DATE',
    component: 'DatePicker',
    label: '成立日期',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'TRADE',
    component: 'Input',
    label: '行业',
  },
  {
    field: 'WEBSITE',
    component: 'Input',
    label: '公司网址',
    componentProps: {
      addonBefore: 'http://',
      addonAfter: 'com',
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'EXP_RANGE',
    component: 'Input',
    label: '经营范围',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'REG_CAP',
    component: 'Input',
    label: '注册资本',
  },
  {
    field: 'COMP_TYPE',
    component: 'Input',
    label: '公司类型',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'FIELD1',
    component: 'Input',
    label: '备用1',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'FIELD2',
    component: 'Input',
    label: '备用2',
  },
  {
    field: 'ADDRESS',
    component: 'Input',
    label: '地址',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'LEAD_USERID',
    component: 'Input',
    label: '负责人',
    componentProps: {
      onclick: (e: any) => {
        console.log(e, '点击了。。。。。');
        debugger;
        SelectUserModel;
      },
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
