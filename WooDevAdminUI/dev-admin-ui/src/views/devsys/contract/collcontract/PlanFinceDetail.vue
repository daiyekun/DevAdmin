<template>
  <div>
    <BasicTable @register="registerTable" @edit-change="handleEditChange">
      <!-- <template #action="{ record, column }">
        <TableAction :actions="createActions(record, column)" />
      </template> -->
    </BasicTable>
    <!-- <a-button block class="mt-5" type="dashed" @click="handleAdd"> 新增计划资金 </a-button> -->
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import {
    BasicTable,
    useTable,
    //TableAction,
    BasicColumn,
    ActionItem,
    EditRecordRow,
  } from '/@/components/Table';
  import {
    getPlanFinceListApi,
    planFinceDelApi,
    planFinceSaveApi,
  } from '/@/api/devsys/contract/collcontract';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { cloneDeep } from 'lodash-es';
  import { contplanfinceSaveInfo } from '/@/api/devsys/model/devcontractModel';
  import { getdataListApi } from '/@/api/devsys/system/datadic';
  // let savedata: custContactSaveInfo = {};
  const columns: BasicColumn[] = [
    {
      title: 'ID',
      dataIndex: 'ID',
      editRow: false,
      ifShow: () => {
        return false;
      },
    },
    {
      title: '计划资金名称',
      dataIndex: 'NAME',
      editRule: true,
      editRow: false,
    },
    {
      title: '金额',
      dataIndex: 'AMOUNT',
      editRow: false,
      editRule: true,
      editComponent: 'InputNumber',
    },
    {
      title: '结算方式',
      dataIndex: 'SETT_YPE', //'SettType',
      editRow: false,
      editComponent: 'ApiSelect',
      editComponentProps: {
        api: getdataListApi,
        labelField: 'NAME',
        valueField: 'StrId',
        params: { LbId: 11 },
      },
    },
    {
      title: '计划完成日期',
      dataIndex: 'PLAN_DATE',
      editRow: true,
      editComponent: 'DatePicker',
      editComponentProps: {
        valueFormat: 'YYYY-MM-DD',
        format: 'YYYY-MM-DD',
      },
    },
    {
      title: '说明',
      dataIndex: 'REMARK',
      editRow: false,
    },
  ];

  export default defineComponent({
    components: { BasicTable }, //TableAction
    props: {
      custid: {
        type: Number,
        default: 0,
      },
    },
    setup(props: any) {
      //debugger;
      // console.log('获取值props--->', props.custid);
      const currcustid = Number(props.custid); //toRef(props, 'custid');
      const [registerTable, { getDataSource, reload }] = useTable({
        columns: columns,
        showIndexColumn: false,
        api: getPlanFinceListApi,
        beforeFetch: (t) => {
          t.CustId = currcustid;
        },
        rowKey: 'ID',
        // actionColumn: {
        //   width: 160,
        //   title: '操作',
        //   dataIndex: 'action',
        //   slots: { customRender: 'action' },
        // },
        pagination: false,
      });
      const { createMessage: msg, createConfirm } = useMessage();
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
            //debugger;
            const data = cloneDeep(record.editValueRefs); //获取修改数据，否则有问题
            console.log(data);
            //TODO 此处将数据提交给服务器保存

            let tsavedata: contplanfinceSaveInfo = {
              ID: record.ID,
              NAME: data.NAME,
              AMOUNT: data.AMOUNT,
              SETT_YPE: data.SETT_YPE,
              PLAN_DATE: `${data.PLAN_DATE}`,
              REMARK: data.REMARK,
              CONT_ID: currcustid,
            };
            await planFinceSaveApi(tsavedata);
            reload();
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

      function handleEditChange(data: Recordable) {
        console.log(data);
      }
      function handleDel(data: Recordable) {
        createConfirm({
          iconType: 'warning',
          title: '系统提示',
          content: '您确定要删除此数据吗？删除将无法恢复',
          onOk() {
            planFinceDelApi({ Ids: data.ID.toString() });
            msg.success({ content: '删除成功', key: 'deling' });
            reload();
          },
        });
      }

      function handleAdd() {
        const data = getDataSource();
        const addRow: EditRecordRow = {
          NAME: '',
          AMOUNT: '',
          SETT_YPE: 0,
          PLAN_DATE: '',
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
        currcustid,
      };
    },
  });
</script>
