<template>
  <BasicTable
    @register="registerTable"
    :rowSelection="{ type: 'checkbox', selectedRowKeys: checkedKeys, onChange: onSelectChange }"
  >
    <!-- <template #form-custom> custom-slot </template> -->
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
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table'; //TableAction
  import { customercolumns, getFormConfig } from './index.data';
  // import { Alert } from 'ant-design-vue';
  import { useGo } from '/@/hooks/web/usePage';
  import { getCusertomerListApi, customerDelApi } from '/@/api/devsys/contract/customer';
  import {
    GetCreatePermissionApi,
    GetDeletePermissionApi,
    GetUpdatePermissionApi,
    GetDetailPermissionApi,
  } from '/@/api/devsys/system/devpermission';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useModal } from '/@/components/Modal';
  import ExportExcelModel from '/@/views/devsys/contract/common/ExportExcelModel.vue';
  import { ExportExcelData } from '/@/api/devsys/model/devCommonModel';
  export default defineComponent({
    name: 'DevCustomer',
    components: { BasicTable, TableAction, ExportExcelModel }, //TableAction  //ExportExcelModel
    setup() {
      const [registerExcelModel, { openModal: openExcelModal }] = useModal();
      const go = useGo();
      const { createMessage: msg } = useMessage();
      const checkedKeys = ref<Array<string | number>>([]);
      // const seardata = ref({});
      // var cols = reactive([]);
      const [registerTable, { getForm, reload, getColumns }] = useTable({
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
          // ellipsis: true,
          fixed: 'right',
          //fixed: undefined,
        },
      });

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

      function getFormValues() {
        console.log(getForm().getFieldsValue());
      }
      function deleCustomer(record: Recordable) {
        customerDelApi({ Ids: record.ID.toString() });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }
      function handleDelete(record: Recordable) {
        try {
          const reqdata = GetDeletePermissionApi({
            PerCode: 'customerdelete',
            Ids: record.ID.toString(),
          });
          reqdata.then(() => {
            deleCustomer(record);
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'deling' });
        }
      }
      function CustomerDelRows() {
        var _ids = checkedKeys.value.toString();
        customerDelApi({ Ids: _ids });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }
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
      function onSelectChange(selectedRowKeys: (string | number)[]) {
        console.log(selectedRowKeys);
        checkedKeys.value = selectedRowKeys;
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
        getFormValues,
        checkedKeys,
        onSelectChange,
        handleDelete,
        handleView,
        handleEdit,
        handleCreate,
        handleDelrolws,
        handleExcel,
        registerExcelModel,
      };
    },
  });
</script>
