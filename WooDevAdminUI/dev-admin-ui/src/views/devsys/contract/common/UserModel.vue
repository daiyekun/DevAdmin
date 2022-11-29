<template>
  <BasicModal
    v-bind="$attrs"
    title="选择用户信息"
    :helpMessage="['选择用户信息', '双击选择用户']"
    width="700px"
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
            v-model="search_word"
            placeholder="请输入用户名"
            enter-button="搜索"
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
  import { getUserList } from '/@/api/devsys/system/devsystem';
  import { h } from 'vue';
  import { Tag } from 'ant-design-vue';
  //import { FormSchema } from '/@/components/Table';
  //:searchInfo="searchInfo" 列表属性
  export default defineComponent({
    components: { BasicModal, BasicTable },
    emits: ['rowUserDbclick'],
    setup(_, { emit }) {
      const searchInfo = reactive<Recordable>({});
      const search_word = ref('');
      // const searchFormSchema: FormSchema[] = [
      //   {
      //     field: 'LOGIN_NAME',
      //     label: '登录名',
      //     component: 'Input',
      //     colProps: { span: 12 },
      //   },
      // ];
      const columns: BasicColumn[] = [
        {
          title: '登录名',
          dataIndex: 'LOGIN_NAME',
          width: 120,
        },
        {
          title: '姓名',
          dataIndex: 'NAME',
          width: 120,
        },

        {
          title: '性别',
          dataIndex: 'SEX',
          width: 100,
          customRender: ({ record }) => {
            const status = record.SEX;
            const enable = ~~status === 1;
            const color = enable ? 'blue' : 'red';
            const text = enable ? '男' : '女';
            return h(Tag, { color: color }, () => text);
          },
        },
        {
          title: '电话',
          dataIndex: 'TEL',
          width: 130,
        },
        {
          title: '状态',
          dataIndex: 'USTATE',
          width: 110,
          customRender: ({ record }) => {
            const status = record.USTATE;
            const enable = ~~status === 1;
            const color = enable ? 'green' : 'red';
            const text = enable ? '启用' : '禁用';
            return h(Tag, { color: color }, () => text);
          },
        },

        {
          title: '创建时间',
          dataIndex: 'CREATE_TIME',
          width: 140,
        },
      ];
      const [registerTable, { reload }] = useTable({
        //{ reload, updateTableDataRecord }
        title: '用户列表',
        api: getUserList,
        beforeFetch: (t) => {
          t.LOGIN_NAME = search_word.value;
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
        emit('rowUserDbclick', record);
        //console.log('onRowDbClick', event);
      }
      /***
       * 查询按钮
       ***/
      function onSearch() {
        reload();
      }
      return { registerTable, searchInfo, onRowDbClick, onSearch, search_word };
    },
  });
</script>
