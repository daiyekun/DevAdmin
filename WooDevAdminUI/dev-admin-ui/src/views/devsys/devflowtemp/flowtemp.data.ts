import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { h, ref } from 'vue';
import { Switch } from 'ant-design-vue';
import { getFlowItemList, getFlowObjectist, setFlowTempStatus } from '/@/api/devsys/flow/flowtemp';
import { useMessage } from '/@/hooks/web/useMessage';
import { getFlowdataListApi } from '/@/api/devsys/system/datadic';
// import { breadcrumbItemProps } from 'ant-design-vue/lib/breadcrumb/BreadcrumbItem';
export const selflowobj = ref(0); //默认客户
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
    title: '审批对象',
    dataIndex: 'ObjTypeDic',
    width: 160,
  },
  {
    title: '状态',
    dataIndex: 'F_STATE',
    width: 120,
    customRender: ({ record }) => {
      if (!Reflect.has(record, 'pendingStatus')) {
        record.pendingStatus = false;
      }
      return h(Switch, {
        checked: record.F_STATE == 1,
        checkedChildren: '已启用',
        unCheckedChildren: '已禁用',
        loading: record.pendingStatus,
        onChange(checked: boolean) {
          record.pendingStatus = true;
          const newStatus = checked ? 1 : 0;
          const { createMessage } = useMessage();
          setFlowTempStatus(record.ID, newStatus)
            .then(() => {
              record.F_STATE = newStatus;
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
    field: 'OBJ_TYPE',
    component: 'ApiSelect',
    label: '审批对象',
    required: true,
    componentProps: ({ formModel, formActionType }) => {
      // console.log('OBJ_TYPE 执行');
      return {
        api: getFlowObjectist,
        resultField: 'result',
        // use name as label
        labelField: 'Desc',
        // use id as value
        valueField: 'Value',
        // not request untill to select
        immediate: true,
        numberToString: false,
        onChange: (e: any) => {
          const sel = Number(e);
          //console.log(e);
          formModel.FLOW_ITEMS_LIST = undefined; //  reset city value
          const { updateSchema } = formActionType;
          updateSchema({
            field: 'FLOW_ITEMS_LIST',
            componentProps: {
              params: { objEnum: e },
            },
          });
          formModel.CATE_IDS_LIST = undefined; //  reset city value
          updateSchema({
            field: 'CATE_IDS_LIST',
            componentProps: {
              params: { FlowObj: e },
            },
          });

          switch (sel) {
            case 3: //合同
              {
                formModel.MIN_MONERY = undefined;
                formModel.MAX_MONERY = undefined;
                updateSchema({
                  field: 'MIN_MONERY',
                  componentProps: {},
                  ifShow: true,
                });
                updateSchema({
                  field: 'MAX_MONERY',
                  componentProps: {},
                  ifShow: true,
                });
              }
              break;
            default:
              {
                {
                  formModel.MIN_MONERY = undefined;
                  formModel.MAX_MONERY = undefined;
                  updateSchema({
                    field: 'MIN_MONERY',
                    componentProps: {},
                    ifShow: false,
                  });
                  updateSchema({
                    field: 'MAX_MONERY',
                    componentProps: {},
                    ifShow: false,
                  });
                }
              }
              break;
          }
        },
      };
    },

    colProps: {
      span: 24,
    },
  },
  {
    field: 'F_STATE',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: 1,
    componentProps: {
      options: [
        { label: '启用', value: 1 },
        { label: '禁用', value: 0 },
      ],
    },
  },
  {
    field: 'FLOW_ITEMS_LIST',
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
      numberToString: false,
      // not request untill to select
      immediate: true,
      onChange: (e) => {
        console.log('selected:', e);
      },
      // atfer request callback
      // onOptionsChange: (options) => {
      //   console.log('get options 测试', options.length, options, selflowobj.value);
      // },
      mode: 'multiple',
    },
    colProps: {
      span: 24,
    },
    // defaultValue: '0',
  },

  {
    label: ' ',
    field: 'DEPART_IDS_LIST',
    slot: 'DEPART_IDS_LIST',
    component: 'Input',
    colProps: { span: 24 },
  },
  // {
  //   label: ' ',
  //   field: 'category',
  //   slot: 'category',
  //   component: 'Input',
  //   colProps: { span: 24 },
  // },

  {
    field: 'CATE_IDS_LIST',
    component: 'ApiSelect',
    label: '类别',
    required: true,
    componentProps: {
      // more details see /src/components/Form/src/components/ApiSelect.vue
      api: getFlowdataListApi,
      params: { FlowObj: selflowobj.value },
      resultField: 'result',
      // use name as label
      labelField: 'NAME',
      // use id as value
      valueField: 'StrId',
      numberToString: false,
      // not request untill to select
      immediate: true,
      onChange: (e) => {
        console.log('selected:', e);
      },
      // atfer request callback
      // onOptionsChange: (options) => {
      //   //console.log('get options 测试', options.length, options, selflowobj.value);
      // },
      mode: 'multiple',
    },
    colProps: {
      span: 24,
    },
    // defaultValue: '0',
  },
  {
    field: 'MIN_MONERY',
    label: '开始金额',
    // required: true,
    ifShow: false,
    component: 'InputNumber',
    colProps: { span: 24 },
  },
  {
    field: 'MAX_MONERY',
    label: '结束金额',
    // required: true,
    ifShow: false,
    component: 'InputNumber',
    colProps: { span: 24 },
  },
];
