import Vue from 'vue'
import App from './App.vue'
import router from './router'//这里的别名必须是router 不可以修改，不然会冲突报错
/* 导入全局样式 */
import './assets/css/global.css'
// 手动配置element-ui
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import './plugins/element.js'
/* 导入阿里字体图标 */
import './assets/fonts/iconfont.css'
/* tree-table */
import TreeTable from 'vue-table-with-tree-grid'

import _ from 'lodash'
import Store from './utils/store.js'
//import config from '../public/static/config.js'
import eIconPicker from 'e-icon-picker';
import "e-icon-picker/lib/symbol.js"; //基本彩色图标库
import 'e-icon-picker/lib/index.css'; // 基本样式，包含基本图标
import 'font-awesome/css/font-awesome.min.css'; //font-awesome 图标库
import 'element-ui/lib/theme-chalk/icon.css'; //element-ui 图标库
import {
  listToTree,
  getTreeParents,
  getListParents,
  getTreeParentsWithSelf,
} from './utils/tree'



/* 配置全局http 远程连接，并完成默认的连接前缀 */
import axios from 'axios'
axios.defaults.baseURL = window.config.baseUrl;

/*添加axios 拦截器  */
axios.interceptors.request.use(config => {
  //console.log(config)
  const tokenStr = window.sessionStorage.getItem('token')
  if (tokenStr != null) {
    config.headers.Authorization = 'Bearer ' + tokenStr
  }
  if (config.url === Vue.prototype.$gersonalManager_UploadImageUrl) {
    // 此处设置文件上传，配置单独请求头
    config.headers = {
      'Content-Type': 'multipart/form-data'
    }
    config.headers.Authorization = 'Bearer ' + tokenStr
  }

  //最后必须要加上这个
  return config
})

/* 使用treetable 名字是自定义的 */
Vue.component('tree-table', TreeTable)

Vue.prototype.defaultConfig=window.config
Vue.prototype.$http = axios
Vue.prototype._ = _
// 配置全局请求后端url路径
Vue.prototype.$loginUrl = "Identity/GetTokenByLogin"
Vue.prototype.$refreshTokenUrl = "Identity/RefreshToken"//刷新token
Vue.prototype.$getVerificationCodeUrl =axios.defaults.baseURL+ "Identity/GetVerificationCode"//获取验证码
Vue.prototype.$getKey =axios.defaults.baseURL+ "Identity/GetGuid"//获取唯一Key
Vue.prototype.$postRequest = async function (url, params) {
  const {
    data: res
  } = await this.$http.post(url, params)
  if (res.code !== 200) {
    return this.$message.error('获取数据失败！' + res.message)
  }
  var data = res.data.list
  return data
}

Vue.prototype.$getRequest = async function (url, params = '') {
    const paramsStr = ''
    if (params.length > 0) {
      paramsStr = "?" + params
    }
    const {
      data: res
    } = await this.$http.get(url + paramsStr)
    if (res.code !== 200) {
      return this.$message.error('获取数据失败！' + res.message)
    }
    var data = res.data.list
    return data
  },

  Vue.prototype.$initPermissions = function (permissions) {
    sessionStorage.removeItem("permissions");
    window.sessionStorage.setItem('permissions', JSON.stringify(permissions))

  },
  Vue.prototype.$getMenu = function () { //获得菜单数据  
    const tempData = JSON.parse(window.sessionStorage.getItem('permissions'))
    const menuTree = listToTree(tempData.filter(x => x.type === 2 || x.type === 1)) //返回权限类别为分组和菜单)
    return menuTree
  },


  Vue.prototype.$hasPermission = function(value) { //操作权限判断

    const permissions = JSON.parse(window.sessionStorage.getItem('permissions'))
    if (!(permissions instanceof Array)) {
      return false
    }
    let hasPermission = false
    if (value instanceof Array && value.length > 0) {
      hasPermission = permissions.some(permission => {
        return value.includes(permission.code)
      })
    } else {
      hasPermission = permissions.includes(value)
    }
    return hasPermission
  },

  

  


//1、用户管理
Vue.prototype.$userManagerUrl = "SysUser/GetPageUserList"
Vue.prototype.$userManager_GetUserUrl = "SysUser/Get"
Vue.prototype.$userManager_CreateUserUrl = "SysUser/CreateUser"
Vue.prototype.$userManager_EditUserUrl = "SysUser/UpdateUser"
Vue.prototype.$userManager_DeleteUserUrl = "SysUser/DeleteUserById"

//2、角色管理
Vue.prototype.$sysRoleManagerUrl = 'SysRole/GetPageSysRoleList'
Vue.prototype.$sysRoleManagerUrl_GetSysRoleListUrl = 'SysRole/GetSysRoleList'
Vue.prototype.$sysRoleManager_GetSysRoleUrl = 'SysRole/Get'
Vue.prototype.$sysRoleManager_CreateSysRoleUrl = 'SysRole/CreateSysRole'
Vue.prototype.$sysRoleManager_EditSysRoleUrl = 'SysRole/UpdateSysRole'
Vue.prototype.$sysRoleManager_DeleteSysRoleUrl = 'SysRole/DeleteSysRoleById'

//3、角色权限
Vue.prototype.$sysRoleManager_UpdateSysRolePermissionsUrl = 'SysRole/UpdateSysRolePermissions'

