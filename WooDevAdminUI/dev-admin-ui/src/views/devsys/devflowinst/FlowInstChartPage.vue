<template>
  <div>
    <PageWrapper title="查看审批实例流出图" contentBackground contentFullHeight="true">
      <template #extra>
        <a-button @click="closeCurrent"> 关闭 </a-button>
        <!-- <a-button type="primary" @click="submitFlow"> 提交流程 </a-button>  -->
      </template>
      <template #default>
        <div class="container" ref="container" style="height: 1000px"> </div>
        <InstNodeDrawer @register="registerNodeInfo" />
      </template>
    </PageWrapper>
    <!-- <BasicModal @register="register" title="流程数据" width="50%">
      <JsonPreview :data="graphData" />
    </BasicModal> -->
  </div>
</template>

<script lang="ts">
  import { defineComponent, ref, onMounted } from 'vue';
  //import { Tabs, TabPane } from 'ant-design-vue';
  import { LogicFlow, RectNode, RectNodeModel } from '@logicflow/core';
  //import { JsonPreview } from '/@/components/CodeEditor';
  import { flowInstChartViewApi } from '/@/api/devsys/flow/flowinst';
  // import { createFlowInstApi } from '/@/api/devsys/flow/flowinst';
  // import { FlowSubmitModel } from '/@/api/devsys/model/flow/flowInstModel';
  import {
    DndPanel,
    SelectionSelect,
    Control,
    MiniMap,
    Menu,
    BpmnElement,
  } from '@logicflow/extension';
  import '@logicflow/core/dist/style/index.css';
  import '@logicflow/extension/lib/style/index.css';
  import '/@/views/devsys/devflowtemp/css/devflowcustom.css';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { PageWrapper } from '/@/components/Page';
  //import { useModal, BasicModal } from '/@/components/Modal';
  import { useDrawer } from '/@/components/Drawer';
  import InstNodeDrawer from '/@/views/devsys/devflowinst/InstNodeDrawer.vue';
  import { useTabs } from '/@/hooks/web/useTabs';
  import { useRoute } from 'vue-router';

  export default defineComponent({
    name: 'FlowInstChartPage',
    components: { PageWrapper, InstNodeDrawer },
    setup(props) {
      const { closeCurrent } = useTabs();
      const container = ref();
      const { createMessage: msg } = useMessage();
      // const [register, { openModal }] = useModal();
      const [registerNodeInfo, { openDrawer: openNodeDrawer }] = useDrawer();
      const graphData = ref({});
      const route = useRoute();
      //debugger;
      const instId = ref(route.params?.instid);
      console.log('参数-====', instId.value, props);
      function getViewChat(lf: LogicFlow) {
        const reqdata = flowInstChartViewApi(Number(instId.value));
        reqdata.then((values) => {
          //console.log('values', values);
          //debugger;

          lf.render(values);
        });
      }

      onMounted(() => {
        LogicFlow.use(Control);
        LogicFlow.use(Menu);
        let lf = new LogicFlow({
          container: container.value,
          grid: true,
          plugins: [BpmnElement, DndPanel, SelectionSelect, Control, MiniMap, Menu],
        });

        // 提供节点
        class UserNode extends RectNode {}
        // 提供节点的属性
        class UserModel extends RectNodeModel {
          constructor(data) {
            super(data, lf.graphModel);
            const { size } = data.properties;
            this.width = size * 40;
            this.height = size * 40;
            this.fill = 'green';
          }
        }

        //获取图像数据并渲染
        getViewChat(lf);
        lf.register({
          type: 'user',
          view: UserNode,
          model: UserModel,
        });

        var clickFlag = null; //是否点击标识（定时器编号）
        lf.on('node:click,edge:click', async (tdata) => {
          //console.log('node:click,edge:click');
          // debugger;
          // console.log(resdata);
          if (clickFlag) {
            //取消上次延时未执行的方法
            clickFlag = clearTimeout(clickFlag);
          }

          clickFlag = setTimeout(function () {
            if (resdata) {
              openNodeDrawer(true, {
                data: tdata,
                tempId: Number(instId.value),
              });
            } else {
              msg.warn({ content: '请先点就右上角保存按钮，保存流出图', key: 'nodemsg' });
            }
          }, 300); //延时300毫秒执行
        });
        lf.on('node:dbclick,edge:dbclick', () => {
          console.log('node:dbclick,edge:dbclick');
          if (clickFlag) {
            //取消上次延时未执行的方法
            clickFlag = clearTimeout(clickFlag);
          }
        });

        lf.render();
      });

      return {
        container,
        //register,
        graphData,
        registerNodeInfo,
        openNodeDrawer,
        closeCurrent,
      };
    },
  });
</script>
