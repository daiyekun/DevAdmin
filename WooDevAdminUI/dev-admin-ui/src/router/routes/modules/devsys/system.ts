import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const system: AppRouteModule = {
  path: '/devsystem',
  name: 'devSystem',
  component: LAYOUT,
  redirect: '/devsys/account',
  meta: {
    orderNo: 2000,
    icon: 'ion:settings-outline',
    title: t('routes.devsys.devsystem.moduleName'),
  },
  children: [
    {
      path: 'account',
      name: 'DevAccountManagement',
      meta: {
        title: t('routes.demo.system.account'),
        ignoreKeepAlive: false,
      },
      component: () => import('/@/views/devsys/system/user/index.vue'),
    },
    {
      path: 'account_detail/:id',
      name: 'devAccountDetail',
      meta: {
        hideMenu: true,
        title: t('routes.demo.system.account_detail'),
        ignoreKeepAlive: true,
        showMenu: false,
        currentActiveMenu: '/devsys/account',
      },
      component: () => import('/@/views/devsys/system/user/AccountDetail.vue'),
    },
    {
      path: 'account_build/:id',
      name: 'devAccountBuild',
      meta: {
        hideMenu: true,
        title: t('routes.demo.system.account_build'),
        ignoreKeepAlive: true,
        showMenu: false,
        currentActiveMenu: '/devsys/account',
      },
      component: () => import('/@/views/devsys/system/user/AccountBuild.vue'),
    },
    {
      path: 'usersetting',
      name: 'AccountSettingPage',
      component: () => import('/@/views/devsys/system/user/setting/index.vue'),
      meta: {
        title: t('routes.demo.page.accountSetting'),
        ignoreKeepAlive: true,
        showMenu: false,
        hideMenu: true,
      },
    },
    {
      path: 'devchangePassword',
      name: 'devChangePassword',
      meta: {
        title: t('routes.demo.system.password'),
        ignoreKeepAlive: true,
        showMenu: false,
        hideMenu: true,
      },
      component: () => import('/@/views/devsys/system/password/index.vue'),
    },
    {
      path: 'datadic',
      name: 'DataDicManagement',
      meta: {
        title: t('routes.devsys.devsystem.datadicmanger'),
        ignoreKeepAlive: false,
      },
      component: () => import('/@/views/devsys/system/datadic/index.vue'),
    },
    {
      path: 'role',
      name: 'DevRoleManagement',
      meta: {
        title: t('routes.demo.system.role'),
        ignoreKeepAlive: true,
      },
      component: () => import('/@/views/devsys/system/role/index.vue'),
    },

    {
      path: 'menu',
      name: 'MenuManagement',
      meta: {
        title: t('routes.demo.system.menu'),
        ignoreKeepAlive: true,
      },
      component: () => import('/@/views/demo/system/menu/index.vue'),
    },
    {
      path: 'dept',
      name: 'DevDeptManagement',
      meta: {
        title: t('routes.demo.system.dept'),
        ignoreKeepAlive: true,
      },
      component: () => import('/@/views/devsys/system/depart/index.vue'),
    },
    {
      path: 'changePassword',
      name: 'ChangePassword',
      meta: {
        title: t('routes.demo.system.password'),
        ignoreKeepAlive: true,
      },
      component: () => import('/@/views/demo/system/password/index.vue'),
    },
    {
      path: 'loginlog',
      name: 'DevLoginLog',
      meta: {
        title: t('routes.demo.system.loginlog'),
        ignoreKeepAlive: false,
      },
      component: () => import('/@/views/devsys/system/devlogs/index.vue'),
    },
    {
      path: 'optionlog',
      name: 'DevOptionLog',
      meta: {
        title: t('routes.demo.system.optionlog'),
        ignoreKeepAlive: false,
      },
      component: () => import('/@/views/devsys/system/devlogs/OptionLogindex.vue'),
    },
  ],
};

export default system;
