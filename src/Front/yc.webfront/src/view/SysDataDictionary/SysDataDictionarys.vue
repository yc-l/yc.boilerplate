<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>数据字典管理</el-breadcrumb-item>
      <el-breadcrumb-item>数据字典列表</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片试图区域 -->
    <el-card>
      <!-- 搜索与添加区域 -->

      <el-row :gutter="20">
        <el-col :span="7">
          <el-input placeholder="请输入搜索的数据字典名称" class="input-with-select" v-model="queryInfo.query" clearable
            @clear="getSysDataDictionaryList">
            <el-button slot="append" icon="el-icon-search" @click="getSysDataDictionaryList"></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" @click="showAddOrEditDialog()"
            v-if="$hasPermission(['dataDictionaryManager.createDataDictionary'])">添加数据字典</el-button>
        </el-col>
      </el-row>
      <!-- 数据字典列表 -->
      <tree-table class="tree-table" :selection-type="false" :expand-type="false" :is-fold="false"
        :data="sysDataDictionaryList" :columns="columns" :show-index="true" border stripe show-row-hover tree-type>
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
          <el-button type="primary" icon="el-icon-edit" size="mini" @click="showAddOrEditDialog(scope.row.id)"
            v-if="$hasPermission(['dataDictionaryManager.editDataDictionary'])">编辑
          </el-button>
          <!-- 删除按钮 -->
          <el-button type="danger" icon="el-icon-delete" size="mini" @click="removeSysDataDictionaryById(scope.row.id)"
            v-if="$hasPermission(['dataDictionaryManager.deleteDataDictionary'])">
            删除
          </el-button>
        </template>
      </tree-table>
    </el-card>

    <!-- 添加数据字典的对话框 -->
    <el-dialog :title="(addOrEditForm.id > 0 ? '编辑' : '新增') + '数据字典'" :visible.sync="addOrEditDialogVisible" width="60%"
      @close="addOrEditDialogClosed">
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="数据字典管理" name="first">
          <el-form :model="addOrEditForm" :rules="addOrEditFormRules" ref="addOrEditFormRef" label-width="120px">
            <el-input v-model="addOrEditForm.id" prop="id" type="hidden"></el-input>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="名称：" prop="label">
                  <el-input v-model="addOrEditForm.label"></el-input>
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item label="编码：" prop="key">
                  <el-input v-model="addOrEditForm.key"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <!-- <el-col :span="12">
                <el-form-item label="类型：" prop="type">
                  <el-input v-model="addOrEditForm.type"></el-input>
                </el-form-item>
              </el-col> -->

              <el-col :span="12">
                <el-form-item label="父级节点：" prop="selectedKeys">
                  <el-cascader :key="cascaderKey" v-model="selectedKeys" :options="groupTree" style="width: 100%"
                    placeholder="试试搜索关键词" :props="{
                      expandTrigger: 'hover',
                      value: 'id',
                      label: 'label',
                      children: 'children',
                    }" @change="parentKeyChange" filterable clearable change-on-select="true"></el-cascader>
                </el-form-item>
              </el-col>
               <el-col :span="12">
                <el-form-item label="排序：" prop="sort">
                  <el-input v-model="addOrEditForm.sort"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
             

              <el-col :span="12">
                <el-form-item label="助记符：" prop="memoni">
                  <el-input v-model="addOrEditForm.memoni"></el-input>
                </el-form-item>
              </el-col>
            </el-row>

            <!-- <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="备注：" prop="value">
                  <el-input v-model="addOrEditForm.value"></el-input>
                </el-form-item>
              </el-col>
            </el-row> -->
          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrEditDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditSysDataDictionary()">确 定</el-button>
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

        sysDataDictionaryList: [],
        activeName: 'first',
        // 控制添加数据字典对话框的显示与隐藏
        addOrEditDialogVisible: false,
        // 添加数据字典的表单数据
        addOrEditForm: {
          id: '',
          key: '',
          label: '',
          parentId: 0,
          type: 0,
          memoni: '',
          sort: 0,
          value: '',



        },

        groupTree: [], //编辑数据字典 父选框
        selectedKeys: [0],
        cascaderKey: 1,
        editState: false, //编辑状态

        // 添加表单的验证规则对象
        addOrEditFormRules: {
          key: [{
              required: true,
              message: '请输入编码',
              trigger: 'blur'
            },
            {
              max: 32,
              message: '编码的长度在32字符之内',
              trigger: 'blur'
            },
          ],
          label: [{
              required: true,
              message: '请输入名称',
              trigger: 'blur'
            },
            {
              max: 50,
              message: '名称的长度在50字符之内',
              trigger: 'blur'
            },
          ],
          parentId: [{
            required: true,
            message: '请输入父节点',
            trigger: 'blur'
          },
          
          ],
          type: [{
            required: false,
            message: '请输入类型',
            trigger: 'blur'
          }, ],
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
          sort: [{
            required: false,
            message: '请输入排序',
            trigger: 'blur'
          }, ],
          value: [{
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


        },
        // 控制修改数据字典对话框的显示与隐藏
        editDialogVisible: false,

        columns: [
          //第一列默认是树形符号打开的操作，id列如果放这里会有变形
          {
            label: '编码',
            minWidth: '260px',
            prop: 'key'
          },
          {
            label: '名称',
            minWidth: '100px',
            prop: 'label'
          },
          // {
          //   label: '父节点',
          //   minWidth: '100px',
          //   prop: 'parentId'
          // },
          // {
          //   label: '类型',
          //   minWidth: '100px',
          //   prop: 'type'
          // },
          {
            label: '助记符',
            minWidth: '100px',
            prop: 'memoni'
          },
          {
            label: '排序',
            minWidth: '100px',
            prop: 'sort'
          },
          {
            label: '备注',
            minWidth: '100px',
            prop: 'value'
          },

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
      this.getSysDataDictionaryList()
      const isAllowed = this.$hasPermission(['dataDictionaryManager.deleteDataDictionary']) || this.$hasPermission([
        'dataDictionaryManager.editDataDictionary'
      ]);
      if (!isAllowed) { //过滤权限
        this.columns = _.cloneDeep(this.columns).filter(x => x.label !== '操作')
      }
    },


    methods: {
      async getSysDataDictionaryList() {

        var data = await this.$postRequest(this.$sysDataDictionaryManagerUrl, {
          queryString: this.queryInfo.query,
        })
        this.sysDataDictionaryList = listToTree(data)

      },


      // 监听添加数据字典对话框的关闭事件
      addOrEditDialogClosed() {
        this.editState = false //编辑状态改为false
        this.addOrEditForm = {
            id: '',
            key: '',
            label: '',
            parentId: 0,
            type: 0,
            memoni: '',
            sort: 0,
            value: '',


          },
          ++this.cascaderKey //处理一些vue异常报错需要用到的key，需要加载改变地方，让它的key变化
        this.groupTree = [],
          this.selectedKeys = [], //重置父级分类选中内容
          this.$refs.addOrEditFormRef.resetFields()
      },
      // 点击按钮，添加数据字典
      addOrEditSysDataDictionary() {
        this.$refs.addOrEditFormRef.validate(async (valid) => {
          if (!valid) return

          if (this.addOrEditForm.id > 0) {

            //编辑状态
            // 发起修改数据字典信息的数据请求
            const {
              data: res
            } = await this.$http.put(
              this.$sysDataDictionaryManager_EditSysDataDictionaryUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('更新数据字典信息失败！' + res.message)
            }else{
             // 提示修改成功
            this.$message.success('更新数据字典信息成功！')
            }

          
          } else {
            // 可以发起添加数据字典的网络请求
            const {
              data: res
            } = await this.$http.post(
              this.$sysDataDictionaryManager_CreateSysDataDictionaryUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
            return  this.$message.error('添加数据字典失败！' + res.message)
            }else{

               this.$message.success('添加数据字典成功！')
            }

           
          }

          // 隐藏添加数据字典的对话框
          this.addOrEditDialogVisible = false
          // 重新获取数据字典列表数据
          this.getSysDataDictionaryList()
        })
      },
      // 根据Id删除对应的数据字典信息
      async removeSysDataDictionaryById(id) {
        // 弹框询问数据字典是否删除数据
        const confirmResult = await this.$confirm(
          '此操作将永久删除该数据字典, 是否继续?',
          '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
          }
        ).catch((err) => err)

        // 如果数据字典确认删除，则返回值为字符串 confirm
        // 如果数据字典取消了删除，则返回值为字符串 cancel
        // console.log(confirmResult)
        if (confirmResult !== 'confirm') {
          return this.$message.info('已取消删除')
        }

        const {
          data: res
        } = await this.$http.delete(
          this.$sysDataDictionaryManager_DeleteSysDataDictionaryUrl + '?id=' + id
        )

        if (res.code !== 200) {
          return this.$message.error('删除数据字典失败！' + res.message)
        }

        this.$message.success('删除数据字典成功！')
        this.getSysDataDictionaryList()
      },


      // 展示数据字典的对话框
      async showAddOrEditDialog(id) {
        this.addOrEditDialogVisible = true

        if (id > 0) {
          //编辑状态
          // console.log(id)
          const {
            data: res
          } = await this.$http.get(
            this.$sysDataDictionaryManager_GetSysDataDictionaryUrl + '?id=' + id
          )

          if (res.code !== 200) {
            return this.$message.error('查询数据字典信息失败！' + res.message)
          }


          this.addOrEditForm = res.data
          const tempData = await this.$postRequest(this.$sysDataDictionaryManager_GetSysDataDictionaryFilterByPidUrl,
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
          var tempData = await this.$postRequest(this.$sysDataDictionaryManagerUrl, {
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
