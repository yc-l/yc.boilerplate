<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>租户管理管理</el-breadcrumb-item>
      <el-breadcrumb-item>租户管理列表</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 搜索与添加区域 -->

      <el-row :gutter="20">
        <el-col :span="7">
          <el-input placeholder="请输入搜索的内容" class="input-with-select" v-model="queryInfo.query" clearable
            @clear="getSysTenantList">
            <el-button slot="append" icon="el-icon-search" @click="getSysTenantList"></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" v-if="$hasPermission(['sysTenantManager.createSysTenant'])"
            @click="showAddOrEditDialog()">添加租户</el-button>
        </el-col>
      </el-row>
      <!-- 租户管理列表 -->
      <el-table :data="sysTenantList" style="width: 100%" border stripe>
        <el-table-column type="index" width="60" label="序号"> </el-table-column>
        <el-table-column prop='tenantId' width="60" label='租户Id'></el-table-column>
        <el-table-column prop='tenantName' label='租户名称'></el-table-column>
        <el-table-column prop='dbType'  label='数据库类别'>
        <template slot-scope="scope">
        <el-popover trigger="hover" placement="top">
         <p>标识: {{ scope.row.dbType }}</p>
         
          <div slot="reference" class="name-wrapper">
            <el-tag size="medium">{{ scope.row.dbTypeName}}</el-tag>
          </div>
        </el-popover>
      </template>

        </el-table-column>
        <el-table-column prop='dbConnectionString' width="550px" label='数据库连接字符串'></el-table-column>
        <el-table-column prop='isActive' label='是否启用'>
          <template slot-scope="scope">
            <el-tag :type="scope.row.isActive === true ? 'primary' : 'warning'">{{
              scope.row.isActive === true ? '是' : '否'
            }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop='isDefaultTenant' label='是否默认租户'>
          <template slot-scope="scope">
            <el-tag :type="scope.row.isDefaultTenant === true ? 'primary' : 'warning'">{{
              scope.row.isDefaultTenant === true ? '默认租户' : '普通租户'
            }}</el-tag>
          </template>
        </el-table-column>

        <el-table-column label="操作" width="220"
          v-if="$hasPermission(['sysTenantManager.editSysTenant'] || $hasPermission(['sysTenantManager.deleteSysTenant']))">
          <template slot-scope="scope">
            <!-- 修改按钮 -->
            <el-button type="primary" icon="el-icon-edit" size="mini" @click="showAddOrEditDialog(scope.row.id)"
              v-if="$hasPermission(['sysTenantManager.editSysTenant'])">编辑</el-button>
            <!-- 删除按钮 -->
            <el-button type="danger" icon="el-icon-delete" size="mini" @click="removeSysTenantById(scope.row.id)"
              v-if="$hasPermission(['sysTenantManager.deleteSysTenant'])">删除</el-button>

          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
        :current-page="queryInfo.pageNum" :page-sizes="[5, 10, 50, 100]" :page-size="queryInfo.pageSize"
        layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </el-card>

    <!-- 添加或编辑租户管理的对话框 -->
    <el-dialog :title="(addOrEditForm.id > 0 ? '编辑' : '新增') + '租户管理'" :visible.sync="addOrEditDialogVisible" width="60%"
      @close="addOrEditDialogClosed">
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="租户管理管理" name="first">
          <el-form :model="addOrEditForm" :rules="addOrEditFormRules" ref="addOrEditFormRef" label-width="100px">
            <el-input v-model="addOrEditForm.id" prop="id" type="hidden"></el-input>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='租户Id：' prop='tenantId'>
                  <el-input v-model='addOrEditForm.tenantId'></el-input>
                </el-form-item>
              </el-col>
              <el-col :span='12'>
                <el-form-item label='租户名称：' prop='tenantName'>
                  <el-input v-model='addOrEditForm.tenantName'></el-input>
                </el-form-item>
              </el-col>

            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='数据库类别：' prop='dbType'>

                  <el-select v-model="addOrEditForm.dbType" placeholder="请选择">
                    <el-option v-for="item in dbTypeOptions" :key="item.value" :label="item.label" :value="item.value">
                    </el-option>
                  </el-select>

                </el-form-item>
              </el-col>
              <el-col :span='12'>
               
                  <el-form-item label=" 是否启用" prop="isActive">
                    <el-switch v-model="addOrEditForm.isActive" active-text="是" inactive-text="否">
                    </el-switch>
                  </el-form-item>
              
              </el-col>



            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='是否默认租户：' prop='isDefaultTenant'>
                
                    <el-switch v-model="addOrEditForm.isDefaultTenant" active-text="是" inactive-text="否">
                    </el-switch>
                </el-form-item>
                
              </el-col>
            
            </el-row>

            <el-row :gutter='20'>
              <el-col :span='20'>
                <el-form-item label='数据库连接字符串：' prop='dbConnectionString'>
                  <el-input type="textarea" v-model='addOrEditForm.dbConnectionString'></el-input>
                </el-form-item>
              </el-col>



            </el-row>


          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditSysTenant()">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
  export default {
    data() {

      return {

        dbTypeOptions: [{
          value: 0,
          label: 'Mysql'
        }, {
          value: 1,
          label: 'SqlServer'
        }, {
          value: 2,
          label: 'PostgreSQL'
        }],

        queryInfo: {
          query: '',
          pageNum: 1,
          pageSize: 10,
        },
        sysTenantList: [],
        total: 0,
        activeName: 'first',
        // 控制添加租户管理对话框的显示与隐藏
        addOrEditDialogVisible: false,
        // 添加租户管理的表单数据
        addOrEditForm: {
          id: '',
          tenantName: '',
          dbType: 0,
          dbConnectionString: '',
          isActive: true,
          isDefaultTenant: true,
          tenantId: 0,

        },
        editState: false, //编辑状态

        // 添加表单的验证规则对象
        addOrEditFormRules: {
          tenantId: [{
            required: true,
            message: '请输入租户Id',
            trigger: 'blur'
          }],
          tenantName: [{
              required: true,
              message: '请输入租户名称',
              trigger: 'blur'
            },
            {
              max: 100,
              message: '租户名称的长度在100字符之内',
              trigger: 'blur'
            },
          ],
          dbType: [{
            required: false,
            message: '请输入数据库类别',
            trigger: 'blur'
          }, ],
          dbConnectionString: [{
              required: false,
              message: '请输入数据库连接字符串',
              trigger: 'blur'
            },
            {
              max: 300,
              message: '数据库连接字符串的长度在300字符之内',
              trigger: 'blur'
            },
          ],
          isActive: [{
            required: false,
            message: '请输入是否启用',
            trigger: 'blur'
          }, ],
          isDefaultTenant: [{
            required: false,
            message: '请输入是否默认租户',
            trigger: 'blur'
          }, ],

        },
        // 控制修改租户管理对话框的显示与隐藏
        editDialogVisible: false,

      }
    },
    created() {
      this.getSysTenantList()
    },
    methods: {
      async getSysTenantList() {
        const {
          data: res
        } = await this.$http.post(this.$sysTenantManagerUrl, {
          currentPage: this.queryInfo.pageNum,
          pageSize: this.queryInfo.pageSize,
          filter: {
            queryString: this.queryInfo.query,
          },
        })
        if (res.code !== 200) {
          return this.$message.error('获取租户管理列表失败！' + res.message)
        }
        this.sysTenantList = res.data.list
        this.total = res.data.total
        console.log(res)
      },
      // 监听 pagesize 改变的事件
      handleSizeChange(newSize) {
        // console.log(newSize)
        this.queryInfo.pageSize = newSize
        this.getSysTenantList()
      },
      // 监听 页码值 改变的事件
      handleCurrentChange(newPage) {
        console.log(newPage)
        this.queryInfo.pageNum = newPage
        this.getSysTenantList()
      },
      // 监听添加租户管理对话框的关闭事件
      addOrEditDialogClosed() {
        this.editState = false //编辑状态改为false
        this.addOrEditForm = {
            id: '',
            tenantId: 0,
            tenantName: '',
            dbType: 0,
            dbConnectionString: '',
            isActive: true,
            isDefaultTenant: true,


          },
          this.$refs.addOrEditFormRef.resetFields()
      },
      // 点击按钮，添加或编辑租户管理
      addOrEditSysTenant() {
        this.$refs.addOrEditFormRef.validate(async (valid) => {
          if (!valid) return

          if (this.addOrEditForm.id > 0) {
            //编辑状态
            // 发起修改租户管理信息的数据请求
            const {
              data: res
            } = await this.$http.put(
              this.$sysTenantManager_EditSysTenantUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('更新租户管理信息失败！' + res.message)
            }

            // 提示修改成功
            this.$message.success('更新租户管理信息成功！')
          } else {
            // 可以发起添加租户管理的网络请求
            const {
              data: res
            } = await this.$http.post(
              this.$sysTenantManager_CreateSysTenantUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('添加租户管理失败！' + res.message)
            }

            this.$message.success('添加租户管理成功！')
          }

          // 隐藏添加租户管理的对话框
          this.addOrEditDialogVisible = false
          // 重新获取租户管理列表数据
          this.getSysTenantList()
        })
      },
      // 根据Id删除对应的租户管理信息
      async removeSysTenantById(id) {
        // 弹框询问租户管理是否删除数据
        const confirmResult = await this.$confirm(
          '此操作将永久删除该租户管理, 是否继续?',
          '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
          }
        ).catch((err) => err)

        // 如果租户管理确认删除，则返回值为字符串 confirm
        // 如果租户管理取消了删除，则返回值为字符串 cancel
        // console.log(confirmResult)
        if (confirmResult !== 'confirm') {
          return this.$message.info('已取消删除')
        }

        const {
          data: res
        } = await this.$http.delete(
          this.$sysTenantManager_DeleteSysTenantUrl + '?id=' + id
        )

        if (res.code !== 200) {
          return this.$message.error('删除租户管理失败！' + res.message)
        }

        this.$message.success('删除租户管理成功！')
        this.getSysTenantList()
      },

      // 展示租户管理的对话框
      async showAddOrEditDialog(id) {
        this.addOrEditDialogVisible = true
        if (id > 0) {
          //编辑状态
          // console.log(id)
          const {
            data: res
          } = await this.$http.get(
            this.$sysTenantManager_GetSysTenantUrl + '?id=' + id
          )

          if (res.code !== 200) {
            return this.$message.error('查询租户管理信息失败！' + res.message)
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
