/*
 Navicat Premium Data Transfer

 Source Server         : sqlserver
 Source Server Type    : SQL Server
 Source Server Version : 11002100
 Source Host           : 127.0.0.1:1433
 Source Catalog        : bigDataDB2
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 11002100
 File Encoding         : 65001

 Date: 01/09/2021 16:52:16
*/


-- ----------------------------
-- Table structure for sys_auditlog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_auditlog]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_auditlog]
GO

CREATE TABLE [dbo].[sys_auditlog] (
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [Key] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IP] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Browser] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Os] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Device] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [BrowserInfo] nvarchar(300) COLLATE Chinese_PRC_CI_AS  NULL,
  [ElapsedMilliseconds] bigint  NULL,
  [TenantId] int  NULL,
  [UserId] bigint  NULL,
  [UserAccount] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParamsString] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [StartTime] datetime2(0)  NULL,
  [StopTime] datetime2(0)  NULL,
  [RequestMethod] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [RequestApi] nvarchar(300) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(0)  NULL,
  [ResponseState] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [ResponseData] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[sys_auditlog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'拦截Key',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'Key'
GO

EXEC sp_addextendedproperty
'MS_Description', N'IP',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'IP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'浏览器',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'Browser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作系统',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'Os'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'Device'
GO

EXEC sp_addextendedproperty
'MS_Description', N'浏览器信息',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'BrowserInfo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'耗时（毫秒）',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'ElapsedMilliseconds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'租户Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'TenantId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户id',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户账号',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'UserAccount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请求参数',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'ParamsString'
GO

EXEC sp_addextendedproperty
'MS_Description', N'开始执行时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'结束执行时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'StopTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请求方式',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'RequestMethod'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请求Api',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'RequestApi'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 创建时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'CreationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返回是否成功',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'ResponseState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返回数据',
'SCHEMA', N'dbo',
'TABLE', N'sys_auditlog',
'COLUMN', N'ResponseData'
GO


-- ----------------------------
-- Records of sys_auditlog
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_auditlog] ON
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'1', N'2e7246e3-270f-4113-9ffa-ba9a12b4e4b4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'149', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 0
  }
}', N'2021-05-05 15:53:01', N'2021-05-05 15:53:01', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 15:53:42', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'2', N'847095a0-5c70-44a7-860f-8400f7695c37', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'34674', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 0
  }
}', N'2021-05-05 16:05:33', N'2021-05-05 16:06:08', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:06:09', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'3', N'847095a0-5c70-44a7-860f-8400f7695c37', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'53155', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 0
  }
}', N'2021-05-05 16:06:14', N'2021-05-05 16:07:07', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:07:09', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'4', N'847095a0-5c70-44a7-860f-8400f7695c37', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'90202', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 0
  }
}', N'2021-05-05 16:07:30', N'2021-05-05 16:09:00', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:09:01', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'5', N'a17aeb6b-c7dc-4395-a697-c3f9f4e7b3ec', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'8659', N'0', N'0', NULL, N'{"loginUserDto":{"userId":"admin","pwd":"123456","TenantId":1}}', N'2021-05-05 16:13:35', N'2021-05-05 16:13:43', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:13:45', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'6', N'9759818c-7a23-40fa-ae47-9ad2a204e620', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'8765', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 16:16:34', N'2021-05-05 16:16:43', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:16:43', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'7', N'9759818c-7a23-40fa-ae47-9ad2a204e620', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'115', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 16:16:50', N'2021-05-05 16:16:50', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:16:50', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'8', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'160', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:16:53', N'2021-05-05 16:16:53', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:16:53', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'9', N'8cedbc6c-6b17-4bee-a3d9-368f34b5fd5f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'127', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 16:17:35', N'2021-05-05 16:17:35', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 16:17:35', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'10', N'3964c777-acbe-428f-a467-7c37934cb2f7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'150', N'1', N'1', N'admin', N'{}', N'2021-05-05 16:17:35', N'2021-05-05 16:17:35', N'get', N'api/sysrole/getsysrolelist', N'2021-05-05 16:17:35', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'11', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'96', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:17:37', N'2021-05-05 16:17:37', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:17:37', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'12', N'3964c777-acbe-428f-a467-7c37934cb2f7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'128074', N'1', N'1', N'admin', N'{}', N'2021-05-05 16:18:27', N'2021-05-05 16:20:35', N'get', N'api/sysrole/getsysrolelist', N'2021-05-05 16:20:35', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'13', N'8cedbc6c-6b17-4bee-a3d9-368f34b5fd5f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'128075', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 16:18:27', N'2021-05-05 16:20:35', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 16:20:35', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'14', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'1077', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:20:35', N'2021-05-05 16:20:36', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:20:36', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'15', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'2762', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:22:04', N'2021-05-05 16:22:07', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:22:07', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'16', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'1757', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:24:31', N'2021-05-05 16:24:33', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:24:33', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'17', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'4285', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:25:03', N'2021-05-05 16:25:07', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:25:07', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'18', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'120', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:26:57', N'2021-05-05 16:26:57', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:26:57', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'19', N'f708ede1-c410-44b4-b953-088cee0e4ef3', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'148', N'1', N'1', N'admin', N'{
  "id": 17
}', N'2021-05-05 16:27:10', N'2021-05-05 16:27:10', N'get', N'api/sysauditlog/get', N'2021-05-05 16:27:10', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'20', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'116', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:28:43', N'2021-05-05 16:28:43', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:28:43', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'21', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'107', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:29:29', N'2021-05-05 16:29:30', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:29:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'22', N'f708ede1-c410-44b4-b953-088cee0e4ef3', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'118', N'1', N'1', N'admin', N'{
  "id": 20
}', N'2021-05-05 16:29:34', N'2021-05-05 16:29:34', N'get', N'api/sysauditlog/get', N'2021-05-05 16:29:34', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'23', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'91', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:32:14', N'2021-05-05 16:32:14', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:32:14', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'24', N'f708ede1-c410-44b4-b953-088cee0e4ef3', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'110', N'1', N'1', N'admin', N'{
  "id": 22
}', N'2021-05-05 16:32:20', N'2021-05-05 16:32:20', N'get', N'api/sysauditlog/get', N'2021-05-05 16:32:20', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'25', N'f708ede1-c410-44b4-b953-088cee0e4ef3', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'162', N'1', N'1', N'admin', N'{
  "id": 21
}', N'2021-05-05 16:34:13', N'2021-05-05 16:34:14', N'get', N'api/sysauditlog/get', N'2021-05-05 16:34:14', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'26', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'71', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": "post"
    }
  }
}', N'2021-05-05 16:34:47', N'2021-05-05 16:34:47', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:34:47', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'27', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'78', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:36:47', N'2021-05-05 16:36:47', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:36:47', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'28', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'75', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": "post"
    }
  }
}', N'2021-05-05 16:37:30', N'2021-05-05 16:37:30', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:37:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'29', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'75', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": "post"
    }
  }
}', N'2021-05-05 16:37:31', N'2021-05-05 16:37:31', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:37:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'30', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'96', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:38:31', N'2021-05-05 16:38:31', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:38:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'31', N'f708ede1-c410-44b4-b953-088cee0e4ef3', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'155', N'1', N'1', N'admin', N'{
  "id": 29
}', N'2021-05-05 16:39:57', N'2021-05-05 16:39:58', N'get', N'api/sysauditlog/get', N'2021-05-05 16:39:58', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'32', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'109', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:41:15', N'2021-05-05 16:41:15', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:41:15', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'33', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'88', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:41:49', N'2021-05-05 16:41:49', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:41:49', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'34', N'd9d5b5c0-9305-47be-bf5e-501d0e10fcdd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'88', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:42:25', N'2021-05-05 16:42:25', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:42:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'35', N'0f9f4999-de23-41f4-b612-785270bd180f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'622', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:47:14', N'2021-05-05 16:47:14', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:47:15', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'36', N'e826326e-a163-4288-8ac6-c783fe3e96ec', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'4700', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 16:47:16', N'2021-05-05 16:47:21', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:47:21', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'37', N'e826326e-a163-4288-8ac6-c783fe3e96ec', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'120', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 16:47:23', N'2021-05-05 16:47:23', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 16:47:23', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'38', N'0f9f4999-de23-41f4-b612-785270bd180f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'84', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:47:26', N'2021-05-05 16:47:26', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:47:26', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'39', N'acd2764c-020a-4dd8-b97e-e25f1fb94b89', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'118', N'1', N'1', N'admin', N'{
  "id": 37
}', N'2021-05-05 16:47:30', N'2021-05-05 16:47:30', N'get', N'api/sysauditlog/get', N'2021-05-05 16:47:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'40', N'0f9f4999-de23-41f4-b612-785270bd180f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'79', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 16:51:17', N'2021-05-05 16:51:17', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:51:17', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'41', N'0f9f4999-de23-41f4-b612-785270bd180f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'77', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": "admin"
    }
  }
}', N'2021-05-05 16:51:24', N'2021-05-05 16:51:24', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-05 16:51:24', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'42', N'9f963e3a-57a8-40ba-a406-0914e89569b7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'943', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:04:55', N'2021-05-05 17:04:56', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:04:56', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'43', N'fd86f470-8c26-439f-875b-07d3ade5999c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'151', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:05:00', N'2021-05-05 17:05:01', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:05:01', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'44', N'6100a9ed-c234-45ea-975c-2890d33d1628', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'162', N'1', N'1', N'admin', N'{
  "id": 112
}', N'2021-05-05 17:05:14', N'2021-05-05 17:05:14', N'get', N'api/syspermission/get', N'2021-05-05 17:05:14', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'45', N'473d6304-09f8-495c-a858-d7e236d07bd8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'106', N'1', N'1', N'admin', N'{
  "pid": 112
}', N'2021-05-05 17:05:15', N'2021-05-05 17:05:15', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-05 17:05:15', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'46', N'6100a9ed-c234-45ea-975c-2890d33d1628', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'139', N'1', N'1', N'admin', N'{
  "id": 113
}', N'2021-05-05 17:05:23', N'2021-05-05 17:05:23', N'get', N'api/syspermission/get', N'2021-05-05 17:05:23', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'47', N'473d6304-09f8-495c-a858-d7e236d07bd8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'82', N'1', N'1', N'admin', N'{
  "pid": 113
}', N'2021-05-05 17:05:24', N'2021-05-05 17:05:24', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-05 17:05:24', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'48', N'069ebe1f-a78c-41a5-b493-99c458e206c8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'1194', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "111",
    "Label": "角色管理",
    "Code": "system.roleManager",
    "Type": 2,
    "View": "SysRoles",
    "Api": "",
    "Path": "/sysRoles",
    "Icon": "el-icon-share",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 4,
    "Description": "",
    "Id": "113"
  }
}', N'2021-05-05 17:08:01', N'2021-05-05 17:08:02', N'put', N'api/syspermission/updatesyspermission', N'2021-05-05 17:08:02', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'49', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'206', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:08:02', N'2021-05-05 17:08:03', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:08:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'50', N'aee85a2a-e825-48eb-8a93-7fdf0129d103', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'161', N'1', N'1', N'admin', N'{
  "id": 114
}', N'2021-05-05 17:08:10', N'2021-05-05 17:08:10', N'get', N'api/syspermission/get', N'2021-05-05 17:08:10', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'51', N'cb7517e8-0c32-46e4-bff1-b80e3d5d7d7d', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'88', N'1', N'1', N'admin', N'{
  "pid": 114
}', N'2021-05-05 17:08:10', N'2021-05-05 17:08:10', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-05 17:08:10', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'52', N'069ebe1f-a78c-41a5-b493-99c458e206c8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'181', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "111",
    "Label": "权限管理",
    "Code": "permissionManager",
    "Type": 2,
    "View": "SysPermissions",
    "Api": "",
    "Path": "/sysPermissions",
    "Icon": "el-icon-s-operation",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 5,
    "Description": "",
    "Id": "114"
  }
}', N'2021-05-05 17:08:15', N'2021-05-05 17:08:16', N'put', N'api/syspermission/updatesyspermission', N'2021-05-05 17:08:16', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'53', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'118', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:08:16', N'2021-05-05 17:08:16', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:08:16', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'54', N'aee85a2a-e825-48eb-8a93-7fdf0129d103', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'109', N'1', N'1', N'admin', N'{
  "id": 130
}', N'2021-05-05 17:08:27', N'2021-05-05 17:08:27', N'get', N'api/syspermission/get', N'2021-05-05 17:08:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'55', N'cb7517e8-0c32-46e4-bff1-b80e3d5d7d7d', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'75', N'1', N'1', N'admin', N'{
  "pid": 130
}', N'2021-05-05 17:08:27', N'2021-05-05 17:08:28', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-05 17:08:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'56', N'069ebe1f-a78c-41a5-b493-99c458e206c8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'123', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "111",
    "Label": "角色权限",
    "Code": "rolePermissionManager",
    "Type": 2,
    "View": "SysRolePermissions",
    "Api": "",
    "Path": "/sysRolePemissions",
    "Icon": "el-icon-share",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 7,
    "Description": "",
    "Id": "130"
  }
}', N'2021-05-05 17:08:31', N'2021-05-05 17:08:31', N'put', N'api/syspermission/updatesyspermission', N'2021-05-05 17:08:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'57', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'138', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:08:31', N'2021-05-05 17:08:31', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:08:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'58', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'147', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:08:54', N'2021-05-05 17:08:55', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:08:55', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'59', N'01aae876-9348-4877-a993-da372cc42b75', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'553', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "0",
    "Label": "组织机构管理",
    "Code": "sysOrganizationManager",
    "Type": 2,
    "View": "SysOrganizations",
    "Api": "",
    "Path": "/sysOrganizations",
    "Icon": "el-icon-s-custom",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 0,
    "Description": "",
    "Id": ""
  }
}', N'2021-05-05 17:12:30', N'2021-05-05 17:12:31', N'post', N'api/syspermission/createsyspermission', N'2021-05-05 17:12:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'60', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'103', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:12:31', N'2021-05-05 17:12:31', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:12:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'61', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'112', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:13:05', N'2021-05-05 17:13:05', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:13:06', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'62', N'01aae876-9348-4877-a993-da372cc42b75', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'556', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "139",
    "Label": "查看组织机构列表",
    "Code": "sysOrganizationManager.createSysOrganization",
    "Type": 3,
    "View": "",
    "Api": "SysOrganization/GetPageSysOrganizationList,SysOrganization/Get",
    "Path": "",
    "Icon": "",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 0,
    "Description": "",
    "Id": ""
  }
}', N'2021-05-05 17:13:51', N'2021-05-05 17:13:52', N'post', N'api/syspermission/createsyspermission', N'2021-05-05 17:13:52', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'63', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'158', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:13:52', N'2021-05-05 17:13:52', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:13:52', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'64', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'95', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:13:53', N'2021-05-05 17:13:53', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:13:53', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'65', N'01aae876-9348-4877-a993-da372cc42b75', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'197', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "139",
    "Label": "新增组织机构",
    "Code": "sysOrganizationManager.createSysOrganization",
    "Type": 3,
    "View": "",
    "Api": "SysOrganization/CreateSysOrganization",
    "Path": "",
    "Icon": "",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 0,
    "Description": "",
    "Id": ""
  }
}', N'2021-05-05 17:14:29', N'2021-05-05 17:14:29', N'post', N'api/syspermission/createsyspermission', N'2021-05-05 17:14:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'66', N'c0817877-e1b4-4ef4-975b-44a00eedc10e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'102', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:14:29', N'2021-05-05 17:14:29', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:14:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'67', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'562', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:21:24', N'2021-05-05 17:21:24', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:21:24', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'68', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'95', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:21:25', N'2021-05-05 17:21:25', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:21:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'69', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'89', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:21:32', N'2021-05-05 17:21:32', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:21:32', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'70', N'7e2dbea5-a1b9-42d1-b082-d0446c7f2541', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'339', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "139",
    "Label": "编辑组织机构",
    "Code": "sysOrganizationManager.editSysOrganization",
    "Type": 3,
    "View": "",
    "Api": "SysOrganization/UpdateSysOrganization",
    "Path": "",
    "Icon": "",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 0,
    "Description": "",
    "Id": ""
  }
}', N'2021-05-05 17:22:03', N'2021-05-05 17:22:04', N'post', N'api/syspermission/createsyspermission', N'2021-05-05 17:22:04', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'71', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'116', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:22:04', N'2021-05-05 17:22:04', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:22:04', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'72', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'126', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:22:05', N'2021-05-05 17:22:06', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:22:06', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'73', N'7e2dbea5-a1b9-42d1-b082-d0446c7f2541', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'206', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "139",
    "Label": "删除组织机构",
    "Code": "sysOrganizationManager.deleteSysOrganization",
    "Type": 3,
    "View": "",
    "Api": "SysOrganization/DeleteSysOrganizationById",
    "Path": "",
    "Icon": "",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 0,
    "Description": "",
    "Id": ""
  }
}', N'2021-05-05 17:22:52', N'2021-05-05 17:22:52', N'post', N'api/syspermission/createsyspermission', N'2021-05-05 17:22:52', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'74', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'105', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:22:52', N'2021-05-05 17:22:52', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:22:52', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'75', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'139', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:23:40', N'2021-05-05 17:23:40', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:23:40', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'76', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'95', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:23:44', N'2021-05-05 17:23:44', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:23:44', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'77', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'182', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:23:48', N'2021-05-05 17:23:48', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:23:48', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'78', N'c3cdd84d-aff0-4f41-af27-0527dd6b4c3e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'131', N'1', N'1', N'admin', N'{}', N'2021-05-05 17:23:48', N'2021-05-05 17:23:48', N'get', N'api/sysrole/getsysrolelist', N'2021-05-05 17:23:48', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'79', N'c3cdd84d-aff0-4f41-af27-0527dd6b4c3e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'72', N'1', N'1', N'admin', N'{}', N'2021-05-05 17:23:49', N'2021-05-05 17:23:49', N'get', N'api/sysrole/getsysrolelist', N'2021-05-05 17:23:49', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'80', N'60ecafe4-c40b-4ca6-963a-f11fb255bdfd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'500', N'1', N'1', N'admin', N'{
  "input": {
    "Id": "1",
    "PermissionIds": [
      "111",
      "113",
      "126",
      "127",
      "128",
      "129",
      "114",
      "122",
      "123",
      "124",
      "125",
      "130",
      "131",
      "137",
      "116",
      "117",
      "138",
      "112",
      "118",
      "119",
      "120",
      "121",
      "115",
      "132",
      "133",
      "134",
      "135",
      "136",
      "139",
      "140",
      "141",
      "142",
      "143"
    ]
  }
}', N'2021-05-05 17:23:52', N'2021-05-05 17:23:52', N'put', N'api/sysrole/updatesysrolepermissions', N'2021-05-05 17:23:52', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'81', N'58a4f39e-6e26-48e7-b786-a441155bb5f9', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'612', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:24:02', N'2021-05-05 17:24:03', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:24:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'82', N'8d790c23-37ab-43c5-a713-066b01fe7ca3', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'81', N'1', N'1', N'admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-05 17:24:09', N'2021-05-05 17:24:09', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-05 17:24:09', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'83', N'58a4f39e-6e26-48e7-b786-a441155bb5f9', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'126', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:24:36', N'2021-05-05 17:24:36', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:24:36', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'84', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'69', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:24:47', N'2021-05-05 17:24:48', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:24:48', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'85', N'257005e5-f3fc-40fb-9491-b413fa438308', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'150', N'1', N'1', N'admin', N'{
  "id": 139
}', N'2021-05-05 17:24:50', N'2021-05-05 17:24:50', N'get', N'api/syspermission/get', N'2021-05-05 17:24:50', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'86', N'e24c03b2-1b95-42be-acaf-0b9d37f5db83', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'68', N'1', N'1', N'admin', N'{
  "pid": 139
}', N'2021-05-05 17:24:50', N'2021-05-05 17:24:50', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-05 17:24:50', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'87', N'9ec6e1a0-9063-441d-a761-967c26555d55', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'247', N'1', N'1', N'admin', N'{
  "input": {
    "ParentId": "0",
    "Label": "组织机构管理",
    "Code": "sysOrganizationManager",
    "Type": 2,
    "View": "SysOrganizations",
    "Api": "",
    "Path": "/sysOrganizations",
    "Icon": "el-icon-s-custom",
    "Hidden": true,
    "Closable": true,
    "Opened": true,
    "NewWindow": true,
    "External": true,
    "Sort": 999,
    "Description": "",
    "Id": "139"
  }
}', N'2021-05-05 17:24:55', N'2021-05-05 17:24:55', N'put', N'api/syspermission/updatesyspermission', N'2021-05-05 17:24:55', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'88', N'6ad13064-48fe-4ab2-81ca-a548a388af22', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'135', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:24:55', N'2021-05-05 17:24:55', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-05 17:24:56', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'89', N'58a4f39e-6e26-48e7-b786-a441155bb5f9', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'109', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:24:59', N'2021-05-05 17:24:59', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:24:59', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'90', N'58a4f39e-6e26-48e7-b786-a441155bb5f9', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'152', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:31:48', N'2021-05-05 17:31:48', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:31:48', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'91', N'58a4f39e-6e26-48e7-b786-a441155bb5f9', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'11011', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:34:12', N'2021-05-05 17:34:23', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:34:23', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'92', N'58a4f39e-6e26-48e7-b786-a441155bb5f9', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'105035', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:35:39', N'2021-05-05 17:37:24', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:37:24', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'93', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'3980', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:41:22', N'2021-05-05 17:41:26', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:41:26', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'94', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'5133', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:41:38', N'2021-05-05 17:41:43', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:41:43', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'95', N'e13be961-1259-4d2a-bb5a-356ebb7c5b29', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'218', N'1', N'1', N'admin', N'{
  "input": {
    "Label": "总机构",
    "ParentId": 0,
    "OrganType": 1,
    "Sort": 1,
    "PostId": 0,
    "Fax": "",
    "Telephone": "",
    "Address": "",
    "Memoni": "",
    "Remark": "",
    "RangeType": 0,
    "Range": "",
    "Linkman": "",
    "Id": ""
  }
}', N'2021-05-05 17:41:59', N'2021-05-05 17:41:59', N'post', N'api/sysorganization/createsysorganization', N'2021-05-05 17:41:59', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'96', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'117', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:41:59', N'2021-05-05 17:42:00', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:42:00', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'97', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'101', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:42:01', N'2021-05-05 17:42:02', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:42:02', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'98', N'e13be961-1259-4d2a-bb5a-356ebb7c5b29', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'167', N'1', N'1', N'admin', N'{
  "input": {
    "Label": "人事部",
    "ParentId": 1,
    "OrganType": 1,
    "Sort": 0,
    "PostId": 0,
    "Fax": "",
    "Telephone": "",
    "Address": "",
    "Memoni": "",
    "Remark": "",
    "RangeType": 0,
    "Range": "",
    "Linkman": "",
    "Id": ""
  }
}', N'2021-05-05 17:42:11', N'2021-05-05 17:42:12', N'post', N'api/sysorganization/createsysorganization', N'2021-05-05 17:42:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'99', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'152', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:42:12', N'2021-05-05 17:42:12', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:42:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'100', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'85', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:42:14', N'2021-05-05 17:42:14', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:42:14', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'101', N'e13be961-1259-4d2a-bb5a-356ebb7c5b29', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'155', N'1', N'1', N'admin', N'{
  "input": {
    "Label": "司法部",
    "ParentId": 1,
    "OrganType": 1,
    "Sort": 0,
    "PostId": 0,
    "Fax": "",
    "Telephone": "",
    "Address": "",
    "Memoni": "",
    "Remark": "",
    "RangeType": 0,
    "Range": "",
    "Linkman": "",
    "Id": ""
  }
}', N'2021-05-05 17:42:27', N'2021-05-05 17:42:27', N'post', N'api/sysorganization/createsysorganization', N'2021-05-05 17:42:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'102', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'113', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:42:27', N'2021-05-05 17:42:27', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:42:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'103', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'116', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:42:29', N'2021-05-05 17:42:29', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:42:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'104', N'e13be961-1259-4d2a-bb5a-356ebb7c5b29', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'217', N'1', N'1', N'admin', N'{
  "input": {
    "Label": "创新事业部",
    "ParentId": 1,
    "OrganType": 2,
    "Sort": 0,
    "PostId": 0,
    "Fax": "",
    "Telephone": "",
    "Address": "",
    "Memoni": "",
    "Remark": "",
    "RangeType": 0,
    "Range": "",
    "Linkman": "",
    "Id": ""
  }
}', N'2021-05-05 17:43:05', N'2021-05-05 17:43:05', N'post', N'api/sysorganization/createsysorganization', N'2021-05-05 17:43:05', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'105', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'103', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:43:06', N'2021-05-05 17:43:06', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:43:06', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'106', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'118', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:43:06', N'2021-05-05 17:43:07', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:43:07', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'107', N'e13be961-1259-4d2a-bb5a-356ebb7c5b29', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'219', N'1', N'1', N'admin', N'{
  "input": {
    "Label": "编程组",
    "ParentId": 4,
    "OrganType": 3,
    "Sort": 0,
    "PostId": 0,
    "Fax": "",
    "Telephone": "",
    "Address": "",
    "Memoni": "",
    "Remark": "",
    "RangeType": 0,
    "Range": "",
    "Linkman": "",
    "Id": ""
  }
}', N'2021-05-05 17:43:28', N'2021-05-05 17:43:28', N'post', N'api/sysorganization/createsysorganization', N'2021-05-05 17:43:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'108', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'100', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:43:28', N'2021-05-05 17:43:28', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:43:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'109', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'126', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:44:27', N'2021-05-05 17:44:28', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:44:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'110', N'6948d7b4-0188-4b97-9eb0-9013a7d2f253', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'94', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:45:52', N'2021-05-05 17:45:53', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:45:53', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'111', N'4a45a4ac-5fbc-449a-8b10-35c40cfbbd30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'531', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:49:32', N'2021-05-05 17:49:32', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:49:32', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'112', N'4c973bf3-6d7c-45a5-9ca1-6112f140664e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'145', N'1', N'1', N'admin', N'{
  "id": 5
}', N'2021-05-05 17:50:03', N'2021-05-05 17:50:04', N'get', N'api/sysorganization/get', N'2021-05-05 17:50:04', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'113', N'32fd874c-dcb6-4c66-85c3-ba02a09bbf8e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'5264', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "admin",
    "pwd": "123456",
    "TenantId": 1
  }
}', N'2021-05-05 17:52:00', N'2021-05-05 17:52:05', N'post', N'api/identity/gettokenbylogin', N'2021-05-05 17:52:05', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'114', N'4a45a4ac-5fbc-449a-8b10-35c40cfbbd30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'70', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:52:09', N'2021-05-05 17:52:09', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:52:09', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'115', N'4c973bf3-6d7c-45a5-9ca1-6112f140664e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'88', N'1', N'1', N'admin', N'{
  "id": 5
}', N'2021-05-05 17:52:15', N'2021-05-05 17:52:15', N'get', N'api/sysorganization/get', N'2021-05-05 17:52:15', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'116', N'00df91a3-6071-49a2-94f8-12c507ad63b2', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'81', N'1', N'1', N'admin', N'{
  "pid": 5
}', N'2021-05-05 17:52:16', N'2021-05-05 17:52:16', N'post', N'api/sysorganization/getsysorganizationfilterbypid', N'2021-05-05 17:52:16', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'117', N'2a6c321f-d1b3-4f97-9087-b31280a6e343', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'138', N'1', N'1', N'admin', N'{
  "input": {
    "Label": "编程组",
    "ParentId": 4,
    "OrganType": 3,
    "Sort": 0,
    "PostId": 0,
    "Fax": "",
    "Telephone": "",
    "Address": "",
    "Memoni": "",
    "Remark": "",
    "RangeType": 0,
    "Range": "",
    "Linkman": "",
    "Id": "5"
  }
}', N'2021-05-05 17:52:19', N'2021-05-05 17:52:19', N'put', N'api/sysorganization/updatesysorganization', N'2021-05-05 17:52:19', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'118', N'4a45a4ac-5fbc-449a-8b10-35c40cfbbd30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'117', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:52:19', N'2021-05-05 17:52:19', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:52:19', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'119', N'd4410034-1369-4cf1-bd0e-56287e35397b', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'164', N'1', N'1', N'admin', N'{
  "id": 5
}', N'2021-05-05 17:52:22', N'2021-05-05 17:52:22', N'delete', N'api/sysorganization/deletesysorganizationbyid', N'2021-05-05 17:52:22', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'120', N'4a45a4ac-5fbc-449a-8b10-35c40cfbbd30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'65', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:52:22', N'2021-05-05 17:52:22', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:52:22', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'121', N'4a45a4ac-5fbc-449a-8b10-35c40cfbbd30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'155', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:52:25', N'2021-05-05 17:52:25', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:52:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'122', N'8b7915e0-676e-4313-bcb1-d07be2109a4d', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'181', N'1', N'1', N'admin', N'{
  "input": {
    "Label": "测试组",
    "ParentId": 4,
    "OrganType": 3,
    "Sort": 0,
    "PostId": 0,
    "Fax": "",
    "Telephone": "",
    "Address": "",
    "Memoni": "",
    "Remark": "",
    "RangeType": 0,
    "Range": "",
    "Linkman": "",
    "Id": ""
  }
}', N'2021-05-05 17:52:37', N'2021-05-05 17:52:37', N'post', N'api/sysorganization/createsysorganization', N'2021-05-05 17:52:37', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'123', N'4a45a4ac-5fbc-449a-8b10-35c40cfbbd30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', N'83', N'1', N'1', N'admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-05 17:52:37', N'2021-05-05 17:52:37', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-05 17:52:37', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'124', N'f38e830c-db9d-4f74-98a2-bb2ed7d5e1ea', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'921', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 17:43:39', N'2021-05-08 17:43:40', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 17:43:40', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'125', N'4d840642-12d6-4ee5-afbe-048ae48ad5b5', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'66', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:45:12', N'2021-05-08 17:45:12', N'post', N'api/sysuser/getpageuserlist', N'2021-05-08 17:45:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'126', N'be867c92-b7a4-4a37-bc51-c37931ac687d', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'54', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:45:29', N'2021-05-08 17:45:29', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 17:45:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'127', N'50a4160f-4ba6-49cc-93ee-b65ea4f1ee74', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'48', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:45:31', N'2021-05-08 17:45:31', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 17:45:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'128', N'4d840642-12d6-4ee5-afbe-048ae48ad5b5', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'46', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:45:32', N'2021-05-08 17:45:32', N'post', N'api/sysuser/getpageuserlist', N'2021-05-08 17:45:32', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'129', N'4d840642-12d6-4ee5-afbe-048ae48ad5b5', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'38', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:46:41', N'2021-05-08 17:46:41', N'post', N'api/sysuser/getpageuserlist', N'2021-05-08 17:46:41', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'130', N'f38e830c-db9d-4f74-98a2-bb2ed7d5e1ea', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'37', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 17:47:43', N'2021-05-08 17:47:43', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 17:47:43', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'131', N'44e2c9ce-fd75-4fd5-bc88-e70929d6340f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'49', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:51:10', N'2021-05-08 17:51:10', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 17:51:10', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'132', N'796f5f7c-7513-4261-b89d-81f81500b27c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'41', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:51:12', N'2021-05-08 17:51:12', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 17:51:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'133', N'5102aba1-d4e1-4eab-82fc-e321c0dfd5d2', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'41', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:51:12', N'2021-05-08 17:51:12', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 17:51:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'134', N'44e2c9ce-fd75-4fd5-bc88-e70929d6340f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'28', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:51:21', N'2021-05-08 17:51:21', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 17:51:21', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'135', N'796f5f7c-7513-4261-b89d-81f81500b27c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:51:22', N'2021-05-08 17:51:22', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 17:51:22', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'136', N'44e2c9ce-fd75-4fd5-bc88-e70929d6340f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'26', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:51:24', N'2021-05-08 17:51:24', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 17:51:24', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'137', N'42c2aa79-4746-4ffd-89a7-4fcf2b7a8e21', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'41', N'1', N'1', N'T2admin', N'{}', N'2021-05-08 17:51:24', N'2021-05-08 17:51:24', N'get', N'api/sysrole/getsysrolelist', N'2021-05-08 17:51:24', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'138', N'be867c92-b7a4-4a37-bc51-c37931ac687d', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'24', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:51:24', N'2021-05-08 17:51:24', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 17:51:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'139', N'50a4160f-4ba6-49cc-93ee-b65ea4f1ee74', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:51:26', N'2021-05-08 17:51:26', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 17:51:26', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'140', N'44e2c9ce-fd75-4fd5-bc88-e70929d6340f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'23', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:51:27', N'2021-05-08 17:51:27', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 17:51:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'141', N'796f5f7c-7513-4261-b89d-81f81500b27c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:51:28', N'2021-05-08 17:51:28', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 17:51:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'142', N'5102aba1-d4e1-4eab-82fc-e321c0dfd5d2', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:51:29', N'2021-05-08 17:51:29', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 17:51:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'143', N'5102aba1-d4e1-4eab-82fc-e321c0dfd5d2', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'24', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:51:55', N'2021-05-08 17:51:55', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 17:51:55', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'144', N'796f5f7c-7513-4261-b89d-81f81500b27c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:51:58', N'2021-05-08 17:51:58', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 17:51:58', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'145', N'44e2c9ce-fd75-4fd5-bc88-e70929d6340f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 17:52:01', N'2021-05-08 17:52:01', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 17:52:01', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'146', N'5102aba1-d4e1-4eab-82fc-e321c0dfd5d2', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'22', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:52:02', N'2021-05-08 17:52:02', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 17:52:02', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'147', N'796f5f7c-7513-4261-b89d-81f81500b27c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:52:03', N'2021-05-08 17:52:03', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 17:52:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'148', N'796f5f7c-7513-4261-b89d-81f81500b27c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'68', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:52:40', N'2021-05-08 17:52:40', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 17:52:40', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'149', N'f38e830c-db9d-4f74-98a2-bb2ed7d5e1ea', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'27', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 17:52:49', N'2021-05-08 17:52:49', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 17:52:49', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'150', N'796f5f7c-7513-4261-b89d-81f81500b27c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'25', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:53:45', N'2021-05-08 17:53:45', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 17:53:45', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'151', N'5102aba1-d4e1-4eab-82fc-e321c0dfd5d2', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 17:53:45', N'2021-05-08 17:53:45', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 17:53:45', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'152', N'f11c7747-3435-4a4d-8253-dcb069ef71ad', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'43', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 18:00:22', N'2021-05-08 18:00:22', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 18:00:22', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'153', N'6d0f9113-1cab-4b31-b9cb-cdcb0d020531', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'66', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:00:30', N'2021-05-08 18:00:30', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:00:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'154', N'3048bb17-9909-4786-80b8-9c6df9272e8f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'56', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:00:31', N'2021-05-08 18:00:31', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:00:32', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'155', N'7656fb65-5c5b-47e0-a4f0-5b02bcf93f88', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'34', N'1', N'1', N'T2admin', N'{
  "id": 116
}', N'2021-05-08 18:00:42', N'2021-05-08 18:00:42', N'get', N'api/syspermission/get', N'2021-05-08 18:00:42', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'156', N'6d8e0321-4454-4e37-b348-f40a65a01d46', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'40', N'1', N'1', N'T2admin', N'{
  "pid": 116
}', N'2021-05-08 18:00:42', N'2021-05-08 18:00:42', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-08 18:00:42', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'157', N'3048bb17-9909-4786-80b8-9c6df9272e8f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'55', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:01:50', N'2021-05-08 18:01:50', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:01:50', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'158', N'31083964-c0e8-4718-8090-e397b2a4c034', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'27', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:01:53', N'2021-05-08 18:01:53', N'post', N'api/sysuser/getpageuserlist', N'2021-05-08 18:01:53', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'159', N'9cd8da6b-9df6-4371-90be-4bfdd9ffd2d3', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'44', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:01:54', N'2021-05-08 18:01:54', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 18:01:54', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'160', N'1b4fd06e-301c-4b47-819c-e5f57a3ec076', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'52', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:01:55', N'2021-05-08 18:01:55', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:01:55', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'161', N'1b4fd06e-301c-4b47-819c-e5f57a3ec076', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'30', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:06:32', N'2021-05-08 18:06:32', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:06:32', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'162', N'1b4fd06e-301c-4b47-819c-e5f57a3ec076', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'29', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:07:19', N'2021-05-08 18:07:20', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:07:20', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'163', N'cbd2a1f5-3d9e-4c36-bff0-5f6e3434e4e8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'1302', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 18:24:35', N'2021-05-08 18:24:37', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 18:24:37', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'164', N'33bbbbb0-1fed-4afd-aa79-2febf7951854', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'101', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:24:45', N'2021-05-08 18:24:45', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:24:45', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'165', N'68e9e165-f231-41d8-b74d-20ffe42f84af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'85', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:24:56', N'2021-05-08 18:24:56', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 18:24:56', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'166', N'68e9e165-f231-41d8-b74d-20ffe42f84af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'55', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:25:31', N'2021-05-08 18:25:31', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 18:25:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'167', N'33bbbbb0-1fed-4afd-aa79-2febf7951854', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:25:41', N'2021-05-08 18:25:41', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:25:41', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'168', N'b2f6c352-6453-44c1-a4ab-9245f1555cb7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'44', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:25:44', N'2021-05-08 18:25:44', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:25:44', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'169', N'68e9e165-f231-41d8-b74d-20ffe42f84af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'22', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:25:45', N'2021-05-08 18:25:45', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 18:25:45', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'170', N'33bbbbb0-1fed-4afd-aa79-2febf7951854', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'32', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:27:27', N'2021-05-08 18:27:27', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:27:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'171', N'b2f6c352-6453-44c1-a4ab-9245f1555cb7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'23', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:27:28', N'2021-05-08 18:27:28', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:27:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'172', N'68e9e165-f231-41d8-b74d-20ffe42f84af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:27:29', N'2021-05-08 18:27:30', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 18:27:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'173', N'30e64982-14f0-4cba-b7b7-781534f9836f', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'27', N'1', N'1', N'T2admin', N'{
  "id": 171
}', N'2021-05-08 18:27:41', N'2021-05-08 18:27:41', N'get', N'api/sysauditlog/get', N'2021-05-08 18:27:41', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'174', N'68e9e165-f231-41d8-b74d-20ffe42f84af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'34', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:30:43', N'2021-05-08 18:30:43', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 18:30:43', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'175', N'dc66a2c9-6148-4d3f-8628-a6da11497c01', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'55', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:32:41', N'2021-05-08 18:32:41', N'post', N'api/sysuser/getpageuserlist', N'2021-05-08 18:32:41', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'176', N'8a5fa933-7441-4d64-9487-4e4fd4dcda97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'104', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:32:42', N'2021-05-08 18:32:42', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 18:32:42', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'177', N'8a5fa933-7441-4d64-9487-4e4fd4dcda97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'28', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:33:33', N'2021-05-08 18:33:33', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 18:33:33', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'178', N'cbd2a1f5-3d9e-4c36-bff0-5f6e3434e4e8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'33', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 18:33:44', N'2021-05-08 18:33:44', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 18:33:44', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'179', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:33:45', N'2021-05-08 18:33:45', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:33:45', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'180', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'30', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:34:25', N'2021-05-08 18:34:25', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:34:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'181', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'24', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:36:17', N'2021-05-08 18:36:17', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:36:17', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'182', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'23', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:37:01', N'2021-05-08 18:37:01', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:37:01', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'183', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'22', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:39:30', N'2021-05-08 18:39:30', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:39:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'184', N'cbd2a1f5-3d9e-4c36-bff0-5f6e3434e4e8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'34', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 18:43:00', N'2021-05-08 18:43:00', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 18:43:00', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'185', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'22', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:01', N'2021-05-08 18:43:01', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:43:01', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'186', N'8a5fa933-7441-4d64-9487-4e4fd4dcda97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'22', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:02', N'2021-05-08 18:43:02', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 18:43:02', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'187', N'dc66a2c9-6148-4d3f-8628-a6da11497c01', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'68', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:43:03', N'2021-05-08 18:43:03', N'post', N'api/sysuser/getpageuserlist', N'2021-05-08 18:43:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'188', N'8a5fa933-7441-4d64-9487-4e4fd4dcda97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:11', N'2021-05-08 18:43:11', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 18:43:11', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'189', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:12', N'2021-05-08 18:43:12', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:43:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'190', N'68e9e165-f231-41d8-b74d-20ffe42f84af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:43:13', N'2021-05-08 18:43:13', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-08 18:43:13', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'191', N'33bbbbb0-1fed-4afd-aa79-2febf7951854', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:43:15', N'2021-05-08 18:43:15', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:43:15', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'192', N'b2f6c352-6453-44c1-a4ab-9245f1555cb7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:17', N'2021-05-08 18:43:17', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:43:17', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'193', N'b2f6c352-6453-44c1-a4ab-9245f1555cb7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'29', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:18', N'2021-05-08 18:43:18', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:43:18', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'194', N'6f385538-0da1-4499-bf0f-9f94d63ef251', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'37', N'1', N'1', N'T2admin', N'{}', N'2021-05-08 18:43:18', N'2021-05-08 18:43:18', N'get', N'api/sysrole/getsysrolelist', N'2021-05-08 18:43:18', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'195', N'b2f6c352-6453-44c1-a4ab-9245f1555cb7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:19', N'2021-05-08 18:43:19', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:43:19', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'196', N'33bbbbb0-1fed-4afd-aa79-2febf7951854', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'23', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:43:19', N'2021-05-08 18:43:19', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:43:19', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'197', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'18', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:21', N'2021-05-08 18:43:21', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:43:21', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'198', N'8a5fa933-7441-4d64-9487-4e4fd4dcda97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'18', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:22', N'2021-05-08 18:43:22', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-08 18:43:22', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'199', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'18', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:26', N'2021-05-08 18:43:26', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:43:26', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'200', N'33bbbbb0-1fed-4afd-aa79-2febf7951854', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'18', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:43:28', N'2021-05-08 18:43:28', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:43:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'201', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:43:30', N'2021-05-08 18:43:30', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:43:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'202', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36', N'44', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:44:23', N'2021-05-08 18:44:23', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:44:23', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'203', N'cbd2a1f5-3d9e-4c36-bff0-5f6e3434e4e8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'40', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-08 18:48:16', N'2021-05-08 18:48:16', N'post', N'api/identity/gettokenbylogin', N'2021-05-08 18:48:16', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'204', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:48:18', N'2021-05-08 18:48:18', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:48:18', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'205', N'33bbbbb0-1fed-4afd-aa79-2febf7951854', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-08 18:48:19', N'2021-05-08 18:48:19', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-08 18:48:19', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'206', N'b2f6c352-6453-44c1-a4ab-9245f1555cb7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:48:20', N'2021-05-08 18:48:20', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:48:20', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'207', N'b2f6c352-6453-44c1-a4ab-9245f1555cb7', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'30', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:48:21', N'2021-05-08 18:48:21', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-08 18:48:21', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'208', N'6f385538-0da1-4499-bf0f-9f94d63ef251', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{}', N'2021-05-08 18:48:21', N'2021-05-08 18:48:21', N'get', N'api/sysrole/getsysrolelist', N'2021-05-08 18:48:21', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'209', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:48:23', N'2021-05-08 18:48:23', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:48:23', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'210', N'eb1bd081-5ff0-44d3-9efb-e0eee2ffa1c4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'23', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-08 18:48:37', N'2021-05-08 18:48:37', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-08 18:48:38', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'211', N'eaa86137-877c-4569-8515-153be7797dff', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'1427', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 09:36:49', N'2021-05-10 09:36:50', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 09:36:50', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'212', N'2c345ac2-d504-4af2-9c1b-5b6600d60300', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'82', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-10 09:36:54', N'2021-05-10 09:36:54', N'post', N'api/sysuser/getpageuserlist', N'2021-05-10 09:36:54', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'213', N'5cc6122d-c385-4031-af81-b0d0ec2f7ca4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'86', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 09:36:56', N'2021-05-10 09:36:56', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-10 09:36:56', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'214', N'4d082f72-dbad-4e97-adf4-c451fe3cdb10', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'43', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-10 09:37:01', N'2021-05-10 09:37:01', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-10 09:37:01', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'215', N'a7314820-c6a3-4a2e-9da2-ef58889c18ff', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'49', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 09:37:02', N'2021-05-10 09:37:02', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-10 09:37:02', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'216', N'a7314820-c6a3-4a2e-9da2-ef58889c18ff', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'32', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 09:37:03', N'2021-05-10 09:37:03', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-10 09:37:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'217', N'448684c3-9fc7-4fd5-b015-613c2b322347', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'44', N'1', N'1', N'T2admin', N'{}', N'2021-05-10 09:37:03', N'2021-05-10 09:37:03', N'get', N'api/sysrole/getsysrolelist', N'2021-05-10 09:37:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'218', N'a7314820-c6a3-4a2e-9da2-ef58889c18ff', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'23', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 10:15:18', N'2021-05-10 10:15:18', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-10 10:15:18', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'219', N'4d082f72-dbad-4e97-adf4-c451fe3cdb10', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'26', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-10 10:15:19', N'2021-05-10 10:15:19', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-10 10:15:19', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'220', N'4d082f72-dbad-4e97-adf4-c451fe3cdb10', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'37', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-10 10:15:23', N'2021-05-10 10:15:23', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-10 10:15:23', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'221', N'a7314820-c6a3-4a2e-9da2-ef58889c18ff', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 10:15:25', N'2021-05-10 10:15:25', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-10 10:15:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'222', N'a7314820-c6a3-4a2e-9da2-ef58889c18ff', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'26', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 10:15:26', N'2021-05-10 10:15:26', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-10 10:15:26', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'223', N'448684c3-9fc7-4fd5-b015-613c2b322347', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'20', N'1', N'1', N'T2admin', N'{}', N'2021-05-10 10:15:26', N'2021-05-10 10:15:26', N'get', N'api/sysrole/getsysrolelist', N'2021-05-10 10:15:26', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'224', N'5cc6122d-c385-4031-af81-b0d0ec2f7ca4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'19', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 10:15:27', N'2021-05-10 10:15:27', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-10 10:15:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'225', N'2197c64c-c412-4ee9-b824-3581c6f9cc7a', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'133', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 10:15:28', N'2021-05-10 10:15:28', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-10 10:15:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'226', N'2c345ac2-d504-4af2-9c1b-5b6600d60300', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'30', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-10 10:15:29', N'2021-05-10 10:15:29', N'post', N'api/sysuser/getpageuserlist', N'2021-05-10 10:15:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'227', N'c8919c7e-f758-45f8-ab8c-acd10edc57ec', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'601', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 10:47:29', N'2021-05-10 10:47:29', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 10:47:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'228', N'c8919c7e-f758-45f8-ab8c-acd10edc57ec', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'50', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 10:49:28', N'2021-05-10 10:49:28', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 10:49:28', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'229', N'c8919c7e-f758-45f8-ab8c-acd10edc57ec', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'41', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 10:53:03', N'2021-05-10 10:53:03', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 10:53:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'230', N'c8919c7e-f758-45f8-ab8c-acd10edc57ec', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'30', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 10:53:14', N'2021-05-10 10:53:14', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 10:53:14', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'231', N'8188156a-6079-457a-9682-1561d4950e97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'6092', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 11:27:58', N'2021-05-10 11:28:04', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 11:28:04', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'232', N'a671f264-b4cf-415a-8df0-0189930fb6dd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'1597', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-10 11:28:37', N'2021-05-10 11:28:39', N'post', N'api/sysuser/getpageuserlist', N'2021-05-10 11:28:39', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'233', N'8188156a-6079-457a-9682-1561d4950e97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'38', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 13:43:39', N'2021-05-10 13:43:39', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 13:43:39', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'234', N'8188156a-6079-457a-9682-1561d4950e97', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'30', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 14:07:38', N'2021-05-10 14:07:39', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 14:07:39', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'235', N'a671f264-b4cf-415a-8df0-0189930fb6dd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-10 14:07:40', N'2021-05-10 14:07:40', N'post', N'api/sysuser/getpageuserlist', N'2021-05-10 14:07:40', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'236', N'4cba6be3-4d45-4fd8-8f0b-da5dbe3bb4c1', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 14:07:41', N'2021-05-10 14:07:41', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-10 14:07:41', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'237', N'7ab4a0a5-8245-4033-af08-f8981f1f6848', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'26', N'1', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-10 14:07:42', N'2021-05-10 14:07:42', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-10 14:07:42', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'238', N'487d8b9e-5abb-4149-9b06-a8ad8503dfe2', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'648', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-10 18:18:13', N'2021-05-10 18:18:14', N'post', N'api/identity/gettokenbylogin', N'2021-05-10 18:18:14', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'239', N'193ae31b-2152-4432-baa7-c56251385013', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'658', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-11 09:31:29', N'2021-05-11 09:31:30', N'post', N'api/identity/gettokenbylogin', N'2021-05-11 09:31:30', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'240', N'175e5e63-8b81-4742-b11d-1027d1d7d44c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'566', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-11 15:34:30', N'2021-05-11 15:34:31', N'post', N'api/identity/gettokenbylogin', N'2021-05-11 15:34:31', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'241', N'c450f19b-b5dd-4d2b-8ab3-7f8945f252d1', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'95', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 15:34:33', N'2021-05-11 15:34:34', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-11 15:34:34', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'242', N'a6b72768-c4ee-4284-bc97-b5eca10aed30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'35', N'0', N'1', N'T2admin', N'{
  "id": 240
}', N'2021-05-11 15:34:37', N'2021-05-11 15:34:37', N'get', N'api/sysauditlog/get', N'2021-05-11 15:34:37', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'243', N'876b0b9f-684a-4277-a298-22fa3eba46d8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'61', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:34:40', N'2021-05-11 15:34:40', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-11 15:34:40', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'244', N'c89d18d9-94ee-44a7-9258-ca87858cda05', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'59', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:34:43', N'2021-05-11 15:34:43', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-11 15:34:43', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'245', N'876b0b9f-684a-4277-a298-22fa3eba46d8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'27', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:34:51', N'2021-05-11 15:34:51', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-11 15:34:51', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'246', N'22b76714-ece8-44da-8b1a-a46bd4be2484', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'171', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 15:34:53', N'2021-05-11 15:34:54', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-11 15:34:54', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'247', N'da36fe5e-87be-4ed0-a4d0-dae6344a6ecd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'48', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:34:55', N'2021-05-11 15:34:55', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 15:34:55', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'248', N'da36fe5e-87be-4ed0-a4d0-dae6344a6ecd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'32', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:36:00', N'2021-05-11 15:36:00', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 15:36:00', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'249', N'dc99ff4e-bf7b-4fd4-a999-298b453bae2b', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'45', N'0', N'1', N'T2admin', N'{}', N'2021-05-11 15:36:00', N'2021-05-11 15:36:00', N'get', N'api/sysrole/getsysrolelist', N'2021-05-11 15:36:00', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'250', N'175e5e63-8b81-4742-b11d-1027d1d7d44c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'50', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-11 15:37:09', N'2021-05-11 15:37:09', N'post', N'api/identity/gettokenbylogin', N'2021-05-11 15:37:09', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'251', N'156200f1-6500-4018-8bce-3e7218b9bc7d', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'36', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 15:37:23', N'2021-05-11 15:37:23', N'post', N'api/sysuser/getpageuserlist', N'2021-05-11 15:37:23', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'252', N'876b0b9f-684a-4277-a298-22fa3eba46d8', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'23', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:37:32', N'2021-05-11 15:37:32', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-11 15:37:32', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'253', N'c89d18d9-94ee-44a7-9258-ca87858cda05', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'22', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:37:40', N'2021-05-11 15:37:40', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-11 15:37:40', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'254', N'22b76714-ece8-44da-8b1a-a46bd4be2484', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'26', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 15:37:49', N'2021-05-11 15:37:49', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-11 15:37:49', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'255', N'da36fe5e-87be-4ed0-a4d0-dae6344a6ecd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'31', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:37:50', N'2021-05-11 15:37:50', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 15:37:50', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'256', N'da36fe5e-87be-4ed0-a4d0-dae6344a6ecd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'40', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:37:53', N'2021-05-11 15:37:53', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 15:37:53', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'257', N'dc99ff4e-bf7b-4fd4-a999-298b453bae2b', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'41', N'0', N'1', N'T2admin', N'{}', N'2021-05-11 15:37:53', N'2021-05-11 15:37:53', N'get', N'api/sysrole/getsysrolelist', N'2021-05-11 15:37:53', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'258', N'c450f19b-b5dd-4d2b-8ab3-7f8945f252d1', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'47', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 15:38:02', N'2021-05-11 15:38:02', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-11 15:38:02', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'259', N'a6b72768-c4ee-4284-bc97-b5eca10aed30', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'28', N'0', N'1', N'T2admin', N'{
  "id": 256
}', N'2021-05-11 15:38:20', N'2021-05-11 15:38:20', N'get', N'api/sysauditlog/get', N'2021-05-11 15:38:20', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'260', N'da36fe5e-87be-4ed0-a4d0-dae6344a6ecd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'25', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 15:38:25', N'2021-05-11 15:38:25', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 15:38:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'261', N'48e49031-eff6-4be8-aa0e-8fa98f0cf392', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'33', N'0', N'1', N'T2admin', N'{
  "id": 111
}', N'2021-05-11 15:38:27', N'2021-05-11 15:38:27', N'get', N'api/syspermission/get', N'2021-05-11 15:38:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'262', N'9d356e15-6898-42aa-85cc-55112e24be65', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'27', N'0', N'1', N'T2admin', N'{
  "pid": 111
}', N'2021-05-11 15:38:27', N'2021-05-11 15:38:27', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-11 15:38:27', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'263', N'ddc7ab29-d19e-47b8-921c-029ee3b13f61', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'581', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-05-11 16:35:28', N'2021-05-11 16:35:29', N'post', N'api/identity/gettokenbylogin', N'2021-05-11 16:35:29', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'264', N'b38fdb8f-2d4a-42ef-9038-18cbe0f677fe', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'55', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 16:36:03', N'2021-05-11 16:36:03', N'post', N'api/sysuser/getpageuserlist', N'2021-05-11 16:36:03', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'265', N'aebeae52-4230-4150-bb9d-9d00b9c1a7cd', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'49', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 16:37:05', N'2021-05-11 16:37:05', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-05-11 16:37:05', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'266', N'61f299f1-2c91-49c0-8073-c04049752f04', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'51', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 16:37:25', N'2021-05-11 16:37:25', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-05-11 16:37:25', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'267', N'bbae7161-536d-4f99-b81c-f40e10f9097c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'54', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 16:37:50', N'2021-05-11 16:37:50', N'post', N'api/sysauditlog/getpagesysauditloglist', N'2021-05-11 16:37:50', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'268', N'e322e828-b40e-40ad-a7ca-9e297c10bb86', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'50', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 16:38:08', N'2021-05-11 16:38:08', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-11 16:38:08', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'269', N'fc570603-e0a9-40ec-a381-5fba50bcb0a4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'100', N'0', N'1', N'T2admin', N'{}', N'2021-05-11 16:38:12', N'2021-05-11 16:38:12', N'get', N'api/sysrole/getsysrolelist', N'2021-05-11 16:38:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'270', N'4f0ff6df-02fd-4820-b400-4dd686a8cf86', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'118', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 16:38:12', N'2021-05-11 16:38:12', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 16:38:12', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'271', N'4f0ff6df-02fd-4820-b400-4dd686a8cf86', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'24', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 16:38:57', N'2021-05-11 16:38:57', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 16:38:57', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'272', N'3cc91850-4f30-4075-81de-ffeb31e5d273', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'34', N'0', N'1', N'T2admin', N'{
  "id": 111
}', N'2021-05-11 16:39:21', N'2021-05-11 16:39:21', N'get', N'api/syspermission/get', N'2021-05-11 16:39:21', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'273', N'4a916345-0ac7-44ac-a089-2dc40165e45a', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'22', N'0', N'1', N'T2admin', N'{
  "pid": 111
}', N'2021-05-11 16:39:22', N'2021-05-11 16:39:22', N'post', N'api/syspermission/getsyspermissionfilterbypid', N'2021-05-11 16:39:22', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'274', N'4f0ff6df-02fd-4820-b400-4dd686a8cf86', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'34', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 16:41:16', N'2021-05-11 16:41:16', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 16:41:16', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'275', N'fc570603-e0a9-40ec-a381-5fba50bcb0a4', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'35', N'0', N'1', N'T2admin', N'{}', N'2021-05-11 16:41:16', N'2021-05-11 16:41:16', N'get', N'api/sysrole/getsysrolelist', N'2021-05-11 16:41:16', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'276', N'4f0ff6df-02fd-4820-b400-4dd686a8cf86', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'21', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-05-11 16:41:21', N'2021-05-11 16:41:21', N'post', N'api/syspermission/getsyspermissionlist', N'2021-05-11 16:41:21', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'277', N'e322e828-b40e-40ad-a7ca-9e297c10bb86', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36', N'40', N'0', N'1', N'T2admin', N'{
  "input": {
    "CurrentPage": 1,
    "PageSize": 10,
    "Filter": {
      "QueryString": ""
    }
  }
}', N'2021-05-11 16:41:48', N'2021-05-11 16:41:48', N'post', N'api/sysrole/getpagesysrolelist', N'2021-05-11 16:41:48', NULL, NULL)
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'278', N'49d1e514-a114-43a6-ab3e-b88d39d375af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'84', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-09-01 16:41:18', N'2021-09-01 16:41:18', N'post', N'api/identity/gettokenbylogin', N'2021-09-01 16:41:18', N'1', N'{"State":true,"Code":200,"Message":"\u767B\u5F55\u6210\u529F\uFF01","Data":{"Id":1,"Account":"T2admin","Name":"\u8D85\u7EA7\u7BA1\u7406\u5458","Email":"1@qq.con","MobilePhone":null,"Expired":"7200","RefreshTokenExpired":null,"Authentication":"True","IP":null,"RoleInfoList":[{"Id":"1","RoleName":"\u8D85\u7EA7\u7BA1\u7406\u5458"},{"Id":"2","RoleName":"\u8FD0\u7EF4\u7BA1\u7406\u5458"}],"PermissionList":[{"ParentId":"116","Label":"\u5BA1\u8BA1\u65E5\u5FD7","Code":"sysAuditLogManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysAuditLogs","Api":"","Path":"/sysAuditLogs","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"117"},{"ParentId":"112","Label":"\u65B0\u589E\u7528\u6237","Code":"userManager.createUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/CreateUser","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"118"},{"ParentId":"112","Label":"\u67E5\u770B\u7528\u6237\u5217\u8868","Code":"userManager.viewUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/GetPageUserList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"119"},{"ParentId":"112","Label":"\u7F16\u8F91\u7528\u6237","Code":"userManager.editUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/UpdateUser","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"120"},{"ParentId":"112","Label":"\u5220\u9664\u7528\u6237","Code":"userManager.deleteUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/DeleteUserById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"121"},{"ParentId":"114","Label":"\u65B0\u589E\u6743\u9650","Code":"permissionManager.createPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/CreateSysPermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"122"},{"ParentId":"114","Label":"\u7F16\u8F91\u6743\u9650","Code":"permissionManager.editPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/UpdateSysPermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"123"},{"ParentId":"114","Label":"\u5220\u9664\u6743\u9650","Code":"permissionManager.deletePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"permissionManager.deletePermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"124"},{"ParentId":"114","Label":"\u67E5\u770B\u6743\u9650\u5217\u8868","Code":"permissionManager.viewPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/GetSysPermissionList,SysPermission/Get,SysPermission/GetSysPermissionFilterByPid","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"125"},{"ParentId":"113","Label":"\u67E5\u770B\u89D2\u8272\u5217\u8868","Code":"roleManager.viewRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/GetPageSysRoleList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"126"},{"ParentId":"113","Label":"\u7F16\u8F91\u89D2\u8272","Code":"roleManager.editRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/UpdateSysRole","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"127"},{"ParentId":"113","Label":"\u65B0\u589E\u89D2\u8272","Code":"roleManager.createRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/CreateSysRole","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"128"},{"ParentId":"113","Label":"\u5220\u9664\u89D2\u8272","Code":"roleManager.deleteRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/DeleteSysRoleById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"129"},{"ParentId":"130","Label":"\u914D\u7F6E\u89D2\u8272\u6743\u9650","Code":"rolePermissionManager.editRolePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/UpdateSysRolePermissions","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"131"},{"ParentId":"115","Label":"\u65B0\u589E\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.createDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/CreateSysDataDictionary","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"132"},{"ParentId":"115","Label":"\u7F16\u8F91\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.editDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/UpdateSysDataDictionary","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"133"},{"ParentId":"115","Label":"\u5220\u9664\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.deleteDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/DeleteSysDataDictionaryById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"134"},{"ParentId":"115","Label":"\u67E5\u770B\u6570\u636E\u5B57\u5178\u5217\u8868","Code":"dataDictionaryManager.viewDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/GetSysDataDictionaryList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"135"},{"ParentId":"139","Label":"\u67E5\u770B\u7EC4\u7EC7\u673A\u6784\u5217\u8868","Code":"sysOrganizationManager.viewSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/GetSysOrganizationList,SysOrganization/Get,SysOrganization/GetSysOrganizationFilterByPid","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"140"},{"ParentId":"139","Label":"\u65B0\u589E\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.createSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/CreateSysOrganization","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"141"},{"ParentId":"139","Label":"\u7F16\u8F91\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.editSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/UpdateSysOrganization","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"142"},{"ParentId":"139","Label":"\u5220\u9664\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.deleteSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/DeleteSysOrganizationById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"143"},{"ParentId":"130","Label":"\u67E5\u770B\u89D2\u8272\u6743\u9650\u5217\u8868","Code":"rolePermissionManager.viewRolePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/GetSysRoleList,SysPermission/GetSysPermissionList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"137"},{"ParentId":"117","Label":"\u67E5\u770B\u5BA1\u8BA1\u65E5\u5FD7\u5217\u8868","Code":"sysAuditLogManager.viewSysAuditLog","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysAuditLog/GetPageSysAuditLogList,SysAuditLog/Get","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"138"},{"ParentId":"0","Label":"\u9996\u9875","Code":"welcomePage","Type":2,"TypeName":"\u83DC\u5355","View":"Welcome","Api":"","Path":"/welcome","Icon":"el-icon-menu","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":1,"Description":"","Id":"136"},{"ParentId":"0","Label":"\u7528\u6237\u7BA1\u7406","Code":"userManager","Type":2,"TypeName":"\u83DC\u5355","View":"Users","Api":"","Path":"/users","Icon":"el-icon-user","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":3,"Description":"","Id":"112"},{"ParentId":"111","Label":"\u89D2\u8272\u7BA1\u7406","Code":"system.roleManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysRoles","Api":"","Path":"/sysRoles","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":4,"Description":"","Id":"113"},{"ParentId":"111","Label":"\u6743\u9650\u7BA1\u7406","Code":"permissionManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysPermissions","Api":"","Path":"/sysPermissions","Icon":"el-icon-s-operation","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":5,"Description":"","Id":"114"},{"ParentId":"0","Label":"\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysDataDictionarys","Api":"","Path":"/sysDataDictionarys","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":6,"Description":"","Id":"115"},{"ParentId":"111","Label":"\u89D2\u8272\u6743\u9650","Code":"rolePermissionManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysRolePermissions","Api":"","Path":"/sysRolePemissions","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":7,"Description":"","Id":"130"},{"ParentId":"0","Label":"\u65E5\u5FD7\u7BA1\u7406","Code":"logManager","Type":1,"TypeName":"\u5206\u7EC4","View":"","Api":"","Path":"","Icon":"el-icon-document","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":8,"Description":"","Id":"116"},{"ParentId":"0","Label":"\u7CFB\u7EDF\u7BA1\u7406","Code":"systemManager","Type":1,"TypeName":"\u5206\u7EC4","View":"","Api":"","Path":"","Icon":"el-icon-setting","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":8,"Description":"","Id":"111"},{"ParentId":"0","Label":"\u673A\u6784\u7BA1\u7406","Code":"sysOrganizationManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysOrganizations","Api":"","Path":"/sysOrganizations","Icon":"el-icon-s-custom","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":999,"Description":"","Id":"139"}],"TenantId":2,"Token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJBY2NvdW50IjoiVDJhZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiLotoXnuqfnrqHnkIblkZgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL2V4cGlyZWQiOiI3MjAwIiwiUmVmcmVzaFRva2VuRXhwaXJlZCI6IjcyMDAiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9hdXRoZW50aWNhdGlvbiI6IlRydWUiLCJUb2tlbktleSI6InRlbmFudElkXzJfdXNlcklkXzEiLCJ0ZW5hbnRJZCI6IjIiLCJJc3N1ZXIiOiLlhYPno4HkuYvlipsiLCJBdWRpZW5jZSI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsIm5iZiI6MTYzMDQ4NTY3OCwiZXhwIjoxNjMwOTE3Njc4LCJpc3MiOiLlhYPno4HkuYvlipsiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAifQ.U0umTR_ufhILZX5fcdZT6pvLURj0heoqE7xwlXfMw7I"}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'279', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'41', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:41:19', N'2021-09-01 16:41:19', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:41:19', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'280', N'5a61b45d-46b6-463e-961b-626740051a85', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'38', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:41:20', N'2021-09-01 16:41:20', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-09-01 16:41:20', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Label":"\u79DF\u62372-\u603B\u673A\u6784","ParentId":"0","OrganType":1,"Sort":1,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"1"},{"TypeName":null,"Label":"\u79DF\u62372-\u4EBA\u4E8B\u90E8","ParentId":"1","OrganType":1,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"2"},{"TypeName":null,"Label":"\u79DF\u62372-\u53F8\u6CD5\u90E8","ParentId":"1","OrganType":1,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"3"},{"TypeName":null,"Label":"\u79DF\u62372-\u521B\u65B0\u4E8B\u4E1A\u90E8","ParentId":"1","OrganType":2,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"4"},{"TypeName":null,"Label":"\u79DF\u62372-\u6D4B\u8BD5\u7EC4","ParentId":"4","OrganType":3,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"6"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'281', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'43', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:41:23', N'2021-09-01 16:41:23', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:41:23', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'282', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'52', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:41:25', N'2021-09-01 16:41:25', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:41:25', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'283', N'1e565a06-430f-4e3d-b752-bffcc819fbaa', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'80', N'0', N'1', N'T2admin', N'{
  "input": {
    "Key": "23",
    "Label": "aaa",
    "ParentId": 0,
    "Type": 121,
    "Memoni": "111",
    "Sort": 2,
    "Value": "",
    "Id": ""
  }
}', N'2021-09-01 16:41:36', N'2021-09-01 16:41:37', N'post', N'api/sysdatadictionary/createsysdatadictionary', N'2021-09-01 16:41:37', N'1', N'{"State":true,"Code":200,"Message":null,"Data":null}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'284', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'39', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:41:37', N'2021-09-01 16:41:37', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:41:37', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"},{"TypeName":null,"Key":"23","Label":"aaa","ParentId":"0","Type":121,"Memoni":"111","Sort":2,"Value":"","Id":"3"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'285', N'49d1e514-a114-43a6-ab3e-b88d39d375af', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'63', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-09-01 16:42:04', N'2021-09-01 16:42:04', N'post', N'api/identity/gettokenbylogin', N'2021-09-01 16:42:04', N'1', N'{"State":true,"Code":200,"Message":"\u767B\u5F55\u6210\u529F\uFF01","Data":{"Id":1,"Account":"T2admin","Name":"\u8D85\u7EA7\u7BA1\u7406\u5458","Email":"1@qq.con","MobilePhone":null,"Expired":"7200","RefreshTokenExpired":null,"Authentication":"True","IP":null,"RoleInfoList":[{"Id":"1","RoleName":"\u8D85\u7EA7\u7BA1\u7406\u5458"},{"Id":"2","RoleName":"\u8FD0\u7EF4\u7BA1\u7406\u5458"}],"PermissionList":[{"ParentId":"116","Label":"\u5BA1\u8BA1\u65E5\u5FD7","Code":"sysAuditLogManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysAuditLogs","Api":"","Path":"/sysAuditLogs","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"117"},{"ParentId":"112","Label":"\u65B0\u589E\u7528\u6237","Code":"userManager.createUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/CreateUser","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"118"},{"ParentId":"112","Label":"\u67E5\u770B\u7528\u6237\u5217\u8868","Code":"userManager.viewUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/GetPageUserList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"119"},{"ParentId":"112","Label":"\u7F16\u8F91\u7528\u6237","Code":"userManager.editUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/UpdateUser","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"120"},{"ParentId":"112","Label":"\u5220\u9664\u7528\u6237","Code":"userManager.deleteUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/DeleteUserById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"121"},{"ParentId":"114","Label":"\u65B0\u589E\u6743\u9650","Code":"permissionManager.createPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/CreateSysPermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"122"},{"ParentId":"114","Label":"\u7F16\u8F91\u6743\u9650","Code":"permissionManager.editPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/UpdateSysPermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"123"},{"ParentId":"114","Label":"\u5220\u9664\u6743\u9650","Code":"permissionManager.deletePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"permissionManager.deletePermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"124"},{"ParentId":"114","Label":"\u67E5\u770B\u6743\u9650\u5217\u8868","Code":"permissionManager.viewPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/GetSysPermissionList,SysPermission/Get,SysPermission/GetSysPermissionFilterByPid","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"125"},{"ParentId":"113","Label":"\u67E5\u770B\u89D2\u8272\u5217\u8868","Code":"roleManager.viewRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/GetPageSysRoleList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"126"},{"ParentId":"113","Label":"\u7F16\u8F91\u89D2\u8272","Code":"roleManager.editRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/UpdateSysRole","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"127"},{"ParentId":"113","Label":"\u65B0\u589E\u89D2\u8272","Code":"roleManager.createRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/CreateSysRole","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"128"},{"ParentId":"113","Label":"\u5220\u9664\u89D2\u8272","Code":"roleManager.deleteRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/DeleteSysRoleById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"129"},{"ParentId":"130","Label":"\u914D\u7F6E\u89D2\u8272\u6743\u9650","Code":"rolePermissionManager.editRolePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/UpdateSysRolePermissions","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"131"},{"ParentId":"115","Label":"\u65B0\u589E\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.createDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/CreateSysDataDictionary","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"132"},{"ParentId":"115","Label":"\u7F16\u8F91\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.editDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/UpdateSysDataDictionary","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"133"},{"ParentId":"115","Label":"\u5220\u9664\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.deleteDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/DeleteSysDataDictionaryById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"134"},{"ParentId":"115","Label":"\u67E5\u770B\u6570\u636E\u5B57\u5178\u5217\u8868","Code":"dataDictionaryManager.viewDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/GetSysDataDictionaryList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"135"},{"ParentId":"139","Label":"\u67E5\u770B\u7EC4\u7EC7\u673A\u6784\u5217\u8868","Code":"sysOrganizationManager.viewSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/GetSysOrganizationList,SysOrganization/Get,SysOrganization/GetSysOrganizationFilterByPid","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"140"},{"ParentId":"139","Label":"\u65B0\u589E\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.createSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/CreateSysOrganization","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"141"},{"ParentId":"139","Label":"\u7F16\u8F91\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.editSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/UpdateSysOrganization","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"142"},{"ParentId":"139","Label":"\u5220\u9664\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.deleteSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/DeleteSysOrganizationById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"143"},{"ParentId":"130","Label":"\u67E5\u770B\u89D2\u8272\u6743\u9650\u5217\u8868","Code":"rolePermissionManager.viewRolePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/GetSysRoleList,SysPermission/GetSysPermissionList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"137"},{"ParentId":"117","Label":"\u67E5\u770B\u5BA1\u8BA1\u65E5\u5FD7\u5217\u8868","Code":"sysAuditLogManager.viewSysAuditLog","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysAuditLog/GetPageSysAuditLogList,SysAuditLog/Get","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"138"},{"ParentId":"0","Label":"\u9996\u9875","Code":"welcomePage","Type":2,"TypeName":"\u83DC\u5355","View":"Welcome","Api":"","Path":"/welcome","Icon":"el-icon-menu","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":1,"Description":"","Id":"136"},{"ParentId":"0","Label":"\u7528\u6237\u7BA1\u7406","Code":"userManager","Type":2,"TypeName":"\u83DC\u5355","View":"Users","Api":"","Path":"/users","Icon":"el-icon-user","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":3,"Description":"","Id":"112"},{"ParentId":"111","Label":"\u89D2\u8272\u7BA1\u7406","Code":"system.roleManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysRoles","Api":"","Path":"/sysRoles","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":4,"Description":"","Id":"113"},{"ParentId":"111","Label":"\u6743\u9650\u7BA1\u7406","Code":"permissionManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysPermissions","Api":"","Path":"/sysPermissions","Icon":"el-icon-s-operation","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":5,"Description":"","Id":"114"},{"ParentId":"0","Label":"\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysDataDictionarys","Api":"","Path":"/sysDataDictionarys","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":6,"Description":"","Id":"115"},{"ParentId":"111","Label":"\u89D2\u8272\u6743\u9650","Code":"rolePermissionManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysRolePermissions","Api":"","Path":"/sysRolePemissions","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":7,"Description":"","Id":"130"},{"ParentId":"0","Label":"\u65E5\u5FD7\u7BA1\u7406","Code":"logManager","Type":1,"TypeName":"\u5206\u7EC4","View":"","Api":"","Path":"","Icon":"el-icon-document","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":8,"Description":"","Id":"116"},{"ParentId":"0","Label":"\u7CFB\u7EDF\u7BA1\u7406","Code":"systemManager","Type":1,"TypeName":"\u5206\u7EC4","View":"","Api":"","Path":"","Icon":"el-icon-setting","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":8,"Description":"","Id":"111"},{"ParentId":"0","Label":"\u673A\u6784\u7BA1\u7406","Code":"sysOrganizationManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysOrganizations","Api":"","Path":"/sysOrganizations","Icon":"el-icon-s-custom","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":999,"Description":"","Id":"139"}],"TenantId":2,"Token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJBY2NvdW50IjoiVDJhZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiLotoXnuqfnrqHnkIblkZgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL2V4cGlyZWQiOiI3MjAwIiwiUmVmcmVzaFRva2VuRXhwaXJlZCI6IjcyMDAiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9hdXRoZW50aWNhdGlvbiI6IlRydWUiLCJUb2tlbktleSI6InRlbmFudElkXzJfdXNlcklkXzEiLCJ0ZW5hbnRJZCI6IjIiLCJJc3N1ZXIiOiLlhYPno4HkuYvlipsiLCJBdWRpZW5jZSI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsIm5iZiI6MTYzMDQ4NTcyMywiZXhwIjoxNjMwOTE3NzIzLCJpc3MiOiLlhYPno4HkuYvlipsiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAifQ.3YWdkOHtiJDH7r8m0YrwwLRAiROFYyO3zGcZK0D2OEM"}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'286', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'42', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:42:05', N'2021-09-01 16:42:05', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:42:05', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"},{"TypeName":null,"Key":"23","Label":"aaa","ParentId":"0","Type":121,"Memoni":"111","Sort":2,"Value":"","Id":"3"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'287', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'63', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:42:28', N'2021-09-01 16:42:28', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:42:28', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"},{"TypeName":null,"Key":"23","Label":"aaa","ParentId":"0","Type":121,"Memoni":"111","Sort":2,"Value":"","Id":"3"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'288', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'38', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:42:33', N'2021-09-01 16:42:33', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:42:33', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"},{"TypeName":null,"Key":"23","Label":"aaa","ParentId":"0","Type":121,"Memoni":"111","Sort":2,"Value":"","Id":"3"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'289', N'cc1524db-811c-46b8-acdd-5df4a4d51366', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'66', N'0', N'1', N'T2admin', N'{
  "id": 3
}', N'2021-09-01 16:42:37', N'2021-09-01 16:42:37', N'delete', N'api/sysdatadictionary/deletesysdatadictionarybyid', N'2021-09-01 16:42:37', N'1', N'{"State":true,"Code":200,"Message":null,"Data":null}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'290', N'dad2c75a-0b37-4717-b76e-4ea5f4c6102c', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'39', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:42:37', N'2021-09-01 16:42:37', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:42:37', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'291', N'0a3da480-62db-4498-9684-63a63e0621a0', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'85', N'0', N'0', NULL, N'{
  "loginUserDto": {
    "userId": "T2admin",
    "pwd": "111111",
    "TenantId": 2
  }
}', N'2021-09-01 16:50:42', N'2021-09-01 16:50:42', N'post', N'api/identity/gettokenbylogin', N'2021-09-01 16:50:42', N'1', N'{"State":true,"Code":200,"Message":"\u767B\u5F55\u6210\u529F\uFF01","Data":{"Id":1,"Account":"T2admin","Name":"\u8D85\u7EA7\u7BA1\u7406\u5458","Email":"1@qq.con","MobilePhone":null,"Expired":"7200","RefreshTokenExpired":null,"Authentication":"True","IP":null,"RoleInfoList":[{"Id":"1","RoleName":"\u8D85\u7EA7\u7BA1\u7406\u5458"},{"Id":"2","RoleName":"\u8FD0\u7EF4\u7BA1\u7406\u5458"}],"PermissionList":[{"ParentId":"116","Label":"\u5BA1\u8BA1\u65E5\u5FD7","Code":"sysAuditLogManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysAuditLogs","Api":"","Path":"/sysAuditLogs","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"117"},{"ParentId":"112","Label":"\u65B0\u589E\u7528\u6237","Code":"userManager.createUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/CreateUser","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"118"},{"ParentId":"112","Label":"\u67E5\u770B\u7528\u6237\u5217\u8868","Code":"userManager.viewUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/GetPageUserList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"119"},{"ParentId":"112","Label":"\u7F16\u8F91\u7528\u6237","Code":"userManager.editUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/UpdateUser","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"120"},{"ParentId":"112","Label":"\u5220\u9664\u7528\u6237","Code":"userManager.deleteUser","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysUser/DeleteUserById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"121"},{"ParentId":"114","Label":"\u65B0\u589E\u6743\u9650","Code":"permissionManager.createPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/CreateSysPermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"122"},{"ParentId":"114","Label":"\u7F16\u8F91\u6743\u9650","Code":"permissionManager.editPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/UpdateSysPermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"123"},{"ParentId":"114","Label":"\u5220\u9664\u6743\u9650","Code":"permissionManager.deletePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"permissionManager.deletePermission","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"124"},{"ParentId":"114","Label":"\u67E5\u770B\u6743\u9650\u5217\u8868","Code":"permissionManager.viewPermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysPermission/GetSysPermissionList,SysPermission/Get,SysPermission/GetSysPermissionFilterByPid","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"125"},{"ParentId":"113","Label":"\u67E5\u770B\u89D2\u8272\u5217\u8868","Code":"roleManager.viewRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/GetPageSysRoleList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"126"},{"ParentId":"113","Label":"\u7F16\u8F91\u89D2\u8272","Code":"roleManager.editRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/UpdateSysRole","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"127"},{"ParentId":"113","Label":"\u65B0\u589E\u89D2\u8272","Code":"roleManager.createRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/CreateSysRole","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"128"},{"ParentId":"113","Label":"\u5220\u9664\u89D2\u8272","Code":"roleManager.deleteRole","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/DeleteSysRoleById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"129"},{"ParentId":"130","Label":"\u914D\u7F6E\u89D2\u8272\u6743\u9650","Code":"rolePermissionManager.editRolePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/UpdateSysRolePermissions","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"131"},{"ParentId":"115","Label":"\u65B0\u589E\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.createDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/CreateSysDataDictionary","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"132"},{"ParentId":"115","Label":"\u7F16\u8F91\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.editDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/UpdateSysDataDictionary","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"133"},{"ParentId":"115","Label":"\u5220\u9664\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager.deleteDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/DeleteSysDataDictionaryById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"134"},{"ParentId":"115","Label":"\u67E5\u770B\u6570\u636E\u5B57\u5178\u5217\u8868","Code":"dataDictionaryManager.viewDataDictionary","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysDataDictionary/GetSysDataDictionaryList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"135"},{"ParentId":"139","Label":"\u67E5\u770B\u7EC4\u7EC7\u673A\u6784\u5217\u8868","Code":"sysOrganizationManager.viewSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/GetSysOrganizationList,SysOrganization/Get,SysOrganization/GetSysOrganizationFilterByPid","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"140"},{"ParentId":"139","Label":"\u65B0\u589E\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.createSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/CreateSysOrganization","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"141"},{"ParentId":"139","Label":"\u7F16\u8F91\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.editSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/UpdateSysOrganization","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"142"},{"ParentId":"139","Label":"\u5220\u9664\u7EC4\u7EC7\u673A\u6784","Code":"sysOrganizationManager.deleteSysOrganization","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysOrganization/DeleteSysOrganizationById","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"143"},{"ParentId":"130","Label":"\u67E5\u770B\u89D2\u8272\u6743\u9650\u5217\u8868","Code":"rolePermissionManager.viewRolePermission","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysRole/GetSysRoleList,SysPermission/GetSysPermissionList","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"137"},{"ParentId":"117","Label":"\u67E5\u770B\u5BA1\u8BA1\u65E5\u5FD7\u5217\u8868","Code":"sysAuditLogManager.viewSysAuditLog","Type":3,"TypeName":"\u64CD\u4F5C\u70B9","View":"","Api":"SysAuditLog/GetPageSysAuditLogList,SysAuditLog/Get","Path":"","Icon":"","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":0,"Description":"","Id":"138"},{"ParentId":"0","Label":"\u9996\u9875","Code":"welcomePage","Type":2,"TypeName":"\u83DC\u5355","View":"Welcome","Api":"","Path":"/welcome","Icon":"el-icon-menu","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":1,"Description":"","Id":"136"},{"ParentId":"0","Label":"\u7528\u6237\u7BA1\u7406","Code":"userManager","Type":2,"TypeName":"\u83DC\u5355","View":"Users","Api":"","Path":"/users","Icon":"el-icon-user","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":3,"Description":"","Id":"112"},{"ParentId":"111","Label":"\u89D2\u8272\u7BA1\u7406","Code":"system.roleManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysRoles","Api":"","Path":"/sysRoles","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":4,"Description":"","Id":"113"},{"ParentId":"111","Label":"\u6743\u9650\u7BA1\u7406","Code":"permissionManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysPermissions","Api":"","Path":"/sysPermissions","Icon":"el-icon-s-operation","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":5,"Description":"","Id":"114"},{"ParentId":"0","Label":"\u6570\u636E\u5B57\u5178","Code":"dataDictionaryManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysDataDictionarys","Api":"","Path":"/sysDataDictionarys","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":6,"Description":"","Id":"115"},{"ParentId":"111","Label":"\u89D2\u8272\u6743\u9650","Code":"rolePermissionManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysRolePermissions","Api":"","Path":"/sysRolePemissions","Icon":"el-icon-share","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":7,"Description":"","Id":"130"},{"ParentId":"0","Label":"\u65E5\u5FD7\u7BA1\u7406","Code":"logManager","Type":1,"TypeName":"\u5206\u7EC4","View":"","Api":"","Path":"","Icon":"el-icon-document","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":8,"Description":"","Id":"116"},{"ParentId":"0","Label":"\u7CFB\u7EDF\u7BA1\u7406","Code":"systemManager","Type":1,"TypeName":"\u5206\u7EC4","View":"","Api":"","Path":"","Icon":"el-icon-setting","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":8,"Description":"","Id":"111"},{"ParentId":"0","Label":"\u673A\u6784\u7BA1\u7406","Code":"sysOrganizationManager","Type":2,"TypeName":"\u83DC\u5355","View":"SysOrganizations","Api":"","Path":"/sysOrganizations","Icon":"el-icon-s-custom","Hidden":true,"Closable":true,"Opened":true,"NewWindow":true,"External":true,"Sort":999,"Description":"","Id":"139"}],"TenantId":2,"Token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJBY2NvdW50IjoiVDJhZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiLotoXnuqfnrqHnkIblkZgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL2V4cGlyZWQiOiI3MjAwIiwiUmVmcmVzaFRva2VuRXhwaXJlZCI6IjcyMDAiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9hdXRoZW50aWNhdGlvbiI6IlRydWUiLCJUb2tlbktleSI6InRlbmFudElkXzJfdXNlcklkXzEiLCJ0ZW5hbnRJZCI6IjIiLCJJc3N1ZXIiOiLlhYPno4HkuYvlipsiLCJBdWRpZW5jZSI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsIm5iZiI6MTYzMDQ4NjI0MiwiZXhwIjoxNjMwOTE4MjQyLCJpc3MiOiLlhYPno4HkuYvlipsiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAifQ.JVpwPupJsZaeAJM-tVmpN-0_qeXZkqn9UwE1al2jtFw"}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'292', N'71392c24-101d-48f7-99a4-806e6f130aa9', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'118', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:50:44', N'2021-09-01 16:50:44', N'post', N'api/sysorganization/getsysorganizationlist', N'2021-09-01 16:50:44', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Label":"\u79DF\u62372-\u603B\u673A\u6784","ParentId":"0","OrganType":1,"Sort":1,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"1"},{"TypeName":null,"Label":"\u79DF\u62372-\u4EBA\u4E8B\u90E8","ParentId":"1","OrganType":1,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"2"},{"TypeName":null,"Label":"\u79DF\u62372-\u53F8\u6CD5\u90E8","ParentId":"1","OrganType":1,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"3"},{"TypeName":null,"Label":"\u79DF\u62372-\u521B\u65B0\u4E8B\u4E1A\u90E8","ParentId":"1","OrganType":2,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"4"},{"TypeName":null,"Label":"\u79DF\u62372-\u6D4B\u8BD5\u7EC4","ParentId":"4","OrganType":3,"Sort":0,"PostId":0,"Fax":"","Telephone":"","Address":"","Memoni":"","Remark":"","RangeType":0,"Range":"","Linkman":"","Id":"6"}]}}')
GO

