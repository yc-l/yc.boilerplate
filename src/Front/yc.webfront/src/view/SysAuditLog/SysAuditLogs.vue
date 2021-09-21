<template>
  <div >
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>审计日志</el-breadcrumb-item>
      <el-breadcrumb-item>审计日志列表</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 搜索与添加区域 -->

      <el-row :gutter="20">
        <el-col :span="7">
          <el-input placeholder="请输入搜索的内容" class="input-with-select" v-model="queryInfo.query" clearable
            @clear="getSysAuditLogList">
            <el-button slot="append" icon="el-icon-search" @click="getSysAuditLogList"></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" v-if="$hasPermission(['sysAuditLogManager.createSysAuditLog'])"
            @click="showAddOrEditDialog()">添加</el-button>
        </el-col>
      </el-row>
      <!-- 列表 -->
      <el-table :data="sysAuditLogList" style="width: 100%" border stripe>
        <el-table-column type="index" width="60" label="序号"> </el-table-column>
     
        <el-table-column prop='ip' label='IP'></el-table-column>
       
       <el-table-column prop='requestMethod' label='请求方式'></el-table-column>
        <el-table-column prop='requestApi' width="260px" label='请求Api'></el-table-column>
        <el-table-column prop='userAccount' label='用户账号'></el-table-column>
        <el-table-column prop='paramsString' width="300px" label='请求参数'></el-table-column>
         <el-table-column prop='browser' label='浏览器'></el-table-column>
        <el-table-column prop='os' label='操作系统'></el-table-column>
       
        <el-table-column prop='elapsedMilliseconds' label='耗时（毫秒）' width="100px"></el-table-column>
       <el-table-column prop='creationTime' label='创建时间' width="150px"></el-table-column>
        <el-table-column label="操作" width="130"
          v-if="$hasPermission(['sysAuditLogManager.viewSysAuditLog'])">
          <template slot-scope="scope">
            <!-- 查看按钮 -->
            <el-button type="primary" icon="el-icon-view" size="mini" @click="showAddOrEditDialog(scope.row.id)"
              v-if="$hasPermission(['sysAuditLogManager.viewSysAuditLog'])">查看详情</el-button>
        
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
        :current-page="queryInfo.pageNum" :page-sizes="[5, 10, 50, 100]" :page-size="queryInfo.pageSize"
        layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </el-card>

    <!-- 添加或编辑的对话框 -->
    <el-dialog :title="(addOrEditForm.id > 0 ? '查看审计日志' : '新增') + ''" :visible.sync="addOrEditDialogVisible" width="60%"
      @close="addOrEditDialogClosed">
      <!-- 内容主体区域 --> 

      <el-tabs v-model="activeName">
        <el-tab-pane label="审计日志详情" name="first">
          <el-form :model="addOrEditForm"  ref="addOrEditFormRef" label-width="120px">
            <el-input v-model="addOrEditForm.id" prop="id" type="hidden"></el-input>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='IP：' prop='ip'>
                 {{addOrEditForm.ip}}
                </el-form-item>
              </el-col>

              <el-col :span='12'>
                <el-form-item label='拦截Key：' prop='key'>
                 {{addOrEditForm.key}}
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='操作系统：' prop='os'>
                   {{addOrEditForm.os}}
                </el-form-item>
              </el-col>

              <el-col :span='12'>
                <el-form-item label='浏览器：' prop='browser'>                
                   {{addOrEditForm.browser}}
                </el-form-item>
              </el-col>


            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='浏览器信息：' prop='browserInfo'>                
                  {{addOrEditForm.browserInfo}}
                </el-form-item>
              </el-col>

              <el-col :span='12'>
                <el-form-item label='设备：' prop='device'>                
                    {{addOrEditForm.device}}
                </el-form-item>
              </el-col>


            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='用户id：' prop='userId'>              
                   {{addOrEditForm.userId}}
                </el-form-item>
              </el-col>

              <el-col :span='12'>
                <el-form-item label='耗时（毫秒）:'  prop='elapsedMilliseconds'>                
                   {{addOrEditForm.elapsedMilliseconds}}
                </el-form-item>
              </el-col>


            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12' >
                <el-form-item label='请求参数：'  prop='paramsString'>             
                   <span style="background: #e8f3fe;"> {{addOrEditForm.paramsString}} </span>
                </el-form-item>
              </el-col>

              <el-col :span='12'>
                <el-form-item label='用户账号：' prop='userAccount'>                 
                   {{addOrEditForm.userAccount}}
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='请求Api：' prop='requestApi'>                
                   {{addOrEditForm.requestApi}}
                </el-form-item>
              </el-col>

              <el-col :span='12'>
                <el-form-item label='请求方式：' prop='requestMethod'>               
                   {{addOrEditForm.requestMethod}}
                </el-form-item>
              </el-col>


            </el-row>


          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">关闭</el-button>
       
      </span>
    </el-dialog>
  </div>
