<!-- 审批历史 -->
<template>
  <div>
    <BasicTable @register="registerTable" rowKey="ID" :rowSelection="{ type: 'checkbox' }">
      <!-- <template #toolbar>
        <a-button type="primary">打印审批单</a-button>
        <a-button type="primary">查看</a-button>
      </template> -->
      <template #action="{ record }">
        <TableAction
          width="130"
          :actions="[
            {
              icon: 'ant-design:file-pdf-outlined',
              tooltip: '打印审批单',
              onClick: handPrintPdf.bind(null, record),
            },
            {
              icon: 'ant-design:branches-outlined',
              tooltip: '查看',
              onClick: handViewChart.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
  </div>
</template>
<script lang="ts">
  import { defineComponent, h } from 'vue';
  import { BasicTable, useTable, TableAction, BasicColumn } from '/@/components/Table';
  import { GetFlowInstListApi } from '/@/api/devsys/flow/flowinst';
  import { Tag } from 'ant-design-vue';
  const flowinstcolumns: BasicColumn[] = [
    {
      title: 'ID',
      dataIndex: 'ID',
      width: 80,
      defaultHidden: true,
    },
    {
      title: '审批事项',
      dataIndex: 'FlowItemDic',
      width: 150,
      align: 'left',
      fixed: 'left',
    },
    {
      title: '审批状态',
      dataIndex: 'StateDic',
      width: 120,
      customRender: ({ record }) => {
        const status = record.FLOW_STATE;
        let color = 'default';
        const text = record.StateDic;
        switch (~~status) {
          case 1: //未审核
            color = '#0080FF';
            break;
          case 2: //审核通过
            color = 'green';
            break;
          case 3: //打回
            color = '#FF0000';
            break;
          default:
            color = '#F0F2F5';
            break;
        }
        return h(Tag, { color: color }, () => text);
      },
    },
    {
      title: '流程节点',
      dataIndex: 'CURR_NODE_NAME',
      width: 140,
      align: 'left',
    },

    {
      title: '发起人',
      dataIndex: 'StartUserName',
    },
    {
      title: '发起时间',
      dataIndex: 'CREATE_TIME',
      // sorter: true,
    },
    {
      title: '完成时间',
      dataIndex: 'FLOW_END_TIME',
      width: 130,
      // sorter: true,
    },
  ];
  export default defineComponent({
    components: { BasicTable, TableAction },
    props: {
      custid: {
        //审批对象ID
        type: Number,
        default: 0,
      },
      objtype: {
        //审批对象类型
        type: Number,
        default: -1,
      },
    },
    setup(props: any) {
      const currcustid = Number(props.custid);
      const flowobjtype = Number(props.objtype);
      const [registerTable] = useTable({
        title: '审批历史记录',
        api: GetFlowInstListApi,
        columns: flowinstcolumns,
        beforeFetch: (t) => {
          t.CustId = currcustid;
          t.FlowType = flowobjtype;
        },
        //formConfig: getFormConfig(),
        //useSearchForm: true,
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
          width: '130',
        },
      });
      /**
       * 打印审批单
       */
      function handPrintPdf(record: Recordable) {
        console.log(record);
      }
      /***
       * 查看流出图
       */
      function handViewChart(record: Recordable) {
        console.log(record);
      }

      return {
        handPrintPdf,
        handViewChart,
        registerTable,
      };
    },
  });
</script>
