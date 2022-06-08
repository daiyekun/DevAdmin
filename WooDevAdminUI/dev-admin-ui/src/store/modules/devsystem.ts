/**
 * dev 系统store
 */
import { defineStore } from 'pinia';
interface DevsysState {
  lbId: number;
}
export const devSystemStore = defineStore({
  id: 'devsystem',
  state: (): DevsysState => ({
    lbId: 0, //选择字典类别ID
  }),
  getters: {},
  actions: {},
});
