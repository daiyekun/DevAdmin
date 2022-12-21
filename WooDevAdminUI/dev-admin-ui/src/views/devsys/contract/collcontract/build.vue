<template>
  <PageWrapper>
    <!-- class="high-form" title="高级表单" content=" " -->
    <a-card title="合同信息" :bordered="false">
      <BasicForm @register="register" />
    </a-card>
    <a-card title="合同文本" :bordered="false" class="!mt-5">
      <ContTextBuildTable ref="tableTextRef" :custid="coustomerid" />
    </a-card>
    <a-card title="计划资金" :bordered="false" class="!mt-5">
      <PlanFinceBuildTable ref="tablePlanFinceRef" :custid="coustomerid" />
    </a-card>
    <a-card title="合同附件" :bordered="false" class="!mt-5">
      <FileBuildTable ref="tableFileRef" :custid="coustomerid" />
    </a-card>
    <a-card title="合同标的" :bordered="false" class="!mt-5">
      <ContactBuild ref="tableRef" :custid="coustomerid" />
    </a-card>
    <UserSelectModel @register="registerUserModel" @rowUserDbclick="SelleadUser" @selectRowUser="selectRowUser" />
    <CustomerSelectModel @register="registerCustomerModel" @rowCustomerDbclick="DbClickSelCusteomer" @selectRowCustomer="selRowCustomer" />
    <MainDeptModel @register="registerMainDeptModel" @rowMainDeptDbclick="rowMainDeptDbclick" @selectRowMainDept="selectRowMainDept" />
    <!-- <a-button type="primary" class="my-4" @click="openModal1"> 打开弹窗 </a-button> -->
    <template #rightFooter>
      <a-button type="primary" @click="submitAll"> 提交 </a-button>
    </template>
  </PageWrapper>
