<template>
  <div>
    <BasicTable
      @register="registerTable"
      rowKey="ID"
      :rowSelection="{ type: 'checkbox', selectedRowKeys: checkedKeys, onChange: onSelectChange }"
    >
      <template #toolbar>
        <a-button type="primary" @click="handleCreate">新增</a-button>
        <a-button type="primary" @click="handleDelrolws">批量删除</a-button>
        <a-button type="primary" @click="handleExcel">导出Excel</a-button>
        <a-dropdown :trigger="['click']" @click="clickFlowBtn" v-model:visible="dwonvisible">
          <a-button type="primary" @click.prevent @mouseover="dwonmouseover"
            >状态切换
            <DownOutlined />
          </a-button>
          <template #overlay>
            <a-menu
              @click="submitFlow"
              :class="{ active: isActive }"
              v-for="item in currflowitems"
              :key="item.Id"
            >
              <a-menu-item :key="item.Id" :from="item.StartSta" :to="item.EndSta">
                {{ item.Name }}
              </a-menu-item>
            </a-menu>
          </template>
        </a-dropdown>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看详情',
              onClick: handleView.bind(null, record),
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑信息',
              onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此信息',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <ExportExcelModel @register="registerExcelModel" />
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref, VNodeChild, onMounted, reactive } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { useMessage } from '/@/hooks/web/useMessage';
  import {
    getContractListApi,
    constractDelApi,
    updateStateApi,
  } from '/@/api/devsys/contract/collcontract';
  import { contcolumns, getFormConfig } from './index.data';
  import { useGo } from '/@/hooks/web/usePage';
  import { useModal } from '/@/components/Modal';
  import ExportExcelModel from '/@/components/DevComponents/src/ExportExcelModel.vue';
  import { ExportExcelData } from '/@/api/devsys/model/devCommonModel';
  import { Dropdown, Menu, MenuItem } from 'ant-design-vue';
  import { DownOutlined } from '@ant-design/icons-vue';
  import { getFlowItemList } from '/@/api/devsys/flow/flowtemp';
  import { FlowItemListItem } from '/@/api/devsys/model/flow/flowTempModel';
  import { getFlowTempApi } from '/@/api/devsys/flow/flowinst';
  import {
    GetCreatePermissionApi,
    GetDeletePermissionApi,
    GetUpdatePermissionApi,
    GetDetailPermissionApi,
  } from '/@/api/devsys/system/devpermission';
  import { useUserStore } from '/@/store/modules/user';
  import { FlowShowData } from '/@/api/devsys/model/flow/flowInstModel';
  interface MenuInfo {
    key: string;
    keyPath: string[];
    item: VNodeChild;
    domEvent: MouseEvent;
  }

  export default defineComponent({
    name: 'DevCollContract',
    components: {
      BasicTable,
      TableAction,
      ExportExcelModel,
      ADropdown: Dropdown,
      AMenu: Menu,
      AMenuItem: MenuItem,
      DownOutlined,
    },
    setup() {
      const userStore = useUserStore(); //当前用户
      const [registerExcelModel, { openModal: openExcelModal }] = useModal();
      //const {route} = useRoute();
      const go = useGo();
      const { createMessage: msg, createConfirm: confirm } = useMessage();
      const checkedKeys = ref<Array<string | number>>([]);
      let dwonvisible = ref(false);
      let flowItems = Array<FlowItemListItem>(); //审批事项
      let currflowitems: Array<FlowItemListItem> = reactive([]);
      let subFlowTag = ref(0); //提交状态
      let isActive = ref(true);
      const [registerTable, { reload, getColumns, getForm, getSelectRows }] = useTable({
        title: '收款合同列表',
        api: getContractListApi,
        columns: contcolumns,
        formConfig: getFormConfig(),
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        showIndexColumn: false,
        tableSetting: { fullScreen: true },
        rowKey: 'ID',
        actionColumn: {
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
          ellipsis: true,
          fixed: 'right',
        },
      });

      /****
       * 详情
       ****/
      function handleView(record: Recordable) {
        try {
          const reqdata = GetDetailPermissionApi({
            PerCode: 'collcontractdetail',
            Id: Number(record.ID),
          });

          reqdata.then((res) => {
            if (res.result === -1) {
              msg.warn({ content: res.data, key: 'detailing' });
            } else {
              go('/contract/collctract/collcontract_detail/' + record.ID);
            }
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'detailing' });
        }
      }
      function handleCreate() {
        try {
          //go('/company/customer/customer_build/0');
          const reqdata = GetCreatePermissionApi({ PerCode: 'collcontractbuild' });
          //console.log('权限888==reqdata==', reqdata);
          reqdata.then((res) => {
            // console.log('权限888==', res);
            if (res.result === -1) {
              msg.warn({ content: res.data, key: 'adding' });
            } else {
              go('/contract/collctract/collcontract_build/0');
            }
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'adding' });
        }
      }

      function handleEdit(record: Recordable) {
        try {
          const reqdata = GetUpdatePermissionApi({
            PerCode: 'collcontractupdate',
            Id: Number(record.ID),
          });
          //console.log('修改权限 reqdata====', reqdata);
          reqdata.then((res) => {
            //console.log('修改权限====', res);
            if (res.result === -1) {
              msg.warn({ content: res.data, key: 'editing' });
            } else {
              go('/contract/collctract/collcontract_build/' + record.ID);
            }
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'editing' });
        }
      }

      async function handleDelete(record: Recordable) {
        const reqdata = GetDeletePermissionApi({
          PerCode: 'collcontractdelete',
          Ids: record.ID.toString(),
        });
        reqdata.then((res) => {
          if (res.result === -1) {
            msg.warn({ content: res.data, key: 'deling' });
          } else {
            constractDelApi({ Ids: record.ID.toString() });
            msg.success({ content: '删除成功', key: 'deling' });
            reload();
          }
        });
      }
      function DelRows() {
        var _ids = checkedKeys.value.toString();
        constractDelApi({ Ids: _ids });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }
      /**
       * 批量删除
       */
      function handleDelrolws() {
        try {
          if (checkedKeys.value.length <= 0) {
            msg.info({ content: '请选择数据', key: 'deling' });
          } else {
            const reqdata = GetDeletePermissionApi({
              PerCode: 'collcontractdelete',
              Ids: checkedKeys.value.toString(),
            });
            reqdata.then((res) => {
              if (res.result === -1) {
                msg.warn({ content: res.data, key: 'deling' });
              } else {
                DelRows();
              }
            });
          }
        } catch (error) {
          msg.error({ content: '删除失败,' + error, key: 'deling' });
        }
      }
      function onSelectChange(selectedRowKeys: (string | number)[]) {
        console.log(selectedRowKeys);
        checkedKeys.value = selectedRowKeys;
      }

      function handleSuccess() {
        reload();
      }
      //弹窗excel
      function handleExcel() {
        const coloums = getColumns();
        const formvalues = getForm().getFieldsValue();
        const opendata: ExportExcelData = {
          selkey: checkedKeys.value,
          colums: coloums,
          seardata: formvalues,
          extype: 'collcontract',
        };
        openExcelModal(true, opendata);
      }

      /**
       * 提交流程
       **/
      const submitFlow = async ({ key, item }: MenuInfo) => {
        let selrows = getSelectRows();
        let _state = Number(item.to);
        console.log(`Click on item ${key}--${item}`);
        // debugger;
        const tempdata = {
          FlowObj: 3,
          CateId: selrows[0].CATE_ID,
          DeptId: userStore.getUserInfo.departId,
          FlowItem: Number(key),
          Monery: selrows[0].ANT_MONERY,
        };
        let resdata = await getFlowTempApi(tempdata);
        if (resdata != null) {
          // debugger;
          // console.log('返回类型', resdata);
          isActive.value = true;
          const flowdata: FlowShowData = {
            FlowType: tempdata.FlowObj,
            ObjId: selrows[0].ID,
            TempId: resdata.ID,
            FlowItem: tempdata.FlowItem,
            Name: selrows[0].C_NAME,
          };
          //console.log(resdata, flowdata);
          go('/devflow/flowtemp/flowsubmitpage/' + encodeURIComponent(JSON.stringify(flowdata)));
        } else {
          //没有匹配上流程直接修改
          confirm({
            content: '没有匹配上流程，是否直接修改状态',
            onOk: () => {
              //console.log('点击确认....');
              updateStateApi({ state: _state, Id: Number(selrows[0].ID) });
              msg.success({ content: '修改成功', key: 'update' });
              reload();
            },
            iconType: 'warning',
          });
          //msg.info({ content: '没有匹配上流程，是否直接修改', key: 'submitflow' });
        }
      };

      async function loadFlowItems() {
        // console.log('调用getFlowItemList');
        let tdata = await getFlowItemList({ objEnum: 3 });
        let tempArray = Array<FlowItemListItem>();
        for (let i = 0; i < tdata.length; i++) {
          tempArray.push(tdata[i]);
        }
        flowItems = tempArray;
      }
      /*鼠标移入事件* */
      function dwonmouseover() {
        subFlowTag.value = 0;
        isActive.value = true;
        currflowitems.splice(0, currflowitems.length); //清空审批事项
        //console.log('dwonmouseover----');
        let selrows = getSelectRows();
        // var isres = selrows.every((item) => {
        //   return item.C_STATE == selrows[0].C_STATE && item.WF_STATE == 0;
        // });

        // subFlowTag.value = isres ? 1 : 0;
        //debugger;
        let sellength = selrows.length;
        if (sellength == 0) {
          subFlowTag.value = -1;
        } else if (sellength > 1) {
          subFlowTag.value = 2;
        } else if (selrows[0].WF_STATE == 1) {
          subFlowTag.value = 3;
        } else {
          //debugger;
          let tempcurrflowitems = flowItems.filter((a) => a.StartSta == selrows[0].CONT_STATE);
          currflowitems.push(...tempcurrflowitems);
          //console.log('审批事项', currflowitems);
          isActive.value = false;
        }
      }
      function clickFlowBtn() {
        //console.log('点击事件 start--', dwonvisible.value, subFlowTag.value);
        if (subFlowTag.value == -1) {
          msg.warn({ content: '请选择数据..', key: 'tmsg' });
        } else if (subFlowTag.value == 2) {
          msg.warn({ content: '只能选择一条数据进行提交', key: 'tmsg' });
        } else if (subFlowTag.value == 3) {
          msg.warn({ content: '审批中的数据不能提交', key: 'tmsg' });
        } else {
          //isActive.value = false;
        }
        // console.log('isActive状态', isActive);
      }

      onMounted(() => {
        //console.log('onMounted----');
        loadFlowItems();
      });

      return {
        registerTable,
        handleCreate,
        handleEdit,
        handleDelete,
        handleSuccess,
        handleDelrolws,
        checkedKeys,
        onSelectChange,
        handleView,
        handleExcel,
        registerExcelModel,
        submitFlow,
        clickFlowBtn,
        dwonvisible,
        dwonmouseover,
        loadFlowItems,
        flowItems,
        currflowitems,
        subFlowTag,
        isActive,
      };
    },
  });
</script>
<style scoped>
  .active {
    display: none;
  }</style
>>
