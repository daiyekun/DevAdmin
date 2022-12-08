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
      <template #DEPART_IDS_LIST="{ model, field }">
        <BasicTree
          v-model:value="model[field]"
          :treeData="treeData"
          :fieldNames="{ title: 'NAME', key: 'ID' }"
          checkable
          toolbar
          title="经办机构"
          checkStrictly="true"
        />
      </template>
    </BasicForm>
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { formSchema, selflowobj } from './flowtemp.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicTree, TreeItem } from '/@/components/Tree';
  import { flowTempSaveApi } from '/@/api/devsys/flow/flowtemp';
  import { getDepartList } from '/@/api/devsys/system/devsystem';
  import { getdatadictreeList } from '/@/api/devsys/system/datadic';
  // import { cloneDeep } from 'lodash-es';

  export default defineComponent({
    name: 'RoleDrawer',
    components: { BasicDrawer, BasicForm, BasicTree },
    emits: ['success', 'register'],
    setup(_, { emit }) {
      const isUpdate = ref(true);
      const treeData = ref<TreeItem[]>([]);
      const treeDataCate = ref<TreeItem[]>([]);

      const [registerForm, { resetFields, setFieldsValue, validate }] = useForm({
        labelWidth: 90,
        schemas: formSchema,
        showActionButtonGroup: false,
      });
      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        setDrawerProps({ confirmLoading: false });
        // 需要在setFieldsValue之前先填充treeData，否则Tree组件可能会报key not exist警告
        if (unref(treeData).length === 0) {
          treeData.value = (await getDepartList()) as any as TreeItem[];
        }
        if (unref(treeData).length === 0) {
          treeDataCate.value = (await getdatadictreeList({ dtype: 1 })) as any as TreeItem[];
        }
        isUpdate.value = !!data?.isUpdate;

        if (unref(isUpdate)) {
          //debugger;
          console.log('setFieldsValue 绑定。。。。。');
          // updateSchema({
          //   field: 'OBJ_TYPE',
          //   componentProps: {
          //     value: data.record.OBJ_TYPE,
          //   },
          // });
          setFieldsValue({
            ...data.record,
            OBJ_TYPE: data.record.OBJ_TYPE_Str,
          });
          selflowobj.value = data.record.OBJ_TYPE;
        }
      });

      const handleChange = (value: string[]) => {
        console.log(`selected ${value}`);
      };

      const getTitle = computed(() => (!unref(isUpdate) ? '新增流程模板' : '编辑流程模板'));

      async function handleSubmit() {
        try {
          const values = await validate();
          setDrawerProps({ confirmLoading: true });
          // TODO custom api
          console.log(values);
          //debugger;
          await flowTempSaveApi(values);
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
        treeData,
        treeDataCate,
        handleChange,
        // optionsA,
      };
    },
  });
</script>