INSERT INTO [dbo].[sys_auditlog] ([Id], [Key], [IP], [Browser], [Os], [Device], [BrowserInfo], [ElapsedMilliseconds], [TenantId], [UserId], [UserAccount], [ParamsString], [StartTime], [StopTime], [RequestMethod], [RequestApi], [CreationTime], [ResponseState], [ResponseData]) VALUES (N'293', N'a9e3f99d-9bf4-4313-a9e6-012a5bf5365e', N'127.0.0.1', N'Chrome', N'Windows', N'', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', N'48', N'0', N'1', N'T2admin', N'{
  "input": {
    "QueryString": ""
  }
}', N'2021-09-01 16:50:44', N'2021-09-01 16:50:44', N'post', N'api/sysdatadictionary/getsysdatadictionarylist', N'2021-09-01 16:50:44', N'1', N'{"State":true,"Code":200,"Message":null,"Data":{"Total":0,"List":[{"TypeName":null,"Key":"MenuType.Common","Label":"\u516C\u5171\u83DC\u5355","ParentId":"2","Type":0,"Memoni":null,"Sort":null,"Value":"1","Id":"1"},{"TypeName":null,"Key":"MenuType","Label":"\u83DC\u5355\u7C7B\u522B","ParentId":"0","Type":0,"Memoni":null,"Sort":null,"Value":null,"Id":"2"}]}}')
GO

SET IDENTITY_INSERT [dbo].[sys_auditlog] OFF
GO


-- ----------------------------
-- Table structure for sys_datadictionary
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_datadictionary]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_datadictionary]
GO

