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
      <template #menu="{ model, field }">
        <BasicTree
          v-model:value="model[field]"
          :treeData="treeData"
          :fieldNames="{ title: 'NAME', key: 'ID' }"
          checkable
          toolbar
          title="经办机构"
        />
      </template>

      <!-- <template #category="{ model, field }">
        <BasicTree
          v-model:value="model[field]"
          :treeData="treeDataCate"
          :fieldNames="{ title: 'NAME', key: 'ID' }"
          checkable
          toolbar
          title="类别"
        />
      </template> -->
      <!-- <template #flowitem="{ model, field }">
        <BasicTree
          v-model:value="model[field]"
          :treeData="treeData"
          :fieldNames="{ title: 'NAME', key: 'ID' }"
          checkable
          toolbar
          title="审批事项"
        />
      </template> -->
      <template #flowitem="{ model, field }">
        <a-select :options="options" mode="multiple" v-model:value="model[field]" allowClear />
      </template>
      <!-- <template #category="{ model, field }">
        <ApiSelect
          :api="optionsListApi"
          mode="multiple"
          v-model:value="model[field]"
          optionFilterProp="label"
          resultField="list"
          labelField="name"
          valueField="id"
        />
      </template> -->
      <!-- <a-form-item label="类别" required>
        <a-select
          v-model:value="value"
          mode="multiple"
          style="width: 100%"
          placeholder="Please select"
          :options="[...Array(25)].map((_, i) => ({ value: (i + 10).toString(36) + (i + 1) }))"
          @change="handleChange"
        />
      </a-form-item> -->
    </BasicForm>
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { formSchema } from './flowtemp.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicTree, TreeItem } from '/@/components/Tree';
  import { roleSaveApi } from '/@/api/devsys/system/devsystem';
  // import { getMenuList } from '/@/api/sys/menu';
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
          setFieldsValue({
            ...data.record,
          });
        }
      });

      const handleChange = (value: string[]) => {
        console.log(`selected ${value}`);
      };

      const options = ref<Recordable[]>([]);
      for (let i = 1; i < 10; i++) options.value.push({ label: '选项' + i, value: `${i}` });

      // const optionsA = computed(() => {
      //   return cloneDeep(unref(options)).map((op) => {
      //     return op;
      //   });
      // });

      const getTitle = computed(() => (!unref(isUpdate) ? '新增流程模板' : '编辑流程模板'));

      async function handleSubmit() {
        try {
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
        treeData,
        treeDataCate,
        handleChange,
        // optionsA,
        value: ref(['a1', 'b2']),
      };
    },
  });
</script>
