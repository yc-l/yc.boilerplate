<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>角色权限管理</el-breadcrumb-item>
      <el-breadcrumb-item>角色权限列表</el-breadcrumb-item>
    </el-breadcrumb>

    <el-row :gutter="20">
      <!-- 左侧 选择角色区域 -->
      <el-col :span="4">
        <el-card>
          <!--  <el-button type="primary" @click="showPermission()"
            >获取选中权限</el-button
          > -->
          <template #header>
            <div class="clearfix">
              <span>角色</span>
              <el-button
                type="text"
                style="float: right; padding: 3px 0"
                @click="refleshRoles()"
                >刷新</el-button
              >
            </div>
          </template>
          <div
            v-for="r in roleDataList"
            :key="r.id"
            :class="r.id == roleId ? 'active' : ''"
            class="item role-item"
            @click="roleSelect(r.id)"
          >
            <span>{{ r.name }}</span>
            <!--  <span style="float: right">{{ r.memoni }}</span> -->
          </div>
        </el-card>
      </el-col>
      <el-col :span="20">
        <!-- 卡片列表区域 -->
        <el-card>
          <template #header>
            <div class="clearfix">
              <span>权限列表</span>
              <el-button
                type="text"
                style="float: right; padding: 5px 0; margin-left: 20px"
                @click="saveRolePermissions()" v-if="$hasPermission(['rolePermissionManager.editRolePermission'])"
                >保存</el-button
              >
              <el-button
                type="text"
                style="float: right; padding: 5px 0"
                @click="refreshPermissionList()"
                >刷新
              </el-button>
            </div>
          </template>
          <!-- 权限列表 -->
          <tree-table
            ref="treeTable"
            class="tree-table"
            :selection-type="true"
            :expand-type="false"
            :data="sysPermissionList"
            :columns="columns"
            :show-index="true"
            border
            stripe
            show-row-hover
            tree-type
            :is-fold="true"
          >
            <template slot="typeName" slot-scope="scope">
              <el-tag v-if="scope.row.typeName === '分组'">
                {{ scope.row.typeName }}
              </el-tag>
              <el-tag type="success" v-else-if="scope.row.typeName === '菜单'">
                {{ scope.row.typeName }}
              </el-tag>
              <el-tag type="info" v-else-if="scope.row.typeName === '操作点'">
                {{ scope.row.typeName }}
              </el-tag>
            </template>
          </tree-table>
        </el-card>
      </el-col>
    </el-row>
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
        roleId: 0,
        sysPermissionList: [],
        permissionTypeOptions: [{
          value: 1,
          label: '分组'
        }, {
          value: 2,
          label: '菜单'
        }, {
          value: 3,
          label: '操作点'
        }],
        groupTree: [], //编辑权限 父选框
        selectedKeys: [0],
        cascaderKey: 1,

        // 控制修改权限对话框的显示与隐藏
        editDialogVisible: false,

        columns: [{ //第一列默认是树形符号打开的操作，id列如果放这里会有变形
            label: '权限名称',
            minWidth: '260px',
            prop: 'label'
          },
          {
            label: '权限类型',
            prop: 'typeName',
            type: 'template',
            template: 'typeName',
          },
          {
            label: '视图',
            prop: 'view'
          },
          {
            label: '接口',
            width: '130px',
            prop: 'api'
          },
          {
            label: '菜单访问地址',
            width: '230px',
            prop: 'path'
          },

        ],
        //角色列表
        roleDataList: [],
        //选中的权限集合
        selectPermissionKey: [],
      }
    },
    created() {
      this.getRoleList()
      this.getSysPermissionList()
    },
    methods: {
      //行选中项操作   
      roleSelect(id) {
        this.roleId = id;
        //重置选中项
        //1、重置权限列表，清空选中
        this.resetCheck()
        this.refleshRoles()//重新刷新数据,权限数据需要来源于刷新后台得到最新记录
        //2、对关联数据进行checked
        const tempRolePermissionIds=_.cloneDeep(this.roleDataList).filter(x=>x.id==id)[0].permissionIds
        this.$refs.treeTable.bodyData.forEach((item, index) => {
          if (tempRolePermissionIds.filter(l=>l===item['id']).length >0) { //针对关联权限打钩
            item._isChecked = true
          }

        })

      },
      //重置选中
      resetCheck() {

        this.$refs.treeTable.bodyData.forEach((item, index) => {
          if (item._isChecked) {
            item._isChecked = false
          }

        })

      },
      //刷新权限列表
      refreshPermissionList() {
        this.selectPermissionKey = []
        this.getSysPermissionList();
      },

      //刷新角色列表
      refleshRoles() {

        this.getRoleList()
      },
      //保存用户权限记录
      async saveRolePermissions() {
        const checkedPermissions = this.$refs.treeTable.getCheckedProp('id')//有几率出现状态没有清空
       /*  if(checkedPermissions.length==0){
                  this.$message.error('请先勾选的权限！')
        } */
        if(this.roleId==0){
          this.$message.error('请先选中对应的角色！')
        }

            const { data: res } = await this.$http.put(
          this.$sysRoleManager_UpdateSysRolePermissionsUrl,
            {
            id:this.roleId,
            permissionIds:checkedPermissions
          }
          )

        if (res.code !== 200) {
          return this.$message.error(res.message)
        }

        this.$message.success('更新角色权限成功！')
      },

      async getRoleList() {
        var data = await this.$getRequest(this.$sysRoleManagerUrl_GetSysRoleListUrl)
        this.roleDataList = data
      },
      async getSysPermissionList() {

        var data = await this.$postRequest(this.$sysPermissionManagerUrl, {
          queryString: this.queryInfo.query,
        })
        this.sysPermissionList = listToTree(data)

      },
      //选择角色点击时候触发
      roleHandle(row, event, column) {
        console.log(row, event, column)
      },
      showPermission() {

        const data = this.$refs.treeTable.getCheckedProp('id');


        this.$refs.treeTable.bodyData.forEach((item, index) => {
          if (!item._isChecked) {
            item._isChecked = true
          }

        })
        console.log("选中的权限：" + data.join(','))
      },


    },
  }

</script>
<style lang="less" scoped>
.role-item {
  font-size: 14px;
  cursor: pointer;
  padding: 10px;
}

.role-item.active {
  background: #ebf5ff;
}

.role-item:hover {
  background: #ebf5ff;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: '';
}

.clearfix:after {
  clear: both;
}
</style>