CREATE TABLE [dbo].[sys_datadictionary] (
  [Key] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParentId] bigint  NULL,
  [Memoni] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [Sort] int  NULL,
  [Value] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [IsActive] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(0)  NULL,
  [CreatorUserId] bigint  NULL,
  [TenantId] int  NULL,
  [Type] int  NULL,
  [Label] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[sys_datadictionary] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'名称',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'Key'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父节点',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'ParentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'助记符',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'Memoni'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'Sort'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'Value'
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否启用标识',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'IsActive'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 是否删除标识',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'IsDeleted'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 创建时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'CreationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 创建ID',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'CreatorUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'租户名',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'TenantId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别:0 分组， 1叶子节点',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'Type'
GO

EXEC sp_addextendedproperty
'MS_Description', N'名称',
'SCHEMA', N'dbo',
'TABLE', N'sys_datadictionary',
'COLUMN', N'Label'
GO


-- ----------------------------
-- Records of sys_datadictionary
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_datadictionary] ON
GO

INSERT INTO [dbo].[sys_datadictionary] ([Key], [ParentId], [Memoni], [Sort], [Value], [Id], [IsActive], [IsDeleted], [CreationTime], [CreatorUserId], [TenantId], [Type], [Label]) VALUES (N'MenuType.Common', N'2', NULL, NULL, N'1', N'1', NULL, NULL, NULL, NULL, NULL, NULL, N'公共菜单')
GO

