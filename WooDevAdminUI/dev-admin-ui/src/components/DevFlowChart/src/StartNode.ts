// 开始节点重写
import { CircleNode, CircleNodeModel } from '@logicflow/core';

class StartNodeModel extends CircleNodeModel {
  getNodeStyle() {
    const style = super.getNodeStyle();
    const properties = this.properties;
    if (properties.statu === '2') {
      style.stroke = '#22D76E'; //审批通过
    } else if (properties.statu === '1') {
      //审批中
      style.stroke = '#FC8400';
    } else {
      style.stroke = '#187DFF';
    }
    return style;
  }
}

class StartNodeView extends CircleNode {}

export default {
  type: 'StartNode',
  view: StartNodeView,
  model: StartNodeModel,
};
