<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>角色管理</el-breadcrumb-item>
      <el-breadcrumb-item>角色列表</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 搜索与添加区域 -->

      <el-row :gutter="20">
        <el-col :span="7">
          <el-input
            placeholder="请输入搜索的角色名称"
            class="input-with-select"
            v-model="queryInfo.query"
            clearable
            @clear="getSysRoleList"
          >
            <el-button
              slot="append"
              icon="el-icon-search"
              @click="getSysRoleList"
            ></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" @click="showAddOrEditDialog()" v-if="$hasPermission(['roleManager.createRole'])"
            >添加角色</el-button
          >
        </el-col>
      </el-row>
      <!-- 角色列表 -->
      <el-table :data="sysRoleList" style="width: 100%" border stripe>
        <el-table-column type="index" width="70" label="序号">
        </el-table-column>
        <el-table-column prop="name" label="名称"></el-table-column>
        <el-table-column prop="memoni" label="助记符"></el-table-column>
        <el-table-column prop="sort" label="排序"></el-table-column>

        <el-table-column label="操作" width="280" v-if="$hasPermission(['roleManager.deleteRole'])||$hasPermission(['roleManager.editRole'])">
          <template slot-scope="scope">
            <!-- 修改按钮 -->
            <el-button
              type="primary"
              icon="el-icon-edit"
              size="mini"
              @click="showAddOrEditDialog(scope.row.id)" v-if="$hasPermission(['roleManager.editRole'])"
              >编辑</el-button
            >
            <!-- 删除按钮 -->
            <el-button
              type="danger"
              icon="el-icon-delete"
              size="mini"
              @click="removeSysRoleById(scope.row.id)" v-if="$hasPermission(['roleManager.deleteRole'])"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="queryInfo.pageNum"
        :page-sizes="[5, 10, 50, 100]"
        :page-size="queryInfo.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
      >
      </el-pagination>
    </el-card>

    <!-- 添加角色的对话框 -->
    <el-dialog
      :title="(addOrEditForm.id > 0 ? '编辑' : '新增') + '角色'"
      :visible.sync="addOrEditDialogVisible"
      width="60%"
      @close="addOrEditDialogClosed"
    >
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="角色管理" name="first">
          <el-form
            :model="addOrEditForm"
            :rules="addOrEditFormRules"
            ref="addOrEditFormRef"
            label-width="70px"
          >
            <el-input
              v-model="addOrEditForm.id"
              prop="id"
              type="hidden"
            ></el-input>

            <el-row :gutter="20">
                 <el-col :span="12">
                <el-form-item label="名称：" prop="name">
                  <el-input v-model="addOrEditForm.name"></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="助记符：" prop="memoni">
                  <el-input v-model="addOrEditForm.memoni"></el-input>
                </el-form-item>
              </el-col>

           
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="排序：" prop="sort">
                  <el-input v-model="addOrEditForm.sort"></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditSysRole()">确 定</el-button>
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
                sysRoleList: [],
                total: 0,
                activeName: 'first',
                // 控制添加角色对话框的显示与隐藏
                addOrEditDialogVisible: false,
                // 添加角色的表单数据
                addOrEditForm: {
                    id: '',
                    name: '',
                    memoni: '',
                    sort: '',


                },
                editState: false, //编辑状态

                // 添加表单的验证规则对象
                addOrEditFormRules: {
                    name: [{ required: true, message: '请输入名称', trigger: 'blur' },
                    { max: 32, message: '名称的长度在32字符之内', trigger: 'blur' },
                    ],
                    memoni: [{ required: false, message: '请输入助记符', trigger: 'blur' },
                    { max: 32, message: '助记符的长度在32字符之内', trigger: 'blur' },
                    ],
                    sort: [{ required: false, message: '请输入排序', trigger: 'blur' },
                    ],


                },
                // 控制修改角色对话框的显示与隐藏
                editDialogVisible: false,
                // 查询到的角色信息对象
                editForm: {},

                // 控制分配角色对话框的显示与隐藏
                setRoleDialogVisible: false,
                // 需要被分配角色的角色信息
                sysRoleInfo: {},
                // 所有角色的数据列表
                rolesList: [],
                // 已选中的角色Id值
                selectedRoleId: '',
            }
        },
        created() {
            this.getSysRoleList()
        },
        methods: {
            async getSysRoleList() {
                const { data: res } = await this.$http.post(this.$sysRoleManagerUrl, {
                    currentPage: this.queryInfo.pageNum,
                    pageSize: this.queryInfo.pageSize,
                    filter: {
                        queryString: this.queryInfo.query,
                    },
                })
                if (res.code !== 200) {
                    return this.$message.error('获取角色列表失败！'+res.message)
                }
                this.sysRoleList = res.data.list
                this.total = res.data.total
                console.log(res)
            },
            // 监听 pagesize 改变的事件
            handleSizeChange(newSize) {
                // console.log(newSize)
                this.queryInfo.pageSize = newSize
                this.getSysRoleList()
            },
            // 监听 页码值 改变的事件
            handleCurrentChange(newPage) {
                console.log(newPage)
                this.queryInfo.pageNum = newPage
                this.getSysRoleList()
            },
            // 监听添加角色对话框的关闭事件
            addOrEditDialogClosed() {
                this.editState = false //编辑状态改为false
                this.addOrEditForm = {
                    id: '',
                    name: '',
                    memoni: '',
                    sort: '',


                },
                    this.$refs.addOrEditFormRef.resetFields()
            },
            // 点击按钮，添加角色
            addOrEditSysRole() {
                this.$refs.addOrEditFormRef.validate(async (valid) => {
                    if (!valid) return

                    if (this.addOrEditForm.id > 0) {
                        //编辑状态
                        // 发起修改角色信息的数据请求
                        const { data: res } = await this.$http.put(
                            this.$sysRoleManager_EditSysRoleUrl,
                            this.addOrEditForm
                        )

                        if (res.code !== 200) {
                            return this.$message.error('更新角色信息失败！'+res.message)
                        }

                        // 提示修改成功
                        this.$message.success('更新角色信息成功！')
                    } else {
                        // 可以发起添加角色的网络请求
                        const { data: res } = await this.$http.post(
                            this.$sysRoleManager_CreateSysRoleUrl,
                            this.addOrEditForm
                        )

                        if (res.code !== 200) {
                            this.$message.error('添加角色失败！'+res.message)
                        }

                        this.$message.success('添加角色成功！')
                    }

                    // 隐藏添加角色的对话框
                    this.addOrEditDialogVisible = false
                    // 重新获取角色列表数据
                    this.getSysRoleList()
                })
            },
            // 根据Id删除对应的角色信息
            async removeSysRoleById(id) {
                // 弹框询问角色是否删除数据
                const confirmResult = await this.$confirm(
                    '此操作将永久删除该角色, 是否继续?',
                    '提示',
                    {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning',
                    }
                ).catch((err) => err)

                // 如果角色确认删除，则返回值为字符串 confirm
                // 如果角色取消了删除，则返回值为字符串 cancel
                // console.log(confirmResult)
                if (confirmResult !== 'confirm') {
                    return this.$message.info('已取消删除')
                }

                const { data: res } = await this.$http.delete(
                    this.$sysRoleManager_DeleteSysRoleUrl + '?id=' + id
                )

                if (res.code !== 200) {
                    return this.$message.error('删除角色失败！'+res.message)
                }

                this.$message.success('删除角色成功！')
                this.getSysRoleList()
            },

            // 展示角色的对话框
            async showAddOrEditDialog(id) {
                this.addOrEditDialogVisible = true
                if (id > 0) {
                    //编辑状态
                    // console.log(id)
                    const { data: res } = await this.$http.get(
                        this.$sysRoleManager_GetSysRoleUrl + '?id=' + id
                    )

                    if (res.code !== 200) {
                        return this.$message.error('查询角色信息失败！'+res.message)
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

