<template>
  <BasicModal
    v-bind="$attrs"
    title="选择导出条件"
    :helpMessage="['选择导出条件']"
    @register="registerModel"
    @ok="handleExcel"
    :width="400"
    :height="200"
  >
    <BasicForm @register="register" />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal'; //useModalInner
  // import { h } from 'vue';
  // import { Tag } from 'ant-design-vue';
  import { BasicForm, useForm, FormSchema } from '/@/components/Form';
  import { ExportExcelData, ExcelInfo, ExcelReqData } from '/@/api/devsys/model/devCommonModel';
  import { useMessage } from '/@/hooks/web/useMessage';
  // import { BasicColumn } from '/@/components/Table';
  // import { customerParams } from '/@/api/devsys/model/customerModel';
  import { customerExcelApi } from '/@/api/devsys/system/devexportfile';
  import { downloadByUrl } from '/@/utils/file/download';
  import { cloneDeep } from 'lodash-es';
  import { devconfig } from '/@/api/devsys/model/devConfig.data';

  export default defineComponent({
    components: { BasicModal, BasicForm },
    // props: ['selkeys', 'cloms', 'seardata'],
    // emits: ['selCondtion'],

    setup() {
      const { createMessage: msg } = useMessage();
      const schemas: FormSchema[] = [
        {
          field: 'selRow',
          component: 'RadioGroup',
          label: '行选择',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '导出选择行',
                value: 1,
              },
              {
                label: '导出当前所有行',
                value: 2,
              },
            ],
          },
        },
        {
          field: 'selCell',
          component: 'RadioGroup',
          label: '列选择',
          colProps: {
            span: 24,
          },
          componentProps: {
            options: [
              {
                label: '导出选择列',
                value: 1,
              },
              {
                label: '导出所有列',
                value: 2,
              },
            ],
          },
        },
      ];
      const [register, { setProps, getFieldsValue }] = useForm({
        labelWidth: 120,
        schemas,
        actionColOptions: {
          span: 24,
        },
        showActionButtonGroup: false,
      });
      //初始化对象，此处应该用类去定义
      let exportdata: ExportExcelData = {
        selkey: [0],
        colums: [{}],
        seardata: {},
        extype: '',
      };
      const [registerModel, { closeModal }] = useModalInner((data: ExportExcelData) => {
        exportdata = data;
      });

      function handleExcel() {
        const values = getFieldsValue();
        let exportyes = true;
        let selinfo: ExcelInfo = {
          Ids: '0',
          Scell: 0,
          Srow: 0,
          CelKeys: '0',
          CellNames: '0',
          extype: '',
        };
        selinfo.Srow = values.selRow;
        selinfo.Scell = values.selCell;
        selinfo.extype = exportdata.extype;
        if (values.selRow === 1) {
          //导出选择行
          if (exportdata.selkey.length <= 0) {
            exportyes = false;
            msg.info({ content: '请选择导出行', key: 'exporting' });
          } else {
            selinfo.Ids = exportdata.selkey.toString();
          }
        }
        let selindex: string[] = [];
        let seltitle: string[] = [];
        for (let item of exportdata.colums) {
          if (values.selCell === 1) {
            //导出选择列
            if (!item.defaultHidden) {
              if (item.dataIndex != 'action') {
                selindex.push(item.dataIndex as string);
                seltitle.push(item.title as string);
              }
            }
          } else {
            if (item.dataIndex != 'action') {
              selindex.push(item.dataIndex as string);
              seltitle.push(item.title as string);
            }
          }
        }
        selinfo.CellNames = seltitle.toString();
        selinfo.CelKeys = selindex.toString();
        const sdata = cloneDeep(exportdata.seardata); //获取修改数据，否则有问题
        var reqdata: ExcelReqData = {
          Seldata: selinfo,
          SearData: sdata,
        };
        if (exportyes) {
          //调用接口
          const resultdata = customerExcelApi(reqdata);
          resultdata.then((values) => {
            const loadurl = devconfig.devupload.baseurl + values.FilePath;

            downloadByUrl({
              url: loadurl,
              target: '_self',
            });
            closeModal();
          });
        }
      }
      return { setProps, register, registerModel, handleExcel };
    },
  });
</script>
