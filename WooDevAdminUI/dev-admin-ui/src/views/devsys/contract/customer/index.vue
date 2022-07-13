<template>
  <BasicTable
    @register="registerTable"
    :rowSelection="{ type: 'checkbox', selectedRowKeys: checkedKeys, onChange: onSelectChange }"
  >
    <template #form-custom> custom-slot </template>
    <!-- <template #headerTop>
      <a-alert type="info" show-icon>
        <template #message>
          <template v-if="checkedKeys.length > 0">
            <span>已选中{{ checkedKeys.length }}条记录(可跨页)</span>
            <a-button type="link" @click="checkedKeys = []" size="small">清空</a-button>
          </template>
        </template>
      </a-alert>
    </template> -->
    <template #toolbar>
      <a-button type="primary" @click="handleCreate">新增客户</a-button>
      <a-button type="primary" @click="getFormValues">批量删除</a-button>
    </template>
    <!-- <template #bodyCell="{ column }">
      <template v-if="column.key === 'operation'">
        <a>action</a>
      </template>
    </template> -->
    <template #action="{ record }">
      <TableAction
        :actions="[
          {
            icon: 'clarity:info-standard-line',
            tooltip: '查看详情',
            onClick: handleView.bind(null, record),
          },
          {
            icon: 'clarity:note-edit-line',
            tooltip: '编辑信息',
            onClick: handleEdit.bind(null, record),
          },
          {
            icon: 'ant-design:delete-outlined',
            color: 'error',
            tooltip: '删除此信息',
            popConfirm: {
              title: '是否确认删除',
              confirm: handleDelete.bind(null, record),
            },
          },
        ]"
      />
    </template>
  </BasicTable>
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table'; //TableAction
  import { customercolumns, getFormConfig } from './index.data';
  // import { Alert } from 'ant-design-vue';
  import { useGo } from '/@/hooks/web/usePage';
  import { getCusertomerListApi } from '/@/api/devsys/contract/customer';

  export default defineComponent({
    name: 'DevCustomer',
    components: { BasicTable, TableAction }, //TableAction
    setup() {
      const go = useGo();
      const checkedKeys = ref<Array<string | number>>([]);
      const [registerTable, { getForm, reload }] = useTable({
        title: '客户列表',
        api: getCusertomerListApi,
        columns: customercolumns,
        useSearchForm: true,
        formConfig: getFormConfig(),
        showTableSetting: true,
        tableSetting: { fullScreen: true },
        showIndexColumn: false,
        bordered: true,
        rowKey: 'ID',
        actionColumn: {
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
          //width: 150,
          ellipsis: true,
          fixed: 'right',
        },
      });

      function handleCreate() {
        go('/company/customer/customer_build/0');
      }

      function getFormValues() {
        console.log(getForm().getFieldsValue());
      }
      function handleDelete(record: Recordable) {
        console.log('点击了删除', record);
      }
      function handleView(record: Recordable) {
        go('/company/customer/customer_detail/' + record.ID);
      }
      function handleEdit(record: Recordable) {
        go('/company/customer/customer_build/' + record.ID);
      }
      function onSelectChange(selectedRowKeys: (string | number)[]) {
        console.log(selectedRowKeys);
        checkedKeys.value = selectedRowKeys;
      }

      return {
        registerTable,
        getFormValues,
        checkedKeys,
        onSelectChange,
        handleDelete,
        handleView,
        handleEdit,
        handleCreate,
      };
    },
  });
</script>
