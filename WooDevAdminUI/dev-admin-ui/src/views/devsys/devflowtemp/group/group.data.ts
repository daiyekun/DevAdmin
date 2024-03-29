import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { h } from 'vue';
import { Switch } from 'ant-design-vue';
import { setFlowGroupStatus } from '/@/api/devsys/flow/flowgroup';
import { useMessage } from '/@/hooks/web/useMessage';

export const columns: BasicColumn[] = [
  {
    title: '组名称',
    dataIndex: 'NAME',
    width: 200,
  },
  {
    title: '编号',
    dataIndex: 'CODE',
    width: 160,
  },
  {
    title: '状态',
    dataIndex: 'G_STATE',
    width: 120,
    customRender: ({ record }) => {
      if (!Reflect.has(record, 'pendingStatus')) {
        record.pendingStatus = false;
      }
      return h(Switch, {
        checked: record.G_STATE == 0,
        checkedChildren: '启用',
        unCheckedChildren: '停用',
        loading: record.pendingStatus,
        onChange(checked: boolean) {
          record.pendingStatus = true;
          const newStatus = checked ? 0 : 1;
          const { createMessage } = useMessage();
          setFlowGroupStatus(record.ID, newStatus)
            .then(() => {
              record.G_STATE = newStatus;
              createMessage.success(`已成功修改组状态`);
            })
            .catch(() => {
              createMessage.error('修改组状态失败');
            })
            .finally(() => {
              record.pendingStatus = false;
            });
        },
      });
    },
  },
  {
    title: '创建时间',
    dataIndex: 'CREATE_TIME',
    width: 180,
  },
  {
    title: '备注',
    dataIndex: 'REMARK',
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'KeyWord',
    label: '名称、编号',
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
    field: 'ID',
    label: 'ID',
    component: 'Input',
    show: false,
    colProps: { span: 24 },
  },
  {
    field: 'NAME',
    label: '组名称',
    required: true,
    component: 'Input',
    colProps: { span: 24 },
  },
  {
    field: 'CODE',
    label: '编号',
    required: true,
    component: 'Input',
    colProps: { span: 24 },
  },
  {
    field: 'G_STATE',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: 0,
    componentProps: {
      options: [
        { label: '启用', value: 0 },
        { label: '停用', value: 1 },
      ],
    },
  },
  {
    label: '备注',
    field: 'REMARK',
    component: 'InputTextArea',
    colProps: { span: 24 },
  },
  {
    field: 'UserIds',
    label: 'UserIds',
    component: 'Input',
    show: false,
    colProps: { span: 24 },
  },
];
