import { FormProps } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table/src/types/table';

export function getBasicColumns(): BasicColumn[] {
  return [
    {
      title: 'ID',
      dataIndex: 'ID',
      fixed: 'left',
      width: 200,
      ifShow: false,
    },
    {
      title: '登录名称',
      dataIndex: 'NAME',
      width: 150,
    },
    {
      title: 'IP',
      dataIndex: 'IP',
      width: 140,
    },

    {
      title: '登录时间',
      width: 150,
      sorter: true,
      dataIndex: 'CREATE_TIME',
    },
  ];
}

export function getFormConfig(): Partial<FormProps> {
  return {
    labelWidth: 100,
    schemas: [
      {
        field: 'Name',
        label: '登录名称',
        component: 'Input',
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },

      {
        field: 'StartDate',
        label: '开始时间',
        component: 'DatePicker',
        colProps: {
          xl: 8,
          xxl: 8,
        },
        componentProps: {
          style: { width: '100%' },
        },
      },
      {
        field: 'EndDate',
        label: '结束时间',
        component: 'DatePicker',
        colProps: {
          xl: 8,
          xxl: 8,
        },
        componentProps: {
          style: { width: '100%' },
        },
      },
    ],
  };
}

export function getOptionLogColumns(): BasicColumn[] {
  return [
    {
      title: 'ID',
      dataIndex: 'ID',
      fixed: 'left',
      width: 200,
      ifShow: false,
    },
    {
      title: '登录名称',
      dataIndex: 'LoginName',
      width: 130,
    },
    {
      title: '姓名',
      dataIndex: 'ShowName',
      width: 130,
    },
    {
      title: '操作类型',
      dataIndex: 'OpTypeDic',
      width: 140,
    },
    {
      title: '操作名称',
      dataIndex: 'NAME',
      width: 140,
    },
    {
      title: '请求路径',
      dataIndex: 'REQ_URL',
      width: 200,
    },
    {
      title: 'IP',
      dataIndex: 'IP',
      width: 140,
    },

    {
      title: '操作时间',
      width: 140,
      sorter: true,
      dataIndex: 'CREATE_TIME',
    },
    {
      title: '请求参数',
      dataIndex: 'REQ_PARAMETER',
      width: 150,
      ifShow: false,
    },
    {
      title: '方法名称',
      dataIndex: 'ACTION_NAME',
      width: 150,
      ifShow: false,
    },
    {
      title: 'Http 方法',
      dataIndex: 'REQ_METHOD',
      width: 130,
      ifShow: false,
    },

    {
      title: '备注',
      dataIndex: 'REMARK',
      width: 160,
    },
  ];
}

export function getOptionFormConfig(): Partial<FormProps> {
  return {
    labelWidth: 100,
    schemas: [
      {
        field: 'Name',
        label: '登录名称',
        component: 'Input',
        colProps: {
          xl: 8,
          xxl: 8,
        },
      },

      {
        field: 'StartDate',
        label: '开始时间',
        component: 'DatePicker',
        colProps: {
          xl: 8,
          xxl: 8,
        },
        componentProps: {
          style: { width: '100%' },
        },
      },
      {
        field: 'EndDate',
        label: '结束时间',
        component: 'DatePicker',
        colProps: {
          xl: 8,
          xxl: 8,
        },
        componentProps: {
          style: { width: '100%' },
        },
      },
    ],
  };
}
