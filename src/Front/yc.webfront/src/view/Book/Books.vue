<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>大数据组件</el-breadcrumb-item>
      <el-breadcrumb-item>ElasticSearch示例</el-breadcrumb-item>
    </el-breadcrumb>

    <el-row :gutter="20">
      <el-col :span="24">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>搜索区域【本示例内置1000多万条书籍演示数据】</span>
          </div>
          <div class="text item">
            <!-- 搜索与添加区域 -->
            <el-row :gutter="20">
              <el-col :span="12">
                <div class="grid-content bg-purple">
                  <div class="block">
                    发布时间范围：
                    <el-date-picker
                      v-model="queryInfo.publishDateRange"
                      type="datetimerange"
                      :picker-options="pickerOptions"
                      range-separator="至"
                      start-placeholder="开始日期"
                      end-placeholder="结束日期"
                      align="right"
                      :default-time="['00:00:00', '00:00:00']"
                    >
                    </el-date-picker>
                  </div>
                </div>
              </el-col>

              <el-col :span="12">
                <div class="grid-content bg-purple-light">
                  <el-input
                    placeholder="请输入书名/书内容/作者"
                    class="input-with-select"
                    v-model="queryInfo.query"
                    clearable
                    @clear="getBookList"
                  >
                    <el-button
                      slot="append"
                      icon="el-icon-search"
                      @click="getBookList"
                    ></el-button>
                  </el-input>
                </div>
              </el-col>
            </el-row>
          </div>
        </el-card>
      </el-col>
    </el-row>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 列表 -->
      <el-table :data="bookList" style="width: 100%" border stripe>
        <el-table-column type="index" width="60" label="序号">
        </el-table-column>
        <el-table-column prop="bookName" label="书名">
          <template slot-scope="scope">
            <div v-html="scope.row.bookName"></div>
          </template>
        </el-table-column>
        <el-table-column prop="bookContent" label="内容">
          <template slot-scope="scope">
            <div v-html="scope.row.bookContent"></div>
          </template>
        </el-table-column>
        <el-table-column prop="auther" label="作者">
          <template slot-scope="scope">
            <div v-html="scope.row.auther"></div>
          </template>
        </el-table-column>
        <el-table-column prop="publishDate" label="出版时间"></el-table-column>
        <el-table-column prop="price" label="价格">
          <template slot-scope="scope">
            <div v-html="scope.row.price"></div>
          </template>
        </el-table-column>
        <el-table-column prop="createDate" label="创建时间"></el-table-column>

        <el-table-column
          label="操作"
          width="220"
          v-if="
            $hasPermission(
              ['bookManager.editBook'] ||
                $hasPermission(['bookManager.deleteBook'])
            )
          "
        >
          <template slot-scope="scope">
            <!-- 修改按钮 -->
            <el-button
              type="primary"
              icon="el-icon-edit"
              size="mini"
              @click="showAddOrEditDialog(scope.row.id)"
              v-if="$hasPermission(['bookManager.editBook'])"
              >编辑</el-button
            >
            <!-- 删除按钮 -->
            <el-button
              type="danger"
              icon="el-icon-delete"
              size="mini"
              @click="removeBookById(scope.row.id)"
              v-if="$hasPermission(['bookManager.deleteBook'])"
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

    <!-- 添加或编辑的对话框 -->
    <el-dialog
      :title="(addOrEditForm.id > 0 ? '编辑' : '新增') + ''"
      :visible.sync="addOrEditDialogVisible"
      width="60%"
      @close="addOrEditDialogClosed"
    >
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="管理" name="first">
          <el-form
            :model="addOrEditForm"
            :rules="addOrEditFormRules"
            ref="addOrEditFormRef"
            label-width="100px"
          >
            <el-input
              v-model="addOrEditForm.id"
              prop="id"
              type="hidden"
            ></el-input>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="内容：" prop="bookContent">
                  <el-input v-model="addOrEditForm.bookContent"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="书名：" prop="bookName">
                  <el-input v-model="addOrEditForm.bookName"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="出版时间：" prop="publishDate">
                  <el-input v-model="addOrEditForm.publishDate"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="作者：" prop="auther">
                  <el-input v-model="addOrEditForm.auther"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="创建时间：" prop="createDate">
                  <el-input v-model="addOrEditForm.createDate"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="价格：" prop="price">
                  <el-input v-model="addOrEditForm.price"></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditBook()">确 定</el-button>
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
        publishDateRange: '',
        pageNum: 1,
        pageSize: 10
      },

      pickerOptions: {
        shortcuts: [
          {
            text: '最近一周',
            onClick(picker) {
              const end = new Date()
              const start = new Date()
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7)
              picker.$emit('pick', [start, end])
            }
          },
          {
            text: '最近一个月',
            onClick(picker) {
              const end = new Date()
              const start = new Date()
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30)
              picker.$emit('pick', [start, end])
            }
          },
          {
            text: '最近三个月',
            onClick(picker) {
              const end = new Date()
              const start = new Date()
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90)
              picker.$emit('pick', [start, end])
            }
          }
        ]
      },
      bookList: [],
      total: 0,
      activeName: 'first',
      // 控制添加对话框的显示与隐藏
      addOrEditDialogVisible: false,
      // 添加的表单数据
      addOrEditForm: {
        id: '',
        bookName: '',
        bookContent: '',
        auther: '',
        publishDate: '',
        price: '',
        createDate: ''
      },
      editState: false, //编辑状态

      // 添加表单的验证规则对象
      addOrEditFormRules: {
        bookName: [
          {
            required: false,
            message: '请输入书名',
            trigger: 'blur'
          },
          {
            max: 100,
            message: '书名的长度在100字符之内',
            trigger: 'blur'
          }
        ],
        bookContent: [
          {
            required: false,
            message: '请输入内容',
            trigger: 'blur'
          },
          {
            max: 100,
            message: '内容的长度在100字符之内',
            trigger: 'blur'
          }
        ],
        auther: [
          {
            required: false,
            message: '请输入作者',
            trigger: 'blur'
          },
          {
            max: 100,
            message: '作者的长度在100字符之内',
            trigger: 'blur'
          }
        ],
        publishDate: [
          {
            required: false,
            message: '请输入出版时间',
            trigger: 'blur'
          }
        ],
        price: [
          {
            required: false,
            message: '请输入价格',
            trigger: 'blur'
          }
        ],
        createDate: [
          {
            required: false,
            message: '请输入创建时间',
            trigger: 'blur'
          }
        ]
      },
      // 控制修改对话框的显示与隐藏
      editDialogVisible: false
    }
  },
  created() {
    this.getBookList()
  },
  methods: {
    async getBookList() {
      const { data: res } = await this.$http.post(this.$bookManagerUrl, {
        currentPage: this.queryInfo.pageNum,
        pageSize: this.queryInfo.pageSize,
        filter: {
          queryString: this.queryInfo.query
        },
        publishDateRange: this.queryInfo.publishDateRange
      })
      if (res.code !== 200) {
        return this.$message.error('获取列表失败！' + res.message)
      }
      this.bookList = res.data.list
      this.total = res.data.total
      console.log(res)
    },
    // 监听 pagesize 改变的事件
    handleSizeChange(newSize) {
      // console.log(newSize)
      this.queryInfo.pageSize = newSize
      this.getBookList()
    },
    // 监听 页码值 改变的事件
    handleCurrentChange(newPage) {
      console.log(newPage)
      this.queryInfo.pageNum = newPage
      this.getBookList()
    },
    // 监听添加对话框的关闭事件
    addOrEditDialogClosed() {
      this.editState = false //编辑状态改为false
      ;(this.addOrEditForm = {
        id: '',
        bookName: '',
        bookContent: '',
        auther: '',
        publishDate: '',
        price: '',
        createDate: ''
      }),
        this.$refs.addOrEditFormRef.resetFields()
    },
    // 点击按钮，添加或编辑
    addOrEditBook() {
      this.$refs.addOrEditFormRef.validate(async valid => {
        if (!valid) return

        if (this.addOrEditForm.id > 0) {
          //编辑状态
          // 发起修改信息的数据请求
          const { data: res } = await this.$http.put(
            this.$bookManager_EditBookUrl,
            this.addOrEditForm
          )

          if (res.code !== 200) {
            return this.$message.error('更新信息失败！' + res.message)
          }

          // 提示修改成功
          this.$message.success('更新信息成功！')
        } else {
          // 可以发起添加的网络请求
          const { data: res } = await this.$http.post(
            this.$bookManager_CreateBookUrl,
            this.addOrEditForm
          )

          if (res.code !== 200) {
            return this.$message.error('添加失败！' + res.message)
          }

          this.$message.success('添加成功！')
        }

        // 隐藏添加的对话框
        this.addOrEditDialogVisible = false
        // 重新获取列表数据
        this.getBookList()
      })
    },
    // 根据Id删除对应的信息
    async removeBookById(id) {
      // 弹框询问是否删除数据
      const confirmResult = await this.$confirm(
        '此操作将永久删除该, 是否继续?',
        '提示',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }
      ).catch(err => err)

      // 如果确认删除，则返回值为字符串 confirm
      // 如果取消了删除，则返回值为字符串 cancel
      // console.log(confirmResult)
      if (confirmResult !== 'confirm') {
        return this.$message.info('已取消删除')
      }

      const { data: res } = await this.$http.delete(
        this.$bookManager_DeleteBookUrl + '?id=' + id
      )

      if (res.code !== 200) {
        return this.$message.error('删除失败！' + res.message)
      }

      this.$message.success('删除成功！')
      this.getBookList()
    },

    // 展示的对话框
    async showAddOrEditDialog(id) {
      this.addOrEditDialogVisible = true
      if (id > 0) {
        //编辑状态
        // console.log(id)
        const { data: res } = await this.$http.get(
          this.$bookManager_GetBookUrl + '?id=' + id
        )

        if (res.code !== 200) {
          return this.$message.error('查询信息失败！' + res.message)
        }

        this.addOrEditForm = res.data
        this.editState = true
      }

      this.addOrEditDialogVisible = true
    }
  }
}
</script>
<style lang="less" scoped></style>
