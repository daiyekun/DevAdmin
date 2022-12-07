<template>
  <PageWrapper :title="'客户名称:' + customerdata.viewdata.NAME">
    <template #extra>
      <a-button>关闭</a-button>
      <a-button type="primary">审批</a-button>
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
    <div tabkey="2" v-show="tabdata.tab2"> 审批记录显示</div>
  </PageWrapper>
</template>
<script lang="ts">
  import { defineComponent, reactive, ref, onMounted } from 'vue';
  //import { BasicTable } from '/@/components/Table';
  import { useRoute } from 'vue-router';
  import { PageWrapper } from '/@/components/Page';
  import { Divider, Card, Descriptions, Steps, Tabs } from 'ant-design-vue';
  import { Description, DescItem } from '/@/components/Description/index';
  import { CustomerViewApi } from '/@/api/devsys/contract/customer';
  import { CustomerViewInfo } from '/@/api/devsys/model/customerModel';
  import { CollapseContainer } from '/@/components/Container';
  import FileBuildTable from './FileBuild.vue';
  import ContactBuild from './ContactBuild.vue';
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
    },
    setup() {
      const tableFileRef = ref<{ getDataSource: () => any } | null>(null);
      const tableContactRef = ref<{ getDataSource: () => any } | null>(null);
      const tabdata = reactive({ tab1: true, tab2: false });
      const route = useRoute();
      const custId = ref(route.params?.id);
      //let tdata: CustomerViewInfo = {};
      const customerdata = reactive({ viewdata: {} });
      const coustomerid = Number(custId.value);

      const schema: DescItem[] = [
        {
          field: 'NAME',
          label: '用户名',
        },
        {
          field: 'CODE',
          label: '编号',
          render: (curVal, data) => {
            return `${data.NAME}-${curVal}`;
          },
        },
        {
          field: 'TRADE',
          label: '行业',
        },
        {
          field: 'CateName',
          label: '类别',
        },
        {
          field: 'LevelName',
          label: '客户级别',
        },
        {
          field: 'CrateName',
          label: '信用等级',
        },
        {
          field: 'ZIPCODE',
          label: '邮编',
        },
        {
          field: 'DEPUTY',
          label: '法人代表',
        },
        {
          field: 'EST_DATE',
          label: '成立日期',
        },
        {
          field: 'WEBSITE',
          label: '公司网址',
        },
        {
          field: 'LevelName',
          label: '客户级别',
        },
        {
          field: 'EXP_RANGE',
          label: '经营范围',
        },
        {
          field: 'REG_CAP',
          label: '注册资本',
        },
        {
          field: 'COMP_TYPE',
          label: '公司类型',
        },
        {
          field: 'FIELD1',
          label: '备用1',
        },
        {
          field: 'FIELD2',
          label: '备用2',
        },
        {
          field: '地址',
          label: 'ADDRESS',
        },
      ];
      onMounted(async () => {
        console.log('执行 onMounted', customerdata);
        customerdata.viewdata = await CustomerViewApi(Number(custId.value));
        console.log('详情数据 customerdata', customerdata);
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
      function tabClick() {}

      return {
        //registerTimeTable,
        customerdata,
        tableFileRef,
        tableContactRef,
        coustomerid,
        onMounted,
        schema,
        tabChange,
        tabClick,
        tabdata,
      };
    },
  });
</script>
