import Vue from 'vue'
import Vuex from 'vuex'
import {
  listToTree,
  getTreeParents,
  getListParents,
  getTreeParentsWithSelf,
} from '../utils/tree.js'
Vue.use(Vuex)

//目前由于前端获取不到$store,暂时还无法解决相关数据问题，所以直接在main.js 定义全局方法去获取
export default new Vuex.Store({
    // state 中存放的就是全局共享的数据
    state: {
      permissionList: []
    },
     
    mutations: {//同步操作
        initData (state, permissions) {
          sessionStorage.removeItem("permissions");
          window.sessionStorage.setItem('permissions', JSON.stringify(permissions))
          //state.permissionList=permissions//state 会因为刷新而消失
        },
      },
    actions: {//异步
        initDataAsync (context, permissions) {
            context.commit('initData', permissions)
        }},
        getters: {//返回数据，包装器处理用于对 Store 中的数据进行加工处理形成新的数据。不会修改 state 里的源数据，只起到一个包装器的作用，将 state 里的数据变一种形式然后返回出来
            getMenu(state){//获得菜单数据
              
              const tempData=JSON.parse(window.sessionStorage.getItem('permissions'))
             const menuTree= listToTree(tempData.filter(x=>x.type===2||x.type===1))//返回权限类别为分组和菜单)
             return menuTree
            },
            
            hasPermission:(state,permissionCode)=>{//权限判断

                const tempPermission= JSON.parse(window.sessionStorage.getItem('permissions'))
                return tempPermission.some(x=>x.code===permissionCode)//返回是否拥有权限

            },
            permissions:(state)=>{

              const tempPermission= JSON.parse(window.sessionStorage.getItem('permissions'))
              return tempPermission

            }
          }
  })