//4、权限管理
Vue.prototype.$sysPermissionManagerUrl = 'SysPermission/GetSysPermissionList'
Vue.prototype.$sysPermissionManager_GetSysPermissionFilterByPidUrl = 'SysPermission/GetSysPermissionFilterByPid'
Vue.prototype.$sysPermissionManager_GetSysPermissionUrl = 'SysPermission/Get'
Vue.prototype.$sysPermissionManager_CreateSysPermissionUrl = 'SysPermission/CreateSysPermission'
Vue.prototype.$sysPermissionManager_EditSysPermissionUrl = 'SysPermission/UpdateSysPermission'
Vue.prototype.$sysPermissionManager_DeleteSysPermissionUrl = 'SysPermission/DeleteSysPermissionById'

//5、字典管理
Vue.prototype.$sysDataDictionaryManagerUrl = 'SysDataDictionary/GetSysDataDictionaryList'
Vue.prototype.$sysDataDictionaryManager_GetSysDataDictionaryFilterByPidUrl = 'SysDataDictionary/GetSysDataDictionaryFilterByPid'
Vue.prototype.$sysDataDictionaryManager_GetSysDataDictionaryUrl = 'SysDataDictionary/Get'
Vue.prototype.$sysDataDictionaryManager_CreateSysDataDictionaryUrl = 'SysDataDictionary/CreateSysDataDictionary'
Vue.prototype.$sysDataDictionaryManager_EditSysDataDictionaryUrl = 'SysDataDictionary/UpdateSysDataDictionary'
Vue.prototype.$sysDataDictionaryManager_DeleteSysDataDictionaryUrl = 'SysDataDictionary/DeleteSysDataDictionaryById'

//6、审计日志 
Vue.prototype.$sysAuditLogManagerUrl='SysAuditLog/GetPageSysAuditLogList'
Vue.prototype.$sysAuditLogManager_GetSysAuditLogUrl='SysAuditLog/Get'


//6、组织机构管理
Vue.prototype.$sysOrganizationManagerUrl='SysOrganization/GetSysOrganizationList'
Vue.prototype.$sysOrganizationManager_GetSysOrganizationFilterByPidUrl='SysOrganization/GetSysOrganizationFilterByPid'
Vue.prototype.$sysOrganizationManager_GetSysOrganizationUrl='SysOrganization/Get'
Vue.prototype.$sysOrganizationManager_CreateSysOrganizationUrl='SysOrganization/CreateSysOrganization'
Vue.prototype.$sysOrganizationManager_EditSysOrganizationUrl ='SysOrganization/UpdateSysOrganization'
Vue.prototype.$sysOrganizationManager_DeleteSysOrganizationUrl='SysOrganization/DeleteSysOrganizationById'

//7.个人中心
Vue.prototype.$gersonalManager_GetUserInfoUrl='SysUser/GetUserInfo'
Vue.prototype.$gersonalManager_ChangePasswordUrl='SysUser/ChangePassword'
Vue.prototype.$gersonalManager_UploadImageUrl='Identity/UploadImage'


Vue.prototype.$bookManagerUrl='Book/GetPageBookList'

//8. 租户管理
Vue.prototype.$sysTenantManagerUrl='SysTenant/GetPageSysTenantList'
Vue.prototype.$sysTenantManager_GetSysTenantUrl='SysTenant/Get'
Vue.prototype.$sysTenantManager_CreateSysTenantUrl='SysTenant/CreateSysTenant'
Vue.prototype.$sysTenantManager_EditSysTenantUrl ='SysTenant/UpdateSysTenant'
Vue.prototype.$sysTenantManager_DeleteSysTenantUrl='SysTenant/DeleteSysTenantById'

//9. 流程管理
Vue.prototype.$processFlowManagerUrl='ProcessFlow/GetPageProcessFlowList'
Vue.prototype.$processFlowManager_GetProcessFlowUrl='ProcessFlow/Get'
Vue.prototype.$processFlowManager_CreateProcessFlowUrl='ProcessFlow/CreateProcessFlow'
Vue.prototype.$processFlowManager_EditProcessFlowUrl ='ProcessFlow/UpdateProcessFlow'
Vue.prototype.$processFlowManager_DeleteProcessFlowUrl='ProcessFlow/DeleteProcessFlowById'
Vue.prototype.$processFlowManager_GetAllProcessFlowUrl='ProcessFlow/GetAllProcessFlowList'

//8. 区块链存证
Vue.prototype.$bCEvidenceManagerUrl='BCEvidence/GetPageBCEvidenceList'
Vue.prototype.$bCEvidenceManager_GetBCEvidenceUrl='BCEvidence/Get'
Vue.prototype.$bCEvidenceManager_CreateBCEvidenceUrl='BCEvidence/CreateBCEvidence'
Vue.prototype.$bCEvidenceManager_EditBCEvidenceUrl ='BCEvidence/UpdateBCEvidence'
Vue.prototype.$bCEvidenceManager_DeleteBCEvidenceUrl='BCEvidence/DeleteBCEvidenceById'

//全局删除增加图标
Vue.use(eIconPicker, {
  FontAwesome: true,
  ElementUI: true,
  eIcon: true,//自带的图标，来自阿里妈妈
  eIconSymbol: true,//是否开启彩色图标
  addIconList: [],
  removeIconList: [],
  zIndex: 3100//选择器弹层的最低层,全局配置
});
Vue.use(ElementUI)
Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App),
  Store,
}).$mount('#app')