INSERT INTO [dbo].[sys_datadictionary] ([Key], [ParentId], [Memoni], [Sort], [Value], [Id], [IsActive], [IsDeleted], [CreationTime], [CreatorUserId], [TenantId], [Type], [Label]) VALUES (N'MenuType', N'0', NULL, NULL, NULL, N'2', NULL, NULL, NULL, NULL, NULL, N'0', N'菜单类别')
GO

SET IDENTITY_INSERT [dbo].[sys_datadictionary] OFF
GO


-- ----------------------------
-- Table structure for sys_organization
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_organization]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_organization]
GO

CREATE TABLE [dbo].[sys_organization] (
  [Label] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParentId] bigint  NULL,
  [OrganType] int  NULL,
  [Sort] int  NULL,
  [PostId] int  NULL,
  [Fax] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL,
  [Telephone] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL,
  [Address] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [Memoni] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [Remark] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [RangeType] int  NULL,
  [Range] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [Linkman] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreatorUserId] bigint  NULL,
  [CreationTime] datetime2(0)  NULL,
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [IsActive] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [TenantId] int  NULL
)
GO

ALTER TABLE [dbo].[sys_organization] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'名称',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Label'
GO

EXEC sp_addextendedproperty
'MS_Description', N'所属上级',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'ParentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'节点类型',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'OrganType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'序号',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Sort'
GO

