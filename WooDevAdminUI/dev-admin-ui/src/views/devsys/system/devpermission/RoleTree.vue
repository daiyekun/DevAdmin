<template>
  <div class="m-4 mr-0 overflow-hidden bg-white">
    <BasicTree
      title="角色列表"
      toolbar
      search
      :clickRowToExpand="false"
      :treeData="treeData"
      :fieldNames="{ title: 'NAME', key: 'ID' }"
      @select="handleSelect"
    />
  </div>
</template>
<script lang="ts">
  import { defineComponent, onMounted, ref } from 'vue';

  import { BasicTree, TreeItem } from '/@/components/Tree';
  import { getAllRoleList } from '/@/api/devsys/system/devsystem';

  export default defineComponent({
    name: 'DeptTree',
    components: { BasicTree },

    emits: ['select'],
    setup(_, { emit }) {
      const treeData = ref<TreeItem[]>([]);

      async function fetch() {
        treeData.value = (await getAllRoleList()) as unknown as TreeItem[];
      }

      function handleSelect(keys, node) {
        var title = node.node.NAME.el.innerText;
        //console.log('keID', keys[0], title);
        emit('select', keys[0], title);
      }

      onMounted(() => {
        fetch();
      });
      return { treeData, handleSelect };
    },
  });
</script>
