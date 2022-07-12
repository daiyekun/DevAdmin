<template>
  <div>
    <BasicTable @register="registerTable" @edit-change="handleEditChange">
      <template #action="{ record, column }">
        <TableAction :actions="createActions(record, column)" />
      </template>
    </BasicTable>
    <a-button block class="mt-5" type="dashed" @click="handleAdd"> 新增联系人 </a-button>
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import {
    BasicTable,
    useTable,
    TableAction,
    BasicColumn,
    ActionItem,
    EditRecordRow,
  } from '/@/components/Table';
  import {
    getCustContactListApi,
    custContactDelApi,
    custContactSaveApi,
  } from '/@/api/devsys/contract/customer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { cloneDeep } from 'lodash-es';
  import { custContactSaveInfo } from '/@/api/devsys/model/customerModel';
  // let savedata: custContactSaveInfo = {};
  const columns: BasicColumn[] = [
    {
      title: 'ID',
      dataIndex: 'ID',
      editRow: false,
    },
    {
      title: '姓名',
      dataIndex: 'NAME',
      editRow: true,
    },
    {
      title: '职位',
      dataIndex: 'POSITION',
      editRow: true,
    },
    {
      title: '所属部门',
      dataIndex: 'DEPART',
      editRow: true,
    },
    {
      title: '办公电话',
      dataIndex: 'TEL',
      editRow: true,
    },
    {
      title: '手机',
      dataIndex: 'PHONE',
      editRow: true,
    },
    {
      title: 'E-MAIL',
      dataIndex: 'EMAIL',
      editRow: true,
    },
    {
      title: '备注',
      dataIndex: 'REMARK',
      editRow: true,
    },
  ];

  export default defineComponent({
    components: { BasicTable, TableAction },
    setup() {
      const [registerTable, { getDataSource, reload }] = useTable({
        columns: columns,
        showIndexColumn: false,
        api: getCustContactListApi,
        rowKey: 'ID',
        actionColumn: {
          width: 160,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
        pagination: false,
      });
      const { createMessage: msg } = useMessage();
      const currentEditKeyRef = ref('');
      function handleEdit(record: EditRecordRow) {
        record.onEdit?.(true);
      }

      function handleCancel(record: EditRecordRow) {
        record.onEdit?.(false);
        if (record.isNew) {
          const data = getDataSource();
          const index = data.findIndex((item) => item.key === record.key);
          data.splice(index, 1);
        }
      }

      // async function handleSave(record: EditRecordRow) {
      //   record.onEdit?.(false, true);
      // }
      async function handleSave(record: EditRecordRow) {
        // 校验
        msg.loading({ content: '正在保存...', duration: 0, key: 'saving' });
        const valid = await record.onValid?.();
        if (valid) {
          try {
            const data = cloneDeep(record.editValueRefs); //获取修改数据，否则有问题
            console.log(data);
            //TODO 此处将数据提交给服务器保存

            let tsavedata: custContactSaveInfo = {
              ID: record.ID,
              NAME: data.NAME,
              POSITION: data.POSITION,
              DEPART: data.DEPART,
              TEL: data.TEL,
              PHONE: data.PHONE,
              EMAIL: data.EMAIL,
              REMARK: data.REMARK,
              QQ: '',
              COMP_ID: 0,
            };
            await custContactSaveApi(tsavedata);
            // 保存之后提交编辑状态
            const pass = await record.onEdit?.(false, true);
            if (pass) {
              currentEditKeyRef.value = '';
            }
            msg.success({ content: '数据已保存', key: 'saving' });
          } catch (error) {
            msg.error({ content: '保存失败', key: 'saving' });
          }
        } else {
          msg.error({ content: '请填写正确的数据', key: 'saving' });
        }
      }
      function handleSuccess() {
        reload();
      }

      // function handleEditRowEnd() {
      //   //debugger;
      //   const data = getDataSource();
      //   var length = data.length;
      //   var savedata = data[length - 1];
      //   let tsavedata: custContactSaveInfo = {
      //     ID: savedata.ID,
      //     NAME: savedata.NAME,
      //     POSITION: savedata.POSITION,
      //     DEPART: savedata.DEPART,
      //     TEL: savedata.TEL,
      //     PHONE: savedata.PHONE,
      //     EMAIL: savedata.EMAIL,
      //     REMARK: savedata.REMARK,
      //     QQ: '',
      //     COMP_ID: 0,
      //   };
      //   // debugger;
      //   setTimeout(function () {
      //     custContactSaveApi(tsavedata);
      //     reload();
      //   }, 1000);

      //   //createMessage.info(JSON.stringify(data));
      // }

      function handleEditChange(data: Recordable) {
        console.log(data);
      }
      async function handleDel(data: Recordable) {
        await custContactDelApi({ Ids: data.ID.toString() });
        createMessage.success({ content: '删除成功', key: 'deling' });
        reload();
      }

      function handleAdd() {
        const data = getDataSource();
        const addRow: EditRecordRow = {
          NAME: '',
          POSITION: '',
          DEPART: '',
          TEL: '',
          PHONE: '',
          EMAIL: '',
          REMARK: '',
          editable: true,
          isNew: true,
          key: `${Date.now()}`,
        };
        data.push(addRow);
      }

      function createActions(record: EditRecordRow, column: BasicColumn): ActionItem[] {
        if (!record.editable) {
          return [
            {
              label: '编辑',
              onClick: handleEdit.bind(null, record),
            },
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

      return {
        registerTable,
        handleEdit,
        createActions,
        handleAdd,
        getDataSource,
        handleEditChange,
        handleDel,

        handleSuccess,
      };
    },
  });
</script>