EXEC sp_addextendedproperty
'MS_Description', N'岗位编号',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'PostId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'传真',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Fax'
GO

EXEC sp_addextendedproperty
'MS_Description', N'联系电话',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Telephone'
GO

EXEC sp_addextendedproperty
'MS_Description', N'通讯地址',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'助记符',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Memoni'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'RangeType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限范围',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Range'
GO

EXEC sp_addextendedproperty
'MS_Description', N'联系人',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Linkman'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 创建ID',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'CreatorUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 创建时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'CreationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否启用标识',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'IsActive'
GO

EXEC sp_addextendedproperty
'MS_Description', N' 是否删除标识',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'IsDeleted'
GO

EXEC sp_addextendedproperty
'MS_Description', N'租户名',
'SCHEMA', N'dbo',
'TABLE', N'sys_organization',
'COLUMN', N'TenantId'
GO


-- ----------------------------
-- Records of sys_organization
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_organization] ON
GO

INSERT INTO [dbo].[sys_organization] ([Label], [ParentId], [OrganType], [Sort], [PostId], [Fax], [Telephone], [Address], [Memoni], [Remark], [RangeType], [Range], [Linkman], [CreatorUserId], [CreationTime], [Id], [IsActive], [IsDeleted], [TenantId]) VALUES (N'租户2-总机构', N'0', N'1', N'1', N'0', N'', N'', N'', N'', N'', N'0', N'', N'', N'1', N'2021-05-05 17:41:59', N'1', N'0', N'0', N'1')
GO

