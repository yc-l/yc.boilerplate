<template>
  <el-container class="home-container" style="overflow-y:visible;">
    <!-- header区域 -->
    <el-header>
      <div>
        <h3>{{defaultConfig.systemName}}</h3>
          <div class="toggle-button" @click="toggleCollapse">
             <i :class="isCollapse? 'el-icon-s-unfold' : 'el-icon-s-fold'  "></i>
          </div>
       
      </div>
    
      <el-menu
        mode="horizontal"
        background-color="#118eeb"
        text-color="#fff"
      active-text-color="#bdbdbe"
      >
       <el-menu-item index="1"> <a
       style="text-decoration:none"
              target="_blank"
              :href="defaultConfig.githubSourceCodeUrl"
            >
              <img
                style="margin-right: 10px"
                :src='defaultConfig.githubStarUrl'
              />
                <img
                style="margin-right: 10px"
                :src='defaultConfig.githubForkUrl'
              />
            </a></el-menu-item>
       
        <el-submenu index="2" style="color:#fff;">
          <template slot="title" style="color:#fff;">我的工作台</template>
          <el-menu-item index="2-1">
            <i class="el-icon-user"></i>
            <span slot="title">个人中心</span>
          </el-menu-item>
          <el-menu-item index="2-3" @click="refreshToken">
            <i class="el-icon-refresh"></i>刷新token</el-menu-item
          >
        </el-submenu>
        <el-menu-item index="2-2" @click="loginOut">
          <i class="el-icon-switch-button"></i>退出登录</el-menu-item
        >
         <el-menu-item class="themeBtn" index="2-3" @click="drawer = true" type="primary">
      
          <i class="el-icon-eleme" ></i>
         
          </el-menu-item>
       
      </el-menu>
      <el-drawer
  title="配色主题"
  :visible.sync="drawer"
  :direction="direction"
  size="15%">
  <span></span>
</el-drawer>
    </el-header>
    <!-- 下部区域 内部分为 左侧和中间区域 -->
    <el-container style="overflow-y:visible;">
      <!-- 下部区域 左侧区域 -->
      <el-aside :width="isCollapse ? '64px' : '230px'">
        <el-menu
          class="el-menu-vertical"
          :unique-opened="true"
          :collapse="isCollapse"
          :collapse-transition="false"
          router
          :default-active="activePath"
        >
          <el-menu-item
            :index="item.path + ''"
            v-for="item in this.menuListNoChild"
            :key="item.id"
          >
            <i :class="item.icon"></i>
            <span slot="title">{{ item.label }}</span>
          </el-menu-item>

          <!-- 一级菜单 -->
          <el-submenu
            :index="item.id + ''"
            v-for="item in this.menuListHaveChild"
            :key="item.id"
          >
            <!-- 一级菜单的模板区域 -->
            <template slot="title">
              <!-- 图标 -->
              <i :class="item.icon"></i>
              <!-- 文本 -->
              <span>{{ item.label }}</span>
            </template>

            <!-- 二级菜单 -->
            <el-menu-item
              :index="subItem.path"
              v-for="subItem in item.children"
              :key="subItem.id"
              @click="saveNavState(subItem.path)"
            >
              <template slot="title">
                <!-- 图标 -->
                <i :class="subItem.icon"></i>
                <!-- 文本 -->
                <span>{{ subItem.label }}</span>
              </template>
            </el-menu-item>
          </el-submenu>
        </el-menu>
      </el-aside>
      <!-- 下部区域的中间区域 -->
      <el-main style="overflow-y:visible;">
        <div style="min-height:100%;">
           <router-view></router-view>
        </div>
       
        <el-footer>Copyright 2020-{{fullYear}} {{defaultConfig.systemName}} {{defaultConfig.frameWorkName}}.AllRightsReserved.</el-footer>
      </el-main>
       <el-backtop />
    </el-container>
  </el-container>
</template>

<script>
  export default {
    data() {
      return {
        isCollapse: false,
        fullYear: new Date().getFullYear(),
        menuList: [], //菜单数据
        menuListNoChild: [], //菜单数据，没有子节点
        menuListHaveChild: [], //菜单数据，有子节点
        // 被激活的链接地址
        activePath: '',
        // 是否折叠
        isCollapse: false,
        systemName:"",
        drawer: false,
        direction: 'rtl',
      }
    },
    created() {
      this.menuList = this.$getMenu()
      this.menuListNoChild = _.cloneDeep(this.menuList).filter(x => x.children == undefined)
      this.menuListHaveChild = _.cloneDeep(this.menuList).filter(x => x.children !== undefined)
      this.activePath = window.sessionStorage.getItem('activePath')
    },
    methods: {
      loginOut() {
        window.sessionStorage.clear()
        this.$router.push('/login')
      },


      // 点击按钮，切换菜单的折叠与展开
      toggleCollapse() {
        this.isCollapse = !this.isCollapse
      },

      // 保存链接的激活状态
      saveNavState(activePath) {
        window.sessionStorage.setItem('activePath', activePath)
        this.activePath = activePath
        // console.log("激活的菜单:"+activePath)
      },
       //刷新token
      async refreshToken() {
        const tokenTime = new Date(window.sessionStorage.getItem('tokenTime')).getTime() //获取teken存储时间
        const nowDate = new Date().getTime() //当前时间
        var m = parseInt(Math.abs(nowDate - tokenTime) / 1000 / 60) //分钟
        if (m >=60) { //超过60分钟
          const token = window.sessionStorage.getItem('token')
          const {
            data: res
          } = await this.$http.post(this.$refreshTokenUrl+"?token="+token)
          if (res.code !== 200) {
            return this.$message.error('获取数据失败！' + res.message)
          }
          const refreshToken = res.data
          if (refreshToken !== null) { //成功获取

            window.sessionStorage.setItem('token', refreshToken)
            window.sessionStorage.setItem('tokenTime', new Date())

          }

        }
      },

    },
     // 点击按钮，切换菜单的折叠与展开
    toggleCollapse() {
      this.isCollapse = !this.isCollapse
    },
  }

</script>

<style lang="less" scoped>
.home-container {
  height: 100%;

}

//左侧菜单ui
/* 深选择器：如果相对设置了scoped的子组件里的元素进行控制可以使用'>>>'或者'deep'设置选中或悬浮的颜色*/
.el-submenu /deep/ .el-submenu__title i {
   color: #bdbdbe;
  }

.el-submenu /deep/ el-submenu__title{

  //border-bottom-color: rgb(255, 255, 255);
    color: rgb(255, 255, 255);
    background-color: rgb(17, 142, 235);
}

//左侧菜单ui
.el-menu-item i {
    color: #bdbdbe;
}

.el-header {
  background-color: #118eeb;
  display: flex;
  justify-content: space-between;
  padding-left: 0;
  align-items: center;
  height: 20px;

  > div {
    display: flex;
    align-items: center;

    h3 {
      color: white;
      margin-left: 5px;
    }
  }
}

//设置左侧菜单
.el-aside {
  background-color: #ffff;

  .el-menu {
    border-right: none;
  }
}

.el-menu:link{

   background-color: #fff;
}

.el-menu:hover {
  background-color: #fff;
}

.el-main {
  background-color: #f2f3f8;
}
.toggle-button {
  //background-color: #4a5064;
  font-size: 25px;
  line-height: 24px;
  color: #fff;
  text-align: center;
  letter-spacing: 0.2em;
  cursor: pointer;
  margin-left: 15px;
}
.el-footer {
    background-color: #fff;
    color: #333;
    text-align: center;
    line-height: 60px;
 
    //bottom: 0px;

  }
</style>
