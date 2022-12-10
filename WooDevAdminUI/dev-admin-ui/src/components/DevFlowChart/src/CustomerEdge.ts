// 结束节点 重新
import { PolylineEdge, PolylineEdgeModel } from '@logicflow/core';

class CustomerEdgeModel extends PolylineEdgeModel {
  getEdgeStyle() {
    const style = super.getEdgeStyle();
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

//class EndNodeView extends CircleNode {}

export default {
  type: 'bpmn:sequenceFlow',
  view: PolylineEdge,
  model: CustomerEdgeModel,
};
