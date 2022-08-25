 <template>
   <div style="overflow:hidden">
     <el-row :gutter="20">
       <el-col :span="24">
         <el-card class="box-card">
           <div slot="header" class="clearfix">
             <el-row>
               <el-button>默认按钮</el-button>
               <el-button type="primary">重置</el-button>
               <el-button type="success">保存</el-button>
             </el-row>
           </div>
           <div id="graphicContainer"></div>
         </el-card>
       </el-col>
     </el-row>


   </div>
 </template>

 <script>
   import {
     Graph
   } from '@antv/x6';

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


   export default {

     data() {

       return {
         graph: null,
       }
     },
     created() {

     },
     mounted() {
       this.init();

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
         this.drawGraph();

       },

       drawGraph() {
         const graph = new Graph({
           container: document.getElementById('graphicContainer'),
           width: '100%',
           height: 600,
           panning: true, //拖拽平移
           modifiers: 'shift',
          grid: {
           size: 10,      // 网格大小 10px
          visible: true, // 渲染网格背景
         }}
         ).fromJSON(data)
       }
   }
   }
 </script>
 <style lang="less" scoped>
 </style>

 <style>
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