</template>

<script>
  export default {
    data() {

      return {
        queryInfo: {
          query: '',
          pageNum: 1,
          pageSize: 10,
        },
        sysAuditLogList: [],
        total: 0,
        activeName: 'first',
        // 控制添加对话框的显示与隐藏
        addOrEditDialogVisible: false,
        // 添加的表单数据
        addOrEditForm: {
          id: '',
          key: '',
          iP: '',
          browser: '',
          os: '',
          device: '',
          browserInfo: '',
          elapsedMilliseconds: 0,
          userId: 0,
          userAccount: '',
          paramsString: '',
          startTime: '',
          stopTime: '',
          requestMethod: '',
          requestApi: '',


        },
        editState: false, //编辑状态

     
        // 控制修改对话框的显示与隐藏
        editDialogVisible: false,

      }
    },
    created() {
      this.getSysAuditLogList()
    },
    methods: {
      async getSysAuditLogList() {
        const {
          data: res
        } = await this.$http.post(this.$sysAuditLogManagerUrl, {
          currentPage: this.queryInfo.pageNum,
          pageSize: this.queryInfo.pageSize,
          filter: {
            queryString: this.queryInfo.query,
          },
        })
        if (res.code !== 200) {
          return this.$message.error('获取列表失败！' + res.message)
        }
        this.sysAuditLogList = res.data.list
        this.total = res.data.total
        console.log(res)
      },
      // 监听 pagesize 改变的事件
      handleSizeChange(newSize) {
        // console.log(newSize)
        this.queryInfo.pageSize = newSize
        this.getSysAuditLogList()
      },
      // 监听 页码值 改变的事件
      handleCurrentChange(newPage) {
        console.log(newPage)
        this.queryInfo.pageNum = newPage
        this.getSysAuditLogList()
      },
      // 监听添加对话框的关闭事件
      addOrEditDialogClosed() {
        this.editState = false //编辑状态改为false
        this.addOrEditForm = {
            id: '',
            key: '',
            iP: '',
            browser: '',
            os: '',
            device: '',
            browserInfo: '',
            elapsedMilliseconds: 0,
            userId: 0,
            userAccount: '',
            paramsString: '',
            startTime: '',
            stopTime: '',
            requestMethod: '',
            requestApi: '',


          },
          this.$refs.addOrEditFormRef.resetFields()
      },
      // 点击按钮，添加或编辑
      addOrEditSysAuditLog() {
        this.$refs.addOrEditFormRef.validate(async (valid) => {
          if (!valid) return

          if (this.addOrEditForm.id > 0) {
            //编辑状态
            // 发起修改信息的数据请求
            const {
              data: res
            } = await this.$http.put(
              this.$sysAuditLogManager_EditSysAuditLogUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('更新信息失败！' + res.message)
            }

            // 提示修改成功
            this.$message.success('更新信息成功！')
          } else {
            // 可以发起添加的网络请求
            const {
              data: res
            } = await this.$http.post(
              this.$sysAuditLogManager_CreateSysAuditLogUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              this.$message.error('添加失败！' + res.message)
            }

            this.$message.success('添加成功！')
          }

          // 隐藏添加的对话框
          this.addOrEditDialogVisible = false
          // 重新获取列表数据
          this.getSysAuditLogList()
        })
      },
    
      // 展示的对话框
      async showAddOrEditDialog(id) {
        this.addOrEditDialogVisible = true
        if (id > 0) {
          //编辑状态
          // console.log(id)
          const {
            data: res
          } = await this.$http.get(
            this.$sysAuditLogManager_GetSysAuditLogUrl + '?id=' + id
          )

          if (res.code !== 200) {
            return this.$message.error('查询信息失败！' + res.message)
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
