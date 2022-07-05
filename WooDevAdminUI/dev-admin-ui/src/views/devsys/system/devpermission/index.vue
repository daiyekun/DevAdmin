<template>
  <PageWrapper dense fixedHeight contentClass="flex">
    <RoleTree class="w-1/4 xl:w-1/5" @select="handleSelect" />
    <BasicTable
      @register="registerTable"
      class="w-3/4 xl:w-4/5"
      @fetch-success="onFetchSuccess"
      :searchInfo="searchInfo"
    >
      <template #toolbar>
        <a-button type="primary" @click="handleSave"> 保存权限 </a-button>
      </template>
      <!-- <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:note-edit-line',
              // onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              popConfirm: {
                title: '是否确认删除',
                placement: 'left',
                // confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
      </template> -->
    </BasicTable>

    <!-- <MenuDrawer @register="registerDrawer" @success="handleSuccess" /> -->
  </PageWrapper>
</template>
<script lang="ts">
  import { defineComponent, nextTick, reactive } from 'vue';

  import { BasicTable, useTable } from '/@/components/Table';
  import { getRoleMenuList, permssionSaveApi } from '/@/api/devsys/system/devsystem';
  // import { useMessage } from '/@/hooks/web/useMessage';
  import RoleTree from './RoleTree.vue';
  import { columns, searchFormSchema, permssionInfo } from './pessionmenu.data';
  import { PageWrapper } from '/@/components/Page';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'DevPermission',
    components: { BasicTable, PageWrapper, RoleTree },
    setup() {
      //const { createMessage: msg } = useMessage();
      const searchInfo = reactive<Recordable>({});
      const { createMessage: msg } = useMessage();
      const [registerTable, { reload, expandAll, getDataSource }] = useTable({
        title: '权限列表',
        api: getRoleMenuList,
        columns,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
        },
        // isTreeTable: true,
        pagination: true,
        striped: false,
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        showIndexColumn: false,
        canResize: false,
        // actionColumn: {
        //   width: 80,
        //   title: '操作',
        //   dataIndex: 'action',
        //   slots: { customRender: 'action' },
        //   fixed: undefined,
        // },
        handleSearchInfoFn(info) {
          return info;
        },
      });

      function handleSelect(RoleId = 0) {
        searchInfo.RoleId = RoleId;
        reload();
      }

      async function handleSave() {
        let array: Array<permssionInfo> = [];
        let data1 = getDataSource();
        for (let i = 0; i < data1.length; i++) {
          let persInfo: permssionInfo = {
            Id: data1[i].id,
            Permission: data1[i].permission,
            Pssionlb: data1[i].pssionlb,
            RoleId: searchInfo.RoleId,
          };

          array.push(persInfo);
        }
        await permssionSaveApi(array);
        msg.success({ content: '权限保存成功', key: 'saveing' });
        // var data2 = getRawDataSource();
        // console.log('数据---->', data1, '数据2-->', data2);
      }

      function handleSuccess() {
        reload();
      }

      function onFetchSuccess() {
        // 演示默认展开所有表项
        nextTick(expandAll);
      }

      return {
        registerTable,
        handleSave,
        handleSelect,
        // handleEdit,
        // handleDelete,
        handleSuccess,
        onFetchSuccess,
        searchInfo,
      };
    },
  });
</script>
