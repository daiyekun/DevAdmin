<template>
  <div>
    <PageWrapper :title="'客户名称:' + customerdata.viewdata.NAME">
      <template #extra>
        <a-button @click="closeCurrent">关闭</a-button>
        <a-button type="primary" v-show="appperssion.appqx.InstId !== 0" @click="submitOption"
          >审批</a-button
        >
      </template>
      <template #footer>
        <div>
          <a-tabs default-active-key="1" @change="tabChange">
            <a-tab-pane key="1" tab="详情" />
            <a-tab-pane key="2" tab="审批记录" />
          </a-tabs>
        </div>
      </template>
      <div tabkey="1" v-show="tabdata.tab1" style="background: #f0f2f5">
        <div>
          <Description
            class="mt-2"
            title="基本信息"
            :collapseOptions="{ canExpand: true, helpMessage: '客户基本信息' }"
            :column="3"
            :data="customerdata.viewdata"
            :schema="schema"
          />
        </div>
        <CollapseContainer title="客户附件" style="margin-top: 16px">
          <FileBuildTable ref="tableFileRef" :custid="coustomerid" />
        </CollapseContainer>
        <CollapseContainer title="客户联系人" style="margin-top: 16px">
          <ContactBuild ref="tableContactRef" :custid="coustomerid" />
        </CollapseContainer>
      </div>
      <div tabkey="2" v-show="tabdata.tab2">
        <FlowInstHistory :custid="coustomerid" :objtype="0" />
      </div>
    </PageWrapper>
    <FlowOptionModel @register="flowOptionregister" />
  </div>
</template>
<script lang="ts">
  import { defineComponent, reactive, ref, onMounted } from 'vue';
  //import { BasicTable } from '/@/components/Table';
  import { useRoute } from 'vue-router';
  import { PageWrapper } from '/@/components/Page';
  import { Divider, Card, Descriptions, Steps, Tabs } from 'ant-design-vue';
  import { Description } from '/@/components/Description/index';
  import { CustomerViewApi } from '/@/api/devsys/contract/customer';
  import { CustomerViewInfo } from '/@/api/devsys/model/customerModel';
  import { CollapseContainer } from '/@/components/Container';
  import { schema } from './detail.data';
  import FileBuildTable from './FileBuild.vue';
  import ContactBuild from './ContactBuild.vue';
  import FlowInstHistory from '/@/components/DevComponents/src/FlowInstHistory.vue';
  import { useTabs } from '/@/hooks/web/useTabs';
  import { useModal } from '/@/components/Modal';
  import FlowOptionModel from '/@/components/DevComponents/src/FlowOptionModel.vue';
  import { PersionApprovalInfo, ApprovalQx } from '/@/api/devsys/model/flow/flowInstModel';
  import { IsAppExistInfoApi } from '/@/api/devsys/flow/flowinst';
  export default defineComponent({
    components: {
      // BasicTable,
      PageWrapper,
      [Divider.name]: Divider,
      [Card.name]: Card,
      // Empty,
      [Descriptions.name]: Descriptions,
      [Descriptions.Item.name]: Descriptions.Item,
      [Steps.name]: Steps,
      [Steps.Step.name]: Steps.Step,
      [Tabs.name]: Tabs,
      [Tabs.TabPane.name]: Tabs.TabPane,
      Description,
      FileBuildTable,
      ContactBuild,
      CollapseContainer,
      FlowInstHistory,
      FlowOptionModel,
    },
    setup() {
      const { closeCurrent } = useTabs();
      const tableFileRef = ref<{ getDataSource: () => any } | null>(null);
      const tableContactRef = ref<{ getDataSource: () => any } | null>(null);
      const tabdata = reactive({ tab1: true, tab2: false });
      const route = useRoute();
      const custId = ref(route.params?.id);
      const [flowOptionregister, { openModal: flowOptinOpenModal }] = useModal();
      interface customerdata {
        viewdata: CustomerViewInfo;
      }
      const customerdata = reactive<customerdata>({ viewdata: {} as CustomerViewInfo });
      const coustomerid = Number(custId.value);
      const appperssion = reactive<ApprovalQx>({ appqx: {} as PersionApprovalInfo });

      onMounted(async () => {
        //console.log('执行 onMounted', customerdata);
        customerdata.viewdata = await CustomerViewApi(Number(custId.value));
        //审批权限
        appperssion.appqx = await IsAppExistInfoApi({
          AppObjId: coustomerid,
          AppObjType: 0,
          UserId: 0,
        });
        console.log('审批权限 appqx', appperssion);
      });
      /***
       * 标签切换
       */
      function tabChange(activeKey) {
        if (activeKey == 1) {
          tabdata.tab1 = true;
          tabdata.tab2 = false;
        } else if (activeKey == 2) {
          tabdata.tab1 = false;
          tabdata.tab2 = true;
        }
      }
      /***
       * 审批意见
       ***/
      function submitOption() {
        flowOptinOpenModal(true, { appperssion });
      }

      return {
        //registerTimeTable,
        customerdata,
        tableFileRef,
        tableContactRef,
        coustomerid,
        onMounted,
        tabChange,
        submitOption,
        tabdata,
        schema,
        closeCurrent,
        flowOptionregister,
        flowOptinOpenModal,
        appperssion,
      };
    },
  });
</script>
