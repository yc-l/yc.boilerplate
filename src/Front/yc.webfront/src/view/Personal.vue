 <template>
  <div style="overflow:hidden">
    <el-row :gutter="20">
      <el-col :span="24">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>用户信息</span>
           
          </div>
          <div class="text item">

<el-descriptions class="margin-top" title="" :column="1" size="100" border>
  
     <el-descriptions-item >
      <template slot="label">
        <i class="el-icon-picture-outline"></i>
        头像
      </template>
    <el-upload
  class="avatar-uploader"
  action=""
  :show-file-list="false"
  :headers="headers"
      ref="upload"
      :on-change="changeFile"
      :auto-upload="false"
  :before-upload="beforeAvatarUpload">
  <img v-if="this.person.avatar" :src="this.person.avatar" class="avatar">
  <i v-else class="el-icon-plus avatar-uploader-icon"></i>
 
</el-upload>
 <el-button style="margin-left: 10px;" size="small" type="success" @click="submitUpload">上传到服务器</el-button>
  <div slot="tip" class="el-upload__tip">只能上传jpg/png/gif文件，且不超过2MB</div>
 
 </el-descriptions-item>
<el-descriptions-item >
      <template slot="label">
        <i class="el-icon-user"></i>
        账号
      </template>
      {{this.person.account}}
    </el-descriptions-item>
    <el-descriptions-item >
      <template slot="label">
        <i class="el-icon-user"></i>
        用户名
      </template>
      {{this.person.name}}
    </el-descriptions-item>
    <el-descriptions-item >
      <template slot="label">
        <i class="el-icon-user"></i>
        性别
      </template>
       <el-tag :type="this.person.sex === 0 ? 'primary' : 'warning'">{{
              this.person.sex === 0 ? '男' : '女'
            }}</el-tag>
    </el-descriptions-item>
    <el-descriptions-item>
      <template slot="label">
        <i class="el-icon-mobile-phone"></i>
        手机号
      </template>
       {{this.person.mobile}}
    </el-descriptions-item>
     <el-descriptions-item>
      <template slot="label">
        <i class="el-icon-message"></i>
        邮箱
      </template>
       {{this.person.email}}
    </el-descriptions-item>
 <el-descriptions-item >
      <template slot="label">
        <i class="el-icon-user"></i>
        修改密码
      </template>
      ********* &nbsp;&nbsp;&nbsp;&nbsp;
      <el-button type="primary" size="small" @click="showChangePwdDialog">修改</el-button>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">
        <i class="el-icon-location-outline"></i>
        居住地
      </template>
      苏州市
    </el-descriptions-item>
    <el-descriptions-item>
      <template slot="label">
        <i class="el-icon-tickets"></i>
        备注
      </template>
      <el-tag size="small">{{this.person.remark}}</el-tag>
    </el-descriptions-item>
    <el-descriptions-item>
      <template slot="label">
        <i class="el-icon-office-building"></i>
        联系地址
      </template>
      江苏省苏州市吴中区吴中大道 1188 号
    </el-descriptions-item>
  </el-descriptions>
          </div>
        </el-card>
      </el-col>
    </el-row>
  
    <!-- 添加修改的对话框 -->
    <el-dialog
      title="修改密码"
      :visible.sync="changePwdDialogVisible"
      width="60%"
      @close="changePwdDialogClosed"
    >
      <!-- 内容主体区域 -->

      <el-tabs v-model="activeName">
        <el-tab-pane label="" name="first">
          <el-form
            :model="changePwdForm"
            :rules="changePwdFormRules"
            ref="changePwdFormRef"
            label-width="120px"
          >
            <el-input
              v-model="changePwdForm.id"
              prop="id"
              type="hidden"
            ></el-input>

            <el-row :gutter="20">
              <el-col :span="24">
                <el-form-item label="原始密码" prop="oldPassword">
                  <el-input v-model="changePwdForm.oldPassword" type="password"></el-input>
                </el-form-item>
              </el-col>
        <el-col :span="24">
                <el-form-item label="新密码" prop="newPassword">
                  <el-input v-model="changePwdForm.newPassword"  type="password"></el-input>
                </el-form-item>
              </el-col>
               <el-col :span="24">
                <el-form-item label="确认密码" prop="confirmPassword">
                  <el-input v-model="changePwdForm.confirmPassword"  type="password"></el-input>
                </el-form-item>
              </el-col>
            
            </el-row>

          </el-form>
        </el-tab-pane>
      </el-tabs>

      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="changePwdDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="changePasswordSubmit()"
          >确 定</el-button
        >
      </span>
    </el-dialog>
     
  </div>
</template>

