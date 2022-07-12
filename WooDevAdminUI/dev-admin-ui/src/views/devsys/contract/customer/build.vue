<template>
  <PageWrapper>
    <!-- class="high-form" title="高级表单" content=" " -->
    <a-card title="客户信息" :bordered="false">
      <BasicForm @register="register" />
    </a-card>
    <a-card title="客户附件" :bordered="false" class="!mt-5">
      <FileBuildTable ref="tableFileRef" />
    </a-card>
    <a-card title="客户联系人" :bordered="false" class="!mt-5">
      <ContactBuild ref="tableRef" />
    </a-card>
    <UserSelectModel @register="registerUserModel" @rowUserDbclick="SelleadUser" />
    <!-- <a-button type="primary" class="my-4" @click="openModal1"> 打开弹窗 </a-button> -->
    <template #rightFooter>
      <a-button type="primary" @click="submitAll"> 提交 </a-button>
    </template>
  </PageWrapper>
</template>
<script lang="ts">
  import { getdataListApi } from '/@/api/devsys/system/datadic';
  //import { FormSchema } from '/@/components/Form';
  import { BasicForm, useForm, FormSchema } from '/@/components/Form';
  import { defineComponent, ref } from 'vue'; //ComponentOptions
  import FileBuildTable from './FileBuild.vue';
  import ContactBuild from './ContactBuild.vue';
  import { PageWrapper } from '/@/components/Page';
  //import { schemas, taskSchemas } from './build.data';
  import { taskSchemas } from './build.data';
  import { Card } from 'ant-design-vue';
  import { useModal } from '/@/components/Modal';
  import { useTabs } from '/@/hooks/web/useTabs';
  import UserSelectModel from '/@/views/devsys/contract/common/UserModel.vue';
  import { customerSaveApi } from '/@/api/devsys/contract/customer';
  import { useGo } from '/@/hooks/web/usePage';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useRoute } from 'vue-router';
  export default defineComponent({
    name: 'CustomerBuild',
    components: {
      BasicForm,
      ContactBuild,
      PageWrapper,
      UserSelectModel,
      FileBuildTable,
      [Card.name]: Card,
    },
    setup() {
      //const currentModal = shallowRef<Nullable<ComponentOptions>>(null);
      const tableRef = ref<{ getDataSource: () => any } | null>(null);
      const tableFileRef = ref<{ getDataSource: () => any } | null>(null);
      const [registerUserModel, { openModal: openModal1, closeModal }] = useModal();
      const go = useGo();
      const route = useRoute();
      const customerId = ref(route.params?.id);
      const schemas1: FormSchema[] = [
        {
          field: 'ID',
          component: 'Input',
          label: 'ID',
          show: false,
        },
        {
          field: 'NAME',
          component: 'Input',
          label: '名称',
          required: true,
        },
        {
          field: 'CODE',
          component: 'Input',
          label: '编号',
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
            params: { LbId: 6 },
          },
          label: '客户类别',
          required: true,
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'TRADE',
          component: 'Input',
          label: '行业',
        },
        {
          field: 'LEVEL_ID',
          component: 'ApiSelect',
          componentProps: {
            api: getdataListApi,
            labelField: 'NAME',
            valueField: 'ID',
            params: { LbId: 7 },
          },
          label: '客户级别',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'CREATE_ID',
          component: 'ApiSelect',
          componentProps: {
            api: getdataListApi,
            labelField: 'NAME',
            valueField: 'ID',
            params: { LbId: 8 },
          },
          label: '信用等级',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'ZIPCODE',
          component: 'Input',
          label: '邮编',
        },
        {
          field: 'DEPUTY',
          component: 'Input',
          label: '法人代表',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'EST_DATE',
          component: 'DatePicker',
          label: '成立日期',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'WEBSITE',
          component: 'Input',
          label: '公司网址',
          componentProps: {
            addonBefore: 'http://',
            addonAfter: 'com',
          },
        },
        {
          field: 'EXP_RANGE',
          component: 'Input',
          label: '经营范围',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'REG_CAP',
          component: 'Input',
          label: '注册资本',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'COMP_TYPE',
          component: 'Input',
          label: '公司类型',
        },
        {
          field: 'FIELD1',
          component: 'Input',
          label: '备用1',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'FIELD2',
          component: 'Input',
          label: '备用2',
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'ADDRESS',
          component: 'Input',
          label: '地址',
        },
        {
          field: 'LEAD_USERNAME',
          component: 'Input',
          label: '负责人',
          componentProps: {
            onclick: () => {
              //console.log(e, '点击了。。。。。');
              openModal1();
            },
          },
          colProps: {
            offset: 2,
          },
        },
        {
          field: 'LEAD_USERID',
          component: 'Input',
          label: 'LEAD_USERID',
          show: false,
          colProps: {
            offset: 2,
          },
        },
      ];
      const [register, { validate, setFieldsValue }] = useForm({
        layout: 'vertical',
        baseColProps: {
          span: 6,
        },
        schemas: schemas1, //schemas,
        showActionButtonGroup: false,
      });

      const [registerTask, { validate: validateTaskForm }] = useForm({
        layout: 'vertical',
        baseColProps: {
          span: 6,
        },
        schemas: taskSchemas,
        showActionButtonGroup: false,
      });
      const { createMessage: msg } = useMessage();
      const { setTitle, closeCurrent } = useTabs();
      if (customerId.value !== '' && customerId.value !== '0') {
        setTitle('客户[修改]');
      } else {
        setTitle('客户[新增]');
      }

      async function submitAll() {
        msg.loading({ content: '正在保存...', duration: 0, key: 'saving' });
        try {
          if (tableRef.value) {
            console.log('table data:', tableRef.value.getDataSource());
          }

          const [values, taskValues] = await Promise.all([validate(), validateTaskForm()]);
          console.log('form data:', values, taskValues);
          await customerSaveApi(values);
          msg.success({ content: '数据已保存', key: 'saving' });
          closeCurrent();
          goBack();
        } catch (error) {
          msg.error({ content: '保存失败,' + error, key: 'saving' });
        }
      }

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
      function goBack() {
        // 返回列表
        go('/company/customer');
      }

      return {
        register,
        registerTask,
        submitAll,
        tableRef,
        registerUserModel,
        openModal1,
        SelleadUser,
        tableFileRef,
      };
    },
  });
</script>
<style lang="less" scoped>
  .high-form {
    padding-bottom: 48px;
  }
</style>
