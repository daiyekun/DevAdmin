<template>
  <div>
    <BasicTable @register="registerTable" rowKey="ID" :rowSelection="{ type: 'checkbox' }">
      <template #toolbar>
        <a-button type="primary" @click="handleCreate"> 新增模板 </a-button>
        <!-- <a-button type="primary" @click="handleMenusPermission"> 菜单分配 </a-button> -->
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:note-edit-line',
              tooltip: '修改模板',
              onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除模板',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
            },
            {
              icon: 'ant-design:branches-outlined',
              tooltip: '设置流程图',
              onClick: handSetFlow.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <FlowTempDrawer @register="registerDrawer" @success="handleSuccess" />
  </div>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';

  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getFlowTempList, flowTempDelApi } from '/@/api/devsys/flow/flowtemp';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useDrawer } from '/@/components/Drawer';
  // import { useModal } from '/@/components/Modal';
  import FlowTempDrawer from './FlowTempDrawer.vue';
  // import RoleModal from './RoleModal.vue';
  import { useGo } from '/@/hooks/web/usePage';
  import { columns, searchFormSchema } from './flowtemp.data';

  export default defineComponent({
    name: 'DevFlowTemp',
    components: { BasicTable, FlowTempDrawer, TableAction },
    setup() {
      const go = useGo();
      const [registerDrawer, { openDrawer }] = useDrawer();
      // const [registerModal, { openModal }] = useModal();
      const { createMessage: msg } = useMessage();
      const [registerTable, { reload }] = useTable({
        title: '流程模板列表',
        api: getFlowTempList,
        columns,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
        },
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        showIndexColumn: false,
        actionColumn: {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
          fixed: undefined,
        },
      });

      function handleCreate() {
        openDrawer(true, {
          isUpdate: false,
        });
      }

      function handleEdit(record: Recordable) {
        openDrawer(true, {
          record,
          isUpdate: true,
        });
      }
      /***
       * 跳转流程图界面
       */
      function handSetFlow(record: Recordable) {
        go('/devflow/flowtemp/flowtemp_set/' + record.ID);
      }

      async function handleDelete(record: Recordable) {
        await flowTempDelApi({ Ids: record.ID.toString() });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }

      function handleSuccess() {
        reload();
      }

      return {
        registerTable,
        registerDrawer,
        // registerModal,
        handleCreate,
        handleEdit,
        handleDelete,
        handleSuccess,
        handSetFlow,
        // handleMenusPermission,
      };
    },
  });
</script>