<script>


  export default {
    data() {
      
  var validatePass2 = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.changePwdForm.newPassword) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return {

        activeName: 'first',
        // 控制添加组织机构对话框的显示与隐藏
        changePwdDialogVisible: false,
        // 添加组织机构的表单数据
        changePwdForm: {
          id:"",
         oldPassword:"",
         newPassword:"",
         confirmPassword:""
        },
         person:{
          id: '',
          no:'',
          name: '',
          avatar:'',
          account:'',
          password: '',
          email: '',
          mobile: '',
          sex: 0,
          remark: ''
        },
        editState: false, //编辑状态
     
        // 添加表单的验证规则对象
        changePwdFormRules: {
          oldPassword:  [{
              required: true,
              message: '请输入原始密码',
              trigger: 'blur'
            },
            {
              min: 6,
              max: 100,
              message: '密码的长度在100个字符之间',
              trigger: 'blur',
            },
          ],
            newPassword:  [{
              required: true,
              message: '请输入新密码',
              trigger: 'blur'
            },
            {
              min: 6,
              max: 100,
              message: '密码的长度在100个字符之间',
              trigger: 'blur',
            },
          
          ],
         
          confirmPassword: [
            {
              required: true,
              message: '请输入确认密码',
              trigger: 'blur'
            },
            { validator: validatePass2, trigger: 'blur' }
          ],

        },
        // 控制修改密码对话框的显示与隐藏
        editDialogVisible: false,
         
      fileList: [], // 文件临时存放列表
      uploadForm: {
        versionCode: '',
        description: '',
        file: '' // file文件
      }

      
      }
    },
    created() {
     this.getUserInfo();
    },
    computed: {
           
            headers(){
                return {
                    "Authorization":'Bearer ' + window.sessionStorage.getItem('token')
                }
            }
        },
    methods: {

      async getUserInfo() {
           const {
              data: res
            } = await this.$http.post(
             this.$gersonalManager_GetUserInfoUrl
            )
        
         if (res.code !== 200) {
              return this.$message.error( res.message)
            }
            this.person=res.data
      },

      // 监听对话框的关闭事件
      changePwdDialogClosed() {
        this.editState = false //编辑状态改为false
        this.changePwdForm = {
           oldPassword:"",
           newPassword:"",
          confirmPassword:""
          },

          this.$refs.changePwdFormRef.resetFields()
      },
      // 点击按钮，提交修改密码
      changePasswordSubmit() {
        this.$refs.changePwdFormRef.validate(async (valid) => {
          if (!valid) return

         this.changePwdForm.id=this.person.id
         // 可以发起修改的网络请求
            const {
              data: res
            } = await this.$http.post(
              this.$gersonalManager_ChangePasswordUrl,
              this.changePwdForm
            )

            if (res.code !== 200) {
              this.$message.error('修改密码失败！' + res.message)
            }

            this.$message.success('修改密码成功！')

          // 隐藏修改密码对话框
          this.changePwdDialogVisible = false
          //刷新数据
            this.getUserInfo();
        })
      },
     
      // 展示修改密码的对话框
      async showChangePwdDialog() {
        this.changePwdDialogVisible = true

      },
     
     handleAvatarSuccess(res, file) {
        this.person.avatar = URL.createObjectURL(file.raw);
      },
      beforeAvatarUpload(file) {
        let fileTypeArray=['image/jpeg','image/png','image/gif']
        let isAllowedType=false
        for (let index = 0; index < fileTypeArray.length; index++) {
          const element = fileTypeArray[index];
          if(element===file.type){
                 isAllowedType=true
          }
        }
        //const isJPG = file.type === 'image/jpeg';
        const isLt2M = file.size / 1024 / 1024 < 2;

        if (!isAllowedType) {
          this.$message.error('上传头像图片只能是 JPG/PNG/GIF 格式!');
        }
        if (!isLt2M) {
          this.$message.error('上传头像图片大小不能超过 2MB!');
        }
        return isAllowedType && isLt2M;
      },
       //文件上传
     async  submitUpload() {
       if(this.uploadForm.file==""){

         this.$message.error("请选择上传图片！");
         return;
       }
      var formData = new FormData()
      formData.append('File', this.uploadForm.file)
      //formData.append('versionCode', this.uploadForm.versionCode)
      //formData.append('descript', this.uploadForm.description)
      //   const {
      //         data: res
      //       } = await this.$http.post(this.$gersonalManager_UploadImageUrl,formData)

      // if(res.code==200){
      //   alert("Hello");

      // }
       this.$http.post(this.$gersonalManager_UploadImageUrl,formData).then(req => {
         const res=req.data
         if (res.code == "200") {
       this.person.avatar = res.data 
       this.$message.success('图片上传成功！')
       this.uploadForm={
        versionCode: '',
        description: '',
        file: '' // file文件
      }
      } else {
        this.$message.error('图片上传失败!'+res.message);
      }

      });
        
        

    },
    // //文件上传成功时的钩子
    uploadSuccess(response, file, fileList) {
      if (response.code == "200") {
         this.person.avatar = response.data 
       this.$message.success('图片上传成功！')
      } else {
        this.$message.error('图片上传失败!'+response.message);
      }
    },
    //文件上传失败时的钩子
    uploadError(response, file, fileList) {
      console.log("项目添加失败");
    },
    changeFile(file, fileList){
      this.person.avatar = URL.createObjectURL(file.raw);
      this.uploadForm.file= file.raw
    }

    },
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


</style>