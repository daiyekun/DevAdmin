<template>
  <div>
    <BasicTable @register="registerTable">
      <template #toolbar>
        <a-button type="primary" @click="handleCreate"> 新增角色 </a-button>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:note-edit-line',
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
          ]"
        />
      </template>
    </BasicTable>
    <!-- <RoleDrawer @register="registerDrawer" @success="handleSuccess" /> -->
    <RoleModal @register="registerModal" @success="handleSuccess" />
  </div>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';

  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getRoleList, roleDelApi } from '/@/api/devsys/system/devsystem';
  import { useMessage } from '/@/hooks/web/useMessage';
  // import { useDrawer } from '/@/components/Drawer';
  import { useModal } from '/@/components/Modal';
  // import RoleDrawer from './RoleDrawer.vue';
  import RoleModal from './RoleModal.vue';

  import { columns, searchFormSchema } from './role.data';

  export default defineComponent({
    name: 'DevRoleManagement',
    components: { BasicTable, RoleModal, TableAction },
    setup() {
      //const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerModal, { openModal }] = useModal();
      const { createMessage: msg } = useMessage();
      const [registerTable, { reload }] = useTable({
        title: '角色列表',
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
          width: 80,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
          fixed: undefined,
        },
      });

      function handleCreate() {
        openModal(true, {
          isUpdate: false,
        });
      }

      function handleEdit(record: Recordable) {
        openModal(true, {
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

      return {
        registerTable,
        //registerDrawer,
        registerModal,
        handleCreate,
        handleEdit,
        handleDelete,
        handleSuccess,
      };
    },
  });
</script>
