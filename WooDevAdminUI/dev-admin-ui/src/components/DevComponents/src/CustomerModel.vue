<template>
  <BasicModal
    v-bind="$attrs"
    title="选择客户信息信息"
    :helpMessage="['选择客户信息', '双击选择客户']"
    width="700px"
    @ok="handleOk"
  >
    <BasicTable
      @register="registerTable"
      :rowSelection="{ type: 'checkbox' }"
      size="small"
      @row-dbClick="onRowDbClick"
    >
      <template #toolbar>
        <!-- <a-button type="primary" @click="searchHand"> 增加 </a-button> -->
        <div style="margin-bottom: 12px; text-align: left">
          <a-input-search
            v-model:value="search_word"
            placeholder="请输入名称"
            enter-button="查询"
            size="large"
            style="width: 240px; height: 22px"
            @search="onSearch"
          />
        </div>
      </template>
    </BasicTable>
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, reactive, ref } from 'vue';
  import { BasicModal } from '/@/components/Modal';
  import { BasicTable, useTable, BasicColumn } from '/@/components/Table';
  import { getCusertomerListApi } from '/@/api/devsys/contract/customer';
  import { h } from 'vue';
  import { Tag } from 'ant-design-vue';
  //import { FormSchema } from '/@/components/Table';
  //:searchInfo="searchInfo" 列表属性
  export default defineComponent({
    //name: 'CustomerModel',
    components: { BasicModal, BasicTable },
    emits: ['rowCustomerDbclick', 'selectRowCustomer'],
    setup(_, { emit }) {
      const searchInfo = reactive<Recordable>({});
      const search_word = ref('');
      const columns: BasicColumn[] = [
        {
          title: '名称',
          dataIndex: 'NAME',
          width: 130,
        },
        {
          title: '编号',
          dataIndex: 'CODE',
          width: 120,
        },

        {
          title: '类别',
          dataIndex: 'CateName',
          width: 130,
          // align: 'left',
        },
        {
          title: '状态',
          dataIndex: 'StateDic',
          width: 120,
          customRender: ({ record }) => {
            const status = record.C_STATE;
            let color = 'default';
            const text = record.StateDic;
            switch (~~status) {
              case 0: //未审核
                color = 'default';
                break;
              case 1: //审核通过
                color = 'green';
                break;
              case 2: //终止
                color = '#f50';
                break;
              default:
                break;
            }
            return h(Tag, { color: color }, () => text);
          },
        },
      ];
      const [registerTable, { reload, getSelectRows }] = useTable({
        //{ reload, updateTableDataRecord }
        title: '客户列表',
        api: getCusertomerListApi,
        beforeFetch: (t) => {
          t.Name = search_word.value;
          t.SelecType = 1;
        },
        rowKey: 'ID',
        columns,
        formConfig: {
          labelWidth: 120,
          //schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: false,
        showTableSetting: true,
        bordered: true,
        handleSearchInfoFn(info) {
          //console.log('handleSearchInfoFn', info);
          return info;
        },
      });

      function onRowDbClick(record) {
        emit('rowCustomerDbclick', record);
        //console.log('onRowDbClick', event);
      }
      function handleOk() {
        var selectrow = getSelectRows();
        emit('selectRowCustomer', selectrow);
        console.log('handleOk--', selectrow);
      }
      /***
       * 查询按钮
       ***/
      function onSearch() {
        reload();
      }
      return { registerTable, searchInfo, onRowDbClick, onSearch, search_word, handleOk };
    },
  });
</script>
