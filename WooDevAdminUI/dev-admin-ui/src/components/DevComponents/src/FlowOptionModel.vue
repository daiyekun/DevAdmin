<template>
  <BasicModal v-bind="$attrs" @register="registerModal" title="审批意见" :width="500" :height="100">
    <template #footer>
      <a-button key="2" @click="handleMsg(2)">不同意</a-button>
      <a-button key="1" type="primary" @click="handleMsg(1)">同意</a-button>
    </template>
    <a-textarea v-model:value="flowoption" placeholder="请输入审批意见" :rows="6" />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent, ref, createVNode } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { ExclamationCircleOutlined } from '@ant-design/icons-vue';
  import { Modal } from 'ant-design-vue';
  export default defineComponent({
    name: 'FlowOptionModel',
    components: { BasicModal },

    setup() {
      //const rowId = ref('');
      const flowoption = ref<string>('');
      const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
        setModalProps({ confirmLoading: false });
        console.log('===data', data);
      });

      function handleMsg(sta: number) {
        try {
          setModalProps({ confirmLoading: true });
          // closeModal();
          showConfirm(sta);
        } finally {
          setModalProps({ confirmLoading: false });
        }
      }

      const showConfirm = (sta: number) => {
        let tjmsg = sta == 1 ? '同意' : '不同意';
        Modal.confirm({
          title: () => '系统提示',
          icon: () => createVNode(ExclamationCircleOutlined),
          content: () => `您点击的是${tjmsg}，确认吗？`,
          onOk() {
            // return new Promise((resolve, reject) => {
            //   setTimeout(Math.random() > 0.5 ? resolve : reject, 1000);
            // }).catch(() => console.log('Oops errors!'));
            closeModal();
          },

          onCancel() {},
        });
      };

      return { registerModal, handleMsg, flowoption, showConfirm };
    },
  });
</script>
