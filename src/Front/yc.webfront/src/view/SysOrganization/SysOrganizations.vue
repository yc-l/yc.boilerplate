<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>组织机构管理</el-breadcrumb-item>
      <el-breadcrumb-item>组织机构列表</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 搜索与添加区域 -->

      <el-row :gutter="20">
        <el-col :span="7">
          <el-input
            placeholder="请输入搜索的组织机构名称"
            class="input-with-select"
            v-model="queryInfo.query"
            clearable
            @clear="getSysOrganizationList"
          >
            <el-button
              slot="append"
              icon="el-icon-search"
              @click="getSysOrganizationList"
            ></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button
            type="primary"
            @click="showAddOrEditDialog()"
            v-if="
              $hasPermission(['sysOrganizationManager.createSysOrganization'])
            "
            >添加组织机构</el-button
          >
        </el-col>
      </el-row>
      <!-- 组织机构列表 -->
      <tree-table
        class="tree-table"
        :selection-type="false"
        :expand-type="false"
        :is-fold="false"
        :data="sysOrganizationList"
        :columns="columns"
        :show-index="true"
        border
        stripe
        show-row-hover
        tree-type
      >
        <template slot="typeName" slot-scope="scope">
          <el-tag v-if="scope.row.typeName === '分组'">
            {{ scope.row.typeName }}
          </el-tag>
          <el-tag type="success" v-else-if="scope.row.typeName === '菜单'">
            {{ scope.row.typeName }}
          </el-tag>
          <el-tag type="info" v-else-if="scope.row.typeName === '接口'">
            {{ scope.row.typeName }}
          </el-tag>
        </template>
        <template slot="opt" slot-scope="scope">
          <!-- 修改按钮 -->
          <el-button
            type="primary"
            icon="el-icon-edit"
            size="mini"
            @click="showAddOrEditDialog(scope.row.id)"
            v-if="
              $hasPermission(['sysOrganizationManager.editSysOrganization'])
            "
            >编辑
          </el-button>
          <!-- 删除按钮 -->
          <el-button
            type="danger"
            icon="el-icon-delete"
            size="mini"
            @click="removeSysOrganizationById(scope.row.id)"
            v-if="
              $hasPermission(['sysOrganizationManager.deleteSysOrganization'])
            "
            >删除
          </el-button>
        </template>
      </tree-table>
    </el-card>

    <!-- 添加组织机构的对话框 -->
    <el-dialog
      :title="(addOrEditForm.id > 0 ? '编辑' : '新增') + '组织机构'"
      :visible.sync="addOrEditDialogVisible"
      width="60%"
      @close="addOrEditDialogClosed"
    >
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="组织机构管理" name="first">
          <el-form
            :model="addOrEditForm"
            :rules="addOrEditFormRules"
            ref="addOrEditFormRef"
            label-width="120px"
          >
            <el-input
              v-model="addOrEditForm.id"
              prop="id"
              type="hidden"
            ></el-input>

            <el-row :gutter="20">
              <el-col :span="12"> </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="所属上级：" prop="selectedKeys">
                  <el-cascader
                    :key="cascaderKey"
                    v-model="selectedKeys"
                    :options="groupTree"
                    style="width: 100%"
                    placeholder="试试搜索关键词"
                    :props="{
                      expandTrigger: 'hover',
                      value: 'id',
                      label: 'label',
                      children: 'children',
                    }"
                    @change="parentKeyChange"
                    filterable
                    clearable
                    change-on-select="true"
                  ></el-cascader>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="名称：" prop="label">
                  <el-input v-model="addOrEditForm.label"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="排序：" prop="sort">
                  <el-input v-model="addOrEditForm.sort"></el-input>
                </el-form-item>
              </el-col>
 <el-col :span="12">
                <el-form-item label="备注：" prop="remark">
                  <el-input v-model="addOrEditForm.remark"></el-input>
                </el-form-item>
              </el-col>
            
            </el-row>

            <!-- <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="传真：" prop="fax">
                  <el-input v-model="addOrEditForm.fax"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="岗位编号：" prop="postId">
                  <el-input v-model="addOrEditForm.postId"></el-input>
                </el-form-item>
              </el-col>
            </el-row> -->

            <!-- <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="通讯地址：" prop="address">
                  <el-input v-model="addOrEditForm.address"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="联系电话：" prop="telephone">
                  <el-input v-model="addOrEditForm.telephone"></el-input>
                </el-form-item>
              </el-col>
            </el-row> -->

            <!-- <el-row :gutter="20">
               <el-col :span="12">
                <el-form-item label="节点类型：" prop="organType">
                  <el-input v-model="addOrEditForm.organType"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="助记符：" prop="memoni">
                  <el-input v-model="addOrEditForm.memoni"></el-input>
                </el-form-item>
              </el-col>
            </el-row> -->

            <!-- <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="权限范围：" prop="range">
                  <el-input v-model="addOrEditForm.range"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="权限：" prop="rangeType">
                  <el-input v-model="addOrEditForm.rangeType"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="联系人：" prop="linkman">
                  <el-input v-model="addOrEditForm.linkman"></el-input>
                </el-form-item>
              </el-col>
            </el-row> -->
          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditSysOrganization()"
          >确 定</el-button
        >
      </span>
    </el-dialog>
  </div>
