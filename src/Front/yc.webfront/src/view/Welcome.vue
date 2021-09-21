 <template>
  <div style="overflow:hidden">
    <el-row :gutter="20">
      <el-col :span="24">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>YC.Boilerplate 介绍</span>
           
          </div>
          <div class="text item">
            &nbsp; &nbsp;
            “DRY——避免重复代码”是一个优秀的开发者在开发软件时所具备的最重要的思想之一。<br /><br />
            我们在开发企业WEB应用程序时都有一些类似的需求，例如：都需要登录页面、用户/角色管理、权限验证、数据有效性验证、多语言/本地化等等。
            一个高品质的大型软件都会运用一些最佳实践，例如分层体系结构、领域驱动设计、依赖注入等。我们也可能会采用ORM、数据库迁移（Database
            Migrations）、日志记录（Logging）等工具。<br /><br />
            从零开始创建一个企业应用程序是一件繁琐的事，因为需要重复做很多常见的基础工作。
            许多公司都在开发自己的应用程序框架来重用于不同的项目，然后在框架的基础上开发一些新的功能。但并不是每个公司都有这样的实力。
            假如我们可以分享的更多，也许可以避免每个公司或每个项目的重复编写类似的代码。<br /><br />
            元磁之力为了解决上述需求而研发的快速开发框架，之所以把项目命名为“YC.Boilerplate”，就是希望它能成为开发一般企业WEB应用的新起点，直接作为项目模板。
          </div>
        </el-card>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="24">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>框架特性</span>
          </div>
          <div class="text item">
            <el-row :gutter="20">
              <el-col :span="12">
                1.基于最新的.NET技术 NET 5.0。<br /><br />
                2.实现领域驱动设计（实体、仓储、领域服务、领域事件、应用服务、数据传输对象，工作单元等等）。<br /><br />
                3.实现分层体系结构（领域层，应用层，展现层和基础设施层）。<br /><br />
                4.提供了一个基础架构来开发可重用可配置的模块。<br /><br />
                5.集成一些最流行的开源框架/库，也许有些是你正在使用的。<br /><br />
                6.提供了一个基础架构让我们很方便地使用依赖注入（使用Autofac作为依赖注入的容器）。<br /><br />
                7.提供Repository仓储模式支持不同的ORM（已实现dapperFramework
                、freesqlFrameWork、Redis等）。<br /><br />
                8.支持并实现数据库迁移（采用自由映射Table ）。<br /><br />
                9.模块化开发（每个模块有独立的采用autofac模块注入形式，根据不同业务可以动态切换指定数据库）。<br /><br />
                10.统一的异常处理（应用层几乎不需要处理自己写异常处理代码）。<br /><br />
                11.通过Application
                Services自动创建WebApi层（不需要写ApiController层了）。<br /><br />
                12.提供基类和帮助类让我们方便地实现一些常见的任务。<br /><br />
                13.使用“约定优于配置原则”。<br /><br />
                14.实现多租户，按照不同的租户分库服务端。<br /><br />
                15.实现了基于表模型生成对应的全流程代码，包括：model、DTO、Service、前端展示界面（常规CRUD和树形功能）、路由规则、Mapper
                model和Dto映射等代码。<br /><br />
                16.框架已经实现常规基础功能，如：身份验证用户&角色管理、系统设置、存取管理（系统级、租户级、用户级，作用范围自动管理）、审计日志（自动记录每一次接口的调用者和参数）、组织机构等，实现框架开箱即用。<br /><br />
                17.框架采用redis 作为cache和session 存储，脱离cookie
                使用，解决非web的等也可以使用框架进行多样的场景需求改造。<br /><br />
              </el-col>
              <el-col :span="12">
                <img
                  src="../assets/images/框架图.png"
                  alt=""
                  style="
                    display: inline-block;
                    width: 100%;
                    max-width: 100%;
                    height: auto;
                  "
                />
              </el-col>
            </el-row>
          </div>
        </el-card>
      </el-col>
    </el-row>
     
  </div>
</template>
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
</style>