INSERT INTO [dbo].[sys_organization] ([Label], [ParentId], [OrganType], [Sort], [PostId], [Fax], [Telephone], [Address], [Memoni], [Remark], [RangeType], [Range], [Linkman], [CreatorUserId], [CreationTime], [Id], [IsActive], [IsDeleted], [TenantId]) VALUES (N'租户2-人事部', N'1', N'1', N'0', N'0', N'', N'', N'', N'', N'', N'0', N'', N'', N'1', N'2021-05-05 17:42:12', N'2', N'0', N'0', N'1')
GO

INSERT INTO [dbo].[sys_organization] ([Label], [ParentId], [OrganType], [Sort], [PostId], [Fax], [Telephone], [Address], [Memoni], [Remark], [RangeType], [Range], [Linkman], [CreatorUserId], [CreationTime], [Id], [IsActive], [IsDeleted], [TenantId]) VALUES (N'租户2-司法部', N'1', N'1', N'0', N'0', N'', N'', N'', N'', N'', N'0', N'', N'', N'1', N'2021-05-05 17:42:27', N'3', N'0', N'0', N'1')
GO

INSERT INTO [dbo].[sys_organization] ([Label], [ParentId], [OrganType], [Sort], [PostId], [Fax], [Telephone], [Address], [Memoni], [Remark], [RangeType], [Range], [Linkman], [CreatorUserId], [CreationTime], [Id], [IsActive], [IsDeleted], [TenantId]) VALUES (N'租户2-创新事业部', N'1', N'2', N'0', N'0', N'', N'', N'', N'', N'', N'0', N'', N'', N'1', N'2021-05-05 17:43:05', N'4', N'0', N'0', N'1')
GO

