<template>
  <BasicModal
    v-bind="$attrs"
    title="选择签约主体信息"
    :helpMessage="['选择签约主体信息', '双击选择签约主体']"
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
  import { getdepartMainList } from '/@/api/devsys/system/devsystem';
  import { h } from 'vue';
  import { Tag } from 'ant-design-vue';
  //import { FormSchema } from '/@/components/Table';
  //:searchInfo="searchInfo" 列表属性
  export default defineComponent({
    components: { BasicModal, BasicTable },
    emits: ['rowMainDeptDbclick', 'selectRowMainDept'],
    setup(_, { emit }) {
      const searchInfo = reactive<Recordable>({});
      const search_word = ref('');
      const columns: BasicColumn[] = [
        {
          title: '名称',
          dataIndex: 'NAME',
          width: 180,
        },
        {
          title: '编号',
          dataIndex: 'CODE',
          width: 130,
        },

        {
          title: '状态',
          dataIndex: 'DSTATE',
          width: 100,
          customRender: ({ record }) => {
            const DSTATE = record.DSTATE;
            const enable = ~~DSTATE === 0;
            const color = enable ? 'red' : 'green';
            const text = enable ? '停用' : '启用';
            return h(Tag, { color: color }, () => text);
          },
        },
        {
          title: '创建时间',
          dataIndex: 'CREATE_TIME',
          width: 140,
        },
      ];
      const [registerTable, { reload, getSelectRows }] = useTable({
        //{ reload, updateTableDataRecord }
        title: '签约主体列表',
        api: getdepartMainList,
        beforeFetch: (t) => {
          t.depName = search_word.value;
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
        emit('rowMainDeptDbclick', record);
        //console.log('onRowDbClick', event);
      }
      function handleOk() {
        var selectrow = getSelectRows();
        emit('selectRowMainDept', selectrow);
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
