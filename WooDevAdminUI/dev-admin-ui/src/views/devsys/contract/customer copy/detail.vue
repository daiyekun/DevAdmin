<template>
  <PageWrapper :title="'编号:'+customerdata.viewdata.CODE" contentBackground>
    <template #extra>
      <!-- <a-button> 操作一 </a-button>
      <a-button> 操作二 </a-button>
      <a-button type="primary"> 主操作 </a-button> -->
    </template>

    <template #footer>
      <a-tabs default-active-key="1">
        <a-tab-pane key="1" tab="详情" />
        <!-- <a-tab-pane key="2" tab="操作日志" /> -->
      </a-tabs>
    </template>

    <div class="pt-4 m-4 desc-wrap">
      <a-descriptions size="small" :column="3">
        <a-descriptions-item label="名称"> {{customerdata.viewdata.NAME}} </a-descriptions-item>
        <a-descriptions-item label="编号"> {{customerdata.viewdata.CODE}}  </a-descriptions-item>
        <a-descriptions-item label="行业"> {{customerdata.viewdata.TRADE}}  </a-descriptions-item>
        <a-descriptions-item label="类别"> {{customerdata.viewdata.CateName}}  </a-descriptions-item>
        <a-descriptions-item label="客户级别"> {{customerdata.viewdata.LevelName}}  </a-descriptions-item>
        <a-descriptions-item label="信用等级"> {{customerdata.viewdata.CrateName}} </a-descriptions-item>
        <a-descriptions-item label="邮编"> {{customerdata.viewdata.ZIPCODE}} </a-descriptions-item>
        <a-descriptions-item label="法人代表"> {{customerdata.viewdata.DEPUTY}}  </a-descriptions-item>
        <a-descriptions-item label="成立日期"> {{customerdata.viewdata.EST_DATE}}  </a-descriptions-item>
        <a-descriptions-item label="公司网址"> {{customerdata.viewdata.WEBSITE}} </a-descriptions-item>
        <a-descriptions-item label="经营范围"> {{customerdata.viewdata.EXP_RANGE}}  </a-descriptions-item>
        <a-descriptions-item label="注册资本"> {{customerdata.viewdata.REG_CAP}} </a-descriptions-item>
        <a-descriptions-item label="公司类型"> {{customerdata.viewdata.COMP_TYPE}}  </a-descriptions-item>
        <a-descriptions-item label="备用1"> {{customerdata.viewdata.FIELD1}}  </a-descriptions-item>
        <a-descriptions-item label="备用2"> {{customerdata.viewdata.FIELD2}}  </a-descriptions-item>
        <a-descriptions-item label="地址"> {{customerdata.viewdata.ADDRESS}} </a-descriptions-item>
      </a-descriptions>
      <a-card title="客户附件" class="my-5">
        <FileBuildTable ref="tableFileRef" :custid="coustomerid" />
      </a-card>
      <a-card title="客户联系人" class="my-5 !mt-5">
      <ContactBuild ref="tableContactRef" :custid="coustomerid" />
      </a-card>
    </div>
  </PageWrapper>
</template>
<script lang="ts">
  import { defineComponent,reactive,ref } from 'vue';
  import { BasicTable } from '/@/components/Table';
  import { useRoute } from 'vue-router';
  import { PageWrapper } from '/@/components/Page';
  import { Divider, Card, Empty, Descriptions, Steps, Tabs } from 'ant-design-vue';
  import { CustomerViewApi } from '/@/api/devsys/contract/customer';
  import { CustomerViewInfo } from '/@/api/devsys/model/customerModel';
  import FileBuildTable from './FileBuild.vue';
  import ContactBuild from './ContactBuild.vue';
  export default defineComponent({
    components: {
      BasicTable,
      PageWrapper,
      [Divider.name]: Divider,
      [Card.name]: Card,
      Empty,
      [Descriptions.name]: Descriptions,
      [Descriptions.Item.name]: Descriptions.Item,
      [Steps.name]: Steps,
      [Steps.Step.name]: Steps.Step,
      [Tabs.name]: Tabs,
      [Tabs.TabPane.name]: Tabs.TabPane,
      ContactBuild,
      FileBuildTable

    },
    setup() {
      const tableFileRef = ref<{ getDataSource: () => any } | null>(null);
       const tableContactRef = ref<{ getDataSource: () => any } | null>(null);
      
      const route = useRoute();
      const custId = ref(route.params?.id);
       const customerdata = reactive({
        viewdata:<CustomerViewInfo>{}
      });
      const coustomerid=Number(custId.value);
      const reqdata= CustomerViewApi(Number(custId.value));
      reqdata.then((values) => {
        customerdata.viewdata = values;
      });

      return {
        //registerTimeTable,
        customerdata,
        tableFileRef,
        tableContactRef,
        coustomerid
      };
    },
  });
</script>