INSERT INTO [dbo].[sys_organization] ([Label], [ParentId], [OrganType], [Sort], [PostId], [Fax], [Telephone], [Address], [Memoni], [Remark], [RangeType], [Range], [Linkman], [CreatorUserId], [CreationTime], [Id], [IsActive], [IsDeleted], [TenantId]) VALUES (N'租户2-测试组', N'4', N'3', N'0', N'0', N'', N'', N'', N'', N'', N'0', N'', N'', N'1', N'2021-05-05 17:52:37', N'6', N'0', N'0', N'1')
GO

SET IDENTITY_INSERT [dbo].[sys_organization] OFF
GO


-- ----------------------------
-- Table structure for sys_permission
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_permission]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_permission]
GO

CREATE TABLE [dbo].[sys_permission] (
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [ParentId] bigint  NOT NULL,
  [Label] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Code] nvarchar(550) COLLATE Chinese_PRC_CI_AS  NULL,
  [Type] int  NOT NULL,
  [View] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Api] nvarchar(500) COLLATE Chinese_PRC_CI_AS  NULL,
  [Path] nvarchar(500) COLLATE Chinese_PRC_CI_AS  NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Hidden] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [IsActive] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Closable] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Opened] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [NewWindow] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [External] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Sort] int  NULL,
  [Description] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [TenantId] bigint  NULL,
  [IsDeleted] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreatorUserId] bigint  NULL,
  [CreationTime] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreatedTime] datetime2(0)  NULL,
  [LastModifierUserId] int  NULL,
  [LastModificationTime] datetime2(0)  NULL
)
GO

ALTER TABLE [dbo].[sys_permission] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父级节点',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'ParentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限名称',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Label'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限编码',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Code'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限类型',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Type'
GO

EXEC sp_addextendedproperty
'MS_Description', N'视图',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'View'
GO

EXEC sp_addextendedproperty
'MS_Description', N'接口',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Api'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单访问地址',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Path'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'隐藏',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Hidden'
GO

EXEC sp_addextendedproperty
'MS_Description', N'启用',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'IsActive'
GO

EXEC sp_addextendedproperty
'MS_Description', N'可关闭',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Closable'
GO

EXEC sp_addextendedproperty
'MS_Description', N'打开组',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Opened'
GO

EXEC sp_addextendedproperty
'MS_Description', N'打开新窗口',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'NewWindow'
GO

EXEC sp_addextendedproperty
'MS_Description', N'链接外显',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'External'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Sort'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'租户Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'TenantId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'IsDeleted'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建者Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'CreatorUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建者',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'CreationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'CreatedTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改者Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'LastModifierUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission',
'COLUMN', N'LastModificationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限',
'SCHEMA', N'dbo',
'TABLE', N'sys_permission'
GO


-- ----------------------------
-- Records of sys_permission
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_permission] ON
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'111', N'0', N'系统管理', N'systemManager', N'1', N'', N'', N'', N'el-icon-setting', N'1', N'1', N'1', N'1', N'1', N'1', N'8', N'', N'1', N'0', N'1', N'2021-05-02 18:35:03.773', NULL, N'0', N'2021-05-03 11:14:36')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'112', N'0', N'用户管理', N'userManager', N'2', N'Users', N'', N'/users', N'el-icon-user', N'1', N'1', N'1', N'1', N'1', N'1', N'3', N'', N'1', N'0', N'1', N'2021-05-02 18:37:37.656', NULL, N'0', N'2021-05-03 10:34:10')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'113', N'111', N'角色管理', N'system.roleManager', N'2', N'SysRoles', N'', N'/sysRoles', N'el-icon-share', N'1', N'1', N'1', N'1', N'1', N'1', N'4', N'', N'1', N'0', N'1', N'2021-05-02 18:39:33.200', NULL, N'0', N'2021-05-05 17:08:02')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'114', N'111', N'权限管理', N'permissionManager', N'2', N'SysPermissions', N'', N'/sysPermissions', N'el-icon-s-operation', N'1', N'1', N'1', N'1', N'1', N'1', N'5', N'', N'1', N'0', N'1', N'2021-05-02 18:40:20.539', NULL, N'0', N'2021-05-05 17:08:15')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'115', N'0', N'数据字典', N'dataDictionaryManager', N'2', N'SysDataDictionarys', N'', N'/sysDataDictionarys', N'el-icon-share', N'1', N'1', N'1', N'1', N'1', N'1', N'6', N'', N'1', N'0', N'1', N'2021-05-02 18:41:16.319', NULL, N'0', N'2021-05-03 11:11:33')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'116', N'0', N'日志管理', N'logManager', N'1', N'', N'', N'', N'el-icon-document', N'1', N'1', N'1', N'1', N'1', N'1', N'8', N'', N'1', N'0', N'1', N'2021-05-02 18:43:29.923', NULL, N'0', N'2021-05-03 11:14:49')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'117', N'116', N'审计日志', N'sysAuditLogManager', N'2', N'SysAuditLogs', N'', N'/sysAuditLogs', N'', N'1', N'1', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-02 18:44:24.935', NULL, N'0', N'2021-05-05 13:47:54')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'118', N'112', N'新增用户', N'userManager.createUser', N'3', N'', N'SysUser/CreateUser', N'', N'', N'1', N'1', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-02 18:46:06.639', NULL, N'0', N'2021-05-03 10:34:57')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'119', N'112', N'查看用户列表', N'userManager.viewUser', N'3', N'', N'SysUser/GetPageUserList', N'', N'', N'1', N'1', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-02 18:50:42.411', NULL, N'0', N'2021-05-03 10:32:26')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'120', N'112', N'编辑用户', N'userManager.editUser', N'3', N'', N'SysUser/UpdateUser', N'', N'', N'1', N'1', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:27:32.738', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'121', N'112', N'删除用户', N'userManager.deleteUser', N'3', N'', N'SysUser/DeleteUserById', N'', N'', N'1', N'1', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:35:40.971', NULL, N'0', N'2021-05-03 10:35:52')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'122', N'114', N'新增权限', N'permissionManager.createPermission', N'3', N'', N'SysPermission/CreateSysPermission', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:46:41.669', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'123', N'114', N'编辑权限', N'permissionManager.editPermission', N'3', N'', N'SysPermission/UpdateSysPermission', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:47:11.061', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'124', N'114', N'删除权限', N'permissionManager.deletePermission', N'3', N'', N'permissionManager.deletePermission', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:47:39.828', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'125', N'114', N'查看权限列表', N'permissionManager.viewPermission', N'3', N'', N'SysPermission/GetSysPermissionList,SysPermission/Get,SysPermission/GetSysPermissionFilterByPid', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:49:20.263', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'126', N'113', N'查看角色列表', N'roleManager.viewRole', N'3', N'', N'SysRole/GetPageSysRoleList', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:52:19.301', NULL, N'0', N'2021-05-03 10:55:28')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'127', N'113', N'编辑角色', N'roleManager.editRole', N'3', N'', N'SysRole/UpdateSysRole', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:53:49.424', NULL, N'0', N'2021-05-03 10:54:54')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'128', N'113', N'新增角色', N'roleManager.createRole', N'3', N'', N'SysRole/CreateSysRole', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:54:40.920', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'129', N'113', N'删除角色', N'roleManager.deleteRole', N'3', N'', N'SysRole/DeleteSysRoleById', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 10:56:37.772', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'130', N'111', N'角色权限', N'rolePermissionManager', N'2', N'SysRolePermissions', N'', N'/sysRolePemissions', N'el-icon-share', N'1', N'0', N'1', N'1', N'1', N'1', N'7', N'', N'1', N'0', N'1', N'2021-05-03 10:59:13.100', NULL, N'0', N'2021-05-05 17:08:31')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'131', N'130', N'配置角色权限', N'rolePermissionManager.editRolePermission', N'3', N'', N'SysRole/UpdateSysRolePermissions', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 11:00:38.300', NULL, N'0', N'2021-05-03 11:08:25')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'132', N'115', N'新增数据字典', N'dataDictionaryManager.createDataDictionary', N'3', N'', N'SysDataDictionary/CreateSysDataDictionary', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 11:07:23.817', NULL, N'0', N'2021-05-03 11:08:44')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'133', N'115', N'编辑数据字典', N'dataDictionaryManager.editDataDictionary', N'3', N'', N'SysDataDictionary/UpdateSysDataDictionary', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 11:09:27.438', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'134', N'115', N'删除数据字典', N'dataDictionaryManager.deleteDataDictionary', N'3', N'', N'SysDataDictionary/DeleteSysDataDictionaryById', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 11:10:11.542', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'135', N'115', N'查看数据字典列表', N'dataDictionaryManager.viewDataDictionary', N'3', N'', N'SysDataDictionary/GetSysDataDictionaryList', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 11:10:46.715', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'136', N'0', N'首页', N'welcomePage', N'2', N'Welcome', N'', N'/welcome', N'el-icon-menu', N'1', N'0', N'1', N'1', N'1', N'1', N'1', N'', N'1', N'0', N'1', N'2021-05-03 11:16:56.241', NULL, N'0', N'2021-05-04 15:01:24')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'137', N'130', N'查看角色权限列表', N'rolePermissionManager.viewRolePermission', N'3', N'', N'SysRole/GetSysRoleList,SysPermission/GetSysPermissionList', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-03 11:19:55.125', NULL, N'0', N'2021-05-03 11:23:45')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'138', N'117', N'查看审计日志列表', N'sysAuditLogManager.viewSysAuditLog', N'3', N'', N'SysAuditLog/GetPageSysAuditLogList,SysAuditLog/Get', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-05 12:59:33.332', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'139', N'0', N'机构管理', N'sysOrganizationManager', N'2', N'SysOrganizations', N'', N'/sysOrganizations', N'el-icon-s-custom', N'1', N'0', N'1', N'1', N'1', N'1', N'999', N'', N'1', N'0', N'1', N'2021-05-05 17:12:30.131', NULL, N'0', N'2021-05-05 17:24:55')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'140', N'139', N'查看组织机构列表', N'sysOrganizationManager.viewSysOrganization', N'3', N'', N'SysOrganization/GetSysOrganizationList,SysOrganization/Get,SysOrganization/GetSysOrganizationFilterByPid', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-05 17:13:51.264', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'141', N'139', N'新增组织机构', N'sysOrganizationManager.createSysOrganization', N'3', N'', N'SysOrganization/CreateSysOrganization', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-05 17:14:29.066', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'142', N'139', N'编辑组织机构', N'sysOrganizationManager.editSysOrganization', N'3', N'', N'SysOrganization/UpdateSysOrganization', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-05 17:22:03.526', NULL, N'0', N'0001-01-01 00:00:00')
GO

