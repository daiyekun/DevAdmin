import { getAllRoleList, getDepartList } from '/@/api/devsys/system/devsystem';
import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { h } from 'vue';
import { Tag } from 'ant-design-vue';
import { DescItem } from '/@/components/Description/index';
//性别
const storeSexOptions: LabelValueOptions = [
  {
    label: '男',
    value: 1,
  },
  {
    label: '女',
    value: 0,
  },
];
export const columns: BasicColumn[] = [
  {
    title: '登录名',
    dataIndex: 'LOGIN_NAME',
    width: 120,
  },
  {
    title: '姓名',
    dataIndex: 'NAME',
    width: 120,
  },

  {
    title: '性别',
    dataIndex: 'SEX',
    width: 100,
    customRender: ({ record }) => {
      const status = record.SEX;
      const enable = ~~status === 1;
      const color = enable ? 'blue' : 'red';
      const text = enable ? '男' : '女';
      return h(Tag, { color: color }, () => text);
    },
  },
  {
    title: '电话',
    dataIndex: 'TEL',
    width: 130,
  },
  {
    title: '入职时间',
    dataIndex: 'ENTRY_TIME',
    width: 130,
  },
  {
    title: '邮箱',
    dataIndex: 'EMAIL',
    width: 140,
  },
  {
    title: '状态',
    dataIndex: 'USTATE',
    width: 110,
    customRender: ({ record }) => {
      const status = record.USTATE;
      const enable = ~~status === 1;
      const color = enable ? 'green' : 'red';
      const text = enable ? '启用' : '禁用';
      return h(Tag, { color: color }, () => text);
    },
  },

  {
    title: '创建时间',
    dataIndex: 'CREATE_TIME',
    width: 140,
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'LOGIN_NAME',
    label: '登录名',
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'NAME',
    label: '姓名',
    component: 'Input',
    colProps: { span: 8 },
  },
];

export const accountFormSchema: FormSchema[] = [
  {
    field: 'ID',
    label: 'ID',
    component: 'Input',
    show: false,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'LOGIN_NAME',
    label: '登录名',
    component: 'Input',
    helpMessage: ['不能输入带有admin的用户名'],
    rules: [
      {
        required: true,
        message: '请输入用户名',
      },
      // {
      //   validator(_, value) {
      //     console.log(JSON.stringify(_));
      //     return new Promise((resolve, reject) => {
      //       isAccountExist(value)
      //         .then(() => resolve())
      //         .catch((err) => {
      //           reject(err.message || '验证失败');
      //         });
      //     });
      //   },
      // },
    ],
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'NAME',
    label: '姓名',
    component: 'Input',
    required: true,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'PWD',
    label: '密码',
    component: 'InputPassword',
    required: true,
    // ifShow: false,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'confirmPassword',
    label: '确认密码',
    component: 'InputPassword',
    required: true,
    // ifShow: false,
    colProps: {
      offset: 2,
    },
    dynamicRules: ({ values }) => {
      return [
        {
          required: true,
          validator: (_, value) => {
            if (!value) {
              return Promise.reject('不能为空');
            }
            if (value !== values.PWD) {
              return Promise.reject('两次输入的密码不一致!');
            }
            return Promise.resolve();
          },
        },
      ];
    },
  },
  {
    label: '角色',
    field: 'ROLE_ID',
    component: 'ApiSelect',

    componentProps: {
      api: getAllRoleList,
      labelField: 'NAME',
      valueField: 'ID',
    },
    required: true,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'DEPART_ID',
    label: '所属部门',
    component: 'ApiTreeSelect',
    componentProps: {
      api: getDepartList,
      fieldNames: {
        label: 'NAME',
        key: 'ID',
        value: 'ID',
      },
      getPopupContainer: () => document.body,
    },
    required: true,
    colProps: {
      offset: 2,
    },
  },

  {
    label: '邮箱',
    field: 'EMAIL',
    component: 'Input',
    required: true,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'SEX',
    component: 'Select',
    label: '性别',
    componentProps: {
      options: storeSexOptions,
    },
    required: true,
    colProps: {
      offset: 2,
    },
  },

  {
    label: '电话',
    field: 'TEL',
    component: 'Input',
    colProps: {
      offset: 2,
    },
  },
  {
    label: '身份证号',
    field: 'ID_NO',
    component: 'Input',
    colProps: {
      offset: 2,
    },
  },
  {
    label: '微信号',
    field: 'WX_CODE',
    component: 'Input',
    colProps: {
      offset: 2,
    },
  },
  {
    label: '地址',
    field: 'ADDRESS',
    component: 'InputTextArea',
    colProps: {
      span: 8,
      offset: 2,
    },
  },
  {
    label: '备注',
    field: 'REMARK',
    component: 'InputTextArea',
    colProps: {
      span: 8,
      offset: 2,
    },
  },
];

//详情页面
export const userData: Recordable = {
  LOGIN_NAME: 'test',
  NAME: 'VB',
  ID: 21,
  RoleName: '123',
  TEL: '15695909xxx',
  EMAIL: '190848757@qq.com',
  addr: '厦门市思明区',
  SexDic: '男',
  ID_NO: '3504256199xxxxxxxxx',
  DepName: '市场部门',
  StateDic: '启用',
  PWD: null,
  CODE: null,
  DEPART_ID: 1,
  SEX: 1,
  ENTRY_TIME: null,
  USTATE: 1,
  IS_DELETE: 0,
  CREATE_USERID: 1,
  CREATE_TIME: '2022-06-17',
  UPDATE_USERID: 0,
  UPDATE_TIME: '0001-01-01',
  WX_CODE: null,
  ROLE_ID: 0,
};
export interface userviewinfo {
  NAME: string;
  LOGIN_NAME: string;
}
export const schemauserview: DescItem[] = [
  {
    field: 'LOGIN_NAME',
    label: '登录名',
  },
  {
    field: 'NAME',
    label: '姓名',
    // render: (curVal, data) => {
    //   return `${data.username}-${curVal}`;
    // },
  },
  {
    field: 'RoleName',
    label: '角色',
  },
  {
    field: 'DepName',
    label: '部门名称',
  },
  {
    field: 'Sex',
    label: '性别',
  },
  {
    field: 'TEL',
    label: '联系电话',
  },
  {
    field: 'EMAIL',
    label: '邮箱',
  },
  {
    field: 'ID_NO',
    label: '身份证号',
  },
  {
    field: 'StateDic',
    label: '状态',
  },
];
