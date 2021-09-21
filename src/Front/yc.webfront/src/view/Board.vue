 <template>
   <div style="">
     <!--基础统计-->
     <el-row :gutter="20">
       <el-col :span="6">
         <el-card class="box-card">
           <div class="kt-widget24">
             <div class="kt-widget24__details">
               <div class="kt-widget24__info">
                 <h4 class="kt-widget24__title">
                   新反馈
                 </h4>
                 <span class="kt-widget24__desc">
                   用户评论数
                 </span>
               </div>
               <span class="kt-widget24__stats kt-font-warning">
                 1349
               </span>
             </div>
             <div class="progress progress--sm">
               <div class="progress-bar kt-bg-warning" role="progressbar" style="width: 84%;" aria-valuenow="50"
                 aria-valuemin="0" aria-valuemax="100"></div>
             </div>
             <div class="kt-widget24__action">
               <span class="kt-widget24__change">
                 增长率
               </span>
               <span class="kt-widget24__number">
                 84%
               </span>
             </div>
           </div>
         </el-card>
       </el-col>
       <el-col :span="6">
         <el-card class="box-card">

           <div class="text item">
             <div class="kt-widget24">
               <div class="kt-widget24__details">
                 <div class="kt-widget24__info">
                   <h4 class="kt-widget24__title">
                     利润总额
                   </h4>
                   <span class="kt-widget24__desc">
                     税后价格
                   </span>
                 </div>
                 <span class="kt-widget24__stats kt-font-brand">
                   $18M
                 </span>
               </div>
               <div class="progress progress--sm">
                 <div class="progress-bar kt-bg-brand" role="progressbar" style="width: 78%;" aria-valuenow="50"
                   aria-valuemin="0" aria-valuemax="100"></div>
               </div>
               <div class="kt-widget24__action">
                 <span class="kt-widget24__change">
                   增长率
                 </span>
                 <span class="kt-widget24__number">
                   78%
                 </span>
               </div>
             </div>
           </div>
         </el-card>
       </el-col>
       <el-col :span="6">
         <el-card class="box-card">
           <div class="kt-widget24">
             <div class="kt-widget24__details">
               <div class="kt-widget24__info">
                 <h4 class="kt-widget24__title">
                   新订单
                 </h4>
                 <span class="kt-widget24__desc">
                   订单数量
                 </span>
               </div>
               <span class="kt-widget24__stats kt-font-danger">
                 567
               </span>
             </div>
             <div class="progress progress--sm">
               <div class="progress-bar kt-bg-danger" role="progressbar" style="width: 69%;" aria-valuenow="50"
                 aria-valuemin="0" aria-valuemax="100"></div>
             </div>
             <div class="kt-widget24__action">
               <span class="kt-widget24__change">
                 增长率
               </span>
               <span class="kt-widget24__number">
                 69%
               </span>
             </div>
           </div>

         </el-card>
       </el-col>
       <el-col :span="6">
         <el-card class="box-card">
           <div class="kt-widget24">
             <div class="kt-widget24__details">
               <div class="kt-widget24__info">
                 <h4 class="kt-widget24__title">
                   新用户
                 </h4>
                 <span class="kt-widget24__desc">
                   新加入用户数量
                 </span>
               </div>
               <span class="kt-widget24__stats kt-font-success">
                 276
               </span>
             </div>
             <div class="progress progress--sm">
               <div class="progress-bar kt-bg-success" role="progressbar" style="width: 90%;" aria-valuenow="50"
                 aria-valuemin="0" aria-valuemax="100"></div>
             </div>
             <div class="kt-widget24__action">
               <span class="kt-widget24__change">
                 增长率
               </span>
               <span class="kt-widget24__number">
                 90%
               </span>
             </div>
           </div>
         </el-card>
       </el-col>
     </el-row>

     <!--报表-->
     <el-row :gutter="20">
       <el-col :span="12">
         <el-card class="box-card">
           <div class="kt-portlet kt-portlet--tabs kt-portlet--height-fluid">
             <div class="kt-portlet__head">
               <div class="kt-portlet__head-label">
                 <h3 class="kt-portlet__head-title">
                   销售报表
                 </h3>
               </div>
               <div class="kt-portlet__head-toolbar">
                 <ul class="nav nav-tabs nav-tabs-line nav-tabs-bold nav-tabs-line-brand" role="tablist">
                   <li class="nav-item">
                     <a class="nav-link active" data-toggle="tab" href="#kt_widget11_tab1_content" role="tab">
                       上个月
                     </a>
                   </li>
                   <li class="nav-item">
                     <a class="nav-link" data-toggle="tab" href="#kt_widget11_tab2_content" role="tab">
                       总时间
                     </a>
                   </li>
                 </ul>
               </div>
             </div>
             <div class="kt-portlet__body">

               <!--Begin::Tab Content-->
               <div class="tab-content">

                 <!--begin::tab 1 content-->
                 <div class="tab-pane active" id="kt_widget11_tab1_content">

                   <!--begin::Widget 11-->
                   <div class="kt-widget11">
                     <div class="table-responsive">
                       <table class="table">
                         <thead>
                           <tr>
                             <td style="width:1%">#</td>
                             <td style="width:40%">应用</td>
                             <td style="width:14%">销售</td>
                             <td style="width:15%">增长率</td>
                             <td style="width:15%">状态</td>
                             <td style="width:15%" class="kt-align-right">总计</td>
                           </tr>
                         </thead>
                         <tbody>
                           <tr>
                             <td>
                               <label class="kt-checkbox kt-checkbox--single">
                                 <input type="checkbox"><span></span>
                               </label>
                             </td>
                             <td>
                               <a href="#" class="kt-widget11__title">Loop</a>
                               <span class="kt-widget11__sub">CRM 系统</span>
                             </td>
                             <td>19,200</td>
                             <td>$63</td>
                             <td><span class="kt-badge kt-badge--inline kt-badge--brand">new</span></td>
                             <td class="kt-align-right kt-font-brand kt-font-bold">$34,740</td>
                           </tr>
                           <tr>
                             <td>
                               <label class="kt-checkbox kt-checkbox--single"><input
                                   type="checkbox"><span></span></label>
                             </td>
                             <td>
                               <a href="#" class="kt-widget11__title">浏览器</a>
                               <span class="kt-widget11__sub">建站</span>
                             </td>
                             <td>24,310</td>
                             <td>$39</td>
                             <td><span class="kt-badge kt-badge--inline kt-badge--success">核准</span></td>
                             <td class="kt-align-right kt-font-brand kt-font-bold">$46,010</td>
                           </tr>
                           <tr>
                             <td>
                               <label class="kt-checkbox kt-checkbox--single"><input
                                   type="checkbox"><span></span></label>
                             </td>
                             <td>
                               <a href="#" class="kt-widget11__title">Jippo</a>
                               <span class="kt-widget11__sub">
                                 最畅销的应用程序</span>
                             </td>
                             <td>9,076</td>
                             <td>$105</td>
                             <td><span class="kt-badge kt-badge--inline kt-badge--warning">pending</span></td>
                             <td class="kt-align-right kt-font-brand kt-font-bold">$67,800</td>
                           </tr>
                           <tr>
                             <td>
                               <label class="kt-checkbox kt-checkbox--single"><input
                                   type="checkbox"><span></span></label>
                             </td>
                             <td>
                               <a href="#" class="kt-widget11__title">Verto</a>
                               <span class="kt-widget11__sub">Web 开发工具</span>
                             </td>
                             <td>11,094</td>
                             <td>$16</td>
                             <td><span class="kt-badge kt-badge--inline kt-badge--danger">on hold</span></td>
                             <td class="kt-align-right kt-font-brand kt-font-bold">$18,520</td>
                           </tr>
                         </tbody>
                       </table>
                     </div>
                     <div class="kt-widget11__action kt-align-right">
                       <button type="button" class="btn btn-label-brand btn-bold btn-sm">导入报表</button>
                     </div>
                   </div>

                   <!--end::Widget 11-->
                 </div>

                 <!--end::tab 1 content-->

               </div>

               <!--End::Tab Content-->
             </div>
           </div>

         </el-card>
       </el-col>
       <el-col :span="12">
         <el-card class="box-card">
           <div class="kt-portlet kt-portlet--height-fluid">
             <div class="kt-portlet__head">
               <div class="kt-portlet__head-label">
                 <h3 class="kt-portlet__head-title">
                   订单统计
                 </h3>
               </div>
               <div class="kt-portlet__head-toolbar">
                 <a href="#" class="btn btn-label-brand" data-toggle="dropdown">
                   Export
                 </a>
                 <div class="dropdown-menu dropdown-menu-fit dropdown-menu-right">

                   <!--begin::Nav-->
                   <ul class="kt-nav">
                     <li class="kt-nav__head">
                       Export Options
                       <i class="flaticon2-information" data-toggle="kt-tooltip" data-placement="right"
                         title="Click to learn more..."></i>
                     </li>
                     <li class="kt-nav__separator"></li>
                     <li class="kt-nav__item">
                       <a href="#" class="kt-nav__link">
                         <i class="kt-nav__link-icon flaticon2-drop"></i>
                         <span class="kt-nav__link-text">活动</span>
                       </a>
                     </li>
                     <li class="kt-nav__item">
                       <a href="#" class="kt-nav__link">
                         <i class="kt-nav__link-icon flaticon2-calendar-8"></i>
                         <span class="kt-nav__link-text">FAQ</span>
                       </a>
                     </li>
                     <li class="kt-nav__item">
                       <a href="#" class="kt-nav__link">
                         <i class="kt-nav__link-icon flaticon2-link"></i>
                         <span class="kt-nav__link-text">设置</span>
                       </a>
                     </li>
                     <li class="kt-nav__item">
                       <a href="#" class="kt-nav__link">
                         <i class="kt-nav__link-icon flaticon2-new-email"></i>
                         <span class="kt-nav__link-text">支持</span>
                         <span class="kt-nav__link-badge">
                           <span class="kt-badge kt-badge--success">5</span>
                         </span>
                       </a>
                     </li>
                     <li class="kt-nav__separator"></li>
                     <li class="kt-nav__foot">
                       <a class="btn btn-label-danger btn-bold btn-sm" href="#">升级计划</a>
                       <a class="btn btn-clean btn-bold btn-sm" href="#" data-toggle="kt-tooltip" data-placement="right"
                         title="Click to learn more...">Learn more</a>
                     </li>
                   </ul>

                   <!--end::Nav-->
                 </div>
               </div>
             </div>
             <div class="kt-portlet__body kt-portlet__body--fluid">
               <div class="kt-widget12">
                 <div class="kt-widget12__content">
                   <div class="kt-widget12__item">
                     <div class="kt-widget12__info">
                       <span class="kt-widget12__desc">年税</span>
                       <span class="kt-widget12__value">$400,000</span>
                     </div>
                     <div class="kt-widget12__info">
                       <span class="kt-widget12__desc">财务审查日期</span>
                       <span class="kt-widget12__value">July 24,2019</span>
                     </div>
                   </div>
                   <div class="kt-widget12__item">
                     <div class="kt-widget12__info">
                       <span class="kt-widget12__desc">

                         收入损失</span>
                       <span class="kt-widget12__value">$60M</span>
                     </div>
                     <div class="kt-widget12__info">
                       <span class="kt-widget12__desc">

                         收入差额</span>
                       <div class="kt-widget12__progress">
                         <div class="progress kt-progress--sm">
                           <div class="progress-bar kt-bg-brand" role="progressbar" style="width: 40%;"
                             aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>
                         </div>
                         <span class="kt-widget12__stat">
                           40%
                         </span>
                       </div>
                     </div>
                   </div>
                 </div>
                 <div class="kt-widget12__chart" style="height:250px;">
                   <!-- 2. 为ECharts准备一个具备大小（宽高）的Dom -->
                   <div id="main" style="width: 100%;height:250px;"></div>
                 </div>
               </div>
             </div>
           </div>
         </el-card>
       </el-col>
     </el-row>

   </div>
 </template>
 <script>
