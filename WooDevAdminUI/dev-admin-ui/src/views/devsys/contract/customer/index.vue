<template>
  <div>
    <BasicTable
      @register="registerTable"
      rowKey="ID"
      :rowSelection="{ type: 'checkbox', selectedRowKeys: checkedKeys, onChange: onSelectChange }"
    >
      <template #toolbar>
        <a-button type="primary" @click="handleCreate">新增</a-button>
        <a-button type="primary" @click="handleDelrolws">批量删除</a-button>
        <a-button type="primary" @click="handleExcel">导出Excel</a-button>
      </template>
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
    <ExportExcelModel @register="registerExcelModel" />
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { getCusertomerListApi, customerDelApi } from '/@/api/devsys/contract/customer';
  import { customercolumns, getFormConfig } from './index.data';
  import { useGo } from '/@/hooks/web/usePage';
  import { useModal } from '/@/components/Modal';
  import ExportExcelModel from '/@/views/devsys/contract/common/ExportExcelModel.vue';
  import { ExportExcelData } from '/@/api/devsys/model/devCommonModel';
  import {
    GetCreatePermissionApi,
    GetDeletePermissionApi,
    GetUpdatePermissionApi,
    GetDetailPermissionApi,
  } from '/@/api/devsys/system/devpermission';
  export default defineComponent({
    name: 'DevCustomer',
    components: { BasicTable, TableAction, ExportExcelModel },
    setup() {
      const [registerExcelModel, { openModal: openExcelModal }] = useModal();
      const go = useGo();
      const { createMessage: msg } = useMessage();
      const checkedKeys = ref<Array<string | number>>([]);
      const [registerTable, { reload, getColumns, getForm }] = useTable({
        title: '客户列表',
        api: getCusertomerListApi,
        columns: customercolumns,
        formConfig: getFormConfig(),
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        showIndexColumn: false,
        tableSetting: { fullScreen: true },
        rowKey: 'ID',
        actionColumn: {
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
          ellipsis: true,
          fixed: 'right',
          //fixed: undefined,
        },
      });

      /****
       * 详情
       ****/
      function handleView(record: Recordable) {
        try {
          const reqdata = GetDetailPermissionApi({
            PerCode: 'customerdetail',
            Id: Number(record.ID),
          });

          reqdata.then(() => {
            go('/company/customer/customer_detail/' + record.ID);
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'detailing' });
        }
      }
      function handleCreate() {
        try {
          //go('/company/customer/customer_build/0');
          const reqdata = GetCreatePermissionApi({ PerCode: 'customerbuild' });

          reqdata.then(() => {
            go('/company/customer/customer_build/0');
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'adding' });
        }
      }

      function handleEdit(record: Recordable) {
        try {
          const reqdata = GetUpdatePermissionApi({
            PerCode: 'customerbuild',
            Id: Number(record.ID),
          });

          reqdata.then(() => {
            go('/company/customer/customer_build/' + record.ID);
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'editing' });
        }
      }

      async function handleDelete(record: Recordable) {
        customerDelApi({ Ids: record.ID.toString() });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }
      function CustomerDelRows() {
        var _ids = checkedKeys.value.toString();
        customerDelApi({ Ids: _ids });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }
      /**
       * 批量删除
       */
      function handleDelrolws() {
        try {
          if (checkedKeys.value.length <= 0) {
            msg.info({ content: '请选择数据', key: 'deling' });
          } else {
            const reqdata = GetDeletePermissionApi({
              PerCode: 'customerdelete',
              Ids: checkedKeys.value.toString(),
            });
            reqdata.then(() => {
              CustomerDelRows();
            });
          }
        } catch (error) {
          msg.error({ content: '删除失败,' + error, key: 'deling' });
        }
      }
      function onSelectChange(selectedRowKeys: (string | number)[]) {
        console.log(selectedRowKeys);
        checkedKeys.value = selectedRowKeys;
      }

      function handleSuccess() {
        reload();
      }
      //弹窗excel
      function handleExcel() {
        const coloums = getColumns();
        const formvalues = getForm().getFieldsValue();
        const opendata: ExportExcelData = {
          selkey: checkedKeys.value,
          colums: coloums,
          seardata: formvalues,
          extype: 'customer',
        };
        openExcelModal(true, opendata);
      }

      return {
        registerTable,
        handleCreate,
        handleEdit,
        handleDelete,
        handleSuccess,
        handleDelrolws,
        checkedKeys,
        onSelectChange,
        handleView,
        handleExcel,
        registerExcelModel,
      };
    },
  });
</script>
