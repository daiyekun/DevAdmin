export {};

declare module 'vue-router' {
  interface RouteMeta extends Record<string | number | symbol, unknown> {
    orderNo?: number;
    // title
    title: string;
    // dynamic router level.
    dynamicLevel?: number;
    // dynamic router real route path (For performance).
    realPath?: string;
    // Whether to ignore permissions--是否忽略权限
    ignoreAuth?: boolean;
    // role info
    roles?: RoleEnum[];
    // Whether not to cache
    ignoreKeepAlive?: boolean;
    // Is it fixed on tab-->是否固定在标签上
    affix?: boolean;
    // icon on tab
    icon?: string;
    frameSrc?: string;
    // current page transition-->当前页面转换
    transitionName?: string;
    // Whether the route has been dynamically added-->是否已动态添加路由
    hideBreadcrumb?: boolean;
    // Hide submenu-->是否隐藏子菜单
    hideChildrenInMenu?: boolean;
    // Carrying parameters -->是否有参数
    carryParam?: boolean;
    // Used internally to mark single-level menus--》内部用于标记单层菜单
    single?: boolean;
    // Currently active menu--当前活动菜单
    currentActiveMenu?: string;
    // Never show in tab--是否显示标签
    hideTab?: boolean;
    // Never show in menu--是否显示菜单
    hideMenu?: boolean;
    isLink?: boolean;
    // only build for Menu  --仅针对菜单生成
    ignoreRoute?: boolean;
    // Hide path for children --隐藏子级路由
    hidePathForChildren?: boolean;
  }
}
