<template>
  <!-- 宽度可以百分比 -->
  <BasicDrawer
    v-bind="$attrs"
    @register="register"
    title="详细信息"
    width="500px"
    showFooter
    @ok="handleOk"
  >
    <div>
      <BasicForm @register="registerForm" />
    </div>
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  // import { PageWrapper } from '/@/components/Page';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form/index';
  const schemas: FormSchema[] = [
    {
      field: 'Name',
      component: 'Input',
      componentProps: { disabled: true },
      label: '名称',
      colProps: {
        span: 24,
      },
      defaultValue: '',
    },
    {
      field: 'id',
      component: 'Input',
      componentProps: { disabled: true },
      label: '编号',
      colProps: {
        span: 24,
      },
    },
  ];

  export default defineComponent({
    components: { BasicDrawer, BasicForm },
    setup() {
      const [registerForm, { setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 24,
        },
      });
      const [register] = useDrawerInner((data) => {
        // 方式1
        setFieldsValue({
          id: data.data.data.id,
          Name: data.data.data.text.value,
        });
      });

      function handleOk() {
        console.log('=====================');
        console.log('ok');
        console.log('======================');
      }
      return { register, schemas, registerForm, handleOk };
    },
  });
</script>
