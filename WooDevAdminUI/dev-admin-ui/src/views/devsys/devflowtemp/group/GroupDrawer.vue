<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="500px"
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm" />
    <div>
      <a-button block class="mt-1" type="primary" @click="handleAdd"> 新增成员 </a-button>
      <BasicTable @register="registerTable" @edit-change="handleEditChange">
        <template #action="{ record, column }">
          <TableAction :actions="createActions(record, column)" />
        </template>
      </BasicTable>
    </div>
    <UserSelectModel
      @register="registerUserModel"
      @rowUserDbclick="SelleadUser"
      @selectRowUser="selectRowUser"
    />
    <!-- <UserTable ref="tableRef" :currgroupId="tmpgroupId.value" /> -->
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { formSchema } from './group.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { flowGroupSaveApi, getGroupUserList } from '/@/api/devsys/flow/flowgroup';
  import { useMessage } from '/@/hooks/web/useMessage';
  //import UserTable from './UserTable.vue';
  import {
    BasicTable,
    useTable,
    TableAction,
    BasicColumn,
    ActionItem,
    EditRecordRow,
  } from '/@/components/Table';
  import UserSelectModel from '/@/components/DevComponents/src/UserModel.vue';
  import { useModal } from '/@/components/Modal';
  // import { getGroupUserList } from '/@/api/devsys/flow/flowgroup
  const columns: BasicColumn[] = [
    {
      title: '登录名',
      dataIndex: 'LOGIN_NAME',
      editRow: true,
    },
    {
      title: '姓名',
      dataIndex: 'NAME',
      editRow: true,
    },
  ];
  export default defineComponent({
    name: 'RoleDrawer',
    components: { BasicDrawer, BasicForm, BasicTable, TableAction, UserSelectModel },
    emits: ['success', 'register'],
    setup(_, { emit }) {
      const { createMessage: msg } = useMessage();
      const tableRef = ref<{ getDataSource: () => any } | null>(null);
      const isUpdate = ref(true);
      const [registerForm, { resetFields, setFieldsValue, validate }] = useForm({
        labelWidth: 90,
        schemas: formSchema,
        showActionButtonGroup: false,
      });
      const tgroupId = ref(0);
      const tmpgroupId = ref(0);
      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        setDrawerProps({ confirmLoading: false });
        // 需要在setFieldsValue之前先填充treeData，否则Tree组件可能会报key not exist警告
        // if (unref(treeData).length === 0) {
        //   treeData.value = (await getMenuList()) as any as TreeItem[];
        // }

        isUpdate.value = !!data?.isUpdate;
        tgroupId.value = data.record?.ID;
        tmpgroupId.value = data.record?.ID;
        console.log('组ID======', data.record?.ID);
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
        }
        reloadUser();
      });

      const getTitle = computed(() => (!unref(isUpdate) ? '新增审批组' : '编辑审批组'));

      async function handleSubmit() {
        let array: Array<number> = [];
        let tabuserdata = getUserData();
        try {
          if (tabuserdata.length > 0) {
            //console.log('table data:', tableRef.value.getDataSource());
            const tadata = tabuserdata;
            for (let i = 0; i < tadata.length; i++) {
              // if (tadata[i].isNew) {
              array.push(Number(tadata[i].key));
              //}
            }
          }

          const values = await validate();
          setDrawerProps({ confirmLoading: true });
          // TODO custom api
          //console.log(values);
          values.UserIds = array.toString();
          console.log('保存组数据====', values);
          await flowGroupSaveApi(values);
          emit('success');
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      /***
       * 用户表
       */
      const [registerTable, { getDataSource, deleteTableDataRecord, reload }] = useTable({
        columns: columns,
        showIndexColumn: false,
        //dataSource: data,
        api: getGroupUserList,
        beforeFetch: (t) => {
          t.GroupId = tmpgroupId.value;
          t.trand = new Date().getMilliseconds();
        },
        actionColumn: {
          width: 160,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
        pagination: false,
      });

      const [registerUserModel, { openModal: openModal1, closeModal }] = useModal();

      //删除
      function handleDel(record: EditRecordRow) {
        deleteTableDataRecord(record.key);
      }

      function handleCancel(record: EditRecordRow) {
        record.onEdit?.(false);
        if (record.isNew) {
          const data = getDataSource();
          const index = data.findIndex((item) => item.key === record.key);
          data.splice(index, 1);
        }
      }

      function handleSave(record: EditRecordRow) {
        record.onEdit?.(false, true);
      }

      function handleEditChange(data: Recordable) {
        console.log(data);
      }

      function handleAdd() {
        openModal1();
      }
      /***
       * 刷新用户
       */
      function reloadUser() {
        reload();
      }
      /***
       *获取用户
       */
      function getUserData() {
        let tpdata = getDataSource();
        return tpdata;
      }

      function createActions(record: EditRecordRow, column: BasicColumn): ActionItem[] {
        if (!record.editable) {
          return [
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
      /***
       * 选择用户
       ****/
      function SelleadUser(rd: any) {
        // console.log('触发了--1233SelleadUser', rd);
        // debugger;
        const data = getDataSource();
        const addRow: EditRecordRow = {
          LOGIN_NAME: rd.LOGIN_NAME,
          NAME: rd.NAME,
          editable: false,
          isNew: true,
          key: `${rd.ID}`,
        };
        data.push(addRow);

        // console.log('触发了--》SelleadUser', rd.NAME, rd.ID);

        // closeModal();
      }

      /***
       * 选择用户
       ****/
      function selectRowUser(rds: any) {
        if (rds.length <= 0) {
          msg.warn({
            content: '请选择数据',
            key: 'tmsg',
          });
        } else {
          //添加到后台
          const data = getDataSource();
          for (let i = 0; i < rds.length; i++) {
            const addRow: EditRecordRow = {
              LOGIN_NAME: rds[i].LOGIN_NAME,
              NAME: rds[i].NAME,
              editable: false,
              isNew: true,
              key: `${rds[i].ID}`,
            };
            data.push(addRow);
          }
          closeModal();
        }
      }

      return {
        registerDrawer,
        registerForm,
        getTitle,
        handleSubmit,
        tableRef,
        tmpgroupId,
        registerTable,
        handleDel,
        createActions,
        handleAdd,
        getDataSource,
        handleEditChange,
        registerUserModel,
        SelleadUser,
        selectRowUser,
        reloadUser,
        getUserData,
        // treeData,
      };
    },
  });
</script>
