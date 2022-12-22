<template>
  <div>
    <BasicTable @register="registerTable" @edit-change="handleEditChange" @edit-end="handleEditEnd">
      <!-- <template #toolbar>
        <BasicUpload
          :maxSize="200"
          :maxNumber="5"
          @change="handleChange"
          :api="devuploadApi"
          :uploadParams="uploadparam"
          :value="fileList"
          class="my-5"
          :showPreviewNumber="showPreviewNumber"
          :accept="devconfig.devupload.accept"
        />
      </template> -->
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'ant-design:arrow-down',
              tooltip: '下载文件',
              onClick: handleDownload.bind(null, record),
            },
            // {
            //   icon: 'ant-design:delete-outlined',
            //   color: 'error',
            //   tooltip: '删除文件',
            //   popConfirm: {
            //     title: '是否确认删除',
            //     confirm: handleDelete.bind(null, record),
            //   },
            // },
          ]"
        />
      </template>
    </BasicTable>
  </div>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicTable, useTable, TableAction, BasicColumn } from '/@/components/Table';
  //import FileModal from './FileModal.vue';
  import { BasicUpload } from '/@/components/Upload';
  import { devuploadApi } from '/@/api/devsys/system/devupload';
  import { uploadInfo } from '/@/api/devsys/model/uploadModel';
  import { devUpdateField } from '/@/api/devsys/model/devCommonModel';
  import { devconfig } from '/@/api/devsys/model/devConfig.data';
  import { useMessage } from '/@/hooks/web/useMessage';
  import {
    contTextSaveApi,
    getContTextListApi,
    contTextUpdateFieldApi,
    contTextDelApi,
  } from '/@/api/devsys/contract/collcontract';
  import { getdataListApi } from '/@/api/devsys/system/datadic';
  import { downloadByUrl } from '/@/utils/file/download';
  const uploadparam = { FolderIndex: 6 };
  const fileList = ref<string[]>([]);
  const showPreviewNumber = false;
  const columns: BasicColumn[] = [
    {
      title: '文本名称',
      dataIndex: 'NAME',
    },
    {
      title: '文本类别',
      dataIndex: 'CateName',
      //edit: true,
      editRule: true,
      editComponent: 'ApiSelect',
      editComponentProps: {
        api: getdataListApi,
        labelField: 'NAME',
        valueField: 'ID',
        params: { LbId: 10 },
      },
    },
    {
      title: '文本描述',
      dataIndex: 'REMARK',
      //edit: true,
      editComponent: 'Input',
    },
    {
      title: '创建人',
      dataIndex: 'CreateUserName',
    },
    {
      title: '创建时间',
      dataIndex: 'CREATE_TIME',
    },
  ];
  export default defineComponent({
    components: { BasicTable, TableAction }, //FileModal BasicUpload
    props: {
      custid: {
        type: Number,
        default: 0,
      },
    },
    setup(props: any) {
      const currcustid = Number(props.custid);
      const { createMessage } = useMessage();
      const [registerTable, { getDataSource, reload }] = useTable({
        columns: columns,
        showIndexColumn: false,
        api: getContTextListApi,
        beforeFetch: (t) => {
          t.CustId = currcustid;
        },

        bordered: false,
        rowKey: 'ID',
        actionColumn: {
          width: 160,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
        pagination: false,
      });

      // function handleEdit(record: EditRecordRow) {
      //   record.onEdit?.(true);
      // }
      //下载
      function handleDownload(record: Recordable) {
        const loadurl = devconfig.devupload.baseurl + record.TEXT_PATH;
        downloadByUrl({
          url: loadurl,
          target: '_self',
        });
      }
      //删除
      async function handleDelete(record: Recordable) {
        await contTextDelApi({ Ids: record.ID.toString() });
        createMessage.success({ content: '删除成功', key: 'deling' });
        handleSuccess();
      }

      function handleEditChange(data: Recordable) {
        console.log(data);
      }
      //编辑列
      async function handleEditEnd(editdata) {
        // debugger;
        var upobj: devUpdateField = {
          Id: editdata.record.ID,
          Field: editdata.key,
          Value: editdata.value,
        };
        //console.log(JSON.stringify(upobj));
        await contTextUpdateFieldApi(upobj);
        createMessage.success('修改成功');
      }

      function handleSuccess() {
        reload();
      }

      //文件保存
      function handleChange(list: string[]) {
        let array: Array<uploadInfo> = [];
        //debugger;
        for (let i = 0; i < list.length; i++) {
          var objinfo = JSON.parse(list[i]);

          let uploadinfo: uploadInfo = {
            Extension: objinfo.Extension,
            FolderIndex: objinfo.FolderIndex,
            FolderName: objinfo.FolderName,
            FolderPath: objinfo.FolderPath,
            GuidFileName: objinfo.GuidFileName,
            RemGuidName: objinfo.RemGuidName,
            SourceFileName: objinfo.SourceFileName,
            TempId: currcustid,
          };
          array.push(uploadinfo);
          //createMessage.info(`单文件-->${objinfo}`);
        }
        contTextSaveApi(array);
        //var objlist = JSON.parse(list);
        //console.log(objlist);
        // createMessage.info(`已上传文件${JSON.stringify(list)}`);
        createMessage.success('文件保存成功');
        handleSuccess();
      }

      return {
        registerTable,
        handleDownload,
        handleDelete,
        getDataSource,
        handleEditChange,
        handleSuccess,
        devuploadApi,
        uploadparam,
        fileList,
        showPreviewNumber,
        handleChange,
        handleEditEnd,
        downloadByUrl,
        devconfig,
      };
    },
  });
</script>
