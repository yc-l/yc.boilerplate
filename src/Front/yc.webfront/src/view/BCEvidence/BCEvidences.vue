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
        <el-table-column prop='businessFlowName' label='业务流程类别'></el-table-column>
        <el-table-column prop='behaviorTypeName' label='行为类别'></el-table-column>
        <el-table-column prop='typeName' label='存证名称'></el-table-column>
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

              <el-row :gutter='20'>
                <el-col :span='12'>
                  <el-form-item label='业务类别：' prop='typeName'>
                    <el-select v-model="addOrEditForm.businessFlowId" placeholder="请选择" @change="showBehavior"
                      style="width: 100%">
                      <el-option v-for="item in processFlowList" :key="item.id" :label="item.name" :value="item.id">
                      </el-option>
                    </el-select>
                  </el-form-item>
                </el-col>

                <el-col :span='12'>
                  <el-form-item label='行为类别：' prop='typeName'>
                    <el-select v-model="addOrEditForm.behaviorTypeId" placeholder="请选择" style="width: 100%"
                      @change="detailBehaviorData">
                      <el-option v-for="item in behaviorList" :key="item.id" :label="item.attrs.text.text"
                        :value="item.id">
                      </el-option>
                    </el-select>
                  </el-form-item>
                </el-col>

              </el-row>


              <el-col :span='12'>
                <el-form-item label='存证名称：' prop='typeName'>
                  <el-input v-model='addOrEditForm.typeName'></el-input>
                </el-form-item>
              </el-col>
              <el-col :span='12'>
                <el-form-item label='事务Id：' prop='serviceId'>
                  <el-input v-model='addOrEditForm.serviceId'></el-input>
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
    <el-dialog :title=" '查看区块链存证'" :visible.sync="viewDialogVisible" width="80%" @close="viewDialogClosed">
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
                <el-col :span='12'>
                  <el-form-item label='业务流程：' prop='businessFlowName'>
                    <el-input v-model='viewForm.businessFlowName' disabled></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span='12'>
                  <el-form-item label='行为类别：' prop='behaviorTypeName'>
                    <el-input v-model='viewForm.behaviorTypeName' disabled></el-input>
                  </el-form-item>
                </el-col>

                <el-row :gutter='20'>
                </el-row>

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
        <el-tab-pane label="存证事件状态机" name="second">
          <!-- <el-row :gutter="20"> <p>已完成区块链存证的业务节点，可以详细查看链上数据</p></el-row>  -->
          <el-row :gutter="20">

            <el-col :span="24">
              <div id="graphViewContainer" disabled="true"></div>
            </el-col>

          </el-row>
        </el-tab-pane>
      </el-tabs>


    </el-dialog>
  </div>
</template>

