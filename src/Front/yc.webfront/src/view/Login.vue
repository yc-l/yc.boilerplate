<template>
  <div
    class="login_container"
    v-loading="loading"
    element-loading-text="登录中......"
    element-loading-spinner="el-icon-loading"
    element-loading-background="rgba(0, 0, 0, 0.7)"
  >
    <div class="login_box">
      <!-- 头像区域 -->
      <div class="avatar_box">
        <img src="../assets/images/logos/logo-4.png" alt="" />
        <h2>系统登录</h2>
      </div>

      <!-- 表单区域 -->
      <el-form
        label-width="0px"
        ref="loginFormRef"
        :model="loginForm"
        :rules="loginFormRules"
        class="login_form"
        style="margin-top: -25px"
      >
        <el-form-item prop="tenantId">
          <el-select
            v-model="loginForm.tenantId"
            placeholder="请选择租户"
            style="width: 100%"
            v-if="isMultiTnancy"
            @change="selectTenantIdChanged"
          >
            <el-option label="租户1" :value="1"></el-option>
            <el-option label="租户2" :value="2"></el-option>
          </el-select>
        </el-form-item>

        <!-- 用户名 prop 是验证展示 对应的rules 绑定校验 -->
        <el-form-item prop="userName">
          <el-input
            v-model="loginForm.userName"
            prefix-icon="iconfont icon-users"
          ></el-input>
        </el-form-item>
        <!-- 密码 -->
        <el-form-item prop="password">
          <el-input
            v-model="loginForm.password"
            prefix-icon="iconfont icon-3702mima"
            type="password"
          ></el-input>
        </el-form-item>

        <!-- 登录按钮 -->
        <el-form-item class="btns">
          <el-button type="primary" @click="login">登录</el-button>
          <el-button type="info" @click="resetLoginForm">重置</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
  export default {
    created(){
     this.init() 
    },
    data() {
      return {
        /* 这是登录表单的数据对象绑定 */
        loginForm: {
          userName: 'admin',
          password: '123456',
          tenantId: 1
        },
        loading: false,
        isMultiTnancy:true,//是否开启多租户

        /* 表单规则验证 */
        loginFormRules: {
          userName: [
            /* 和prop 属性中的userName 对应 */
            {
              required: true,
              message: '请输入用户名',
              trigger: 'blur'
            },
            {
              min: 5,
              max: 20,
              message: '长度在 5 到 20个字符',
              trigger: 'blur'
            }
          ],
          password: [
            /* 和prop 属性中的password 对应 */
            {
              required: true,
              message: '请输入密码',
              trigger: 'blur'
            },
            {
              min: 6,
              max: 15,
              message: '长度在 6 到 15个字符',
              trigger: 'blur'
            }
          ]
        }
      }
    },
    methods: {
       init(){
        if(!this.isMultiTnancy){

          this.loginForm.tenantId=0
        }

       },
      /* 重置事件 */
      resetLoginForm() {
        /* console.log(this); */
        this.$refs.loginFormRef.resetFields()
      },
      /* 登录事件 */
      login() {
        this.$refs.loginFormRef.validate(async (valid, validateObj) => {
          /*  console.log(isSucessed);
          console.log("验证不通过对象：" + validateObj); */
          if (!valid) return
          this.loading = true
          const {
            data: responseData
          } = await this.$http.post(this.$loginUrl, {
            userId: this.loginForm.userName,
            pwd: this.loginForm.password,
            tenantId: this.loginForm.tenantId
          })
          this.loading = false
          console.log(responseData)
          if (responseData.code != 200) return this.$msg.error(responseData.message)
          this.$msg.success('登录成功！')
          //将当前的token 存储到sessionStorage
          window.sessionStorage.clear()
             window.sessionStorage.setItem("tokenTime", new Date())//当前登录事件
          window.sessionStorage.setItem('token', responseData.data.token)
          //相关的全局权限处理
          this.$initPermissions(responseData.data.permissionList)
          //this.$store.dispatch('initDataAsync',responseData.data.permissionList) //同步数据到vuex中
          console.log("权限列表:" + responseData.data.permissionList);
          this.$router.push('/home') /* 跳转到后台页面 */
        })
      },
      selectTenantIdChanged() {
        if (this.loginForm.tenantId == 2) {
          this.loginForm = {
            userName: 'T2admin',
            password: '111111',
            tenantId: 2
          }
        } else if (this.loginForm.tenantId == 1) {

          this.loginForm = {
            userName: 'admin',
            password: '123456',
            tenantId: 1
          }
        }




      },
    
    }
  }

</script>

<!-- 一定要加入scoped 标识当前组件自己使用，去掉的话，会是全局-->
<style lang="less" scoped>
.login_container {
  background-color: #2b4b6b;
  height: 100%;
  //background-image: url('../assets/images/bg/bg-5.jpg');
  background-size: 100% 100%;
  background-size: cover;
}

.login_box {
  width: 380px;
  height: 420px;
  background-color: #ffff;
  /*   background-image: url("../assets/images/bg/bg-5.jpg");
  background-size: cover; */
  border-radius: 3px;
  position: absolute;
  /**绝对定位**/
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);

  /** left 和top 需要和它配置才可以在中间位置**/
  h2 {
    color: rgb(31, 28, 28);
  }
}

.avatar_box {
  height: 100px;
  width: 100px;
  border: 1px solid #eee;
  border-radius: 50%;
  /**圆角**/
  padding: 10px;
  box-shadow: 0 0 10px #ddd;
  /**加入阴影**/
  position: absolute; //绝对定位
  left: 50%;
  transform: translate(-50%, -50%);
  /* 位移 */
  background-color: #fff;

  img {
    width: 100%;
    height: 100%;
    border-radius: 50%;
    /**圆角**/
    background-color: #eee;
  }
}

.loginTitle {
  /* font-display: ; */
}

.btns {
  display: flex;
  /* 弹性盒子模型 */
  justify-content: flex-end;
  /* 尾部对齐 */
}

.login_form {
  position: absolute;
  bottom: 0;
  width: 100%;
  padding: 0 20px;
  box-sizing: border-box;
  top: 40%;
}
</style>
