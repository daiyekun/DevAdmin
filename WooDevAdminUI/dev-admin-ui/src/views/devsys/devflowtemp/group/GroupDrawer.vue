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
    <UserTable ref="tableRef" :currgroupId="tmpgroupId" />
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { formSchema } from './group.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { flowGroupSaveApi } from '/@/api/devsys/flow/flowgroup';
  import UserTable from './UserTable.vue';

  export default defineComponent({
    name: 'RoleDrawer',
    components: { BasicDrawer, BasicForm, UserTable },
    emits: ['success', 'register'],
    setup(_, { emit }) {
      const tableRef = ref<{ getDataSource: () => any } | null>(null);
      const isUpdate = ref(true);
      const [registerForm, { resetFields, setFieldsValue, validate }] = useForm({
        labelWidth: 90,
        schemas: formSchema,
        showActionButtonGroup: false,
      });
      const tgroupId = ref(0);
      let tmpgroupId = 0;
      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        setDrawerProps({ confirmLoading: false });
        // 需要在setFieldsValue之前先填充treeData，否则Tree组件可能会报key not exist警告
        // if (unref(treeData).length === 0) {
        //   treeData.value = (await getMenuList()) as any as TreeItem[];
        // }
        debugger;
        isUpdate.value = !!data?.isUpdate;
        tgroupId.value = data.record?.ID;
        tmpgroupId = tgroupId.value;
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
        }
      });

      const getTitle = computed(() => (!unref(isUpdate) ? '新增审批组' : '编辑审批组'));

      async function handleSubmit() {
        let array: Array<number> = [];
        try {
          if (tableRef.value) {
            //console.log('table data:', tableRef.value.getDataSource());
            const tadata = tableRef.value.getDataSource();
            for (let i = 0; i < tadata.length; i++) {
              if (tadata[i].isNew) {
                array.push(Number(tadata[i].key));
              }
            }
          }

          const values = await validate();
          setDrawerProps({ confirmLoading: true });
          // TODO custom api
          //console.log(values);
          values.UserIds = array.toString();
          await flowGroupSaveApi(values);
          emit('success');
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }

      return {
        registerDrawer,
        registerForm,
        getTitle,
        handleSubmit,
        tableRef,
        tmpgroupId,
        // treeData,
      };
    },
  });
</script>
