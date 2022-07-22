<template>
  <div>
    <a-button block class="mt-1" type="primary" @click="handleAdd"> 新增成员 </a-button>
    <BasicTable @register="registerTable" @edit-change="handleEditChange">
      <template #action="{ record, column }">
        <TableAction :actions="createActions(record, column)" />
      </template>
    </BasicTable>
  </div>
  <UserSelectModel @register="registerUserModel" @rowUserDbclick="SelleadUser" />
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import {
    BasicTable,
    useTable,
    TableAction,
    BasicColumn,
    ActionItem,
    EditRecordRow,
  } from '/@/components/Table';
  import UserSelectModel from '/@/views/devsys/contract/common/UserModel.vue';
  import { useModal } from '/@/components/Modal';
  const columns: BasicColumn[] = [
    {
      title: '登录名',
      dataIndex: 'LoginName',
      editRow: true,
    },
    {
      title: '姓名',
      dataIndex: 'Name',
      editRow: true,
    },
  ];

  const data: any[] = [];
  export default defineComponent({
    components: { BasicTable, TableAction, UserSelectModel },
    setup() {
      const [registerTable, { getDataSource, deleteTableDataRecord }] = useTable({
        columns: columns,
        showIndexColumn: false,
        dataSource: data,
        actionColumn: {
          width: 160,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
        pagination: false,
      });
      const [registerUserModel, { openModal: openModal1 }] = useModal();

      //删除
      function handleDel(record: EditRecordRow) {
        deleteTableDataRecord(record.key);
      }

      function handleCancel(record: EditRecordRow) {
        record.onEdit?.(false);
        if (record.isNew) {
          const data = getDataSource();
          const index = data.findIndex((item) => item.key === record.key);
          data.splice(index, 1);
        }
      }

      function handleSave(record: EditRecordRow) {
        record.onEdit?.(false, true);
      }

      function handleEditChange(data: Recordable) {
        console.log(data);
      }

      function handleAdd() {
        openModal1();
      }

      function createActions(record: EditRecordRow, column: BasicColumn): ActionItem[] {
        if (!record.editable) {
          return [
            {
              label: '删除',
              onClick: handleDel.bind(null, record),
            },
          ];
        }
        return [
          {
            label: '保存',
            onClick: handleSave.bind(null, record, column),
          },
          {
            label: '取消',
            popConfirm: {
              title: '是否取消编辑',
              confirm: handleCancel.bind(null, record, column),
            },
          },
        ];
      }
      /***
       * 选择用户
       ****/
      function SelleadUser(rd: any) {
        // console.log('触发了--1233SelleadUser', rd);
        // debugger;
        const data = getDataSource();
        const addRow: EditRecordRow = {
          LoginName: rd.LOGIN_NAME,
          Name: rd.NAME,
          editable: false,
          isNew: true,
          key: `${rd.ID}`,
        };
        data.push(addRow);

        // console.log('触发了--》SelleadUser', rd.NAME, rd.ID);

        // closeModal();
      }

      return {
        registerTable,
        handleDel,
        createActions,
        handleAdd,
        getDataSource,
        handleEditChange,
        registerUserModel,
        SelleadUser,
      };
    },
  });
</script>
