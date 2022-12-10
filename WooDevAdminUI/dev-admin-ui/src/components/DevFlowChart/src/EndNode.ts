// 结束节点 重新
import { CircleNode, CircleNodeModel } from '@logicflow/core';

class EndNodeModel extends CircleNodeModel {
  getNodeStyle() {
    const style = super.getNodeStyle();
    const properties = this.properties;
    if (properties.statu === '2') {
      style.stroke = '#22D76E';
    } else if (properties.statu === '1') {
      style.stroke = '#FC8400';
    } else {
      style.stroke = '#187DFF';
    }
    return style;
  }
}

class EndNodeView extends CircleNode {}

export default {
  type: 'EndNode',
  view: EndNodeView,
  model: EndNodeModel,
};
