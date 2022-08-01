import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { h, ref } from 'vue';
import { Switch } from 'ant-design-vue';
import { setRoleStatus } from '/@/api/devsys/system/devsystem';
import { getFlowItemList, getFlowObjectist } from '/@/api/devsys/flow/flowtemp';
import { useMessage } from '/@/hooks/web/useMessage';
const selflowobj = ref(0); //默认客户
export const columns: BasicColumn[] = [
  {
    title: '模板名称',
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
    dataIndex: 'RUSTATE',
    width: 120,
    customRender: ({ record }) => {
      if (!Reflect.has(record, 'pendingStatus')) {
        record.pendingStatus = false;
      }
      return h(Switch, {
        checked: record.RUSTATE == 0,
        checkedChildren: '已启用',
        unCheckedChildren: '已禁用',
        loading: record.pendingStatus,
        onChange(checked: boolean) {
          record.pendingStatus = true;
          const newStatus = checked ? 0 : 1;
          const { createMessage } = useMessage();
          setRoleStatus(record.ID, newStatus)
            .then(() => {
              record.RUSTATE = newStatus;
              createMessage.success(`已成功修改模板状态`);
            })
            .catch(() => {
              createMessage.error('修改模板状态失败');
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
    label: '模板名称',
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
    field: 'FlowObj',
    component: 'ApiSelect',
    label: '审批对象',
    required: true,
    // componentProps: {
    //   api: getFlowObjectist,
    //   // params: {
    //   //   objEnum: 0,
    //   // },
    //   resultField: 'result',
    //   // use name as label
    //   labelField: 'Desc',
    //   // use id as value
    //   valueField: 'Value',
    //   // not request untill to select
    //   immediate: false,
    //   onChange: (e) => {
    //     //selflowobj.value = e; //设置选择值
    //     //debugger;
    //     console.log('selected:', e);
    //   },
    //   // atfer request callback
    //   onOptionsChange: (options) => {
    //     selflowobj.value = options;
    //     console.log('get options', options.length, options);
    //   },
    //   // mode: 'multiple',
    // },
    componentProps: ({ formModel, formActionType }) => {
      return {
        api: getFlowObjectist,
        resultField: 'result',
        // use name as label
        labelField: 'Desc',
        // use id as value
        valueField: 'Value',
        // not request untill to select
        immediate: false,
        onChange: (e: any) => {
          console.log(e);
          formModel.flowitem = undefined; //  reset city value
          const { updateSchema } = formActionType;
          updateSchema({
            field: 'flowitem',
            componentProps: {
              params: { objEnum: e },
            },
          });
        },
      };
    },

    colProps: {
      span: 24,
    },
    defaultValue: 0,
  },
  {
    field: 'RUSTATE',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: '0',
    componentProps: {
      options: [
        { label: '启用', value: '0' },
        { label: '停用', value: '1' },
      ],
    },
  },
  {
    field: 'flowitem',
    component: 'ApiSelect',
    label: '审批事项',
    required: true,
    componentProps: {
      // more details see /src/components/Form/src/components/ApiSelect.vue
      api: getFlowItemList,
      params: {
        objEnum: selflowobj.value,
      },
      resultField: 'result',
      // use name as label
      labelField: 'Name',
      // use id as value
      valueField: 'Id',
      // not request untill to select
      immediate: false,
      onChange: (e) => {
        console.log('selected:', e);
      },
      // atfer request callback
      onOptionsChange: (options) => {
        console.log('get options 测试', options.length, options, selflowobj.value);
      },
      mode: 'multiple',
    },
    colProps: {
      span: 24,
    },
    // defaultValue: '0',
  },
  {
    label: '备注',
    field: 'REMARK',
    component: 'InputTextArea',
    colProps: { span: 24 },
  },
  {
    label: ' ',
    field: 'menu',
    slot: 'menu',
    component: 'Input',
    colProps: { span: 24 },
  },
  {
    label: ' ',
    field: 'category',
    slot: 'category',
    component: 'Input',
    colProps: { span: 24 },
  },
];
