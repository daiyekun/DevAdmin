<template>
  <!-- 宽度可以百分比 -->
  <BasicDrawer
    v-bind="$attrs"
    @register="register"
    title="详细信息"
    width="500px"
    showFooter
    @ok="handleOk"
  >
    <div>
      <BasicForm @register="registerForm" />
      <hr />
      <div class="selsp">
        <BasicForm @register="registerForm1" />
      </div>
      <div class="p-4">
        <BasicTable
          :bordered="true"
          @register="registerTable"
          @edit-end="handleEditEnd"
          @edit-cancel="handleEditCancel"
          :beforeEditSubmit="beforeEditSubmit"
        >
          <template #toolbar>
            <a-button type="primary" @click="addAppObj"> 增加 </a-button>
          </template>
        </BasicTable>
      </div>
    </div>
  </BasicDrawer>
  <UserSelectModel
    @register="registerUserModel"
    @rowUserDbclick="SelleadUser"
    @selectRowUser="selectRowUser"
  />
</template>
<script lang="ts">
  import { defineComponent, reactive } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { useModal } from '/@/components/Modal';
  import UserSelectModel from '/@/views/devsys/contract/common/UserModel.vue';
  // import { PageWrapper } from '/@/components/Page';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form/index';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { BasicTable, useTable, BasicColumn } from '/@/components/Table';
  import { getNodeInfoByNodeIdApi, flowNodeInfoSaveApi } from '/@/api/devsys/flow/flowtemp';
  import { FlowTempNodeInfo } from '/@/api/devsys/model/flow/flowTempModel';

  const columns: BasicColumn[] = [
    {
      title: '审批类型',
      dataIndex: 'O_TYPE',
      //edit: true,
      editComponent: 'Select',
      editComponentProps: {
        options: [
          {
            label: '人力资源',
            value: '1',
          },
          {
            label: '审批组',
            value: '2',
          },
        ],
      },
      width: 200,
    },
    {
      title: '审批对象',
      dataIndex: 'OPT_NAME',
      //edit: true,
      editComponentProps: {
        prefix: '$',
      },
      width: 200,
    },
  ];
  export default defineComponent({
    components: { BasicDrawer, BasicForm, UserSelectModel, BasicTable },
    setup() {
      // let nodeId = ref(0); //当前节点
      // let tempId = ref(0); //模板ID
      let nodeInfo = reactive({ nodeId: '', tempId: 0 });
      const { createMessage: msg } = useMessage();
      const [register] = useDrawerInner((data) => {
        // 方式1
        setFieldsValue({
          id: data.data.data.id,
          Name: data.data.data.text.value,
        });
        //节点ID
        nodeInfo.nodeId = data.data.data.id;
        nodeInfo.tempId = data.tempId;
        //刷新列表
        reload();
      });
      const [registerTable, { reload }] = useTable({
        title: '审批对象列表',
        api: getNodeInfoByNodeIdApi,
        beforeFetch: (t) => {
          t.NodeStr = nodeInfo.nodeId;
        },
        rowKey: 'ID',
        columns: columns,
        showIndexColumn: false,
        bordered: true,
      });
      const [registerUserModel, { openModal: openModal1, closeModal }] = useModal();
      const schemas: FormSchema[] = [
        {
          field: 'Name',
          component: 'Input',
          componentProps: { disabled: true },
          label: '名称',
          colProps: {
            span: 24,
          },
          defaultValue: '',
        },
        {
          field: 'id',
          component: 'Input',
          componentProps: { disabled: true },
          label: '编号',
          colProps: {
            span: 24,
          },
        },
      ];

      const schemas1: FormSchema[] = [
        {
          field: 'O_TYPE',
          component: 'Select',
          label: '审批类型',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '人力资源',
                value: '1',
                key: '1',
              },
              {
                label: '审批组',
                value: '2',
                key: '2',
              },
            ],
          },
        },
        {
          field: 'selSpId',
          component: 'Input',
          componentProps: ({ formModel }) => {
            return {
              onclick: () => {
                if (formModel.O_TYPE == undefined) {
                  msg.warn({ content: '请选择审批类型', key: 'msg' });
                } else if (formModel.O_TYPE == '1') {
                  openModal1();
                } else if (formModel.O_TYPE == '2') {
                } else {
                  msg.error({ content: '此类型未实现', key: 'msg' });
                }
              },
            };
          },
          label: '审批对象',
          colProps: {
            span: 24,
          },
        },
        {
          field: 'SpRules',
          component: 'Select',
          label: '审批规则',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '全部审批',
                value: '1',
                key: '1',
              },
              {
                label: '任意通过',
                value: '2',
                key: '2',
              },
            ],
          },
        },
      ];
      const [registerForm, { setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 24,
        },
      });
      const [registerForm1, { getFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: schemas1,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 24,
        },
      });
      /**
       * 单元格编辑
       */
      function handleEditEnd({ record, index, key, value }: Recordable) {
        console.log(record, index, key, value);
        return false;
      }
      /**
       * 单元格编辑取消
       */
      function handleEditCancel() {
        console.log('cancel');
      }
      //新增列
      function addAppObj() {
        let fdata = getFieldsValue();
        if (fdata.O_TYPE == undefined) {
          msg.warn({ content: '请选择审批类型', key: 'msg' });
        } else if (fdata.O_TYPE == '1') {
          openModal1(); //选择用户
        } else if (fdata.O_TYPE == '2') {
          //选择组
        } else {
          msg.error({ content: '此类型未实现', key: 'msg' });
        }

        //console.log('addAppObj', fdata);
      }

      // 模拟将指定数据保存
      function feakSave({ value, key, id }) {
        msg.loading({
          content: `正在模拟保存${key}`,
          key: '_save_fake_data',
          duration: 0,
        });
        return new Promise((resolve) => {
          setTimeout(() => {
            if (value === '') {
              msg.error({
                content: '保存失败：不能为空',
                key: '_save_fake_data',
                duration: 2,
              });
              resolve(false);
            } else {
              msg.success({
                content: `记录${id}的${key}已保存`,
                key: '_save_fake_data',
                duration: 2,
              });
              resolve(true);
            }
          }, 2000);
        });
      }

      async function beforeEditSubmit({ record, index, key, value }) {
        console.log('单元格数据正在准备提交', { record, index, key, value });
        return await feakSave({ id: record.id, key, value });
      }

      function handleOk() {
        console.log('=====================');
        console.log('ok');
        console.log('======================');
      }
      /**
       * 保存节点信息
       */
      async function SaveNodeInfo(sdata: FlowTempNodeInfo) {
        try {
          await flowNodeInfoSaveApi(sdata);
          closeModal();
          reload();
          msg.success({ content: '数据已保存', key: 'saving' });
        } catch (error) {
          msg.error({ content: '保存失败,' + error, key: 'saving' });
        }
      }
      /***
       * 选择审批对象
       */
      function SelleadUser(rd: any) {
        setFieldsValue({
          LEAD_USERID: rd.ID,
          LEAD_USERNAME: rd.NAME,
        });
        closeModal();
      }
      /**
       * 选择用户弹出框 确认按钮
       */
      function selectRowUser(rds: any) {
        if (rds.length <= 0) {
          msg.warn({
            content: '请选择数据',
            key: 'tmsg',
          });
        } else {
          //添加到后台
          let fdata = getFieldsValue();
          let userIds = [];
          for (let i = 0; i < rds.length; i++) {
            userIds.push(rds[i].ID);
          }
          const savedata: FlowTempNodeInfo = {
            TEMP_ID: nodeInfo.tempId,
            NODE_STRID: nodeInfo.nodeId,
            O_TYPE: fdata.O_TYPE,
            SpObjIds: userIds,
          };
          SaveNodeInfo(savedata);
        }
      }

      return {
        register,
        schemas,
        registerForm,
        handleOk,
        registerForm1,
        registerUserModel,
        openModal1,
        SelleadUser,
        registerTable,
        handleEditEnd,
        handleEditCancel,
        beforeEditSubmit,
        addAppObj,
        selectRowUser,
        SaveNodeInfo,
        nodeInfo,
      };
    },
  });
</script>
<style lang="less" scoped>
  .selsp {
    padding-top: 6px;
  }
</style>
