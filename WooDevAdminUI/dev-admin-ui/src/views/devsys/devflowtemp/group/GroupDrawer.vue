<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="500px"
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm">
      <!-- <template #menu="{ model, field }">
        <BasicTree
          v-model:value="model[field]"
          :treeData="treeData"
          :fieldNames="{ title: 'meta.title', key: 'id' }"
          checkable
          toolbar
          title="菜单分配"
        />
      </template> -->
    </BasicForm>
    <UserTable ref="tableRef" />
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { formSchema } from './group.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  // import { BasicTree, TreeItem } from '/@/components/Tree';
  import { roleSaveApi } from '/@/api/devsys/system/devsystem';
  import UserTable from './UserTable.vue';

  // import { getMenuList } from '/@/api/sys/menu';

  export default defineComponent({
    name: 'RoleDrawer',
    components: { BasicDrawer, BasicForm, UserTable },
    emits: ['success', 'register'],
    setup(_, { emit }) {
      const tableRef = ref<{ getDataSource: () => any } | null>(null);
      const isUpdate = ref(true);
      // const treeData = ref<TreeItem[]>([]);

      const [registerForm, { resetFields, setFieldsValue, validate }] = useForm({
        labelWidth: 90,
        schemas: formSchema,
        showActionButtonGroup: false,
      });

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        setDrawerProps({ confirmLoading: false });
        // 需要在setFieldsValue之前先填充treeData，否则Tree组件可能会报key not exist警告
        // if (unref(treeData).length === 0) {
        //   treeData.value = (await getMenuList()) as any as TreeItem[];
        // }
        isUpdate.value = !!data?.isUpdate;

        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
        }
      });

      const getTitle = computed(() => (!unref(isUpdate) ? '新增审批组' : '编辑审批组'));

      async function handleSubmit() {
        try {
          if (tableRef.value) {
            console.log('table data:', tableRef.value.getDataSource());
          }
          const values = await validate();
          setDrawerProps({ confirmLoading: true });
          // TODO custom api
          console.log(values);
          await roleSaveApi(values);
          closeDrawer();
          emit('success');
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
        // treeData,
      };
    },
  });
</script>
