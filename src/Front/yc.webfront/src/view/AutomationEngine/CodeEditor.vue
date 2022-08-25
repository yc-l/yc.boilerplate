<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>自动化引擎</el-breadcrumb-item>
      <el-breadcrumb-item>代码编辑器</el-breadcrumb-item>
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
              <span>文件目录</span>
              <el-button type="text" style="float: right; padding: 3px 0" click="">刷新</el-button>
            </div>
          </template>
          ddd
        </el-card>
      </el-col>
      <el-col :span="20">
        <!-- 卡片列表区域 -->
        <el-card>
          <template #header>

            <div class="clearfix">
              <el-select class="code-mode-select" v-model="mode" @change="changeMode">
                <el-option v-for="mode in modes" :key="mode.value" :label="mode.label" :value="mode.value">
                </el-option>
              </el-select>
              <el-button type="text" style="float: right; padding: 5px 0; margin-left: 20px"
                @click="saveRolePermissions()" v-if="$hasPermission(['rolePermissionManager.editRolePermission'])">保存
              </el-button>
              <el-button type="text" style="float: right; padding: 5px 0" @click="refreshPermissionList()">刷新
              </el-button>
            </div>
          </template>
          <!-- 操作列表 -->

          <template>
            <textarea ref="mycode" class="codesql" v-model="code"></textarea>
          </template>


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

  import "codemirror/theme/monokai.css";
  import "codemirror/lib/codemirror.css";
  import "codemirror/addon/hint/show-hint.css";
  import "codemirror/addon/fold/foldgutter.css";

  let CodeMirror = require("codemirror/lib/codemirror");
  require("codemirror/addon/edit/matchbrackets");
  require("codemirror/addon/selection/active-line");
  require("codemirror/mode/sql/sql");
  require("codemirror/addon/hint/show-hint");
  require("codemirror/addon/hint/sql-hint");
  require("codemirror/addon/fold/brace-fold");
  require("codemirror/addon/fold/comment-fold");
  require("codemirror/addon/fold/foldcode");
  require("codemirror/addon/fold/foldgutter");
  require("codemirror/addon/fold/indent-fold");

  import 'codemirror/mode/javascript/javascript.js'
  import 'codemirror/mode/css/css.js'
  import 'codemirror/mode/xml/xml.js'
  import 'codemirror/mode/clike/clike.js'
  import 'codemirror/mode/markdown/markdown.js'
  import 'codemirror/mode/python/python.js'
  import 'codemirror/mode/r/r.js'
  import 'codemirror/mode/shell/shell.js'
  import 'codemirror/mode/swift/swift.js'
  import 'codemirror/mode/vue/vue.js'

  export default {
    name: "codeMirror",
    props: {
      // 外部传入的内容，用于实现双向绑定
      value: String,
      // 外部传入的语法类型
      language: {
        type: String,
        default: null
      }
    },
    data() {
      return {
        code: '//按Tab键进行代码智能提示',
        // 默认的语法类型
        mode: 'x-csharp',
        // 编辑器实例
        editor: null,

        // 支持切换的语法高亮类型，对应 JS 已经提前引入
        // 使用的是 MIME-TYPE ，不过作为前缀的 text/ 在后面指定时写死了
        modes: [{
            value: 'x-csharp',
            label: 'C#'
          },
          {
            value: 'css',
            label: 'CSS'
          }, {
            value: 'javascript',
            label: 'Javascript'
          }, {
            value: 'html',
            label: 'XML/HTML'
          }, {
            value: 'x-java',
            label: 'Java'
          }, {
            value: 'x-objectivec',
            label: 'Objective-C'
          }, {
            value: 'x-python',
            label: 'Python'
          }, {
            value: 'x-rsrc',
            label: 'R'
          }, {
            value: 'x-sh',
            label: 'Shell'
          }, {
            value: 'x-sql',
            label: 'SQL'
          }, {
            value: 'x-swift',
            label: 'Swift'
          }, {
            value: 'x-vue',
            label: 'Vue'
          }, {
            value: 'markdown',
            label: 'Markdown'
          }
        ]

      }
    },

    //created 在模板渲染成html前调用，即通常初始化某些属性值，然后再渲染成视图
    //mounted在模板渲染成html后调用，通常是初始化页面完成后，再对html的dom节点进行一些需要的操作。
    mounted() {

      this._initialize()
    },

    methods: {

      // 初始化
      _initialize() {
     
        // 初始化编辑器实例，传入需要被实例化的文本域对象和默认配置
        this.editor = CodeMirror.fromTextArea(this.$refs.mycode, {
          mode: "text/"+this.mode, //选择对应代码编辑器的语言，根据个人情况自行设置即可
          indentWithTabs: true,
          smartIndent: true,
          lineNumbers: true,
          matchBrackets: true, //括号匹配
          lineWrapping: true, //代码折叠
          foldGutter: true,
          //readOnly: true,        //只读
          theme: 'monokai', //设置主题，不设置的会使用默认主题
          autofocus: true,
          extraKeys: {
            'Tab': 'autocomplete'
          }, //自定义快捷键
          hintOptions: { //自定义提示选项
            tables: {
              users: ['name', 'score', 'birthDate'],
              countries: ['name', 'population', 'size']
            }
          }
        })

         //代码自动提示功能，记住使用cursorActivity事件不要使用change事件，这是一个坑，那样页面直接会卡死
      // this.editor.on('cursorActivity', function () {
      //   this.editor.showHint()
      // })

        // 编辑器赋值
        this.editor.setValue(this.value || this.code)

        this.editor.setSize('100%', '800px'); //参数1 宽度，参数2 高度
        //  this.editor.setValue("");    //给代码框赋值
        //  this.editor.getValue();    //获取代码框的值
        //this.editor.setOption("readOnly", true);	//其他地方设置新的属性,类似这种


        // 支持双向绑定
        this.editor.on('change', (coder) => {
          this.code = coder.getValue()

          if (this.$emit) {
            this.$emit('input', this.code)
          }
        })

        // 尝试从父容器获取语法类型
        if (this.language) {
          // 获取具体的语法类型对象
          let modeObj = this._getLanguage(this.language)

          // 判断父容器传入的语法是否被支持
          if (modeObj) {
            this.mode = modeObj.label
          }
        }
      },

      // 获取当前语法类型
      getLanguage(language) {
        // 在支持的语法类型列表中寻找传入的语法类型
        return this.modes.find((mode) => {
          // 所有的值都忽略大小写，方便比较
          let currentLanguage = language.toLowerCase()
          let currentLabel = mode.label.toLowerCase()
          let currentValue = mode.value.toLowerCase()

          // 由于真实值可能不规范，例如 java 的真实值是 x-java ，所以讲 value 和 label 同时和传入语法进行比较
          return currentLabel === currentLanguage || currentValue === currentLanguage
        })
      },

      // 更改模式
      changeMode(val) {
        // 修改编辑器的语法配置
        this.editor.setOption('mode', `text/${val}`)

        // 获取修改后的语法
        let label = this.getLanguage(val).label.toLowerCase()

        // 允许父容器通过以下函数监听当前的语法值
        this.$emit('language-change', label)
      }

    }
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
