import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../view/Login.vue'
import Home from '../view/Home.vue'
import Welcome from '../view/Welcome'
import Users from '../view/User/Users'
import SysRoles from '../view/SysRole/SysRoles'
import SysRolePemission from '../view/SysRolePermission/SysRolePermissions'
import SysPermissions from '../view/SysPermission/SysPermissions'
import SysDataDictionarys from '../view/SysDataDictionary/SysDataDictionarys'
import NoPermission from '../view/NoPermission.vue'
import SysAuditLogs from '../view/SysAuditLog/SysAuditLogs'
import SysOrganizations from '../view/SysOrganization/SysOrganizations'
import Board from '../view/Board'


Vue.use(VueRouter)

const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', name: 'Login', component: Login },
  { path: '/noPermission', name: 'noPermission', component: NoPermission },
  
  {
    path: '/home',
    name: 'home',
    component: Home,
    redirect: '/welcome',
    children: [
      { path: '/welcome', component: Welcome },
      { path: '/users', component: Users },
      { path: '/sysRoles', component: SysRoles },
      { path: '/sysRolePemissions', component: SysRolePemission },
      { path: '/sysPermissions', component: SysPermissions },
      { path: '/sysDataDictionarys', component: SysDataDictionarys },
      { path: '/sysAuditLogs', component: SysAuditLogs },
      { path: '/sysOrganizations', component: SysOrganizations },
      { path: '/board', component: Board },  

    ]
  }    
]

const router = new VueRouter({
  mode: 'hash',
  routes: routes
})

/* 添加导航守卫 */
router.beforeEach((to, from, next) => {
  if (to.path === '/login') return next() /*默认登录直接放行  */
  const tokenStr = window.sessionStorage.getItem('token')
  if (!tokenStr) return next('/login') /* token 为空等，需要跳转到登录 */
   //菜单权限过滤
  const tempData = JSON.parse(window.sessionStorage.getItem('permissions'))
  const hasPermission = tempData.some(x => x.path.indexOf(to.path)!==-1)
  if (!(hasPermission&&to.path!=='/home')) return next('/noPermission')//如果没有权限就直接让他跳转没有权限页面
  
  next() /* 原先正常跳转 */
})



export default router
