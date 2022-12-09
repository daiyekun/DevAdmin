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
  import { defineComponent, ref, createVNode, reactive } from 'vue'; //PropType
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { ExclamationCircleOutlined } from '@ant-design/icons-vue';
  import {
    PersionApprovalInfo,
    ApprovalQx,
    FlowOptionDto,
  } from '/@/api/devsys/model/flow/flowInstModel';
  import { Modal } from 'ant-design-vue';
  import { submitOptionApi } from '/@/api/devsys/flow/flowinst';
  export default defineComponent({
    name: 'FlowOptionModel',
    components: { BasicModal },
    // props: {
    //   FlowObj: Object as PropType<PersionApprovalInfo>,
    // },
    emits: ['sumitMsg', 'register'],
    setup(_, { emit }) {
      //const rowId = ref('');
      const flowoption = ref<string>('');
      let appperssion = reactive<ApprovalQx>({ appqx: {} as PersionApprovalInfo });
      const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
        setModalProps({ confirmLoading: false });
        appperssion = await data.appperssion;
        // debugger;
        // console.log('===data', data);
        //console.log('===appperssion', appperssion);
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
            if (flowoption.value === '') {
              Modal.warning({
                title: () => '系统提示',
                content: () => '请填写意见.....',
              });
            } else {
              var optiondata: FlowOptionDto = {
                WaitId: appperssion.appqx.WaitId,
                InstId: appperssion.appqx.InstId,
                NodeId: appperssion.appqx.NodeId,
                Sta: sta,
                Msg: flowoption.value,
              };
              console.log('提交数据', optiondata);
              submitOptionApi(optiondata)
                .then(() => {
                  Modal.success({
                    title: () => '系统提示',
                    content: () => '提交成功',
                  });
                  closeModal();
                  emit('sumitMsg');
                })
                .catch((error) => {
                  Modal.error({
                    title: () => '系统异常',
                    content: () => '错误消息：' + error,
                  });
                });
            }
          },

          onCancel() {},
        });
      };

      return { registerModal, handleMsg, flowoption, showConfirm };
    },
  });
</script>
