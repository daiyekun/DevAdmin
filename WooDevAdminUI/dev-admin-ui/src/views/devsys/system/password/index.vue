<template>
  <PageWrapper title="修改当前用户密码" content="修改成功后会自动退出当前登录！">
    <div class="py-8 bg-white flex flex-col justify-center items-center">
      <BasicForm @register="register" />
      <div class="flex justify-center">
        <a-button @click="resetFields"> 重置 </a-button>
        <a-button class="!ml-4" type="primary" @click="handleSubmit"> 确认 </a-button>
      </div>
    </div>
  </PageWrapper>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { PageWrapper } from '/@/components/Page';
  import { BasicForm, useForm } from '/@/components/Form';
  import { useUserStore } from '/@/store/modules/user';
  import { userUpdatePwdApi } from '/@/api/devsys/system/devsystem';
  import { formSchema } from './pwd.data';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { PageEnum } from '/@/enums/pageEnum';
  import { useRouter } from 'vue-router';
  export default defineComponent({
    name: 'ChangePassword',
    components: { BasicForm, PageWrapper },
    setup() {
      const [register, { validate, resetFields }] = useForm({
        size: 'large',
        labelWidth: 100,
        showActionButtonGroup: false,
        schemas: formSchema,
      });
      const { createMessage } = useMessage();
      const userStore = useUserStore();
      const userinfo = userStore.getUserInfo;
      const router = useRouter();
      async function handleSubmit() {
        try {
          const values = await validate();
          const { passwordOld, passwordNew } = values;

          // TODO custom api
          console.log(passwordOld, passwordNew);
          await userUpdatePwdApi(Number(userinfo.userId), passwordNew, passwordOld);
          createMessage.success('修改成功');
          console.log(JSON.stringify(router));
          setTimeout(function () {
            router.push(PageEnum.BASE_LOGIN);
          }, 2000);
        } catch (error) {
          createMessage.error('修改失败,' + error);
        }
      }

      return { register, resetFields, handleSubmit };
    },
  });
</script>
