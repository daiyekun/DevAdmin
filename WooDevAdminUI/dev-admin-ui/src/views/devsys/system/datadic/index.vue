<template>
  <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
    <DeptTree class="w-1/4 xl:w-1/5" @select="handleSelect" />

    <BasicTable
      @register="registerTable"
      class="w-3/4 xl:w-4/5"
      :searchInfo="searchInfo"
      @edit-change="onEditChange"
    >
      <template #toolbar>
        <a-button type="primary" @click="handleCreate">新增</a-button>
      </template>
      <template #action="{ record, column }">
        <TableAction :actions="createActions(record, column)" />
      </template>
    </BasicTable>
    <DataDicModal @register="registerModal" @success="handleSuccess" />
  </PageWrapper>
</template>
<script lang="ts">
  import { defineComponent, reactive, ref } from 'vue';

  import {
    BasicTable,
    useTable,
    TableAction,
    ActionItem,
    BasicColumn,
    EditRecordRow,
  } from '/@/components/Table';
  import {
    getdatadicList,
    datadicAddApi,
    datadicDelApi,
    datadicSaveApi,
  } from '/@/api/devsys/system/datadic';
  import { PageWrapper } from '/@/components/Page';
  import DeptTree from './DataDicTree.vue';

  import { useModal } from '/@/components/Modal';
  import DataDicModal from './DataDicModal.vue';

  import { columns, searchFormSchema } from './datadic.data';
  import { cloneDeep } from 'lodash-es';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { devSystemStore } from '/@/store/modules/devsystem';

  export default defineComponent({
    name: 'DataDicManagement',
    components: { BasicTable, PageWrapper, DeptTree, DataDicModal, TableAction },
    setup() {
      const store = devSystemStore();
      store.lbId = 0; //初始化
      const [registerModal] = useModal();
      const searchInfo = reactive<Recordable>({});
      const { createMessage: msg } = useMessage();
      const currentEditKeyRef = ref('');
      const [registerTable, { reload, updateTableDataRecord }] = useTable({
        title: '字典列表',
        api: getdatadicList,
        rowKey: 'ID',
        columns,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        handleSearchInfoFn(info) {
          console.log('handleSearchInfoFn', info);
          return info;
        },
        actionColumn: {
          width: 130,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
      });

      async function handleCreate() {
        if (store.lbId <= 0 || store.lbId == undefined) {
          msg.error({ content: '请选择字典类别', key: 'adding' });
        } else {
          await datadicAddApi({ TypeInt: store.lbId });
          msg.success({ content: '数据创建成功', key: 'adding' });
          reload();
        }
      }

      function handleEdit(record: EditRecordRow) {
        currentEditKeyRef.value = record.key;
        record.onEdit?.(true);
      }

      async function handleDelete(record: Recordable) {
        await datadicDelApi({ Ids: record.ID.toString() });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }

      function handleSuccess({ isUpdate, values }) {
        if (isUpdate) {
          // 演示不刷新表格直接更新内部数据。
          // 注意：updateTableDataRecord要求表格的rowKey属性为string并且存在于每一行的record的keys中
          const result = updateTableDataRecord(values.id, values);
          console.log(result);
        } else {
          reload();
        }
      }

      function handleSelect(deptId = '') {
        // console.log(deptId);
        searchInfo.LbId = deptId;
        reload();
      }

      function handleCancel(record: EditRecordRow) {
        currentEditKeyRef.value = '';
        record.onEdit?.(false, false);
      }

      async function handleSave(record: EditRecordRow) {
        // 校验
        msg.loading({ content: '正在保存...', duration: 0, key: 'saving' });
        const valid = await record.onValid?.();
        if (valid) {
          try {
            let data = cloneDeep(record.editValueRefs);
            // console.log(data);
            // console.log(record);
            data.ID = record.ID;
            //TODO 此处将数据提交给服务器保存
            await datadicSaveApi(data);

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

      function createActions(record: EditRecordRow, column: BasicColumn): ActionItem[] {
        if (!record.editable) {
          return [
            {
              label: '编辑',
              tooltip: '编辑信息',
              icon: 'clarity:note-edit-line',
              disabled: currentEditKeyRef.value ? currentEditKeyRef.value !== record.key : false,
              onClick: handleEdit.bind(null, record),
            },
            {
              label: '删除',
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此信息',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
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

      function onEditChange({ column, value, record }) {
        // 本例
        if (column.dataIndex === 'id') {
          record.editValueRefs.name4.value = `${value}`;
        }
        console.log(column, value, record);
      }

      return {
        registerTable,
        registerModal,
        handleCreate,
        handleEdit,
        handleDelete,
        handleSuccess,
        handleSelect,
        searchInfo,
        createActions,
        onEditChange,
      };
    },
  });
</script>
