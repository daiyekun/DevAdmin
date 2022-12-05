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
  import { getCusertomerListApi, customerDelApi } from '/@/api/devsys/contract/customer';
  import { customercolumns, getFormConfig } from './index.data';
  import { useGo } from '/@/hooks/web/usePage';
  import { useModal } from '/@/components/Modal';
  import ExportExcelModel from '/@/views/devsys/contract/common/ExportExcelModel.vue';
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
    name: 'DevCustomer',
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
      const { createMessage: msg } = useMessage();
      const checkedKeys = ref<Array<string | number>>([]);
      let dwonvisible = ref(false);
      let flowItems = Array<FlowItemListItem>(); //审批事项
      let currflowitems: Array<FlowItemListItem> = reactive([]);
      let subFlowTag = ref(0); //提交状态
      let isActive = ref(true);
      const [registerTable, { reload, getColumns, getForm, getSelectRows }] = useTable({
        title: '客户列表',
        api: getCusertomerListApi,
        columns: customercolumns,
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
            PerCode: 'customerdetail',
            Id: Number(record.ID),
          });

          reqdata.then(() => {
            go('/company/customer/customer_detail/' + record.ID);
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'detailing' });
        }
      }
      function handleCreate() {
        try {
          //go('/company/customer/customer_build/0');
          const reqdata = GetCreatePermissionApi({ PerCode: 'customerbuild' });

          reqdata.then(() => {
            go('/company/customer/customer_build/0');
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'adding' });
        }
      }

      function handleEdit(record: Recordable) {
        try {
          const reqdata = GetUpdatePermissionApi({
            PerCode: 'customerbuild',
            Id: Number(record.ID),
          });

          reqdata.then(() => {
            go('/company/customer/customer_build/' + record.ID);
          });
        } catch (error) {
          msg.error({ content: '' + error, key: 'editing' });
        }
      }

      async function handleDelete(record: Recordable) {
        customerDelApi({ Ids: record.ID.toString() });
        msg.success({ content: '删除成功', key: 'deling' });
        reload();
      }
      function CustomerDelRows() {
        var _ids = checkedKeys.value.toString();
        customerDelApi({ Ids: _ids });
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
              PerCode: 'customerdelete',
              Ids: checkedKeys.value.toString(),
            });
            reqdata.then(() => {
              CustomerDelRows();
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
          extype: 'customer',
        };
        openExcelModal(true, opendata);
      }

      /**
       * 提交流程
       **/
      const submitFlow = async ({ key, item }: MenuInfo) => {
        let selrows = getSelectRows();
        console.log(`Click on item ${key}--${item}`);
        const tempdata = {
          FlowObj: 0,
          CateId: selrows[0].CATE_ID,
          DeptId: userStore.getUserInfo.departId,
          FlowItem: Number(key),
          Monery: 0,
        };
        let resdata = await getFlowTempApi(tempdata);
        // debugger;
        isActive.value = true;
        const flowdata: FlowShowData = {
          FlowType: tempdata.FlowObj,
          ObjId: selrows[0].ID,
          TempId: resdata.ID,
          FlowItem: tempdata.FlowItem,
          Name: selrows[0].NAME,
        };
        console.log(resdata, flowdata);
        go('/devflow/flowtemp/flowsubmitpage/' + encodeURIComponent(JSON.stringify(flowdata)));
      };

      async function loadFlowItems() {
        // console.log('调用getFlowItemList');
        let tdata = await getFlowItemList({ objEnum: 0 });
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
          let tempcurrflowitems = flowItems.filter((a) => a.StartSta == selrows[0].C_STATE);
          currflowitems.push(...tempcurrflowitems);
          //console.log('审批事项', currflowitems);
          isActive.value = false;
        }
      }
      function clickFlowBtn() {
        console.log('点击事件 start--', dwonvisible.value, subFlowTag.value);
        if (subFlowTag.value == -1) {
          msg.warn({ content: '请选择数据..', key: 'tmsg' });
        } else if (subFlowTag.value == 2) {
          msg.warn({ content: '只能选择一条数据进行提交', key: 'tmsg' });
        } else if (subFlowTag.value == 3) {
          msg.warn({ content: '审批中的数据不能提交', key: 'tmsg' });
        } else {
          //isActive.value = false;
        }
        console.log('isActive状态', isActive);
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
