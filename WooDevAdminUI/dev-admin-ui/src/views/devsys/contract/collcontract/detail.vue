<template>
  <div>
    <PageWrapper :title="'合同名称:' + customerdata.viewdata.C_NAME">
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
            :collapseOptions="{ canExpand: true, helpMessage: '合同基本信息' }"
            :column="3"
            :data="customerdata.viewdata"
            :schema="schema"
          />
        </div>
        <CollapseContainer title="合同文本" style="margin-top: 16px">
          <ContTextDetailTable ref="tableTextRef" :custid="coustomerid" />
        </CollapseContainer>
        <CollapseContainer title="计划资金" style="margin-top: 16px">
          <PlanFinceDetailTable ref="tablePlanFinceRef" :custid="coustomerid" />
        </CollapseContainer>
        <CollapseContainer title="合同附件" style="margin-top: 16px">
          <FileDetailTable ref="tableFileRef" :custid="coustomerid" />
        </CollapseContainer>
        <CollapseContainer title="合同标的" style="margin-top: 16px">
          <SubMatterDetailTable ref="tableSubMatterRef" :custid="coustomerid" />
        </CollapseContainer>
      </div>
      <div tabkey="2" v-show="tabdata.tab2">
        <FlowInstHistory :custid="coustomerid" :objtype="3" />
      </div>
    </PageWrapper>
    <FlowOptionModel @register="flowOptionregister" @sumitMsg="flowSucc" />
  </div>
</template>
<script lang="ts">
  import { defineComponent, reactive, ref, onMounted } from 'vue';
  //import { BasicTable } from '/@/components/Table';
  import { useRoute } from 'vue-router';
  import { PageWrapper } from '/@/components/Page';
  import { Divider, Card, Descriptions, Steps, Tabs } from 'ant-design-vue';
  import { Description } from '/@/components/Description/index';
  import { constractViewApi } from '/@/api/devsys/contract/collcontract';
  import { contractViewInfo } from '/@/api/devsys/model/devcontractModel';
  import { CollapseContainer } from '/@/components/Container';
  import { schema } from './detail.data';
  import FileDetailTable from './FileDetail.vue';
  import ContTextDetailTable from './ContTextDetail.vue';
  import SubMatterDetailTable from './SubMatterDetail.vue';
  import PlanFinceDetailTable from './PlanFinceDetail.vue';
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
      FileDetailTable,
      ContTextDetailTable,
      CollapseContainer,
      FlowInstHistory,
      FlowOptionModel,
      SubMatterDetailTable,
      PlanFinceDetailTable,
    },
    setup() {
      const { closeCurrent } = useTabs();
      const tableSubMatterRef = ref<{ getDataSource: () => any } | null>(null);
      const tableFileRef = ref<{ getDataSource: () => any } | null>(null);
      const tableTextRef = ref<{ getDataSource: () => any } | null>(null);
      const tablePlanFinceRef = ref<{ getDataSource: () => any } | null>(null);
      const tabdata = reactive({ tab1: true, tab2: false });
      const route = useRoute();
      const custId = ref(route.params?.id);
      const [flowOptionregister, { openModal: flowOptinOpenModal }] = useModal();
      interface customerdata {
        viewdata: contractViewInfo;
      }
      const customerdata = reactive<customerdata>({ viewdata: {} as contractViewInfo });
      const coustomerid = Number(custId.value);
      const appperssion = reactive<ApprovalQx>({ appqx: {} as PersionApprovalInfo });

      onMounted(async () => {
        //console.log('执行 onMounted', customerdata);
        customerdata.viewdata = await constractViewApi(Number(custId.value));
        //审批权限
        appperssion.appqx = await IsAppExistInfoApi({
          AppObjId: coustomerid,
          AppObjType: 3,
          UserId: 0,
        });
        // console.log('审批权限 appqx', appperssion);
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
      /***
       * 意见提交成功
       */
      function flowSucc() {
        closeCurrent();
      }

      return {
        //registerTimeTable,
        customerdata,
        tableFileRef,
        tableSubMatterRef,
        tableTextRef,
        tablePlanFinceRef,
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
        flowSucc,
      };
    },
  });
</script>
