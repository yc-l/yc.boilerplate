 <template>
   <div style="overflow:hidden">
     <el-row :gutter="20">
       <el-col :span="24">
         <el-card class="box-card">
           <div slot="header" class="clearfix">
             <el-row>

               <el-button type="primary" @click="resetGraph">重置</el-button>
               <el-button type="success" @click="outJsonGraph">保存流程</el-button>
               <el-button type="success" @click="outImage">导出为图片</el-button>
                <el-button type="success" @click="viewNode">查看节点</el-button>
             </el-row>
             <header style="font-size:20px">

               <el-tooltip class="item" effect="dark" content="放大" placement="bottom">
                 <i class="el-icon-zoom-in" @click="zoomFn(0.2)" />
               </el-tooltip>
               <el-tooltip class="item" effect="dark" content="缩小" placement="bottom">
                 <i class="el-icon-zoom-out" @click="zoomFn(-0.2)" />
               </el-tooltip>
               <el-tooltip class="item" effect="dark" content="适应屏幕" placement="bottom">
                 <i class="el-icon-full-screen" @click="centerFn" />
               </el-tooltip>

               <el-tooltip class="item" effect="dark" content="保存图片" placement="bottom">
                 <i class="el-icon-upload" @click="outImage()" />
               </el-tooltip>
               <el-tooltip class="item" effect="dark" content="撤销" placement="bottom">
                 <i class="el-icon-back" @click="undo()" />
               </el-tooltip>
               <el-tooltip class="item" effect="dark" content="重做" placement="bottom">
                 <i class="el-icon-right" @click="redo()" />
               </el-tooltip>
             </header>
             <!-- <el-row :gutter="2">
               <el-divider></el-divider>
               <el-col :span="1">
                 <el-button type="info" circle @click="undo()">撤销</el-button>
               </el-col>
               <el-col :span="2">
                 <el-button type="primary"  circle @click="redo()">重做</el-button>
               </el-col>               
             </el-row> -->

           </div>
           <el-row :gutter="20">
             <el-col :span="3">
               <div id="menuContainer">abc</div>
             </el-col>
             <el-col :span="18">
               <div id="graphicContainer"></div>
             </el-col>
             <el-col :span="3">
               <el-row>
                 <div id="minimapContainer"></div>
               </el-row>
               <el-row>
                 <el-tabs value="first">
                   <el-tab-pane label="属性设置" v-if="checkControl.checkControlNode!=null" name="first">
                     <div id="attributeContainer">
                       <div v-for="(item,index) in checkControl.checkAttribulteForm" :key="index">
                         <div class="formCotantBox">
                           <!-- 表单内容 -->
                           <el-form label-width="80px">
                             <el-form-item :label="item.key">
                               <el-input v-model="item.value" :disabled="item.key=='id'" :show-overflow-tooltip="true"
                                 :blur="changeAttr(checkControl.checkControlNode,item)"></el-input>
                             </el-form-item>

                           </el-form>
                         </div>

                       </div>

                     </div>
                   </el-tab-pane>
                 </el-tabs>
               </el-row>



             </el-col>
           </el-row>

         </el-card>
       </el-col>
     </el-row>

     <exportDialog :title="exportDialogData.title" :infoContent="exportDialogData.infoContent"
       :dialogVisible="exportDialogData.dialogVisible" @handleClose="handleClose"></exportDialog>
     <contentMemu v-if="showContextMenu" ref="menuBar" @callBack="contextMenuFn" />
   </div>


 </template>

 <script>
   import {
     Graph,
     Node,
     Path,
     DataUri,
     Shape,
     Addon,
     Cell
   } from '@antv/x6';
   import {
     Router
   } from '@antv/x6/lib/registry';
   import '@antv/x6-vue-shape';
   import exportDialog from '@/components/dialog.vue';
   import contentMemu from '@/components/graphCompoments/contextMenu.vue';
   const data = {
     // 节点
     nodes: [{
         id: 'node1', // String，可选，节点的唯一标识
         x: 40, // Number，必选，节点位置的 x 值
         y: 40, // Number，必选，节点位置的 y 值
         width: 80, // Number，可选，节点大小的 width 值
         height: 40, // Number，可选，节点大小的 height 值
         label: 'hello', // String，节点标签
       },
       {
         id: 'node2', // String，节点的唯一标识
         x: 160, // Number，必选，节点位置的 x 值
         y: 180, // Number，必选，节点位置的 y 值
         width: 80, // Number，可选，节点大小的 width 值
         height: 40, // Number，可选，节点大小的 height 值
         label: 'world', // String，节点标签
       },
     ],
     // 边
     edges: [{
       source: 'node1', // String，必须，起始节点 id
       target: 'node2', // String，必须，目标节点 id
     }, ],
   };
   import database from '@/components/nodeTheme/database.vue'
   import condition from '@/components/nodeTheme/condition.vue'
   import onlyout from '@/components/nodeTheme/onlyOut.vue'
   import onlyin from '@/components/nodeTheme/onlyIn.vue'


   export default {
     components: {
       exportDialog,
       contentMemu
     },
     data() {

       return {
         graph: null,
         tempData: {
           nodes: [],
           edges: []
         },
         checkControl: {
           checkControlNode: null,
           checkAttribulteForm: [{
             key: "",
             value: null
           }],


         },
         state: {
           canRedo: false,
           canUndo: false,
         },
         exportDialogData: {
           title: '保存流程',
           infoContent: '',
           dialogVisible: false,

         },
         showContextMenu: false

       }
     },
     created() {

     },
     mounted() {
       this.init();

       // this.graph.fromJSON( eval("(" + localStorage.getItem('x6Json') + ")")) 
     },
     computed: {

       headers() {
         return {
           "Authorization": 'Bearer ' + window.sessionStorage.getItem('token')
         }
       }
     },
     methods: {

       init() {
         //画布初始化
         this.drawGraph();
         //初始化测试
         this.initTest();
         //初始化菜单
         this.initMenu();
         //初始化快捷键
         this.initShortcutKey();
         //初始化事件
         this.initEvent();


       },

     viewNode(){
      
     console.log(this.graph.getRootNodes());
     },

       drawGraph() {
         this.graph = new Graph({
           container: document.getElementById('graphicContainer'),
           width: 1200,
           height: 600,
           panning: true, //拖拽平移
           history: { //启动历史记录
             enabled: true,

           },
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
           minimap: {
             width: 200,
             height: 200,
             padding: 5,
             enabled: true,
             container: minimapContainer,
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

       },

       //初始化设计器菜单
       initMenu() {

         // #region 初始化 stencil,添加菜单
         const stencil = new Addon.Stencil({
           title: '流程图',
           target: this.graph,
           stencilGraphWidth: 300,
           stencilGraphHeight: 380,
           collapsable: true,
           groups: [{
               title: '基础流程图',
               name: 'group1',
             },
             {
               title: '自动化合约设计',
               name: 'group2',
               graphHeight: 250,
               layoutOptions: {
                 rowHeight: 70,
               },
             },
             {
               title: 'AI引擎',
               name: 'group3',
               graphHeight: 250,
               layoutOptions: {
                 rowHeight: 70,

               },
             },
           ],
           layoutOptions: {
             columns: 2,
             columnWidth: 100,
             rowHeight: 55,
           },
         })
         document.getElementById('menuContainer').appendChild(stencil.container)


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
         const imageNodes = imageShapes.map((item) =>
           this.graph.createNode({
             shape: 'custom-image',
             label: item.label,
             attrs: {
               image: {
                 'xlink:href': item.image,
               },
             },
           }),
         )

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
         const r1 = this.graph.createNode({
           shape: 'custom-rect',
           label: '开始',
           attrs: {
             body: {
               rx: 20,
               ry: 26,
             },
           },
         })
         const r2 = this.graph.createNode({
           shape: 'custom-rect',
           label: '过程',
         })
         const r3 = this.graph.createNode({
           shape: 'custom-rect',
           attrs: {
             body: {
               rx: 6,
               ry: 6,
             },
           },
           label: '可选过程',
         })
         const r4 = this.graph.createNode({
           shape: 'custom-polygon',
           attrs: {
             body: {
               refPoints: '0,10 10,0 20,10 10,20',
             },
           },
           label: '决策',
         })
         const r5 = this.graph.createNode({
           shape: 'custom-polygon',
           attrs: {
             body: {
               refPoints: '10,0 40,0 30,20 0,20',
             },
           },
           label: '数据',
         })
         const r6 = this.graph.createNode({
           shape: 'custom-circle',
           label: '连接',
         })
         stencil.load([r1, r2, r3, r4, r5, r6].concat(imageNodes), 'group1')

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


         const t1 = this.graph.createNode({
           shape: 'activity',
           label: '动作',
           description: '描述文本xxxxxxxxxxx',
           width: 80,
           height: 45,
         })

         const t2 = this.graph.createNode({
           shape: 'event',
           width: 40,
           height: 40,
         })

         const t3 = this.graph.createNode({
           shape: 'gateway',
           width: 55,
           height: 55,
           attrs: {
             body: {
               refPoints: '0,10 10,0 20,10 10,20',
             },
           },
         })

         stencil.load([t1, t2, t3], 'group2')

         // 注册DAG 节点
         // 注册节点
        Graph.registerNode(
        'dag-condition',
        {
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

         const d1 = this.graph.createNode({
           shape: 'dag-condition',
           width: 180,
           height: 36,
           data: {
                  label: '动作',
                 },
         })
         const d2 = this.graph.createNode({
           shape: 'dag-onlyIn',
           label: '动作',
           width: 180,
           height: 36,
         })

        
         //stencil.load([d1], 'group3')
       },

       //初始化快捷键
       initShortcutKey() {
         // #region 快捷键与事件
         // copy cut paste
         this.graph.bindKey(['meta+c', 'ctrl+c'], () => {
           const cells = this.graph.getSelectedCells()
           if (cells.length) {
             this.graph.copy(cells)
           }
           return false
         })
         this.graph.bindKey(['meta+x', 'ctrl+x'], () => {
           const cells = this.graph.getSelectedCells()
           if (cells.length) {
             this.graph.cut(cells)
           }
           return false
         })
         this.graph.bindKey(['meta+v', 'ctrl+v'], () => {
           if (!this.graph.isClipboardEmpty()) {
             const cells = this.graph.paste({
               offset: 32
             })
             this.graph.cleanSelection()
             this.graph.select(cells)
           }
           return false
         })
         //undo redo
         this.graph.bindKey(['meta+z', 'ctrl+z'], () => {
           if (this.graph.history.canUndo()) {
             this.graph.history.undo()
           }
           return false
         })
         this.graph.bindKey(['meta+shift+z', 'ctrl+shift+z'], () => {
           if (this.graph.history.canRedo()) {
             this.graph.history.redo()
           }
           return false
         })
         // select all
         this.graph.bindKey(['meta+a', 'ctrl+a'], () => {
           const nodes = this.graph.getNodes()
           if (nodes) {
             this.graph.select(nodes)
           }
         })

         //delete
         this.graph.bindKey('backspace', () => {
           const cells = this.graph.getSelectedCells()
           if (cells.length) {
             this.graph.removeCells(cells)
           }
         })


       },

       //修改选中节点属性
       changeAttr(item, data) {
         if (data.key == "label") {
           item.label = data.value
         }
        
       
       },
       //初始化测试
       initTest() {
         const rect = new Shape.Rect({
           x: 100,
           y: 200,
           id: 'rect',
           width: 80,
           height: 40,
           angle: 30,
           attrs: {
             body: {
               fill: 'blue',
             },
             label: {
               text: 'Hello',
               fill: 'white',
             },
           },
         })
         // 添加到画布
         //this.graph.addNode(rect)

         // 创建边
         const edge = new Shape.Edge({
           source: 'node1',
           target: 'rect', // target: 'rect', 模式找id target: rect,找对象
         })
         // 添加到画布
         //this.graph.addEdge(edge)
       },
       //初始化事件
       initEvent() {

         // 控制连接桩显示/隐藏
         const showPorts = (ports, show) => {
           for (let i = 0, len = ports.length; i < len; i = i + 1) {
             ports[i].style.visibility = show ? 'visible' : 'hidden'
           }
         }

         this.graph.on('node:mouseenter', () => {
           const container = document.getElementById('graphicContainer')
           const ports = container.querySelectorAll('.x6-port-body')
           showPorts(ports, true)
         })
         this.graph.on('node:mouseleave', () => {
           const container = document.getElementById('graphicContainer')
           const ports = container.querySelectorAll('.x6-port-body')
           showPorts(ports, false)
         })
         //单击事件
         this.graph.on('node:click', ({
           e,
           x,
           y,
           node,
           view
         }) => {
           console.log(node)

           //先清空
           this.checkControl = {
             checkControlNode: node,
             checkAttribulteForm: []
           };

           //获取动态属性
           this.checkControl.checkAttribulteForm.push({
             key: "id",
             value: node.id
           }, {
             key: "label",
             value: node.label
           })
           console.log(e)
         })

         //节点的右键事件注册菜单
         this.graph.on('node:contextmenu', ({
           e,
           x,
           y,
           node,
           view
         }) => {
           this.showContextMenu = true

           this.$nextTick(() => {
             // this.$refs.menuBar.setItem({ type: 'node', item: node })
             const p = this.graph.localToPage(x, y)
             this.$refs.menuBar.initFn(p.x, p.y, {
               type: 'node',
               item: node
             })
           })
         })
         //边的右键事件注册菜单
         this.graph.on('edge:contextmenu', ({
           e,
           x,
           y,
           edge,
           view
         }) => {
           this.showContextMenu = true
           this.$nextTick(() => {
             const p = this.graph.localToPage(x, y)
             this.$refs.menuBar.initFn(p.x, p.y, {
               type: 'edge',
               item: edge
             })
             //this.$refs.menuBar.initFn(e.offsetX, e.offsetY, {type: 'edge', item: edge})
           })
         })
       },
       centerFn() {
         const num = 1 - this.graph.zoom()
         num > 1 ? this.graph.zoom(num * -1) : this.graph.zoom(num)
         this.graph.centerContent()
       },

       //放大 
       zoomFn(num) {
         this.graph.zoom(num)
       },
       //重做
       redo() {
         this.graph.history.redo()
       },
       //撤销
       undo() {
         this.graph.history.undo()
       },
       //重置
       resetGraph() {
         this.graph.clearCells()
       },
       //导出图片
       outImage() {
         this.graph.toPNG((dataUri) => {
           DataUri.downloadDataUri(dataUri, 'chart.png')
         })

       },
       handleClose(res) {
         this.exportDialogData.dialogVisible = res
       },
       //导出Json
       outJsonGraph() {
         var graphJsonData = this.graph.toJSON({
           diff: true
         })
         this.tempData.nodes = this.graph.getNodes()
         this.tempData.edges = this.graph.getEdges()
         console.log(this.tempData)
         this.resetGraph()


         var jsonData = JSON.stringify(graphJsonData)

         var graphObj = eval("(" + jsonData + ")") //将json字符串转json object 对象
         this.exportDialogData.infoContent = JSON.stringify(this.tempData, null, 4) //格式化显示json
         this.exportDialogData.dialogVisible = true
         //this.graph.fromJSON(graphObj)//这个格式不一样
         //localStorage.setItem('x6Json', JSON.stringify(this.graph.toJSON({diff: true})))
         //var result = jsonData.parseJSON(jsonData);//jq 转成JSON对象，这里不适用      
         //console.log(jsonData)
         //console.log(result)
       },
       //显示菜单
       contextMenuFn(type, node) {
         switch (type) {
           case 'remove':
             if (node.type == 'edge') {
               this.graph.removeEdge(node.item.id)
             } else if (node.type == 'node') {
               this.graph.removeNode(node.item.id)
             }
             break
           case "source":
             this.$refs.dialogMysql.visible = true
             this.$refs.dialogMysql.init(node)
             break;
         }

         this.showContextMenu = false
       },
     }
   }

 </script>
 <style lang="less" scoped>
 </style>

 <style>
   #stencil {
     width: 180px;
     height: 100%;
     position: relative;
     border-right: 1px solid #dfe3e8;
   }

   #graph-container {
     width: calc(100% - 180px);
     height: 100%;
   }

   .x6-widget-stencil {
     background-color: #fff;
   }

   .x6-widget-stencil-title {
     background-color: #fff;
   }

   .x6-widget-stencil-group-title {
     background-color: #fff !important;
   }

   .x6-widget-transform {
     margin: -1px 0 0 -1px;
     padding: 0px;
     border: 1px solid #239edd;
   }

   .x6-widget-transform>div {
     border: 1px solid #239edd;
   }

   .x6-widget-transform>div:hover {
     background-color: #3dafe4;
   }

   .x6-widget-transform-active-handle {
     background-color: #3dafe4;
   }

   .x6-widget-transform-resize {
     border-radius: 0;
   }

   .x6-widget-selection-inner {
     border: 1px solid #239edd;
   }

   .x6-widget-selection-box {
     opacity: 0;
   }



   .el-row {
     margin-bottom: 20px;
   }

   .el-col {
     border-radius: 4px;
   }

   .bg-purple-dark {
     background: #ffff;
   }

   .bg-purple {
     background: #ffff;
   }

   .bg-purple-light {
     background: #ffff;
   }

   .grid-content {
     border-radius: 4px;
     min-height: 36px;
   }

   .row-bg {
     padding: 10px 0;
     background-color: #ffff;
   }

   .avatar-uploader .el-upload {
     border: 1px dashed #d9d9d9;
     border-radius: 6px;
     cursor: pointer;
     position: relative;
     overflow: hidden;
   }

   .avatar-uploader .el-upload:hover {
     border-color: #409EFF;
   }

   .avatar-uploader-icon {
     font-size: 28px;
     color: #8c939d;
     width: 178px;
     height: 178px;
     line-height: 178px;
     text-align: center;
   }

   .avatar {
     width: 178px;
     height: 178px;
     display: block;
   }

   #graphicContainer {
     width: 100%;
     height: 800px;
   }

 </style>
