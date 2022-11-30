<template>
  <CollapseContainer title="基本设置" :canExpan="false">
    <!-- <a-row :gutter="24">
      <a-col :span="14">
        <BasicForm @register="register" />
      </a-col>
      <a-col :span="10">
        <div class="change-avatar">
          <div class="mb-2">头像</div>
          <CropperAvatar
            :uploadApi="uploadApi"
            :value="avatar"
            btnText="更换头像"
            :btnProps="{ preIcon: 'ant-design:cloud-upload-outlined' }"
            @change="updateAvatar"
            width="150"
          />
        </div>
      </a-col>
    </a-row>
    <Button type="primary" @click="handleSubmit"> 更新基本信息 </Button> -->
    <div class="gutter-example">
      <a-row :gutter="16">
        <a-col class="gutter-row" :span="4">
          <Button type="primary" @click="handleSubmit">系统用户</Button>
        </a-col>
        <a-col class="gutter-row" :span="4">
          <Button type="primary" @click="handleSubmit">审批组</Button>
        </a-col>
        <a-col class="gutter-row" :span="4">
          <Button type="primary" @click="handleSubmit">系统角色</Button>
        </a-col>
        <a-col class="gutter-row" :span="4">
          <Button type="primary" @click="handleSubmit">数据字典</Button>
        </a-col>
      </a-row>
    </div>
  </CollapseContainer>
</template>
<script lang="ts">
  import { Button, Row, Col } from 'ant-design-vue';
  import { defineComponent } from 'vue';
  // import { BasicForm, useForm } from '/@/components/Form/index';
  import { CollapseContainer } from '/@/components/Container';
  // import { CropperAvatar } from '/@/components/Cropper';

  import { useMessage } from '/@/hooks/web/useMessage';

  // import { useUserStore } from '/@/store/modules/user';

  export default defineComponent({
    components: {
      //BasicForm,
      CollapseContainer,
      Button,
      ARow: Row,
      ACol: Col,
      // CropperAvatar,
    },
    setup() {
      const { createMessage } = useMessage();
      //const userStore = useUserStore();

      async function handleSubmit() {
        createMessage.success('更新成功！');
      }
      return { handleSubmit };
    },
  });
</script>

<style lang="less" scoped>
  .change-avatar {
    img {
      display: block;
      margin-bottom: 15px;
      border-radius: 50%;
    }
  }

  .gutter-example :deep(.ant-row > div) {
    background: transparent;
    border: 0;
  }

  .gutter-box {
    background: #00a0e9;
    padding: 5px 0;
  }
</style>
