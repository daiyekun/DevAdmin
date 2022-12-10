<template>
  <!-- 宽度可以百分比 -->
  <BasicDrawer
    v-bind="$attrs"
    @register="register"
    title="审批节点信息"
    width="500px"
    @ok="handleOk"
  >
    <div>
      <BasicForm @register="registerForm" />
      <hr />
      <div class="selsp">
        <BasicForm @register="registerForm1" />
      </div>
      <div class="p-4">
        <BasicTable :bordered="true" @register="registerTable">
          <template #expandedRowRender="{ record }">
            <BasicTable
              style="margin: 0"
              :columns="innerColumns"
              :dataSource="record.NodeMsg"
              :bordered="true"
              :pagination="false"
              :showIndexColumn="false"
            />
          </template>
        </BasicTable>
      </div>
    </div>
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, reactive } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form/index';
  // import { useMessage } from '/@/hooks/web/useMessage';
  import { BasicTable, useTable, BasicColumn } from '/@/components/Table';
  import { getNodeInfoByNodeIdApi } from '/@/api/devsys/flow/flowinst';
  /**
   * 表格列
   **/
  const columns: BasicColumn[] = [
    {
      title: '审批类型',
      dataIndex: 'OtypeDsc',
      editComponent: 'Select',
      editComponentProps: {
        options: [
          {
            label: '人力资源',
            value: '1',
          },
          {
            label: '审批组',
            value: '2',
          },
        ],
      },
      width: 160,
    },
    {
      title: '审批对象',
      dataIndex: 'ObjName',
      //edit: true,
      editComponentProps: {
        prefix: '$',
      },
      width: 160,
    },
  ];
  /**
   * 子表格
   **/
  const innerColumns: BasicColumn[] = [
    { title: '审批人', dataIndex: 'UserName', key: 'UserName' },
    { title: '审批意见', dataIndex: 'Msg', key: 'Msg' },
    { title: '收到时间', dataIndex: 'StartTime', key: 'StartTime' },
    { title: '完成时间', dataIndex: 'EndTime', key: 'EndTime' },
  ];
  export default defineComponent({
    components: { BasicDrawer, BasicForm, BasicTable },
    setup() {
      let nodeInfo = reactive({ nodeId: '', instId: 0 });
      // const { createMessage: msg } = useMessage();
      const [register] = useDrawerInner((data) => {
        // 方式1
        setFieldsValue({
          NodeId: data.data.data.id,
          Name: data.data.data.text.value,
          instId: data.instId,
          SpRules: data.data.data.properties.NRULE,
        });
        //节点ID
        nodeInfo.nodeId = data.data.data.id;
        nodeInfo.instId = data.instId;
        //刷新列表
        reload();
      });
      const [registerTable, { reload }] = useTable({
        title: '审批对象列表',
        titleHelpMessage: ['展开列表加号可以查看当前对象意见'],
        api: getNodeInfoByNodeIdApi,
        beforeFetch: (t) => {
          t.NodeStr = nodeInfo.nodeId;
          t.InstId = nodeInfo.instId;
        },
        rowKey: 'ID',
        columns: columns,
        showIndexColumn: false,
        bordered: true,
        canResize: false,
        expandRowByClick: true,
        pagination: false,
      });

      const schemas: FormSchema[] = [
        {
          field: 'TempId',
          component: 'Input',
          componentProps: { disabled: true },
          label: '流程模板ID',
          colProps: {
            span: 24,
          },
          defaultValue: '',
          show: () => {
            return false;
          },
        },
        {
          field: 'NodeId',
          component: 'Input',
          componentProps: { disabled: true },
          label: '编号',
          colProps: {
            span: 24,
          },
        },
        {
          field: 'Name',
          component: 'Input',
          componentProps: { disabled: true },
          label: '名称',
          colProps: {
            span: 24,
          },
          defaultValue: '',
        },
        {
          field: 'SpRules',
          component: 'Select',
          label: '审批规则',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '全部审批',
                value: '1',
                key: '1',
              },
              {
                label: '任意通过',
                value: '2',
                key: '2',
              },
            ],
          },
        },
      ];

      const schemas1: FormSchema[] = [
        {
          field: 'selSpId',
          component: 'Input',
          componentProps: () => {
            return {
              onclick: () => {},
            };
          },
          label: '审批对象',
          colProps: {
            span: 24,
          },
          show: () => {
            return false;
          },
        },

        {
          field: 'O_TYPE',
          component: 'Select',
          label: '审批类型',
          colProps: {
            span: 24,
          },
          show: () => {
            return false;
          },
          componentProps: {
            options: [
              {
                label: '人力资源',
                value: '1',
                key: '1',
              },
              {
                label: '审批组',
                value: '2',
                key: '2',
              },
            ],
          },
        },
      ];
      const [registerForm, { setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 24,
        },
      });
      const [registerForm1] = useForm({
        labelWidth: 120,
        schemas: schemas1,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 24,
        },
      });

      /**
       * 底部确认按钮
       */

      function handleOk() {}
      return {
        register,
        schemas,
        registerForm,
        handleOk,
        registerForm1,
        registerTable,
        nodeInfo,
        innerColumns,
      };
    },
  });
</script>
<style lang="less" scoped>
  .selsp {
    padding-top: 6px;
  }
</style>
