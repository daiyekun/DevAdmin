<template>
  <!-- 宽度可以百分比 -->
  <BasicDrawer
    v-bind="$attrs"
    @register="register"
    title="审批节点信息"
    width="500px"
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
          :rowSelection="{ type: 'checkbox' }"
        >
          <template #toolbar>
            <a-button type="primary" style="display: none" danger @click="delAppObj">
              删除
            </a-button>
            <a-button type="primary" style="display: none" @click="addAppObj"> 增加 </a-button>
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
  <UserGroupModel
    @register="registerGroupModel"
    @rowGroupDbclick="rowGroupDbclick"
    @selectRowGroup="selectRowGroup"
  />
</template>
<script lang="ts">
  import { defineComponent, reactive } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { useModal } from '/@/components/Modal';
  import UserSelectModel from '/@/views/devsys/contract/common/UserModel.vue';
  import UserGroupModel from '/@/views/devsys/contract/common/UserGroupModel.vue';
  // import { PageWrapper } from '/@/components/Page';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form/index';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { BasicTable, useTable, BasicColumn } from '/@/components/Table';
  import {
    getNodeInfoByNodeIdApi,
    flowNodeInfoSaveApi,
    flowNodeInfoAppObjApi,
    flowNodeUpdateApi,
  } from '/@/api/devsys/flow/flowtemp';
  import { FlowTempNodeInfo, FlowTempNodeUpdateInfo } from '/@/api/devsys/model/flow/flowTempModel';
  /**
   * 表格列
   */
  const columns: BasicColumn[] = [
    {
      title: '审批类型',
      dataIndex: 'OtypeDsc',
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
      width: 160,
    },
    {
      title: '审批对象',
      dataIndex: 'ObjName',
      //edit: true,
      editComponentProps: {
        prefix: '$',
      },
      width: 160,
    },
  ];
  export default defineComponent({
    components: { BasicDrawer, BasicForm, UserSelectModel, BasicTable, UserGroupModel },
    setup() {
      let nodeInfo = reactive({ nodeId: '', tempId: 0 });
      const { createMessage: msg } = useMessage();
      const [register] = useDrawerInner((data) => {
        // 方式1
        setFieldsValue({
          NodeId: data.data.data.id,
          Name: data.data.data.text.value,
          TempId: data.tempId,
          SpRules: data.data.data.properties.NRULE,
        });
        //节点ID
        nodeInfo.nodeId = data.data.data.id;
        nodeInfo.tempId = data.tempId;
        //刷新列表
        reload();
      });
      const [registerTable, { reload, getSelectRows }] = useTable({
        title: '审批对象列表',
        api: getNodeInfoByNodeIdApi,
        beforeFetch: (t) => {
          t.NodeStr = nodeInfo.nodeId;
        },
        rowKey: 'ID',
        columns: columns,
        showIndexColumn: false,
        bordered: true,
        pagination: false,
      });
      const [registerUserModel, { openModal: openModal1, closeModal }] = useModal();
      const [registerGroupModel, { openModal: openGroupModal, closeModal: closeGroupModal }] =
        useModal();
      const schemas: FormSchema[] = [
        {
          field: 'TempId',
          component: 'Input',
          componentProps: { disabled: true },
          label: '流程模板ID',
          colProps: {
            span: 24,
          },
          defaultValue: '',
          show: () => {
            return false;
          },
        },
        {
          field: 'NodeId',
          component: 'Input',
          componentProps: { disabled: true },
          label: '编号',
          colProps: {
            span: 24,
          },
        },
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

      const schemas1: FormSchema[] = [
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
                  openGroupModal();
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
          show: () => {
            return false;
          },
        },

        {
          field: 'O_TYPE',
          component: 'Select',
          label: '审批类型',
          colProps: {
            span: 24,
          },
          show: () => {
            return false;
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
      ];
      const [registerForm, { setFieldsValue, getFieldsValue: getNodeInfoFieldsValue }] = useForm({
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
      //新增列-已经作废，不需要修改
      function addAppObj() {
        let fdata = getFieldsValue();
        if (fdata.O_TYPE == undefined) {
          msg.warn({ content: '请选择审批类型', key: 'msg' });
        } else if (fdata.O_TYPE == '1') {
          openModal1(); //选择用户
        } else if (fdata.O_TYPE == '2') {
          //选择组
          openGroupModal();
        } else {
          msg.error({ content: '此类型未实现', key: 'msg' });
        }
      }
      /***
       * 删除审批对象
       */
      async function delAppObj() {
        let selrecord = getSelectRows();
        if (selrecord.length <= 0) {
          msg.warn({ content: '请选择删除数据', key: 'delinfomsg' });
        } else {
          msg.loading({ content: `正在删除数据`, key: 'delinfomsg', duration: 0 });
          let arrayIds: Array<number> = [];
          for (let i = 0; i < selrecord.length; i++) {
            arrayIds.push(selrecord[i].ID);
          }
          await flowNodeInfoAppObjApi({ Ids: arrayIds.toString() });
          reload();
          msg.success({ content: '删除成功', key: 'delinfomsg' });
        }
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

      /**
       * 保存节点信息
       */
      async function SaveNodeInfo(sdata: FlowTempNodeInfo) {
        try {
          await flowNodeInfoSaveApi(sdata);
          closeModal();
          closeGroupModal();
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
        //closeModal();

        msg.warn({
          content: '请勾选数据点击确认',
          key: 'tmsg',
        });
        console.log(rd);
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

      /***
       * 选择审批对象
       */
      function rowGroupDbclick(rd: any) {
        msg.warn({
          content: '请勾选数据点击确认',
          key: 'tmsg',
        });
        console.log(rd);
        //closeModal();
      }

      /**
       * 选择组弹出框 确认按钮
       */
      function selectRowGroup(rds: any) {
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
      /**
       * 底部确认按钮
       */

      async function handleOk() {
        var savedata = getNodeInfoFieldsValue();
        if (savedata.SpRules == undefined || savedata.SpRules == '') {
          msg.warn({
            content: `请选择审批规则`,
            key: '_save_node_data',
          });
        } else {
          msg.loading({
            content: `正在提交数据`,
            key: '_save_node_data',
            duration: 0,
          });
          let sdata: FlowTempNodeUpdateInfo = {
            NodeId: savedata.NodeId,
            TempId: savedata.TempId,
            Name: savedata.Name,
            SpRules: savedata.SpRules,
          };
          await flowNodeUpdateApi(sdata);
          msg.success({
            content: `保存成功`,
            key: '_save_node_data',
          });
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
        delAppObj,
        selectRowUser,
        SaveNodeInfo,
        nodeInfo,
        registerGroupModel,
        openGroupModal,
        closeGroupModal,
        selectRowGroup,
        rowGroupDbclick,
      };
    },
  });
</script>
<style lang="less" scoped>
  .selsp {
    padding-top: 6px;
  }
</style>
