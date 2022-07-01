<template>
  <PageWrapper>
    <a-card title="用户信息" :bordered="false">
      <BasicForm @register="register" />
    </a-card>
    <template #rightFooter>
      <a-button type="primary" @click="submitAll"> 提交 </a-button>
    </template>
  </PageWrapper>
</template>
<script lang="ts">
  import { BasicForm, useForm } from '/@/components/Form';
  import { defineComponent, ref } from 'vue';

  import { PageWrapper } from '/@/components/Page';
  import { accountFormSchema } from './account.data';
  import { Card } from 'ant-design-vue';
  import { useRoute } from 'vue-router';
  import { useTabs } from '/@/hooks/web/useTabs';
  import { useGo } from '/@/hooks/web/usePage';
  import { userSaveApi, userViewApi, isAccountExistApi } from '/@/api/devsys/system/devsystem';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'FormHightPage',
    components: { BasicForm, PageWrapper, [Card.name]: Card },
    setup() {
      //const router = useRouter();
      const route = useRoute();
      const go = useGo();
      // 此处可以得到用户ID
      const userId = ref(route.params?.id);
      const tableRef = ref<{ getDataSource: () => any } | null>(null);

      const [register, { validate, setFieldsValue }] = useForm({
        layout: 'vertical',
        baseColProps: {
          span: 6,
        },
        schemas: accountFormSchema,
        showActionButtonGroup: false,
      });
      if (Number(userId.value) > 0) {
        userViewApi(Number(userId.value)).then((value) => {
          setFieldsValue(value); //赋值
        });
      }
      const { createMessage: msg } = useMessage();
      const { setTitle, closeCurrent } = useTabs();
      // console.log(`userid -->${userId.value} ${typeof userId.value}`);
      if (userId.value !== '' && userId.value !== '0') {
        setTitle('用户[修改]' + userId.value);
      } else {
        setTitle('用户[新增]' + userId.value);
      }

      // function pushWithQuery() {
      //   router.push({
      //     name: 'DevAccountManagement',
      //     query: {
      //       ...route.query,
      //       //rd: new Date().getTime(),
      //     },
      //   });
      // }

      async function submitAll() {
        msg.loading({ content: '正在保存...', duration: 0, key: 'saving' });
        try {
          // if (tableRef.value) {
          //   console.log('table data:', tableRef.value.getDataSource());
          // }

          const [values] = await Promise.all([validate()]);
          console.log('form data:', values);
          await isAccountExistApi(values.LOGIN_NAME, Number(userId.value));
          await userSaveApi(values);
          msg.success({ content: '数据已保存', key: 'saving' });
          closeCurrent();
          goBack();
        } catch (Error) {
          //console.log(Error);
          msg.error({ content: '保存失败,' + Error, key: 'saving' });
        }
      }

      function goBack() {
        // 返回列表
        go('/devsystem/account');
      }

      return { userId, register, submitAll, tableRef, goBack };
    },
  });
</script>
<style lang="less" scoped>
  .high-form {
    padding-bottom: 48px;
  }
</style>
