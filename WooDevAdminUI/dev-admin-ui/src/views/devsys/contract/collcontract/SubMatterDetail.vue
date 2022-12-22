<template>
  <div>
    <BasicTable @register="registerTable" @edit-change="handleEditChange">
      <!-- <template #action="{ record, column }">
        <TableAction :actions="createActions(record, column)" />
      </template> -->
    </BasicTable>
    <!-- <a-button block class="mt-5" type="dashed" @click="handleAdd"> 新增标的 </a-button> -->
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
    getSubmatterListApi,
    submatterDelApi,
    contSubmatterSaveApi,
  } from '/@/api/devsys/contract/collcontract';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { cloneDeep } from 'lodash-es';
  import { custContactSaveInfo } from '/@/api/devsys/model/customerModel';
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
      title: '名称',
      dataIndex: 'S_NAME',
      editRow: false,
      editRule: true,
    },
    {
      title: '规格',
      dataIndex: 'SPEC',
      editRow: false,
    },
    {
      title: '型号',
      dataIndex: 'S_TYPE',
      editRow: false,
    },
    {
      title: '单位',
      dataIndex: 'UNIT',
      editRow: false,
      editRule: true,
    },
    {
      title: '数量',
      dataIndex: 'AMOUNT',
      editRow: false,
      editRule: true,
    },
    {
      title: '单价',
      dataIndex: 'PRICE',
      editRow: false,
      editRule: true,
    },
    {
      title: '小计',
      dataIndex: 'SUBTOTAL',
      editRow: false,
    },
    {
      title: '销售报价',
      dataIndex: 'SALE_PRICE',
      editRow: false,
      editRule: true,
    },
    {
      title: '折扣率',
      dataIndex: 'DIS_RATE',
      editRow: false,
    },
    {
      title: '计划交付时间',
      dataIndex: 'PLAN_DATE',
      editRow: false,
      editComponent: 'DatePicker',
      editComponentProps: {
        valueFormat: 'YYYY-MM-DD',
        format: 'YYYY-MM-DD',
      },
    },
    {
      title: '备注',
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
        api: getSubmatterListApi,
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
            const data = cloneDeep(record.editValueRefs); //获取修改数据，否则有问题
            console.log(data);
            //TODO 此处将数据提交给服务器保存

            let tsavedata: custContactSaveInfo = {
              ID: record.ID,
              S_NAME: data.S_NAME,
              SPEC: data.SPEC,
              S_TYPE: data.S_TYPE,
              UNIT: data.UNIT,
              AMOUNT: data.AMOUNT,
              PRICE: data.PRICE,
              SUBTOTAL: data.SUBTOTAL,
              SALE_PRICE: data.SALE_PRICE,
              DIS_RATE: data.DIS_RATE,
              PLAN_DATE: `${data.PLAN_DATE}`,
              REMARK: data.REMARK,
              CONT_ID: currcustid,
            };
            await contSubmatterSaveApi(tsavedata);
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
            submatterDelApi({ Ids: data.ID.toString() });
            msg.success({ content: '删除成功', key: 'deling' });
            reload();
          },
        });
      }

      function handleAdd() {
        const data = getDataSource();
        const addRow: EditRecordRow = {
          S_NAME: '',
          SPEC: '',
          S_TYPE: '',
          UNIT: '',
          AMOUNT: '',
          PRICE: '',
          SUBTOTAL: '',
          SALE_PRICE: '',
          DIS_RATE: '',
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
