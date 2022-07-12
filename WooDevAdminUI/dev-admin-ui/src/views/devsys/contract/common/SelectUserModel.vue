<template>
  <div>
    <a-button type="primary" @click="showModal">Open Modal with customized footer</a-button>
    <a-modal v-model:visible="visible" title="Title" @ok="handleOk">
      <template #footer>
        <a-button key="back" @click="handleCancel">Return</a-button>
        <a-button key="submit" type="primary" :loading="loading" @click="handleOk">Submit</a-button>
      </template>
      <p>Some contents...</p>
      <p>Some contents...</p>
      <p>Some contents...</p>
      <p>Some contents...</p>
      <p>Some contents...</p>
    </a-modal>
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';
  export default defineComponent({
    setup() {
      const loading = ref<boolean>(false);
      const visible = ref<boolean>(false);

      const showModal = () => {
        visible.value = true;
      };

      const handleOk = () => {
        loading.value = true;
        setTimeout(() => {
          loading.value = false;
          visible.value = false;
        }, 2000);
      };

      const handleCancel = () => {
        visible.value = false;
      };
      return {
        loading,
        visible,
        showModal,
        handleOk,
        handleCancel,
      };
    },
  });
</script>
