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
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
            },
            {
              icon: 'ant-design:branches-outlined',
              tooltip: '设置流程图',
              onClick: handleEdit.bind(null, record),
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
  import { getRoleList, roleDelApi } from '/@/api/devsys/system/devsystem';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useDrawer } from '/@/components/Drawer';
  // import { useModal } from '/@/components/Modal';
  import FlowTempDrawer from './FlowTempDrawer.vue';
  // import RoleModal from './RoleModal.vue';

  import { columns, searchFormSchema } from './flowtemp.data';

  export default defineComponent({
    name: 'DevFlowTemp',
    components: { BasicTable, FlowTempDrawer, TableAction },
    setup() {
      const [registerDrawer, { openDrawer }] = useDrawer();
      // const [registerModal, { openModal }] = useModal();
      const { createMessage: msg } = useMessage();
      const [registerTable, { reload }] = useTable({
        title: '流程模板列表',
        api: getRoleList,
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

      // function handleCreate() {
      //   openModal(true, {
      //     isUpdate: false,
      //   });
      // }

      // function handleEdit(record: Recordable) {
      //   openModal(true, {
      //     record,
      //     isUpdate: true,
      //   });
      // }
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

      async function handleDelete(record: Recordable) {
        await roleDelApi({ Ids: record.ID.toString() });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }

      function handleSuccess() {
        reload();
      }
      //菜单分配
      // function handleMenusPermission() {
      //   // const selkeys = getSelectRowKeys();
      //   // console.log('kes', selkeys.length);
      //   const record = getRowSelection();
      //   openDrawer(true, {
      //     record,
      //     isUpdate: true,
      //   });
      // }

      return {
        registerTable,
        registerDrawer,
        // registerModal,
        handleCreate,
        handleEdit,
        handleDelete,
        handleSuccess,
        // handleMenusPermission,
      };
    },
  });
</script>
