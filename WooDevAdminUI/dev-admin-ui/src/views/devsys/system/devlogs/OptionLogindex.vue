<template>
  <BasicTable
    @register="registerTable"
    :rowSelection="{ type: 'checkbox', selectedRowKeys: checkedKeys, onChange: onSelectChange }"
  >
    <template #form-custom> custom-slot </template>
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
    <!-- <template #toolbar>
      <a-button type="primary" @click="getFormValues">获取表单数据</a-button>
    </template> -->
  </BasicTable>
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicTable, useTable } from '/@/components/Table';
  import { getOptionLogColumns, getOptionFormConfig } from './devLogsData';
  //import { Alert } from 'ant-design-vue';
  import { getOptionLogList } from '/@/api/devsys/system/devsystem';

  export default defineComponent({
    name: 'DevLoginLog',
    components: { BasicTable },
    setup() {
      const checkedKeys = ref<Array<string | number>>([]);
      const [registerTable, { getForm }] = useTable({
        title: '操作日志列表',
        api: getOptionLogList,
        columns: getOptionLogColumns(),
        useSearchForm: true,
        formConfig: getOptionFormConfig(),
        showTableSetting: true,
        tableSetting: { fullScreen: true },
        showIndexColumn: false,
        rowKey: 'ID',
      });

      function getFormValues() {
        console.log(getForm().getFieldsValue());
      }

      function onSelectChange(selectedRowKeys: (string | number)[]) {
        console.log(selectedRowKeys);
        checkedKeys.value = selectedRowKeys;
      }

      return {
        registerTable,
        getFormValues,
        checkedKeys,
        onSelectChange,
      };
    },
  });
</script>
