<template>
  <BasicModal v-bind="$attrs" @register="registerModal" :title="getTitle" @ok="handleSubmit">
    <BasicForm @register="registerForm" />
  </BasicModal>
  <BasicUpload
    :maxSize="20"
    :maxNumber="10"
    @change="handleChange"
    :api="uploadApi"
    class="my-5"
    :accept="['image/*']"
  />
</template>
<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { FormSchema } from '/@/components/Table';
  import { getdataListApi } from '/@/api/devsys/system/datadic';
  import { BasicUpload } from '/@/components/Upload';
  import { getDepartList, departSaveApi } from '/@/api/devsys/system/devsystem';
  import { uploadApi } from '/@/api/sys/upload';
  export default defineComponent({
    name: 'DeptModal',
    components: { BasicModal, BasicForm, BasicUpload },
    emits: ['success', 'register'],
    setup(_, { emit }) {
      const isUpdate = ref(true);
      const formSchema: FormSchema[] = [
        {
          field: 'field1',
          component: 'Upload',
          label: '字段1',
          colProps: {
            span: 8,
          },
          rules: [{ required: true, message: '请选择上传文件' }],
          componentProps: {
            api: uploadApi,
          },
        },
        {
          field: 'NAME',
          label: '附件名称',
          component: 'Input',
          required: true,
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'CATE_ID',
          component: 'ApiSelect',
          componentProps: {
            api: getdataListApi,
            labelField: 'NAME',
            valueField: 'ID',
            params: { LbId: 4 },
          },
          label: '附件类别',
          required: true,
          colProps: {
            offset: 2,
          },
        },
        {
          label: '备注',
          field: 'REMARK',
          component: 'InputTextArea',
          colProps: {
            offset: 2,
          },
        },
      ];

      const [registerForm, { resetFields, setFieldsValue, updateSchema, validate }] = useForm({
        labelWidth: 100,
        schemas: formSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
        resetFields();
        setModalProps({ confirmLoading: false });
        isUpdate.value = !!data?.isUpdate;

        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
        }
        const treeData = await getDepartList();
        updateSchema({
          field: 'PID',
          componentProps: { treeData },
        });
      });

      const getTitle = computed(() => (!unref(isUpdate) ? '新增部门' : '编辑部门'));

      async function handleSubmit() {
        try {
          const values = await validate();
          setModalProps({ confirmLoading: true });
          // TODO custom api
          console.log(values);
          await departSaveApi(values);
          closeModal();
          emit('success');
        } finally {
          setModalProps({ confirmLoading: false });
        }
      }

      return {
        handleChange: (list: string[]) => {
          createMessage.info(`已上传文件${JSON.stringify(list)}`);
        },
        registerModal,
        registerForm,
        getTitle,
        handleSubmit,
        uploadApi,
      };
    },
  });
</script>
