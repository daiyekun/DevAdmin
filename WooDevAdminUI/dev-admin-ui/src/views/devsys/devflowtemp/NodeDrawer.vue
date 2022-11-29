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
      <hr />
      <div class="selsp">
        <BasicForm @register="registerForm1" />
      </div>
    </div>
  </BasicDrawer>
  <UserSelectModel @register="registerUserModel" @rowUserDbclick="SelleadUser" />
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { useModal } from '/@/components/Modal';
  import UserSelectModel from '/@/views/devsys/contract/common/UserModel.vue';
  // import { PageWrapper } from '/@/components/Page';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form/index';

  export default defineComponent({
    components: { BasicDrawer, BasicForm, UserSelectModel },
    setup() {
      const [register] = useDrawerInner((data) => {
        // 方式1
        setFieldsValue({
          id: data.data.data.id,
          Name: data.data.data.text.value,
        });
      });
      const [registerUserModel, { openModal: openModal1, closeModal }] = useModal();
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

      const schemas1: FormSchema[] = [
        {
          field: 'spType',
          component: 'Select',
          label: '审批类型',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '人力资源',
                value: '1',
                key: '1',
              },
              {
                label: '审批组',
                value: '2',
                key: '2',
              },
            ],
          },
        },
        {
          field: 'selSpId',
          component: 'Input',
          componentProps: {
            onclick: () => {
              //console.log(e, '点击了。。。。。');
              openModal1();
            },
          },
          label: '审批对象',
          colProps: {
            span: 24,
          },
        },
      ];
      const [registerForm, { setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 24,
        },
      });
      const [registerForm1] = useForm({
        labelWidth: 120,
        schemas: schemas1,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 24,
        },
      });

      function handleOk() {
        console.log('=====================');
        console.log('ok');
        console.log('======================');
      }
      /***
       * 选择审批对象
       */
      function SelleadUser(rd: any) {
        // console.log('触发了--1233SelleadUser', rd);
        // debugger;
        // console.log('触发了--》SelleadUser', rd.NAME, rd.ID);
        setFieldsValue({
          LEAD_USERID: rd.ID,
          LEAD_USERNAME: rd.NAME,
        });
        closeModal();
      }

      return {
        register,
        schemas,
        registerForm,
        handleOk,
        registerForm1,
        registerUserModel,
        openModal1,
        SelleadUser,
      };
    },
  });
</script>
<style lang="less" scoped>
  .selsp {
    padding-top: 6px;
  }
</style>