INSERT INTO [dbo].[sys_permission] ([Id], [ParentId], [Label], [Code], [Type], [View], [Api], [Path], [Icon], [Hidden], [IsActive], [Closable], [Opened], [NewWindow], [External], [Sort], [Description], [TenantId], [IsDeleted], [CreatorUserId], [CreationTime], [CreatedTime], [LastModifierUserId], [LastModificationTime]) VALUES (N'143', N'139', N'删除组织机构', N'sysOrganizationManager.deleteSysOrganization', N'3', N'', N'SysOrganization/DeleteSysOrganizationById', N'', N'', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'', N'1', N'0', N'1', N'2021-05-05 17:22:51.792', NULL, N'0', N'0001-01-01 00:00:00')
GO

SET IDENTITY_INSERT [dbo].[sys_permission] OFF
GO


-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_role]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_role]
GO

CREATE TABLE [dbo].[sys_role] (
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [Name] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [IsActive] tinyint  NULL,
  [Memoni] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [Sort] int  NULL,
  [IsDeleted] tinyint  NULL,
  [LastModificationTime] datetime2(0)  NULL,
  [LastModifierUserId] bigint  NULL,
  [OrgId] int  NULL,
  [TenantId] int  NULL
)
GO

ALTER TABLE [dbo].[sys_role] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'名称',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'Name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'启用',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'IsActive'
GO

EXEC sp_addextendedproperty
'MS_Description', N'助记符',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'Memoni'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'Sort'
GO

EXEC sp_addextendedproperty
'MS_Description', N'删除标记',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'IsDeleted'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'LastModificationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'LastModifierUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'组织',
'SCHEMA', N'dbo',
'TABLE', N'sys_role',
'COLUMN', N'OrgId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户角色',
'SCHEMA', N'dbo',
'TABLE', N'sys_role'
GO


-- ----------------------------
-- Records of sys_role
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_role] ON
GO

INSERT INTO [dbo].[sys_role] ([Id], [Name], [IsActive], [Memoni], [Sort], [IsDeleted], [LastModificationTime], [LastModifierUserId], [OrgId], [TenantId]) VALUES (N'1', N'超级管理员', N'0', N'superAdmin', NULL, N'0', N'2021-05-04 12:53:12', N'0', NULL, N'1')
GO

INSERT INTO [dbo].[sys_role] ([Id], [Name], [IsActive], [Memoni], [Sort], [IsDeleted], [LastModificationTime], [LastModifierUserId], [OrgId], [TenantId]) VALUES (N'2', N'运维管理员', N'0', N'operationAdmin', NULL, N'0', N'2021-05-04 12:53:03', N'0', NULL, N'1')
GO

SET IDENTITY_INSERT [dbo].[sys_role] OFF
GO


-- ----------------------------
-- Table structure for sys_rolepermission
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_rolepermission]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_rolepermission]
GO

CREATE TABLE [dbo].[sys_rolepermission] (
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [RoleId] bigint  NOT NULL,
  [PermissionId] bigint  NOT NULL,
  [CreatorUserId] bigint  NULL,
  [CreationTime] datetime2(0)  NULL
)
GO

ALTER TABLE [dbo].[sys_rolepermission] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_rolepermission',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_rolepermission',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_rolepermission',
'COLUMN', N'PermissionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建者Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_rolepermission',
'COLUMN', N'CreatorUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_rolepermission',
'COLUMN', N'CreationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色权限',
'SCHEMA', N'dbo',
'TABLE', N'sys_rolepermission'
GO


-- ----------------------------
-- Records of sys_rolepermission
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_rolepermission] ON
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1136', N'2', N'116', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1137', N'2', N'117', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1138', N'2', N'138', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1139', N'2', N'112', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1140', N'2', N'119', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1141', N'2', N'113', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1142', N'2', N'126', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1143', N'2', N'114', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1144', N'2', N'125', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1145', N'2', N'115', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1146', N'2', N'135', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1147', N'2', N'130', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1148', N'2', N'137', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1149', N'2', N'136', N'1', N'2021-05-05 13:46:51')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1178', N'1', N'111', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1179', N'1', N'113', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1180', N'1', N'126', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1181', N'1', N'127', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1182', N'1', N'128', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1183', N'1', N'129', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1184', N'1', N'114', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1185', N'1', N'122', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1186', N'1', N'123', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1187', N'1', N'124', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1188', N'1', N'125', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1189', N'1', N'130', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1190', N'1', N'131', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1191', N'1', N'137', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1192', N'1', N'116', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1193', N'1', N'117', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1194', N'1', N'138', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1195', N'1', N'112', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1196', N'1', N'118', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1197', N'1', N'119', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1198', N'1', N'120', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1199', N'1', N'121', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1200', N'1', N'115', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1201', N'1', N'132', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1202', N'1', N'133', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1203', N'1', N'134', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1204', N'1', N'135', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1205', N'1', N'136', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1206', N'1', N'139', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1207', N'1', N'140', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1208', N'1', N'141', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1209', N'1', N'142', N'1', N'2021-05-05 17:23:52')
GO

INSERT INTO [dbo].[sys_rolepermission] ([Id], [RoleId], [PermissionId], [CreatorUserId], [CreationTime]) VALUES (N'1210', N'1', N'143', N'1', N'2021-05-05 17:23:52')
GO

SET IDENTITY_INSERT [dbo].[sys_rolepermission] OFF
GO


-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_user]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_user]
GO

CREATE TABLE [dbo].[sys_user] (
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [NO] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [Name] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Account] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Password] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Sex] tinyint  NULL,
  [Mobile] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [Remark] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsActive] tinyint  NULL,
  [LoginCount] int  NULL,
  [Memoni] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] tinyint  NULL,
  [OrgId] int  NULL,
  [LastModificationTime] datetime2(0)  NULL,
  [ModifyUserId] int  NULL,
  [UserType] tinyint  NULL,
  [InterDepartmental] tinyint  NULL,
  [LastModifierUserId] bigint  NULL,
  [DeletionTime] datetime2(0)  NULL,
  [DeleterUserId] int  NULL,
  [CreationTime] datetime2(0)  NULL,
  [CreatorUserId] bigint  NULL,
  [TenantId] int  NULL
)
GO

ALTER TABLE [dbo].[sys_user] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工编号',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'NO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工名称',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录账号',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Account'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Password'
GO

EXEC sp_addextendedproperty
'MS_Description', N'性别',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Sex'
GO

EXEC sp_addextendedproperty
'MS_Description', N'手机号码',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Mobile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'信箱',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'启用',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'IsActive'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录次数',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'LoginCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'助记符',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'Memoni'
GO

EXEC sp_addextendedproperty
'MS_Description', N'删除标记',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'IsDeleted'
GO

EXEC sp_addextendedproperty
'MS_Description', N'组织',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'OrgId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'LastModificationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'ModifyUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跨部门数据查看',
'SCHEMA', N'dbo',
'TABLE', N'sys_user',
'COLUMN', N'InterDepartmental'
GO


-- ----------------------------
-- Records of sys_user
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_user] ON
GO

INSERT INTO [dbo].[sys_user] ([Id], [NO], [Name], [Account], [Password], [Sex], [Mobile], [Email], [Remark], [IsActive], [LoginCount], [Memoni], [IsDeleted], [OrgId], [LastModificationTime], [ModifyUserId], [UserType], [InterDepartmental], [LastModifierUserId], [DeletionTime], [DeleterUserId], [CreationTime], [CreatorUserId], [TenantId]) VALUES (N'1', N'admin', N'超级管理员', N'T2admin', N'96e79218965eb72c92a549dd5a330112', N'1', N'13699856947', N'1@qq.con', N'abc', N'1', N'6', N'234', N'0', NULL, N'2021-05-04 13:17:00', N'1', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO

INSERT INTO [dbo].[sys_user] ([Id], [NO], [Name], [Account], [Password], [Sex], [Mobile], [Email], [Remark], [IsActive], [LoginCount], [Memoni], [IsDeleted], [OrgId], [LastModificationTime], [ModifyUserId], [UserType], [InterDepartmental], [LastModifierUserId], [DeletionTime], [DeleterUserId], [CreationTime], [CreatorUserId], [TenantId]) VALUES (N'29', NULL, N'aaaaa', N'aaaaa', N'e10adc3949ba59abbe56e057f20f883e', N'0', N'13699854789', N'aaaa@wq.com', N'aaaaa', N'0', NULL, NULL, N'0', NULL, N'2021-05-04 13:17:32', NULL, NULL, NULL, N'0', NULL, N'0', N'2021-04-27 16:01:40', N'1', N'1')
GO

SET IDENTITY_INSERT [dbo].[sys_user] OFF
GO


-- ----------------------------
-- Table structure for sys_usersysorganization
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_usersysorganization]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_usersysorganization]
GO

CREATE TABLE [dbo].[sys_usersysorganization] (
  [Id] bigint  IDENTITY(1,1) NOT NULL,
  [SysUser_ID] int  NULL,
  [SysOrganization_ID] int  NULL
)
GO

ALTER TABLE [dbo].[sys_usersysorganization] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_usersysorganization',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_usersysorganization',
'COLUMN', N'SysUser_ID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'组织Id',
'SCHEMA', N'dbo',
'TABLE', N'sys_usersysorganization',
'COLUMN', N'SysOrganization_ID'
GO


-- ----------------------------
-- Records of sys_usersysorganization
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_usersysorganization] ON
GO

SET IDENTITY_INSERT [dbo].[sys_usersysorganization] OFF
GO


-- ----------------------------
-- Table structure for sys_usersysrole
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sys_usersysrole]') AND type IN ('U'))
	DROP TABLE [dbo].[sys_usersysrole]
GO

CREATE TABLE [dbo].[sys_usersysrole] (
  [SysRole_ID] bigint  NOT NULL,
  [SysUser_ID] bigint  NOT NULL,
  [SysRole_Type] bigint  NULL,
  [Id] bigint  IDENTITY(1,1) NOT NULL
)
GO

ALTER TABLE [dbo].[sys_usersysrole] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of sys_usersysrole
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sys_usersysrole] ON
GO

INSERT INTO [dbo].[sys_usersysrole] ([SysRole_ID], [SysUser_ID], [SysRole_Type], [Id]) VALUES (N'1', N'1', N'0', N'5')
GO

INSERT INTO [dbo].[sys_usersysrole] ([SysRole_ID], [SysUser_ID], [SysRole_Type], [Id]) VALUES (N'2', N'1', N'0', N'6')
GO

INSERT INTO [dbo].[sys_usersysrole] ([SysRole_ID], [SysUser_ID], [SysRole_Type], [Id]) VALUES (N'2', N'29', N'0', N'7')
GO

SET IDENTITY_INSERT [dbo].[sys_usersysrole] OFF
GO


-- ----------------------------
-- Auto increment value for sys_auditlog
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_auditlog]', RESEED, 293)
GO


-- ----------------------------
-- Primary Key structure for table sys_auditlog
-- ----------------------------
ALTER TABLE [dbo].[sys_auditlog] ADD CONSTRAINT [PK__sys_audi__3214EC07F30F178C] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_datadictionary
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_datadictionary]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table sys_datadictionary
-- ----------------------------
ALTER TABLE [dbo].[sys_datadictionary] ADD CONSTRAINT [PK__sys_data__3214EC078791FDCC] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_organization
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_organization]', RESEED, 6)
GO


-- ----------------------------
-- Primary Key structure for table sys_organization
-- ----------------------------
ALTER TABLE [dbo].[sys_organization] ADD CONSTRAINT [PK__sys_orga__3214EC07F9BC6B4F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_permission
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_permission]', RESEED, 143)
GO


-- ----------------------------
-- Indexes structure for table sys_permission
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [idx_ad_permission_01]
ON [dbo].[sys_permission] (
  [ParentId] ASC,
  [Label] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table sys_permission
-- ----------------------------
ALTER TABLE [dbo].[sys_permission] ADD CONSTRAINT [PK__sys_perm__3214EC0759B5DB5E] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_role
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_role]', RESEED, 2)
GO


-- ----------------------------
-- Indexes structure for table sys_role
-- ----------------------------
CREATE NONCLUSTERED INDEX [Id]
ON [dbo].[sys_role] (
  [Id] ASC
)
GO

CREATE NONCLUSTERED INDEX [Id_2]
ON [dbo].[sys_role] (
  [Id] ASC
)
GO

CREATE NONCLUSTERED INDEX [Id_3]
ON [dbo].[sys_role] (
  [Id] ASC
)
GO

CREATE NONCLUSTERED INDEX [Id_4]
ON [dbo].[sys_role] (
  [Id] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table sys_role
-- ----------------------------
ALTER TABLE [dbo].[sys_role] ADD CONSTRAINT [PK__sys_role__3214EC07F4FDFADC] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_rolepermission
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_rolepermission]', RESEED, 1210)
GO


-- ----------------------------
-- Indexes structure for table sys_rolepermission
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [idx_ad_role_permission_01]
ON [dbo].[sys_rolepermission] (
  [RoleId] ASC,
  [PermissionId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table sys_rolepermission
-- ----------------------------
ALTER TABLE [dbo].[sys_rolepermission] ADD CONSTRAINT [PK__sys_role__3214EC070AB59907] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_user
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_user]', RESEED, 29)
GO


-- ----------------------------
-- Indexes structure for table sys_user
-- ----------------------------
CREATE NONCLUSTERED INDEX [Id]
ON [dbo].[sys_user] (
  [Id] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table sys_user
-- ----------------------------
ALTER TABLE [dbo].[sys_user] ADD CONSTRAINT [PK__sys_user__3214EC07063281F1] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_usersysorganization
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_usersysorganization]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table sys_usersysorganization
-- ----------------------------
ALTER TABLE [dbo].[sys_usersysorganization] ADD CONSTRAINT [PK__sys_user__3214EC07FD0AABB0] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sys_usersysrole
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sys_usersysrole]', RESEED, 7)
GO


-- ----------------------------
-- Indexes structure for table sys_usersysrole
-- ----------------------------
CREATE NONCLUSTERED INDEX [FK_SYSUSERS_REFERENCE_SYSUSER]
ON [dbo].[sys_usersysrole] (
  [SysUser_ID] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table sys_usersysrole
-- ----------------------------
ALTER TABLE [dbo].[sys_usersysrole] ADD CONSTRAINT [PK__sys_user__3214EC0776DCEA4B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

