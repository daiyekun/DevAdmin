<template>
  <PageWrapper
    :title="`用户` + userId + `的资料`"
    content="这是用户资料详情页面"
    contentBackground
    @back="goBack"
  >
    <template #extra>
      <a-button type="primary" danger @click="disableUser"> 禁用账号 </a-button>
      <!-- <a-button type="primary" @click="changePwd"> 修改密码 </a-button> -->
    </template>
    <template #footer>
      <a-tabs default-active-key="1">
        <a-tab-pane key="1" tab="详情" />
        <!-- <a-tab-pane key="2" tab="规则" /> -->
      </a-tabs>
    </template>
    <a-card title="用户信息" :bordered="false" class="mt-5">
      <a-descriptions :column="3">
        <a-descriptions-item label="登录名称">
          {{ data.userinfo.LOGIN_NAME }}
        </a-descriptions-item>
        <a-descriptions-item label="姓名">{{ data.userinfo.NAME }} </a-descriptions-item>
        <a-descriptions-item label="角色"> {{ data.userinfo.NAME }} </a-descriptions-item>
        <a-descriptions-item label="所属部门"> {{ data.userinfo.DepName }} </a-descriptions-item>
        <a-descriptions-item label="邮箱"> {{ data.userinfo.EMAIL }} </a-descriptions-item>
        <a-descriptions-item label="性别"> {{ data.userinfo.SexDic }} </a-descriptions-item>
        <a-descriptions-item label="电话"> {{ data.userinfo.TEL }} </a-descriptions-item>
        <a-descriptions-item label="身份证号"> {{ data.userinfo.ID_NO }} </a-descriptions-item>
        <a-descriptions-item label="微信号"> {{ data.userinfo.WX_CODE }} </a-descriptions-item>
        <a-descriptions-item label="状态"> {{ data.userinfo.StateDic }} </a-descriptions-item>

        <!-- <a-descriptions-item label="联系地址" :span="2">
          曲丽丽 18100000000 浙江省杭州市西湖区黄姑山路工专路交叉路口
        </a-descriptions-item> -->
      </a-descriptions>
    </a-card>
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, reactive } from 'vue';
  import { useRoute } from 'vue-router';
  import { PageWrapper } from '/@/components/Page';
  import { useGo } from '/@/hooks/web/usePage';
  import { useTabs } from '/@/hooks/web/useTabs';
  import { schemauserview } from './account.data';
  import { Divider, Card, Descriptions, Tabs } from 'ant-design-vue';
  import { userViewApi,userStatusApi } from '/@/api/devsys/system/devsystem';
  import { UserViewInfo } from '/@/api/devsys/model/systemModel';
  import { useMessage } from '/@/hooks/web/useMessage';

  export default defineComponent({
    name: 'DevAccountDetail',
    components: {
      PageWrapper,
      [Divider.name]: Divider,
      [Card.name]: Card,
      [Descriptions.name]: Descriptions,
      [Descriptions.Item.name]: Descriptions.Item,
      [Tabs.name]: Tabs,
      [Tabs.TabPane.name]: Tabs.TabPane,
    },
    setup() {
      const route = useRoute();
      const go = useGo();
      // 此处可以得到用户ID
      const userId = ref(route.params?.id);
      //const currentKey = ref('detail');
      const { setTitle } = useTabs();
      const { createMessage: msg } = useMessage();
      const data = reactive({
        userinfo:<UserViewInfo>{},
    });
      const reqdata= userViewApi(Number(userId.value));
      reqdata.then((values) => {
        //const tuser: ViewUserModel = values;
        data.userinfo = values;
      });

      // 设置Tab的标题（不会影响页面标题）
      setTitle('用户[详情]' + userId.value);

      // 页面左侧点击返回链接时的操作
      function goBack() {
        // 本例的效果时点击返回始终跳转到账号列表页，实际应用时可返回上一页
        go('/devsystem/account');
      }
      async function disableUser(){
        if(data.userinfo.StateDic==='禁用'){
          msg.error({ content: '数据已经禁用了,' + Error, key: 'saving' });
        }else{
          userStatusApi(Number(userId.value),0);
         data.userinfo.StateDic="禁用";
         msg.success({ content: '已禁用', key: 'saving' });

        }
         

      }
      // function changePwd(){
      //   go('/devsystem/devchangePassword/' + userId.value);
      // }

      return { schemauserview, userId, goBack, data,disableUser };
    },
  });
</script>

<style></style>
