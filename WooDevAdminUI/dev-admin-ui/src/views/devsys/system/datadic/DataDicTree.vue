<template>
  <div class="m-4 mr-0 overflow-hidden bg-white">
    <BasicTree
      title="数据字典"
      toolbar
      :defaultExpandAll="true"
      :clickRowToExpand="false"
      :treeData="treeData"
      :fieldNames="{ key: 'id', title: 'title' }"
      @select="handleSelect"
    />
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref } from 'vue';

  import { BasicTree, TreeItem } from '/@/components/Tree';
  // import { getDeptList } from '/@/api/demo/system';
  import { datatreeData } from './datadic.data';
  import { devSystemStore } from '/@/store/modules/devsystem';

  export default defineComponent({
    name: 'DataDicTree',
    components: { BasicTree },

    emits: ['select'],
    setup(_, { emit }) {
      // const treeData = datatreeData;
      const treeData = ref<TreeItem[]>(datatreeData);
      const store = devSystemStore();
      // async function fetch() {
      //   treeData.value = (await getDeptList()) as unknown as TreeItem[];
      // }

      function handleSelect(keys) {
        store.lbId = keys[0];
        console.log('设置：' + store.lbId);
        emit('select', keys[0]);
      }

      // onMounted(() => {
      //   fetch();
      // });
      return { treeData, handleSelect };
    },
  });
</script>
