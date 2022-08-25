<template>
<el-dialog
  :title="title"
  :visible.sync="dialogVisible"
  width="60%"
  @close="handleClose"
  :before-close="handleClose">
  <el-form :model="addOrEditForm" :rules="addOrEditFormRules" ref="addOrEditFormRef" label-width="100px">
      <el-input v-model="addOrEditForm.id" prop="id" type="hidden"></el-input>
            <el-row :gutter='20'>
              <el-col :span='12'>
                <el-form-item label="流程类别：" prop="type">
                  <el-select v-model="addOrEditForm.type" placeholder="请选择" style="width: 100%">
                    <el-option v-for="item in typeOptions" :key="item.value" :label="item.label" :value="item.value">
                    </el-option>
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span='12'>
                <el-form-item label='流程名称：' prop='name'>
                  <el-input v-model='addOrEditForm.name'></el-input>
                </el-form-item>
              </el-col>



            </el-row>

            <el-row :gutter='20'>
              <el-col :span="12">
                <el-form-item label=" 启用状态：" prop="enabled">
                  <el-switch v-model="addOrEditForm.enabled" active-text="是" inactive-text="否">
                  </el-switch>
                </el-form-item>
              </el-col>

            </el-row>
            <el-row :gutter='20'>
              <el-col :span="24">
                <el-input type="textarea" rows="12" v-model="infoContent" style="overflow-y:scoll;" disabled></el-input>
              </el-col>

            </el-row>
  </el-form>
  <span slot="footer" class="dialog-footer">
    <el-button  @click="handleClose">取 消</el-button>
    <el-button type="primary" @click="addOrEditProcessFlow()">保 存</el-button>
  </span>
</el-dialog>
</template>
<script>
  export default {
    name:'exportDialog',
        props:{//属性，让外部访问
           
            title:{
                type: String,
                default: '',
                required: true
            },
          
            infoContent: {
                type: String,
                default: ''
            },
           dialogVisible: {
                type: Boolean,
                default: false
            },
        },
    data() {  
      return {
         // 添加流程的表单数据
        addOrEditForm: {
          id: '',
          flowContent: '',
          type: 0,
          name: '',
          enabled: true,
        },
         typeOptions: [{
          value: 0,
          label: '默认'
        }, {
          value: 1,
          label: '区块链溯源存证'
        }, {
          value: 2,
          label: 'AI自动化引擎'
        }],
        // 添加表单的验证规则对象
        addOrEditFormRules: {
          flowContent: [{
              required: true,
              message: '请输入流程内容',
              trigger: 'blur'
            },
            {
              max: 32,
              message: '流程内容的长度在32字符之内',
              trigger: 'blur'
            },
          ],
          type: [{
            required: true,
            message: '请输入流程类别',
            trigger: 'blur'
          }, ],
          name: [{
              required: true,
              message: '请输入流程名称',
              trigger: 'blur'
            },
            {
              max: 50,
              message: '流程名称的长度在50字符之内',
              trigger: 'blur'
            },
          ],
          enabled: [{
            required: true,
            message: '请输入状态',
            trigger: 'blur'
          }, ],

        },
        // title:'',
        //  infoContent:'',
        // showControl:this.dialogVisible//显示外部状态，由于关闭内容，无法修改dialogVisible 的值，导致异常
      };
    },
    
    mounted(){
     
     console.log(this.dialogVisible)
     console.log(this.showControl)
    },
    
    methods: {
       // 点击按钮，添加或编辑流程
      addOrEditProcessFlow() {
        this.$refs.addOrEditFormRef.validate(async (valid) => {
          if (!valid) return

          this.addOrEditForm.flowContent=this.infoContent
          const {
              data: res
            } = await this.$http.post(
              this.$processFlowManager_CreateProcessFlowUrl,
              this.addOrEditForm
            )

            if (res.code !== 200) {
              return this.$message.error('添加流程失败！' + res.message)
            }

            this.$message.success('添加流程成功！')
            this.handleClose()

        })
      },
      handleClose(done) {
        // this.dialogVisible=false;
        this.$refs.addOrEditFormRef.resetFields()
        this.$emit("handleClose",false)
      //   this.$confirm('确认关闭？')
      //     .then(_ => {
      //       done();
      //     })
      //     .catch(_ => {});
       }
    }
  };
</script>