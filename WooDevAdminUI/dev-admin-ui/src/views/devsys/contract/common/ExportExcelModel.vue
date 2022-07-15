<template>
  <BasicModal
    v-bind="$attrs"
    title="选择导出条件"
    :helpMessage="['选择导出条件']"
    @register="registerModel"
    @ok="handleExcel"
    width="400px"
    height="200px"
  >
    <BasicForm @register="register" />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal'; //useModalInner
  // import { h } from 'vue';
  // import { Tag } from 'ant-design-vue';
  import { BasicForm, useForm, FormSchema } from '/@/components/Form';
  export default defineComponent({
    components: { BasicModal, BasicForm },
    // props: ['selkeys', 'cloms', 'seardata'],
    emits: ['selCondtion'],
    setup() {
      const schemas: FormSchema[] = [
        {
          field: 'selRow',
          component: 'RadioGroup',
          label: '行选择',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '导出选择行',
                value: '1',
              },
              {
                label: '导出当前所有行',
                value: '2',
              },
            ],
          },
        },
        {
          field: 'selCell',
          component: 'RadioGroup',
          label: '列选择',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '导出选择列',
                value: '1',
              },
              {
                label: '导出所有列',
                value: '2',
              },
            ],
          },
        },
      ];
      const [register, { setProps, getFieldsValue }] = useForm({
        labelWidth: 120,
        schemas,
        actionColOptions: {
          span: 24,
        },
        showActionButtonGroup: false,
      });
      const [registerModel] = useModalInner((data) => {
        debugger;
        console.log('====执行 useModalInner');
        console.log(data);
      });

      // function onSelCondtion(record) {
      //   emit('selCondtion', record);
      // }
      function handleExcel() {
        const values = getFieldsValue();
        // console.log('执行 handleExcel');
        // emit('selCondtion', values);
        console.log(JSON.stringify(values));
        // createMessage.success('click search,values:' + JSON.stringify(values));
      }
      return { setProps, register, registerModel, handleExcel };
    },
  });
</script>