import * as echarts from 'echarts';
   import _ from 'lodash'

   export default {
     data() {
       return {
         // 需要合并的数据
         option: {
           title: {
             text: ''
           },
           tooltip: {
             trigger: 'axis',
             axisPointer: {
               type: 'cross',
               label: {
                 backgroundColor: '#6a7985'
               }
             }
           },
           legend: {
             data: ['Email', 'Union Ads', 'Video Ads', 'Direct', 'Search Engine']
           },
           toolbox: {
             feature: {
               saveAsImage: {}
             }
           },
           grid: {
             left: '3%',
             right: '4%',
             bottom: '3%',
             containLabel: true
           },
           xAxis: [{
             type: 'category',
             boundaryGap: false,
             data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
           }],
           yAxis: [{
             type: 'value'
           }],
           series: [{
               name: 'Email',
               type: 'line',
               stack: 'Total',
               areaStyle: {},
               emphasis: {
                 focus: 'series'
               },
               data: [120, 132, 101, 134, 90, 230, 210]
             },
             {
               name: 'Union Ads',
               type: 'line',
               stack: 'Total',
               areaStyle: {},
               emphasis: {
                 focus: 'series'
               },
               data: [220, 182, 191, 234, 290, 330, 310]
             },
             {
               name: 'Video Ads',
               type: 'line',
               stack: 'Total',
               areaStyle: {},
               emphasis: {
                 focus: 'series'
               },
               data: [150, 232, 201, 154, 190, 330, 410]
             },
             {
               name: 'Direct',
               type: 'line',
               stack: 'Total',
               areaStyle: {},
               emphasis: {
                 focus: 'series'
               },
               data: [320, 332, 301, 334, 390, 330, 320]
             },
             {
               name: 'Search Engine',
               type: 'line',
               stack: 'Total',
               label: {
                 show: true,
                 position: 'top'
               },
               areaStyle: {},
               emphasis: {
                 focus: 'series'
               },
               data: [820, 932, 901, 934, 1290, 1330, 1320]
             }
           ]
         },
       }
     },
     created() {},
     // 此时，页面上的元素，已经被渲染完毕了！
     async mounted() {
       // 3. 基于准备好的dom，初始化echarts实例
       var myChart = echarts.init(document.getElementById('main'))
       // 5. 展示数据
       myChart.setOption(this.option)
     },
     methods: {}
   }

 </script>
 <!--局部组件引入css -->
 <style scoped src="../assets/css/style.bundle.css"></style>
 <style scoped>
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

 </style>