</template>
<script lang="ts">
   import { getdataListApi } from '/@/api/devsys/system/datadic';
   import { getDepartList } from '/@/api/devsys/system/devsystem';
  //import { FormSchema } from '/@/components/Form';
  import { BasicForm, useForm,FormSchema } from '/@/components/Form';
  import { defineComponent, ref,reactive } from 'vue'; //ComponentOptions
  import FileBuildTable from './FileBuild.vue';
  import ContTextBuildTable from './ContTextBuild.vue';
  import ContactBuild from './ContactBuild.vue';
  import PlanFinceBuildTable from './PlanFinceBuild.vue';
  import { PageWrapper } from '/@/components/Page';
  //import { schemas } from './build.data';
  //import { taskSchemas } from './build.data';
  import { Card } from 'ant-design-vue';
  import { useModal } from '/@/components/Modal';
  import { useTabs } from '/@/hooks/web/useTabs';
  import CustomerSelectModel from '/@/components/DevComponents/src/CustomerModel.vue';
  import UserSelectModel from '/@/components/DevComponents/src/UserModel.vue';
  import MainDeptModel from '/@/components/DevComponents/src/MainDeptModel.vue';
  import { customerSaveApi } from '/@/api/devsys/contract/customer';
  import { useGo } from '/@/hooks/web/usePage';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useRoute } from 'vue-router';
  import { CustomerViewInfo } from '/@/api/devsys/model/customerModel';
  import { CustomerViewApi,customerClearDataApi } from '/@/api/devsys/contract/customer';
  const storeFrameworkOptions: LabelValueOptions = [
  {
    label: '标准合同',
    value: '0',
  },
  {
    label: '框架合同',
    value: '1',
  },
];
 
  export default defineComponent({
    name: 'CustomerBuild',
    components: {
      BasicForm,
      ContactBuild,
      PageWrapper,
      UserSelectModel,
      FileBuildTable,
      [Card.name]: Card,
      CustomerSelectModel,
      MainDeptModel,
      ContTextBuildTable,
      PlanFinceBuildTable
    },
    setup() {
      //const currentModal = shallowRef<Nullable<ComponentOptions>>(null);
      const tableRef = ref<{ getDataSource: () => any } | null>(null);
      const tableFileRef = ref<{ getDataSource: () => any } | null>(null);
      const tableTextRef = ref<{ getDataSource: () => any } | null>(null);
      const tablePlanFinceRef = ref<{ getDataSource: () => any } | null>(null);
      const [registerCustomerModel, { openModal: openCustModel, closeModal:closeCustModel }] = useModal();
      const [registerUserModel, { openModal: openModal1, closeModal:closeUserModel }] = useModal();
      const [registerMainDeptModel, { openModal: openMainDeptModal, closeModal:closeMainDept }] = useModal();
      
      const go = useGo();
      const route = useRoute();
      const schemas: FormSchema[] = [
  {
    field: 'C_NAME',
    component: 'Input',
    label: '名称',
    required: true,
  },
  {
    field: 'C_CODE',
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
      valueField: 'StrId',
      params: { LbId: 2 },
    },
    label: '合同类别',
    required: true,
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'ANT_MONERY',
    component: 'InputNumber',
    required: true,
    label: '金额',
    componentProps: {
      style: { width: '100%' },
    },
  },
  {
    field: 'SOURCE_ID',
    component: 'ApiSelect',
    componentProps: {
      api: getdataListApi,
      labelField: 'NAME',
      valueField: 'StrId',
      params: { LbId: 9 },
    },
    label: '合同来源',
    colProps: { offset: 2 },
  },
  {
    field: 'DEPART_ID',
    label: '经办机构',
    component: 'ApiTreeSelect',
    colProps: { offset: 2 },
    componentProps: {
      api: getDepartList,
      fieldNames: {
        label: 'NAME',
        key: 'ID',
        value: 'ID',
      },
      getPopupContainer: () => document.body,
    },
    required: true,
  },
  {
    field: 'COMP_NAME',
    component: 'Input',
    label: '合同对方',
    required: true,
    componentProps: {
      onclick: () => {
        //console.log(e, '点击了。。。。。');
        openCustModel();
      },
    }
  },
  {
    field: 'MAIN_DEPART_NAME',
    component: 'Input',
    required: true,
    label: '签约主体',
    componentProps: {
      onclick: () => {
        //console.log(e, '点击了。。。。。');
        openMainDeptModal();
      },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'OTHER_CODE',
    component: 'Input',
    label: '对方编号',
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'SIGNING_DATE',
    component: 'DatePicker',
    label: '签订日期',
    componentProps: {
      style: { width: '100%' },
    },
  },
  {
    field: 'EFFECTIVE_DATE',
    component: 'DatePicker',
    label: '生效日期',
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'PLAN_DATE',
    component: 'DatePicker',
    label: '计划完成日期',
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'IS_SUBCONT',
    component: 'RadioGroup',
    label: '总包合同',

    // itemProps: {
    //   extra: '客户、邀评人默认被分享',
    // },
    defaultValue: '0',
    componentProps: {
      options: [
        {
          label: '是',
          value: '1',
        },
        {
          label: '否',
          value: '0',
        },
      ],
    },
  },
  {
    field: 'HEAD_USER_NAME',
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
    field: 'CONT_SINGNO',
    component: 'Input',
    label: '签订人身份证号',
    colProps: {
      offset: 2,
    },
  },

  {
    field: 'IS_FRAMEWORK',
    component: 'Select',
    label: '合同属性',
    componentProps: {
      options: storeFrameworkOptions,
    },
    required: true,
  },
  {
    field: 'EST_MONERY',
    component: 'InputNumber',
    // required: true,
    label: '预估金额',
    show: ({ model }) => {
      return model.IS_FRAMEWORK === '1';
    },
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
  {
    field: 'ADVANCE_MONERY',
    component: 'InputNumber',
    // required: true,
    label: '预收金额',
    show: ({ model }) => {
      return model.IS_FRAMEWORK === '1';
    },
    componentProps: {
      style: { width: '100%' },
    },
    colProps: {
      offset: 2,
    },
  },
  {
          field: 'HEAD_USER_ID',
          component: 'Input',
          label: '负责人ID',
          show: false,
          colProps: {
            offset: 2,
          },
  },
  {
          field: 'COMP_ID',
          component: 'Input',
          label: '合同对方ID',
          show: false,
          colProps: {
            offset: 2,
          },
  },
  {
          field: 'MAIN_DEPART_ID',
          component: 'Input',
          label: '签约主体ID',
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
        schemas,
        showActionButtonGroup: false,
      });
      //修改时给表单赋值
      const customerId = ref(route.params?.id);
       const customerdata = reactive({
        viewdata:<CustomerViewInfo>{}
      });
      const coustomerid=Number(customerId.value);
      if(coustomerid>0){
      const reqdata= CustomerViewApi(Number(customerId.value));
      reqdata.then((values) => {
        customerdata.viewdata = values;
        setFieldsValue(values);
      });
      
      }else{
        customerClearDataApi();
      }

      // const [registerTask, { validate: validateTaskForm }] = useForm({
      //   layout: 'vertical',
      //   baseColProps: {
      //     span: 6,
      //   },
      //   schemas: taskSchemas,
      //   showActionButtonGroup: false,
      // });
      const { createMessage: msg } = useMessage();
      const { setTitle, closeCurrent } = useTabs();
      if (customerId.value !== '' && customerId.value !== '0') {
        setTitle('收款合同[修改]');
      } else {
        setTitle('收款合同[新增]');
      }

      async function submitAll() {
        msg.loading({ content: '正在保存...', duration: 0, key: 'saving' });
        try {
          if (tableRef.value) {
            console.log('table data:', tableRef.value.getDataSource());
          }

         // const [values, taskValues] = await Promise.all([validate(), validateTaskForm()]);
         const [values] = await Promise.all([validate()]);
          console.log('form data:', values);
          await customerSaveApi(values);
          msg.success({ content: '数据已保存', key: 'saving' });
          closeCurrent();
          goBack();
        } catch (error) {
          msg.error({ content: '保存失败,' + error, key: 'saving' });
        }
      }
      /***
       * 双击选择负责人
       */
      function SelleadUser(rd: any) {
        setFieldsValue({
          HEAD_USER_ID: rd.ID,
          HEAD_USER_NAME: rd.NAME,
        });
        closeUserModel();
      }
      /**
       * 选择用户弹出框 确认按钮
       */
       function selectRowUser(rds: any) {
        if (rds.length!=1) {
          msg.warn({
            content: '请选择一条数据',
            key: 'tmsg',
          });
        } else {
          //添加到后台
          setFieldsValue({
            HEAD_USER_ID: rds[0].ID,
            HEAD_USER_NAME: rds[0].NAME,
        });

        closeUserModel();
        }
      }
        /***
       * 双击选择客户
       */
      function DbClickSelCusteomer(rd: any) {
        setFieldsValue({
          COMP_ID: rd.ID,
          COMP_NAME: rd.NAME,
        });
        closeCustModel();
      }

      /**
       * 选择客户弹出框 确认按钮
       */
       function selRowCustomer(rds: any) {
        if (rds.length!=1) {
          msg.warn({
            content: '请选择一条数据',
            key: 'tmsg',
          });
        } else {
          //添加到后台
          setFieldsValue({
            COMP_ID: rds[0].ID,
            COMP_NAME: rds[0].NAME,
        });

        closeCustModel();
        }
      }

       /***
       * 双击选择签约主体
       */
       function rowMainDeptDbclick(rd: any) {
        setFieldsValue({
          MAIN_DEPART_ID: rd.ID,
          MAIN_DEPART_NAME: rd.NAME,
        });
        closeMainDept();
      }

      /**
       * 选择签约主体弹出框 确认按钮
       */
       function selectRowMainDept(rds: any) {
        if (rds.length!=1) {
          msg.warn({
            content: '请选择一条数据',
            key: 'tmsg',
          });
        } else {
          //添加到后台
          setFieldsValue({
            MAIN_DEPART_ID: rds[0].ID,
            MAIN_DEPART_NAME: rds[0].NAME,
        });

        closeMainDept();
        }
      }
      

      function goBack() {
        // 返回列表
        go('/company/customer');
      }

      return {
        register,
       // registerTask,
        submitAll,
        tableRef,
        registerUserModel,
        openModal1,
        SelleadUser,
        tableFileRef,
        customerdata,
        coustomerid,
        selectRowUser,
        registerCustomerModel,
        DbClickSelCusteomer,
        selRowCustomer,
        openCustModel,
        registerMainDeptModel,
        rowMainDeptDbclick,
        selectRowMainDept,
        tableTextRef
      };
    },
  });
</script>
<style lang="less" scoped>
  .high-form {
    padding-bottom: 48px;
  }
</style>
