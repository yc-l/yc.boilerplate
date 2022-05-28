<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>区块链存证管理</el-breadcrumb-item>
      <el-breadcrumb-item>区块链存证列表</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 搜索与添加区域 -->

      <el-row :gutter="20">
        <el-col :span="7">
          <el-input placeholder="请输入搜索的内容" class="input-with-select" v-model="queryInfo.query" clearable
            @clear="getBCEvidenceList">
            <el-button slot="append" icon="el-icon-search" @click="getBCEvidenceList"></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" v-if="$hasPermission(['bCEvidenceManager.createBCEvidence'])"
            @click="showAddOrEditDialog()">添加区块链存证</el-button>
        </el-col>
      </el-row>
      <!-- 区块链存证列表 -->
      <el-table :data="bCEvidenceList" style="width: 100%" border stripe>
        <el-table-column type="index" width="60" label="序号"> </el-table-column>
        <el-table-column prop='serviceId' label='事务Id'></el-table-column>
        <el-table-column prop='typeName' label='存证类别'></el-table-column>
        <el-table-column prop='dataValue' label='存证数据'></el-table-column>
        <el-table-column prop='transcationHash' label='交易Hash'></el-table-column>
        <el-table-column prop='operaEvidenceDate' label='存证操作时间'></el-table-column>

        <el-table-column label="操作" width="220"
          v-if="$hasPermission(['bCEvidenceManager.viewBCEvidence']) || $hasPermission(['bCEvidenceManager.editBCEvidence'] || $hasPermission(['bCEvidenceManager.deleteBCEvidence']))">
          <template slot-scope="scope">
            <!-- 查看按钮 -->
            <el-button type="primary" icon="el-icon-document" size="mini" @click="showViewDialog(scope.row.id)"
              v-if="$hasPermission(['bCEvidenceManager.viewBCEvidence'])"> 查看</el-button>
            <!-- 修改按钮 -->
            <el-button type="primary" icon="el-icon-edit" size="mini" @click="showAddOrEditDialog(scope.row.id)"
              v-if="$hasPermission(['bCEvidenceManager.editBCEvidence'])">编辑</el-button>
            <!-- 删除按钮 -->
            <el-button type="danger" icon="el-icon-delete" size="mini" @click="removeBCEvidenceById(scope.row.id)"
              v-if="$hasPermission(['bCEvidenceManager.deleteBCEvidence'])">删除</el-button>

          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
        :current-page="queryInfo.pageNum" :page-sizes="[5, 10, 50, 100]" :page-size="queryInfo.pageSize"
        layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </el-card>

    <!-- 添加或编辑区块链存证的对话框 -->
    <el-dialog :title="(addOrEditForm.id > 0 ? '编辑' : '新增') + '区块链存证'" :visible.sync="addOrEditDialogVisible"
      width="60%" @close="addOrEditDialogClosed">
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="区块链存证管理" name="first">
          <el-form :model="addOrEditForm" :rules="addOrEditFormRules" ref="addOrEditFormRef" label-width="100px">
            <el-input v-model="addOrEditForm.id" prop="id" type="hidden"></el-input>

            <el-row :gutter='20'>


              <el-col :span='12'>
                <el-form-item label='事务Id：' prop='serviceId'>
                  <el-input v-model='addOrEditForm.serviceId'></el-input>
                </el-form-item>
              </el-col>
              <el-col :span='12'>
                <el-form-item label='存证类别：' prop='typeName'>
                  <el-input v-model='addOrEditForm.typeName'></el-input>
                </el-form-item>
              </el-col>

            </el-row>

            <el-row :gutter='20'>
             

              <el-col :span='24'>
                <el-form-item label='存证数据：' prop='dataValue'>
                   <el-input type="textarea" rows="10" v-model="addOrEditForm.dataValue"></el-input>
                </el-form-item>
              </el-col>


            </el-row>


      
          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditBCEvidence()">确 定</el-button>
      </span>
    </el-dialog>

    <!-- 查看区块链存证的对话框 -->
    <el-dialog :title=" '查看区块链存证'" :visible.sync="viewDialogVisible" width="60%" @close="viewDialogClosed">
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="区块链存证管理" name="first">
          <el-form :model="viewForm" ref="viewFormRef" label-width="100px">
            <el-input v-model="viewForm.id" prop="id" type="hidden"></el-input>

            <el-row :gutter='20'>
                <el-col :span='12'>
                <el-form-item label='事务Id：' prop='serviceId'>
                  <el-input v-model='viewForm.serviceId' disabled></el-input>
                </el-form-item>
              </el-col>
              <el-col :span='12'>
                <el-form-item label='存证类别：' prop='typeName'>
                  <el-input v-model='viewForm.typeName' disabled></el-input>
                </el-form-item>
              </el-col>

            


            </el-row>

            <el-row :gutter='20'>
            

              <el-col :span='24'>
                <el-form-item label='存证数据：' prop='dataValue'>
                
                   <el-input type="textarea" rows="10" v-model="viewForm.dataValue" disabled></el-input>
                </el-form-item>
              </el-col>


            </el-row>

            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label='交易Hash：' prop='transcationHash'>
                  <el-input v-model='viewForm.transcationHash' disabled></el-input>
                </el-form-item>
              </el-col>

               <el-col :span='12'>
                <el-form-item label='操作时间：' prop='operaEvidenceDate'>
                  <el-input v-model='viewForm.operaEvidenceDate' disabled></el-input>
                </el-form-item>
              </el-col>


            </el-row>

           

          </el-form>
        </el-tab-pane>
      </el-tabs>


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
        bCEvidenceList: [],
        total: 0,
        activeName: 'first',
        // 控制添加区块链存证对话框的显示与隐藏
        addOrEditDialogVisible: false,
        // 添加区块链存证的表单数据
        addOrEditForm: {
          id: '',
          serviceId: '',
          typeName: '',
          dataValue: '',
        },
        editState: false, //编辑状态
        viewDialogVisible: false, //查看数据按钮
        // 查看区块链存证的表单数据
        viewForm: {
          id: '',
          serviceId: '',
          typeName: '',
          dataValue: '',
          evidenceState: 0,
          message: '',
          transcationHash: '',
          operaEvidenceDate: '',


        },
        // 添加表单的验证规则对象
        addOrEditFormRules: {
          serviceId: [{
              required: false,
              message: '请输入事务Id',
              trigger: 'blur'
            },
            {
              max: 32,
              message: '事务Id的长度在32字符之内',
              trigger: 'blur'
            },
          ],
          typeName: [{
              required: false,
              message: '请输入存证类别',
              trigger: 'blur'
            },
            {
              max: 50,
              message: '存证类别的长度在50字符之内',
              trigger: 'blur'
            },
          ],
          dataValue: [{
              required: false,
              message: '请输入存证数据',
              trigger: 'blur'
            },
            {
              max: 2000,
              message: '存证数据的长度在2000字符之内',
              trigger: 'blur'
            },
          ],
        

        },
        // 控制修改区块链存证对话框的显示与隐藏
        editDialogVisible: false,

      }
    },
    created() {
      this.getBCEvidenceList()
    },
    methods: {
      async getBCEvidenceList() {
        const {
          data: res
        } = await this.$http.post(this.$bCEvidenceManagerUrl, {
          currentPage: this.queryInfo.pageNum,
          pageSize: this.queryInfo.pageSize,
          filter: {
            queryString: this.queryInfo.query,
          },
        })
        if (res.code !== 200) {
          return this.$message.error('获取区块链存证列表失败！' + res.message)
        }
        this.bCEvidenceList = res.data.list
        this.total = res.data.total
        console.log(res)
      },
      // 监听 pagesize 改变的事件
      handleSizeChange(newSize) {
        // console.log(newSize)
        this.queryInfo.pageSize = newSize
        this.getBCEvidenceList()
      },
      // 监听 页码值 改变的事件
      handleCurrentChange(newPage) {
        console.log(newPage)
        this.queryInfo.pageNum = newPage
        this.getBCEvidenceList()
      },
      // 监听添加区块链存证对话框的关闭事件
      addOrEditDialogClosed() {
        this.editState = false //编辑状态改为false
        this.addOrEditForm = {
            id: '',
            serviceId: '',
            typeName: '',
            dataValue: '',
          },
          this.$refs.addOrEditFormRef.resetFields()
      },
      // 点击按钮，添加或编辑区块链存证
      addOrEditBCEvidence() {
        this.$refs.addOrEditFormRef.validate(async (valid) => {
          if (!valid) return

          if (this.addOrEditForm.id > 0) {
            //编辑状态
            // 发起修改区块链存证信息的数据请求
            const {
              data: res
            } = await this.$http.put(
              this.$bCEvidenceManager_EditBCEvidenceUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('更新区块链存证信息失败！' + res.message)
            }

            // 提示修改成功
            this.$message.success('更新区块链存证信息成功！')
          } else {
            // 可以发起添加区块链存证的网络请求
            const {
              data: res
            } = await this.$http.post(
              this.$bCEvidenceManager_CreateBCEvidenceUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('添加区块链存证失败！' + res.message)
            }

            this.$message.success('添加区块链存证成功！')
          }

          // 隐藏添加区块链存证的对话框
          this.addOrEditDialogVisible = false
          // 重新获取区块链存证列表数据
          this.getBCEvidenceList()
        })
      },
      // 根据Id删除对应的区块链存证信息
      async removeBCEvidenceById(id) {
        // 弹框询问区块链存证是否删除数据
        const confirmResult = await this.$confirm(
          '此操作将永久删除该区块链存证, 是否继续?',
          '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
          }
        ).catch((err) => err)

        // 如果区块链存证确认删除，则返回值为字符串 confirm
        // 如果区块链存证取消了删除，则返回值为字符串 cancel
        // console.log(confirmResult)
        if (confirmResult !== 'confirm') {
          return this.$message.info('已取消删除')
        }

        const {
          data: res
        } = await this.$http.delete(
          this.$bCEvidenceManager_DeleteBCEvidenceUrl + '?id=' + id
        )

        if (res.code !== 200) {
          return this.$message.error('删除区块链存证失败！' + res.message)
        }

        this.$message.success('删除区块链存证成功！')
        this.getBCEvidenceList()
      },

      // 展示区块链存证的对话框
      async showAddOrEditDialog(id) {
        this.addOrEditDialogVisible = true
        if (id > 0) {
          //编辑状态
          // console.log(id)
          const {
            data: res
          } = await this.$http.get(
            this.$bCEvidenceManager_GetBCEvidenceUrl + '?id=' + id
          )

          if (res.code !== 200) {
            return this.$message.error('查询区块链存证信息失败！' + res.message)
          }

          this.addOrEditForm = res.data
          this.editState = true
        }

        this.addOrEditDialogVisible = true
      },

      // 展示查看看区块链存证的对话框
      async showViewDialog(id) {
        this.viewDialogVisible = true
        if (id > 0) {
          //编辑状态
          // console.log(id)
          const {
            data: res
          } = await this.$http.get(
            this.$bCEvidenceManager_GetBCEvidenceUrl + '?id=' + id
          )

          if (res.code !== 200) {
            return this.$message.error('查询区块链存证信息失败！' + res.message)
          }

          this.viewForm = res.data

        }

        this.viewDialogVisible = true
      },
        // 监听查询区块链存证对话框的关闭事件
      viewDialogClosed() {

        this.viewForm = {
            id: '',
            serviceId: '',
            typeName: '',
            dataValue: '',
            evidenceState: 0,
            message: '',
            transcationHash: '',
            operaEvidenceDate: '',


          },
          this.$refs.viewFormRef.resetFields()
      },
    },
  }

</script>
<style lang="less" scoped>
</style>