</template>

<script>
  import {
    listToTree,
    getTreeParents,
    getListParents,
    getTreeParentsWithSelf,
  } from '../../utils/tree.js'

  export default {
    data() {

      return {
        queryInfo: {
          query: '',
        },

        sysOrganizationList: [],
        activeName: 'first',
        // 控制添加组织机构对话框的显示与隐藏
        addOrEditDialogVisible: false,
        // 添加组织机构的表单数据
        addOrEditForm: {
          id: '',
          label: '',
          parentId: 0,
          organType: 0,
          sort: 0,
          postId: 0,
          fax: '',
          telephone: '',
          address: '',
          memoni: '',
          remark: '',
          rangeType: 0,
          range: '',
          linkman: '',



        },

        groupTree: [], //编辑组织机构 父选框
        selectedKeys: [0],
        cascaderKey: 1,
        editState: false, //编辑状态

        // 添加表单的验证规则对象
        addOrEditFormRules: {
          label: [{
              required: true,
              message: '请输入名称',
              trigger: 'blur'
            },
            {
              max: 32,
              message: '名称的长度在32字符之内',
              trigger: 'blur'
            },
          ],
          parentId: [{
            required: true,
            message: '请输入所属上级',
            trigger: 'blur'
          }, ],
          organType: [{
            required: false,
            message: '请输入节点类型',
            trigger: 'blur'
          }, ],
          sort: [{
            required: false,
            message: '请输入序号',
            trigger: 'blur'
          }, ],
          postId: [{
            required: false,
            message: '请输入岗位编号',
            trigger: 'blur'
          }, ],
          fax: [{
              required: false,
              message: '请输入传真',
              trigger: 'blur'
            },
            {
              max: 16,
              message: '传真的长度在16字符之内',
              trigger: 'blur'
            },
          ],
          telephone: [{
              required: false,
              message: '请输入联系电话',
              trigger: 'blur'
            },
            {
              max: 16,
              message: '联系电话的长度在16字符之内',
              trigger: 'blur'
            },
          ],
          address: [{
              required: false,
              message: '请输入通讯地址',
              trigger: 'blur'
            },
            {
              max: 64,
              message: '通讯地址的长度在64字符之内',
              trigger: 'blur'
            },
          ],
          memoni: [{
              required: false,
              message: '请输入助记符',
              trigger: 'blur'
            },
            {
              max: 32,
              message: '助记符的长度在32字符之内',
              trigger: 'blur'
            },
          ],
          remark: [{
              required: false,
              message: '请输入备注',
              trigger: 'blur'
            },
            {
              max: 128,
              message: '备注的长度在128字符之内',
              trigger: 'blur'
            },
          ],
          rangeType: [{
            required: false,
            message: '请输入权限',
            trigger: 'blur'
          }, ],
          range: [{
              required: false,
              message: '请输入权限范围',
              trigger: 'blur'
            },
            {
              max: 128,
              message: '权限范围的长度在128字符之内',
              trigger: 'blur'
            },
          ],
          linkman: [{
              required: false,
              message: '请输入联系人',
              trigger: 'blur'
            },
            {
              max: 32,
              message: '联系人的长度在32字符之内',
              trigger: 'blur'
            },
          ],


        },
        // 控制修改组织机构对话框的显示与隐藏
        editDialogVisible: false,

        columns: [
          //第一列默认是树形符号打开的操作，id列如果放这里会有变形
          {
            label: '名称',
            minWidth: '200px',
            prop: 'label'
          },
          // {
          //   label: '所属上级',
          //   minWidth: '100px',
          //   prop: 'parentId'
          // },
          // {
          //   label: '节点类型',
          //   minWidth: '100px',
          //   prop: 'organType'
          // },
          // {
          //   label: '序号',
          //   minWidth: '100px',
          //   prop: 'sort'
          // },
          // {
          //   label: '岗位编号',
          //   minWidth: '100px',
          //   prop: 'postId'
          // },
          // {
          //   label: '传真',
          //   minWidth: '100px',
          //   prop: 'fax'
          // },
          // {
          //   label: '联系电话',
          //   minWidth: '100px',
          //   prop: 'telephone'
          // },
          // {
          //   label: '通讯地址',
          //   minWidth: '100px',
          //   prop: 'address'
          // },
          {
            label: '助记符',
            minWidth: '100px',
            prop: 'memoni'
          },
          {
            label: '备注',
            minWidth: '100px',
            prop: 'remark'
          },
          
          // {
          //   label: '联系人',
          //   minWidth: '100px',
          //   prop: 'linkman'
          // },

          {
            label: '操作',
            minWidth: '260px',
            type: 'template',
            template: 'opt',
          },
        ]
      }
    },
    created() {
      this.getSysOrganizationList()
      //权限控制
      const isAllowed = this.$hasPermission(['sysOrganizationManager.deleteSysOrganization']) || this.$hasPermission([
        'permissionManager.editSysOrganization'
      ]);
      if (!isAllowed) { //过滤权限
        this.columns = _.cloneDeep(this.columns).filter(x => x.label !== '操作')
      }
    },
    methods: {
      async getSysOrganizationList() {

        var data = await this.$postRequest(this.$sysOrganizationManagerUrl, {
          queryString: this.queryInfo.query,
        })
        this.sysOrganizationList = listToTree(data)

      },


      // 监听添加组织机构对话框的关闭事件
      addOrEditDialogClosed() {
        this.editState = false //编辑状态改为false
        this.addOrEditForm = {
            id: '',
            label: '',
            parentId: 0,
            organType: 0,
            sort: 0,
            postId: 0,
            fax: '',
            telephone: '',
            address: '',
            memoni: '',
            remark: '',
            rangeType: 0,
            range: '',
            linkman: '',


          },
          ++this.cascaderKey //处理一些vue异常报错需要用到的key，需要加载改变地方，让它的key变化
        this.groupTree = [],
          this.selectedKeys = [], //重置父级分类选中内容
          this.$refs.addOrEditFormRef.resetFields()
      },
      // 点击按钮，添加组织机构
      addOrEditSysOrganization() {
        this.$refs.addOrEditFormRef.validate(async (valid) => {
          if (!valid) return

          if (this.addOrEditForm.id > 0) {

            //编辑状态
            // 发起修改组织机构信息的数据请求
            const {
              data: res
            } = await this.$http.put(
              this.$sysOrganizationManager_EditSysOrganizationUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('更新组织机构信息失败！' + res.message)
            }

            // 提示修改成功
            this.$message.success('更新组织机构信息成功！')
          } else {
            // 可以发起添加组织机构的网络请求
            const {
              data: res
            } = await this.$http.post(
              this.$sysOrganizationManager_CreateSysOrganizationUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              this.$message.error('添加组织机构失败！' + res.message)
            }

            this.$message.success('添加组织机构成功！')
          }

          // 隐藏添加组织机构的对话框
          this.addOrEditDialogVisible = false
          // 重新获取组织机构列表数据
          this.getSysOrganizationList()
        })
      },
      // 根据Id删除对应的组织机构信息
      async removeSysOrganizationById(id) {
        // 弹框询问组织机构是否删除数据
        const confirmResult = await this.$confirm(
          '此操作将永久删除该组织机构, 是否继续?',
          '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
          }
        ).catch((err) => err)

        // 如果组织机构确认删除，则返回值为字符串 confirm
        // 如果组织机构取消了删除，则返回值为字符串 cancel
        // console.log(confirmResult)
        if (confirmResult !== 'confirm') {
          return this.$message.info('已取消删除')
        }

        const {
          data: res
        } = await this.$http.delete(
          this.$sysOrganizationManager_DeleteSysOrganizationUrl + '?id=' + id
        )

        if (res.code !== 200) {
          return this.$message.error('删除组织机构失败！' + res.message)
        }

        this.$message.success('删除组织机构成功！')
        this.getSysOrganizationList()
      },


      // 展示组织机构的对话框
      async showAddOrEditDialog(id) {
        this.addOrEditDialogVisible = true

        if (id > 0) {
          //编辑状态
          // console.log(id)
          const {
            data: res
          } = await this.$http.get(
            this.$sysOrganizationManager_GetSysOrganizationUrl + '?id=' + id
          )

          if (res.code !== 200) {
            return this.$message.error('查询组织机构信息失败！' + res.message)
          }


          this.addOrEditForm = res.data
          const tempData = await this.$postRequest(this.$sysOrganizationManager_GetSysOrganizationFilterByPidUrl,
            "pid=" + id)
          // 分组树
          const groups = tempData
          //要过滤自身的，避免自己选自己，对应的子节点都已经在后台过滤掉了
          this.groupTree = listToTree(_.cloneDeep(groups.filter(x => x.id != id)), {
            id: 0,
            parentId: 0,
            label: '根节点'
          })

          const parents = getListParents(_.cloneDeep(groups), id);
          const parentIds = parents.map(p => p.id)
          parentIds.unshift(0); //如果父节点是0，默认是没有的，需要手动补充一个，让他找到
          this.selectedKeys = parentIds

          console.log("selectedKeys的值：" + this.selectedKeys)
          this.editState = true

        } else {
          var tempData = await this.$postRequest(this.$sysOrganizationManagerUrl, {
            queryString: '',
          })
          const groups = tempData
          this.groupTree = listToTree(_.cloneDeep(groups), {
            id: 0,
            parentId: 0,
            label: '根节点'
          })

        }
        ++this.cascaderKey
        this.addOrEditDialogVisible = true
      },
      //新增和编辑框中的父级分类 改变触发事件
      parentKeyChange() {
        ++this.cascaderKey
        console.log(this.selectedKeys)
        if (this.selectedKeys.length > 0) {
          this.addOrEditForm.parentId = this.selectedKeys[this.selectedKeys.length - 1]
        } else {
          this.addOrEditForm.parentId = 0
        }
      },

    },
  }

</script>
<style lang="less" scoped>
</style>