<script>
  import {
    Graph,
    DataUri,
    Shape,
    Addon,
    Cell
  } from '@antv/x6';
  import {
    Router
  } from '@antv/x6/lib/registry';
  import '@antv/x6-vue-shape';
  import database from '@/components/nodeTheme/database.vue'
  import condition from '@/components/nodeTheme/condition.vue'
  import onlyout from '@/components/nodeTheme/onlyOut.vue'
  import onlyin from '@/components/nodeTheme/onlyIn.vue'
  export default {
    data() {

      return {
        queryInfo: {
          query: '',
          pageNum: 1,
          pageSize: 10,
        },
        bCEvidenceList: [],
        processFlowList: [],
        behaviorList: [],

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
          businessFlowId: '',
          behaviorTypeId: '',
          behaviorTypeName: '',
          businessFlowName: '',


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
          flowContent: '',
          businessFlowId: '',
          behaviorTypeId: '',
          behaviorTypeName: '',
          businessFlowName: '',

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
          behaviorTypeId: [{
            required: false,
            message: '请选择行为类别',
            trigger: 'blur'
          }, ],
          businessFlowId: [{
            required: false,
            message: '请请选择业务类别',
            trigger: 'blur'
          }, ],
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
        graph: null,

      }
    },
    created() {
      this.getBCEvidenceList()

    },
    methods: {
      async getFlowList() {

        const {
          data: res
        } = await this.$http.get(this.$processFlowManager_GetAllProcessFlowUrl)
        if (res.code !== 200) {
          return this.$message.error('获取流程列表失败！' + res.message)
        }
        this.processFlowList = res.data.list

      },
      //下拉显示对应行为数据
      showBehavior() {

        var list = this.processFlowList.filter(x => x.id == this.addOrEditForm.businessFlowId)

        if (list != null) {
          this.addOrEditForm.businessFlowName = list[0].name
          var tempObj = eval("(" + list[0].flowContent + ")")
          var tempBehaviorList = tempObj.nodes.filter(x => x.attrs.text.text != "开始" && x.attrs.text.text != "结束")
          this.behaviorList = tempBehaviorList

        }
      },
      //处理行为选中触发数据更新
      detailBehaviorData() {
        var businessFlowObj = this.processFlowList.filter(x => x.id == this.addOrEditForm.businessFlowId)
        if (businessFlowObj != null) {
          this.addOrEditForm.businessFlowName = businessFlowObj[0].name
        }

        var behaviorTypeObj = this.behaviorList.filter(x => x.id == this.addOrEditForm.behaviorTypeId)
        if (behaviorTypeObj != null) {

          this.addOrEditForm.behaviorTypeName = behaviorTypeObj[0].attrs.text.text
        }
      },

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
            behaviorTypeId: '',
            behaviorTypeName: '',
            businessFlowName: '',
            businessFlowId: '',

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
        this.getFlowList()
        console.log(this.processFlowList)
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
          this.showGraph(this.viewForm.flowContent)
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
            eviDataList:[],

          },
          this.$refs.viewFormRef.resetFields()
      },

      drawGraph() {
        this.graph = new Graph({
          container: document.getElementById('graphViewContainer'),
          width: 1200,
          height: 500,
          panning: true, //拖拽平移
          history: { //启动历史记录
            enabled: true,

          },
          autoPaint: true,
          fitView: true, //自适应

          modifiers: 'shift',
          mousewheel: {
            enabled: true,
            zoomAtMousePosition: true,
            modifiers: 'ctrl',
            minScale: 0.5,
            maxScale: 3,
          },
          connecting: {
            router: {
              name: 'manhattan',
              args: {
                padding: 0,
              },
            },
            connector: {
              name: 'rounded',
              args: {
                radius: 8,
              },
            },
            anchor: 'center',
            connectionPoint: 'boundary', //连接点位控制
            allowBlank: false,
            snap: true, // snap: true,自动吸附，可以用数字50 就是true
            createEdge() {
              return new Shape.Edge({
                attrs: {
                  line: {
                    stroke: '#A2B1C3',
                    strokeWidth: 2,
                    targetMarker: {
                      name: 'block',
                      width: 12,
                      height: 8,
                    },
                  },
                },
                zIndex: 0,
              })
            },
            validateConnection({
              targetMagnet
            }) {
              return !!targetMagnet
            },
          },
          highlighting: {
            magnetAdsorbed: {
              name: 'stroke',
              args: {
                attrs: {
                  fill: '#5F95FF',
                  stroke: '#5F95FF',
                },
              },
            },
          },
          selecting: {
            enabled: true,
            rubberband: true,
            showNodeSelectionBox: true,
          },

          grid: {
            size: 10, // 网格大小 10px
            visible: true, // 渲染网格背景
          },
          // 定制节点和边的交互行为
          interacting: function (cellView) {

            return {
              nodeMovable: true,
              magnetConnectable: true,
              edgeMovable: true, //边是否可以被移动
              edgeLabelMovable: true, //边的标签是否可以被移动
              arrowheadMovable: true, // 边的起始/终止箭头是否可以被移动
              vertexMovable: true, //边的路径点是否可以被移动
              vertexAddable: true, //是否可以添加边的路径点
              vertexDeletable: true //边的路径点是否可以被删除。
            }
            //return true
          },
          async: false, //支持异步，但如果有同步情况或有错误，只能通过监听 render:done 事件来确保所有变更都已经生效
          resizing: true,
          rotating: true,

          snapline: true,
          keyboard: true,
          clipboard: true
        })
        //.fromJSON(data)
        this.init()
        this.graph.on('node:mouseenter', ({
          e,
          node,
          view
        }) => {

          const labelFilter = ["开始", "结束"];
          // var temp = labelFilter.findIndex(x => x != node.label)
          // //console.log("temp:"+temp)
          // if (temp < 1) {
          //   this.showInfo(node.label)
          // }
           var tempList=this.viewForm.eviDataList
           var objList=tempList.filter(x=>x.behaviorTypeId==node.id)
           if(objList!=null){
            if(objList.length>0){
            var showInfoData="<div>交易hash："+  objList[0].transHash.slice(0,40)+"<br/>"+objList[0].transHash.slice(40)+"<br/><p> 存证数据："+  objList[0].dataValue+"</p></div>";
            this.showInfo(showInfoData)

            }
            
           }
        })

      },


      showInfo(content) {
        
       
        //  this.$message({
        //         message: content,
        //         //type: 'warning'
        //       })
        console.log(content)

        this.$alert(content, '详情存证信息', {
          dangerouslyUseHTMLString: true,
           customClass: 'msgbox'
        });
        // this.$confirm(content, '详情存证信息', {
        //     //iconClass: 'el-icon-question', // 自定义图标样式
        //     // confirmButtonText: '确认', // 确认按钮文字更换
        //     // cancelButtonText: '取消', // 取消按钮文字更换
        //     //showClose: true, // 是否显示右上角关闭按钮
        //     // type: 'warning' // 提示类型  success/info/warning/error
        //   }).then(function () {

        //   }).then((data) => {
        //     // 取消操作
        //   })
        //   .catch(function (err) {
        //     // 捕获异常
        //   })


      },

      showGraph(jsonData) {

        if (this.graph == null) {
          this.drawGraph()
          this.graph.clearCells()

        }
        jsonData = eval("(" + jsonData + ")")
        //jsonData.nodes[1].attrs.text.text = "aaaaaaaa"
        this.graph.fromJSON(jsonData)


      },

      //初始化设计器菜单
      init() {


        // #region 初始化图形
        const ports = {
          groups: {
            top: {
              position: 'top',
              attrs: {
                circle: {
                  r: 4,
                  magnet: true,
                  stroke: '#5F95FF',
                  strokeWidth: 1,
                  fill: '#fff',
                  style: {
                    visibility: 'hidden',
                  },
                },
              },
            },
            right: {
              position: 'right',
              attrs: {
                circle: {
                  r: 4,
                  magnet: true,
                  stroke: '#5F95FF',
                  strokeWidth: 1,
                  fill: '#fff',
                  style: {
                    visibility: 'hidden',
                  },
                },
              },
            },
            bottom: {
              position: 'bottom',
              attrs: {
                circle: {
                  r: 4,
                  magnet: true,
                  stroke: '#5F95FF',
                  strokeWidth: 1,
                  fill: '#fff',
                  style: {
                    visibility: 'hidden',
                  },
                },
              },
            },
            left: {
              position: 'left',
              attrs: {
                circle: {
                  r: 4,
                  magnet: true,
                  stroke: '#5F95FF',
                  strokeWidth: 1,
                  fill: '#fff',
                  style: {
                    visibility: 'hidden',
                  },
                },
              },
            },
          },
          items: [{
              group: 'top',
            },
            {
              group: 'right',
            },
            {
              group: 'bottom',
            },
            {
              group: 'left',
            },
          ],
        }

        const imageShapes = [{
            label: 'Client',
            image: 'https://gw.alipayobjects.com/zos/bmw-prod/687b6cb9-4b97-42a6-96d0-34b3099133ac.svg',
          },
          {
            label: 'Http',
            image: 'https://gw.alipayobjects.com/zos/bmw-prod/dc1ced06-417d-466f-927b-b4a4d3265791.svg',
          },
          {
            label: 'Api',
            image: 'https://gw.alipayobjects.com/zos/bmw-prod/c55d7ae1-8d20-4585-bd8f-ca23653a4489.svg',
          },
          {
            label: 'Sql',
            image: 'https://gw.alipayobjects.com/zos/bmw-prod/6eb71764-18ed-4149-b868-53ad1542c405.svg',
          },
          {
            label: 'Clound',
            image: 'https://gw.alipayobjects.com/zos/bmw-prod/c36fe7cb-dc24-4854-aeb5-88d8dc36d52e.svg',
          },
          {
            label: 'Mq',
            image: 'https://gw.alipayobjects.com/zos/bmw-prod/2010ac9f-40e7-49d4-8c4a-4fcf2f83033b.svg',
          },
        ]
        Graph.registerNode(
          'custom-rect', {
            inherit: 'rect',
            width: 66,
            height: 36,
            attrs: {
              body: {
                strokeWidth: 1,
                stroke: '#5F95FF',
                fill: '#EFF4FF',
              },
              text: {
                fontSize: 12,
                fill: '#262626',
              },
            },
            ports: {
              ...ports
            },
          },
          true,
        )

        Graph.registerNode(
          'custom-polygon', {
            inherit: 'polygon',
            width: 66,
            height: 36,
            attrs: {
              body: {
                strokeWidth: 1,
                stroke: '#5F95FF',
                fill: '#EFF4FF',
              },
              text: {
                fontSize: 12,
                fill: '#262626',
              },
            },
            ports: {
              ...ports,
              items: [{
                  group: 'top',
                },
                {
                  group: 'bottom',
                },
              ],
            },
          },
          true,
        )

        Graph.registerNode(
          'custom-circle', {
            inherit: 'circle',
            width: 45,
            height: 45,
            attrs: {
              body: {
                strokeWidth: 1,
                stroke: '#5F95FF',
                fill: '#EFF4FF',
              },
              text: {
                fontSize: 12,
                fill: '#262626',
              },
            },
            ports: {
              ...ports
            },
          },
          true,
        )

        Graph.registerNode(
          'custom-image', {
            inherit: 'rect',
            width: 52,
            height: 52,
            markup: [{
                tagName: 'rect',
                selector: 'body',
              },
              {
                tagName: 'image',
              },
              {
                tagName: 'text',
                selector: 'label',
              },
            ],
            attrs: {
              body: {
                stroke: '#5F95FF',
                fill: '#5F95FF',
              },
              image: {
                width: 26,
                height: 26,
                refX: 13,
                refY: 16,
              },
              label: {
                refX: 3,
                refY: 2,
                textAnchor: 'left',
                textVerticalAnchor: 'top',
                fontSize: 12,
                fill: '#fff',
              },
            },
            ports: {
              ...ports
            },
          },
          true,
        )

        //注册且定义新的node
        Graph.registerNode(
          'event', {
            inherit: 'circle',
            attrs: {
              body: {
                strokeWidth: 2,
                stroke: '#5F95FF',
                fill: '#FFF',
              },
            },
            ports: {
              ...ports
            },
          },
          true,
        )

        Graph.registerNode(
          'activity', {
            inherit: 'rect',
            markup: [{
                tagName: 'rect',
                selector: 'body',
              },
              {
                tagName: 'image',
                selector: 'img',
              },
              {
                tagName: 'text',
                selector: 'label',
              },
            ],
            attrs: {
              body: {
                rx: 6,
                ry: 6,
                stroke: '#5F95FF',
                fill: '#EFF4FF',
                strokeWidth: 1,
              },
              img: {
                x: 6,
                y: 6,
                width: 16,
                height: 16,
                'xlink:href': 'https://gw.alipayobjects.com/mdn/rms_43231b/afts/img/A*pwLpRr7QPGwAAAAAAAAAAAAAARQnAQ',
              },
              label: {
                fontSize: 12,
                fill: '#262626',
              },
            },
            ports: {
              ...ports,
            }
          },
          true,
        )

        Graph.registerNode(
          'gateway', {
            inherit: 'polygon',
            attrs: {
              body: {
                refPoints: '0,10 10,0 20,10 10,20',
                strokeWidth: 2,
                stroke: '#5F95FF',
                fill: '#EFF4FF',
              },
              label: {
                text: '+',
                fontSize: 40,
                fill: '#5F95FF',
              },

            },
            ports: {
              ...ports,
            }
          },
          true,
        )

        Graph.registerEdge(
          'bpmn-edge', {
            inherit: 'edge',
            attrs: {
              line: {
                stroke: '#A2B1C3',
                strokeWidth: 2,
              },
            },
            ports: { //和attrs平级
              ...ports,
            }
          },
          true,
        )



        // 注册DAG 节点
        // 注册节点
        Graph.registerNode(
          'dag-condition', {
            inherit: 'vue-shape',
            width: 180,
            height: 36,
            component: {
              template: `<condition />`,
              components: {
                condition
              }
            },
            ports: {
              ...ports
            }
          },
          true
        )
        Graph.registerNode(
          'dag-output', {
            inherit: 'vue-shape',
            width: 180,
            height: 36,
            component: {
              template: `<onlyout />`,
              components: {
                onlyout
              }
            },
            ports: {
              groups: {
                top: {
                  position: 'top',
                  attrs: {
                    circle: {
                      r: 4,
                      magnet: true,
                      stroke: '#C2C8D5',
                      strokeWidth: 1,
                      fill: '#fff'
                    }
                  }
                },
                bottom: {
                  position: 'bottom',
                  attrs: {
                    circle: {
                      r: 4,
                      magnet: true,
                      stroke: '#C2C8D5',
                      strokeWidth: 1,
                      fill: '#fff'
                    }
                  }
                }
              }
            }
          },
          true
        )
        Graph.registerNode(
          'dag-onlyIn', {
            inherit: 'vue-shape',
            width: 180,
            height: 36,
            component: {
              template: `<onlyin />`,
              components: {
                onlyin
              }
            },
            ports: {
              groups: {
                top: {
                  position: 'top',
                  attrs: {
                    circle: {
                      r: 4,
                      magnet: true,
                      stroke: '#C2C8D5',
                      strokeWidth: 1,
                      fill: '#fff'
                    }
                  }
                },
                bottom: {
                  position: 'bottom',
                  attrs: {
                    circle: {
                      r: 4,
                      magnet: true,
                      stroke: '#C2C8D5',
                      strokeWidth: 1,
                      fill: '#fff'
                    }
                  }
                }
              }
            }
          },
          true
        )
        Graph.registerNode(
          'dag-node', {
            inherit: 'vue-shape',
            width: 180,
            height: 36,
            component: {
              template: `<database />`,
              components: {
                database
              }
            },
            ports: {
              groups: {
                top: {
                  position: 'top',
                  attrs: {
                    circle: {
                      r: 4,
                      magnet: true,
                      stroke: '#C2C8D5',
                      strokeWidth: 1,
                      fill: '#fff'
                    }
                  }
                },
                bottom: {
                  position: 'bottom',
                  attrs: {
                    circle: {
                      r: 4,
                      magnet: true,
                      stroke: '#C2C8D5',
                      strokeWidth: 1,
                      fill: '#fff'
                    }
                  }
                }
              }
            }
          },
          true
        )
        Graph.registerEdge(
          'dag-edge', {
            inherit: 'edge',
            attrs: {
              line: {
                stroke: '#C2C8D5',
                strokeWidth: 2,
                targetMarker: {
                  name: 'block',
                  width: 12,
                  height: 8
                }
              }
            }
          },
          true
        )
        Graph.registerConnector(
          'algo-connector',
          (s, e) => {
            const offset = 4
            const deltaY = Math.abs(e.y - s.y)
            const control = Math.floor((deltaY / 3) * 2)

            const v1 = {
              x: s.x,
              y: s.y + offset + control
            }
            const v2 = {
              x: e.x,
              y: e.y - offset - control
            }

            return Path.normalize(
              `M ${s.x} ${s.y}
           L ${s.x} ${s.y + offset}
           C ${v1.x} ${v1.y} ${v2.x} ${v2.y} ${e.x} ${e.y - offset}
           L ${e.x} ${e.y}
          `
            )
          },
          true
        )


      },
    },
  }

</script>
<style lang="less" scoped>
.msgbox {
  width: 100%;
}
@media screen and (min-width: 768px) {
  .msgbox {
    width: 600px;
  }
}
    
</style>
