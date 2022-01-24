<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>用户管理</el-breadcrumb-item>
      <el-breadcrumb-item>用户列表</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 搜索与添加区域 -->

      <el-row :gutter="20">
        <el-col :span="7">
          <el-input placeholder="请输入搜索的名字或者手机号" class="input-with-select" v-model="queryInfo.query" clearable
            @clear="getUserList">
            <el-button slot="append" icon="el-icon-search" @click="getUserList"></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" @click="showAddOrEditDialog()" v-if="$hasPermission(['userManager.createUser'])">
            添加用户</el-button>
        </el-col>
      </el-row>
      <!-- 用户列表 -->
      <el-table :data="userList" style="width: 100%" border stripe>
        <el-table-column type="index" width="60" label="序号">
        </el-table-column>
        <el-table-column prop="account" label="账号" width="180">
        </el-table-column>
        <el-table-column prop="name" label="姓名"> </el-table-column>
        <el-table-column label="性别">
          <template slot-scope="scope">
            <el-tag :type="scope.row.sex === 0 ? 'primary' : 'warning'">{{
              scope.row.sex === 0 ? '男' : '女'
            }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="mobile" label="手机号"> </el-table-column>
        <el-table-column prop="email" label="邮箱"> </el-table-column>
        <el-table-column prop="remark" label="备注" width="180">
        </el-table-column>
        <el-table-column label="操作" width="360"
          v-if="$hasPermission(['userManager.deleteUser'])||$hasPermission(['userManager.editUser'])">
          <template slot-scope="scope">
            <!-- 修改按钮 -->
            <el-button type="primary" icon="el-icon-edit" size="mini" @click="showAddOrEditDialog(scope.row.id)"
              v-if="$hasPermission(['userManager.editUser'])">编辑
            </el-button>
            <!-- 删除按钮 -->
            <el-button type="danger" icon="el-icon-delete" size="mini" @click="removeUserById(scope.row.id)"
              v-if="$hasPermission(['userManager.deleteUser'])">删除
            </el-button>
            <!-- 分配角色按钮 -->
            <!--  <el-tooltip
              effect="dark"
              content="分配角色"
              placement="top"
              :enterable="false"
            >
              <el-button
                type="warning"
                icon="el-icon-setting"
                size="mini"
                @click="setRole(scope.row)"
                >分配角色</el-button
              >
            </el-tooltip> -->
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
        :current-page="queryInfo.pageNum" :page-sizes="[1, 2, 5, 10]" :page-size="queryInfo.pageSize"
        layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </el-card>

    <!-- 添加用户的对话框 -->
    <el-dialog :title="(addOrEditForm.id > 0 ? '编辑' : '新增') + '用户'" :visible.sync="addOrEditDialogVisible" width="60%"
      @close="addOrEditDialogClosed">
      <el-form :model="addOrEditForm" :rules="addOrEditFormRules" ref="addOrEditFormRef" label-width="100px">
        <!-- 内容主体区域 -->

        <el-tabs v-model="activeName">
          <el-tab-pane label="用户管理" name="first">
            <el-input v-model="addOrEditForm.id" prop="id" type="hidden"></el-input>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="账号：" prop="account">
                  <el-input v-model="addOrEditForm.account"></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="密码：" prop="password">
                  <el-input v-model="addOrEditForm.password" type="password" :disabled="addOrEditForm.id > 0"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="昵称：" prop="userName">
                  <el-input v-model="addOrEditForm.userName"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="性别：" prop="sex">
                  <el-radio-group v-model="addOrEditForm.sex">
                    <el-radio :label="0">男</el-radio>
                    <el-radio :label="1">女</el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="邮箱：" prop="email">
                  <el-input v-model="addOrEditForm.email"></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="手机：" prop="mobile">
                  <el-input v-model="addOrEditForm.mobile"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="24">
                <el-form-item label="备注：" prop="remark">
                  <el-input type="textarea" v-model="addOrEditForm.remark"></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>
          <el-tab-pane label="角色管理" name="third">
            <el-row :gutter="20">
              <el-col :span="24">
                <el-form-item label="授权角色：">
                  <template>
                    <el-checkbox-group v-model="addOrEditForm.userRoleIds">
                      <el-checkbox v-for="item in this.roleDataList" :label="item.id" :key="item.id">{{ item.name }}
                      </el-checkbox>
                    </el-checkbox-group>
                  </template>
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>
        </el-tabs>
      </el-form>
      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditUser()">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
  export default {

    data() {
      // 验证邮箱的规则
      var checkEmail = (rule, value, cb) => {
        // 验证邮箱的正则表达式
        const regEmail = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/

        if (regEmail.test(value)) {
          // 合法的邮箱
          return cb()
        }

        cb(new Error('请输入合法的邮箱'))
      }

      // 验证手机号的规则
      var checkMobile = (rule, value, cb) => {
        // 验证手机号的正则表达式
        const regMobile = /^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/

        if (regMobile.test(value)) {
          return cb()
        }

        cb(new Error('请输入合法的手机号'))
      }

      return {
        queryInfo: {
          query: '',
          pageNum: 1,
          pageSize: 10,
        },
        userList: [],
        total: 0,
        activeName: 'first',
        // 控制添加用户对话框的显示与隐藏
        addOrEditDialogVisible: false,
        // 添加用户的表单数据
        addOrEditForm: {
          id: '',
          userName: '',
          password: '',
          email: '',
          mobile: '',
          sex: 0,
          account: '',
          remark: '',
          userRoleIds: [], //用户所拥有的角色
        },
        editState: false, //编辑状态
        roleDataList: [], //角色集合
        // 添加表单的验证规则对象
        addOrEditFormRules: {
          userName: [{
              required: true,
              message: '请输入用户名',
              trigger: 'blur'
            },
            {
              min: 3,
              max: 10,
              message: '用户名的长度在5~10个字符之间',
              trigger: 'blur',
            },
          ],
          password: [{
              required: true,
              message: '请输入密码',
              trigger: 'blur'
            },
            {
              min: 6,
              max: 100,
              message: '密码的长度在100个字符之间',
              trigger: 'blur',
            },
          ],
          email: [{
              required: true,
              message: '请输入邮箱',
              trigger: 'blur'
            },
            {
              validator: checkEmail,
              trigger: 'blur'
            },
          ],
          mobile: [{
              required: true,
              message: '请输入手机号',
              trigger: 'blur'
            },
            {
              validator: checkMobile,
              trigger: 'blur'
            },
          ],
          userName: [{
              required: true,
              message: '请输入用户名',
              trigger: 'blur'
            },
            {
              min: 3,
              max: 10,
              message: '用户名的长度在3~10个字符之间',
              trigger: 'blur',
            },
          ],

          account: [{
              required: true,
              message: '请输入账号',
              trigger: 'blur'
            },
            {
              min: 3,
              max: 10,
              message: '账号的长度在3~10个字符之间',
              trigger: 'blur',
            },
          ],

          remark: [{
              required: false,
              message: '请输入备注',
              trigger: 'blur'
            },
            {
              max: 128,
              message: '备注的长度在128个字符之内',
              trigger: 'blur',
            },
          ],
        },
        // 控制修改用户对话框的显示与隐藏
        editDialogVisible: false,
        // 查询到的用户信息对象
        editForm: {},
        // 修改表单的验证规则对象
        editFormRules: {
          email: [{
              required: true,
              message: '请输入用户邮箱',
              trigger: 'blur'
            },
            {
              validator: checkEmail,
              trigger: 'blur'
            },
          ],
          mobile: [{
              required: true,
              message: '请输入用户手机',
              trigger: 'blur'
            },
            {
              validator: checkMobile,
              trigger: 'blur'
            },
          ],
        },
        // 控制分配角色对话框的显示与隐藏
        setRoleDialogVisible: false,
        // 需要被分配角色的用户信息
        userInfo: {},
        // 已选中的角色Id值
        selectedRoleId: '',
      }
    },
    created() {
      this.getUserList()
    },
    methods: {
      async getUserList() {
        const {
          data: res
        } = await this.$http.post(this.$userManagerUrl, {
          currentPage: this.queryInfo.pageNum,
          pageSize: this.queryInfo.pageSize,
          filter: {
            queryString: this.queryInfo.query,
          },
        })
        if (res.code !== 200) {
          return this.$message.error('获取用户列表失败！' + res.message)
        }
        this.userList = res.data.list
        this.total = res.data.total
        console.log(res)
      },
      // 监听 pagesize 改变的事件
      handleSizeChange(newSize) {
        // console.log(newSize)
        this.queryInfo.pageSize = newSize
        this.getUserList()
      },
      // 监听 页码值 改变的事件
      handleCurrentChange(newPage) {
        console.log(newPage)
        this.queryInfo.pageNum = newPage
        this.getUserList()
      },
      // 监听添加用户对话框的关闭事件
      addOrEditDialogClosed() {
        this.editState = false //编辑状态改为false
        this.addOrEditForm = {
            id: '',
            userName: '',
            password: '',
            email: '',
            mobile: '',
            sex: 0,
            account: '',
            remark: '',
            userRoleIds: [],
          },
          this.roleDataList = [] //重置添加和编辑角色列表为空
        this.$refs.addOrEditFormRef.resetFields()
      },

      async getRoleList() {
        var data = await this.$getRequest(this.$sysRoleManagerUrl_GetSysRoleListUrl)
        this.roleDataList = data
      },
      // 点击按钮，添加用户
      addOrEditUser() {
        console.log("user Form的值：" + this.addOrEditForm)
        this.$refs.addOrEditFormRef.validate(async (valid) => {
          if (!valid) return
          if (this.addOrEditForm.id > 0) {
            //编辑状态
            // 发起修改用户信息的数据请求
            const resultData = await this.$http.put(
              this.$userManager_EditUserUrl,
              this.addOrEditForm 
            )
            console.log(resultData)
            const {
              data: res
            } = resultData
            if (res.code !== 200) {

              console.log("返回结果：" + res)
              return this.$message.error('更新用户信息失败！' + res.message)
            }

            // 提示修改成功
            this.$message.success('更新用户信息成功！')
          } else {
            // 可以发起添加用户的网络请求
            const {
              data: res
            } = await this.$http.post(
              this.$userManager_CreateUserUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              this.$message.error('添加用户失败！' + res.message)
            }

            this.$message.success('添加用户成功！')
          }

          // 隐藏添加用户的对话框
          this.addOrEditDialogVisible = false
          // 重新获取用户列表数据
          this.getUserList()
        })
      },
      // 根据Id删除对应的用户信息
      async removeUserById(id) {
        // 弹框询问用户是否删除数据
        const confirmResult = await this.$confirm(
          '此操作将永久删除该用户, 是否继续?',
          '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
          }
        ).catch((err) => err)

        // 如果用户确认删除，则返回值为字符串 confirm
        // 如果用户取消了删除，则返回值为字符串 cancel
        // console.log(confirmResult)
        if (confirmResult !== 'confirm') {
          return this.$message.info('已取消删除')
        }

        const {
          data: res
        } = await this.$http.delete(
          this.$userManager_DeleteUserUrl + '?id=' + id
        )

        if (res.code !== 200) {
          return this.$message.error('删除用户失败！' + res.message)
        }

        this.$message.success('删除用户成功！')
        this.getUserList()
      },

      // 展示用户的对话框
      async showAddOrEditDialog(id) {
        this.addOrEditDialogVisible = true
        this.getRoleList() //获取角色列表
        if (id > 0) {
          //编辑状态
          // console.log(id)
          const {
            data: res
          } = await this.$http.get(
            this.$userManager_GetUserUrl + '?id=' + id
          )

          if (res.code !== 200) {
            console.log("返回结果：" + res)
            return this.$message.error('查询用户信息失败！' + res.message)
          }

          this.addOrEditForm = res.data
          this.editState = true
        }

        this.addOrEditDialogVisible = true
      },


    },
  }

</script>
<style lang="less" scoped>
</style>
