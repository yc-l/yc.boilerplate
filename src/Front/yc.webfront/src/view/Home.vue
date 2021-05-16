<template>
  <el-container class="home-container">
    <!-- header区域 -->
    <el-header>
      <div>
        <h3>元磁之力快速开发系统</h3>
      </div>

      <el-menu
        mode="horizontal"
        background-color="#118eeb"
        text-color="#fff"
        active-text-color="#303133"
      >
        <el-submenu index="2" style="">
          <template slot="title">我的工作台</template>
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
      </el-menu>
    </el-header>
    <!-- 下部区域 内部分为 左侧和中间区域 -->
    <el-container>
      <!-- 下部区域 左侧区域 -->
      <el-aside width="230px">
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
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>
<script>
  export default {
    data() {
      return {
        isCollapse: false,

        menuList: [], //菜单数据
        menuListNoChild: [], //菜单数据，没有子节点
        menuListHaveChild: [], //菜单数据，有子节点
        // 被激活的链接地址
        activePath: '',
        // 是否折叠
        isCollapse: false,
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
  }

</script>

<style lang="less" scoped>
.home-container {
  height: 100%;
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

.el-aside {
  background-color: #ffff;

  .el-menu {
    border-right: none;
  }
}

.el-menu:hover {
  background-color: #fff;
}

.el-main {
  background-color: #f2f3f8;
}
</style>
