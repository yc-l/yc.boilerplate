/*
 Navicat Premium Data Transfer

 Source Server         : localhost-mysql5.7
 Source Server Type    : MySQL
 Source Server Version : 50726
 Source Host           : localhost:3306
 Source Schema         : bigdatadb

 Target Server Type    : MySQL
 Target Server Version : 50726
 File Encoding         : 65001

 Date: 09/02/2022 10:18:23
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sys_auditlog
-- ----------------------------
DROP TABLE IF EXISTS `sys_auditlog`;
CREATE TABLE `sys_auditlog`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `Key` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '拦截Key',
  `IP` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'IP',
  `Browser` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '浏览器',
  `Os` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作系统',
  `Device` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备',
  `BrowserInfo` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '浏览器信息',
  `ElapsedMilliseconds` bigint(20) NULL DEFAULT NULL COMMENT '耗时（毫秒）',
  `TenantId` int(11) NULL DEFAULT NULL COMMENT '租户Id',
  `UserId` bigint(20) NULL DEFAULT NULL COMMENT '用户id',
  `UserAccount` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户账号',
  `ParamsString` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '请求参数',
  `StartTime` datetime NULL DEFAULT NULL COMMENT '开始执行时间',
  `StopTime` datetime NULL DEFAULT NULL COMMENT '结束执行时间',
  `RequestMethod` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '请求方式',
  `RequestApi` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '请求Api',
  `CreationTime` datetime NULL DEFAULT NULL COMMENT ' 创建时间',
  `ResponseState` bit(1) NULL DEFAULT NULL,
  `ResponseData` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 27829 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_auditlog
-- ----------------------------

-- ----------------------------
-- Table structure for sys_datadictionary
-- ----------------------------
DROP TABLE IF EXISTS `sys_datadictionary`;
CREATE TABLE `sys_datadictionary`  (
  `Key` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名称',
  `ParentId` bigint(20) NULL DEFAULT NULL COMMENT '父节点',
  `Memoni` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '助记符',
  `Sort` int(11) NULL DEFAULT NULL COMMENT '排序',
  `Value` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `IsActive` bit(1) NULL DEFAULT NULL COMMENT '是否启用标识',
  `IsDeleted` bit(1) NULL DEFAULT NULL COMMENT ' 是否删除标识',
  `CreationTime` datetime NULL DEFAULT NULL COMMENT ' 创建时间',
  `CreatorUserId` bigint(20) NULL DEFAULT NULL COMMENT ' 创建ID',
  `TenantId` int(11) NULL DEFAULT NULL COMMENT '租户名',
  `Type` int(11) NULL DEFAULT NULL COMMENT '类别:0 分组， 1叶子节点',
  `Label` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_datadictionary
-- ----------------------------
INSERT INTO `sys_datadictionary` VALUES ('MenuType.Common', 2, NULL, NULL, '1', 1, NULL, NULL, NULL, NULL, NULL, NULL, '公共菜单');
INSERT INTO `sys_datadictionary` VALUES ('MenuType', 0, NULL, NULL, NULL, 2, NULL, NULL, NULL, NULL, NULL, 0, '菜单类别');

-- ----------------------------
-- Table structure for sys_organization
-- ----------------------------
DROP TABLE IF EXISTS `sys_organization`;
CREATE TABLE `sys_organization`  (
  `Label` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名称',
  `ParentId` bigint(20) NULL DEFAULT NULL COMMENT '所属上级',
  `OrganType` int(11) NULL DEFAULT NULL COMMENT '节点类型',
  `Sort` int(11) NULL DEFAULT NULL COMMENT '序号',
  `PostId` int(11) NULL DEFAULT NULL COMMENT '岗位编号',
  `Fax` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '传真',
  `Telephone` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系电话',
  `Address` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '通讯地址',
  `Memoni` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '助记符',
  `Remark` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `RangeType` int(11) NULL DEFAULT NULL COMMENT '权限',
  `Range` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限范围',
  `Linkman` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系人',
  `CreatorUserId` bigint(20) NULL DEFAULT NULL COMMENT ' 创建ID',
  `CreationTime` datetime NULL DEFAULT NULL COMMENT ' 创建时间',
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `IsActive` bit(1) NULL DEFAULT NULL COMMENT '是否启用标识',
  `IsDeleted` bit(1) NULL DEFAULT NULL COMMENT ' 是否删除标识',
  `TenantId` int(11) NULL DEFAULT NULL COMMENT '租户名',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_organization
-- ----------------------------
INSERT INTO `sys_organization` VALUES ('xxx事业部', 0, 0, 2, 0, '', '', '', '', '', 0, '', '', 0, NULL, 22, b'0', b'0', NULL);
INSERT INTO `sys_organization` VALUES ('研发部', 22, 0, 0, 0, '', '', '', '', '', 0, '', '', 0, NULL, 23, b'0', b'0', NULL);
INSERT INTO `sys_organization` VALUES ('企划部', 22, 0, 0, 0, '', '', '', '', '', 0, '', '', 0, NULL, 24, b'0', b'0', NULL);
INSERT INTO `sys_organization` VALUES ('开发一组', 23, 0, 0, 0, '', '', '', '', '', 0, '', '', 0, NULL, 25, b'0', b'0', NULL);

-- ----------------------------
-- Table structure for sys_permission
-- ----------------------------
DROP TABLE IF EXISTS `sys_permission`;
CREATE TABLE `sys_permission`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `ParentId` bigint(20) NOT NULL COMMENT '父级节点',
  `Label` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限名称',
  `Code` varchar(550) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限编码',
  `Type` int(11) NOT NULL COMMENT '权限类型',
  `View` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '视图',
  `Api` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '接口',
  `Path` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单访问地址',
  `Icon` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标',
  `Hidden` bit(1) NOT NULL COMMENT '隐藏',
  `IsActive` bit(1) NULL DEFAULT b'1' COMMENT '启用',
  `Closable` bit(1) NULL DEFAULT NULL COMMENT '可关闭',
  `Opened` bit(1) NULL DEFAULT NULL COMMENT '打开组',
  `NewWindow` bit(1) NULL DEFAULT NULL COMMENT '打开新窗口',
  `External` bit(1) NULL DEFAULT NULL COMMENT '链接外显',
  `Sort` int(11) NULL DEFAULT 999 COMMENT '排序',
  `Description` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `TenantId` bigint(20) NULL DEFAULT NULL COMMENT '租户Id',
  `IsDeleted` bit(1) NULL DEFAULT NULL COMMENT '是否删除',
  `CreatorUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `CreationTime` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `CreatedTime` datetime(3) NULL DEFAULT NULL COMMENT '创建时间',
  `LastModifierUserId` int(11) NULL DEFAULT NULL COMMENT '修改者Id',
  `LastModificationTime` datetime(3) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `idx_ad_permission_01`(`ParentId`, `Label`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 158 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '权限' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_permission
-- ----------------------------
INSERT INTO `sys_permission` VALUES (111, 0, '系统管理', 'systemManager', 1, '', '', '', 'el-icon-setting', b'1', b'1', b'1', b'1', b'1', b'1', 8, '', 1, b'0', 1, '2021-05-02 18:35:03.773', NULL, 0, '2021-05-03 11:14:36.306');
INSERT INTO `sys_permission` VALUES (112, 0, '用户管理', 'userManager', 2, 'Users', '', '/users', 'el-icon-user', b'1', b'1', b'1', b'1', b'1', b'1', 3, '', 1, b'0', 1, '2021-05-02 18:37:37.656', NULL, 0, '2021-05-03 10:34:10.192');
INSERT INTO `sys_permission` VALUES (113, 111, '角色管理', 'system.roleManager', 2, 'SysRoles', '', '/sysRoles', 'el-icon-share', b'1', b'1', b'1', b'1', b'1', b'1', 4, '', 1, b'0', 1, '2021-05-02 18:39:33.200', NULL, 0, '2021-05-05 17:08:01.619');
INSERT INTO `sys_permission` VALUES (114, 111, '权限管理', 'permissionManager', 2, 'SysPermissions', '', '/sysPermissions', 'el-icon-s-operation', b'1', b'1', b'1', b'1', b'1', b'1', 5, '', 1, b'0', 1, '2021-05-02 18:40:20.539', NULL, 0, '2021-05-05 17:08:15.478');
INSERT INTO `sys_permission` VALUES (115, 0, '数据字典', 'dataDictionaryManager', 2, 'SysDataDictionarys', '', '/sysDataDictionarys', 'el-icon-share', b'1', b'1', b'1', b'1', b'1', b'1', 6, '', 1, b'0', 1, '2021-05-02 18:41:16.319', NULL, 0, '2021-05-03 11:11:32.678');
INSERT INTO `sys_permission` VALUES (116, 0, '日志管理', 'logManager', 1, '', '', '', 'el-icon-document', b'1', b'1', b'1', b'1', b'1', b'1', 8, '', 1, b'0', 1, '2021-05-02 18:43:29.923', NULL, 0, '2021-05-03 11:14:48.619');
INSERT INTO `sys_permission` VALUES (117, 116, '审计日志', 'sysAuditLogManager', 2, 'SysAuditLogs', '', '/sysAuditLogs', '', b'1', b'1', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-02 18:44:24.935', NULL, 0, '2021-05-05 13:47:54.149');
INSERT INTO `sys_permission` VALUES (118, 112, '新增用户', 'userManager.createUser', 3, '', 'SysUser/CreateUser', '', '', b'1', b'1', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-02 18:46:06.639', NULL, 0, '2021-05-03 10:34:57.215');
INSERT INTO `sys_permission` VALUES (119, 112, '查看用户列表', 'userManager.viewUser', 3, '', 'SysUser/GetPageUserList', '', '', b'1', b'1', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-02 18:50:42.411', NULL, 0, '2021-05-03 10:32:25.995');
INSERT INTO `sys_permission` VALUES (120, 112, '编辑用户', 'userManager.editUser', 3, '', 'SysUser/UpdateUser', '', '', b'1', b'1', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:27:32.738', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (121, 112, '删除用户', 'userManager.deleteUser', 3, '', 'SysUser/DeleteUserById', '', '', b'1', b'1', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:35:40.971', NULL, 0, '2021-05-03 10:35:51.595');
INSERT INTO `sys_permission` VALUES (122, 114, '新增权限', 'permissionManager.createPermission', 3, '', 'SysPermission/CreateSysPermission', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:46:41.669', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (123, 114, '编辑权限', 'permissionManager.editPermission', 3, '', 'SysPermission/UpdateSysPermission', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:47:11.061', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (124, 114, '删除权限', 'permissionManager.deletePermission', 3, '', 'SysPermission/DeleteSysPermissionById', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:47:39.828', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (125, 114, '查看权限列表', 'permissionManager.viewPermission', 3, '', 'SysPermission/GetSysPermissionList,SysPermission/Get,SysPermission/GetSysPermissionFilterByPid', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:49:20.263', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (126, 113, '查看角色列表', 'roleManager.viewRole', 3, '', 'SysRole/GetPageSysRoleList', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:52:19.301', NULL, 0, '2021-05-03 10:55:28.470');
INSERT INTO `sys_permission` VALUES (127, 113, '编辑角色', 'roleManager.editRole', 3, '', 'SysRole/UpdateSysRole', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:53:49.424', NULL, 0, '2021-05-03 10:54:53.629');
INSERT INTO `sys_permission` VALUES (128, 113, '新增角色', 'roleManager.createRole', 3, '', 'SysRole/CreateSysRole', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:54:40.920', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (129, 113, '删除角色', 'roleManager.deleteRole', 3, '', 'SysRole/DeleteSysRoleById', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 10:56:37.772', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (130, 111, '角色权限', 'rolePermissionManager', 2, 'SysRolePermissions', '', '/sysRolePemissions', 'el-icon-share', b'1', b'0', b'1', b'1', b'1', b'1', 7, '', 1, b'0', 1, '2021-05-03 10:59:13.100', NULL, 0, '2021-05-05 17:08:30.991');
INSERT INTO `sys_permission` VALUES (131, 130, '配置角色权限', 'rolePermissionManager.editRolePermission', 3, '', 'SysRole/UpdateSysRolePermissions', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 11:00:38.300', NULL, 0, '2021-05-03 11:08:25.225');
INSERT INTO `sys_permission` VALUES (132, 115, '新增数据字典', 'dataDictionaryManager.createDataDictionary', 3, '', 'SysDataDictionary/CreateSysDataDictionary', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 11:07:23.817', NULL, 0, '2021-05-03 11:08:43.803');
INSERT INTO `sys_permission` VALUES (133, 115, '编辑数据字典', 'dataDictionaryManager.editDataDictionary', 3, '', 'SysDataDictionary/UpdateSysDataDictionary', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 11:09:27.438', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (134, 115, '删除数据字典', 'dataDictionaryManager.deleteDataDictionary', 3, '', 'SysDataDictionary/DeleteSysDataDictionaryById', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 11:10:11.542', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (135, 115, '查看数据字典列表', 'dataDictionaryManager.viewDataDictionary', 3, '', 'SysDataDictionary/GetSysDataDictionaryList,SysDataDictionary/GetSysDataDictionaryFilterByPid', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 11:10:46.715', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (136, 0, '首页', 'welcomePage', 2, 'Welcome', '', '/welcome', 'el-icon-s-home', b'1', b'0', b'1', b'1', b'1', b'1', 1, '', 1, b'0', 1, '2021-05-03 11:16:56.241', NULL, 0, '2021-05-04 15:01:23.987');
INSERT INTO `sys_permission` VALUES (137, 130, '查看角色权限列表', 'rolePermissionManager.viewRolePermission', 3, '', 'SysRole/GetSysRoleList,SysPermission/GetSysPermissionList', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-03 11:19:55.125', NULL, 0, '2021-05-03 11:23:44.581');
INSERT INTO `sys_permission` VALUES (138, 117, '查看审计日志列表', 'sysAuditLogManager.viewSysAuditLog', 3, '', 'SysAuditLog/GetPageSysAuditLogList,SysAuditLog/Get', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-05 12:59:33.332', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (139, 0, '机构管理', 'sysOrganizationManager', 2, 'SysOrganizations', '', '/sysOrganizations', 'el-icon-s-custom', b'1', b'0', b'1', b'1', b'1', b'1', 999, '', 1, b'0', 1, '2021-05-05 17:12:30.131', NULL, 0, '2021-05-05 17:24:55.009');
INSERT INTO `sys_permission` VALUES (140, 139, '查看组织机构列表', 'sysOrganizationManager.viewSysOrganization', 3, '', 'SysOrganization/GetSysOrganizationList,SysOrganization/Get,SysOrganization/GetSysOrganizationFilterByPid', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-05 17:13:51.264', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (141, 139, '新增组织机构', 'sysOrganizationManager.createSysOrganization', 3, '', 'SysOrganization/CreateSysOrganization', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-05 17:14:29.066', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (142, 139, '编辑组织机构', 'sysOrganizationManager.editSysOrganization', 3, '', 'SysOrganization/UpdateSysOrganization', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-05 17:22:03.526', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (143, 139, '删除组织机构', 'sysOrganizationManager.deleteSysOrganization', 3, '', 'SysOrganization/DeleteSysOrganizationById', '', '', b'1', b'0', b'1', b'1', b'1', b'1', 0, '', 1, b'0', 1, '2021-05-05 17:22:51.792', NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (145, 0, '看板', 'Board', 2, 'board', '', '/board', 'el-icon-s-marketing', b'0', b'0', b'1', b'1', b'1', b'1', 2, '', NULL, b'0', 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (151, 0, '大数据组件', 'BigData', 1, '', '', '', 'el-icon-box', b'0', b'0', b'1', b'1', b'1', b'1', 15, '', NULL, b'0', 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (152, 151, 'ES 大数据检索示例', 'bookManager', 2, 'Books', '', '/books', 'el-icon-search', b'0', b'0', b'1', b'1', b'1', b'1', 0, '', NULL, b'0', 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (153, 152, '查看列表', 'bookManager.viewBook', 3, '', 'Book/GetPageBookList', '', '', b'0', b'0', b'1', b'1', b'1', b'1', 0, '', NULL, b'0', 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (154, 0, '个人中心', 'personalPage', 2, 'Personal', '', '/personal', 'el-icon-s-home', b'1', NULL, b'1', b'1', b'1', b'1', 3, '', NULL, NULL, 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (155, 154, '个人中心操作', 'personalPage', 3, '', 'SysUser/GetUserInfo,SysUser/ChangePassword,Identity/UploadImage,SysUser/UploadUserAvatar', '', '', b'0', NULL, b'1', b'1', b'1', b'1', 0, '', NULL, NULL, 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (156, 0, '自动化引擎', 'automationEngineMananger', 1, '', '', '', 'fa fa-cogs', b'0', NULL, b'1', b'1', b'1', b'1', 0, '', NULL, NULL, 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');
INSERT INTO `sys_permission` VALUES (157, 156, '图形设计器', 'graphicManager', 2, '', '', '/graphic', 'fa fa-codepen', b'0', NULL, b'1', b'1', b'1', b'1', 0, '', NULL, NULL, 0, NULL, NULL, 0, '0001-01-01 00:00:00.000');

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `Name` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `IsActive` tinyint(4) NULL DEFAULT NULL COMMENT '启用',
  `Memoni` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '助记符',
  `Sort` int(11) NULL DEFAULT NULL COMMENT '排序',
  `IsDeleted` tinyint(4) NULL DEFAULT NULL COMMENT '删除标记',
  `LastModificationTime` datetime NULL DEFAULT NULL COMMENT '修改时间',
  `LastModifierUserId` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `OrgId` int(11) NULL DEFAULT NULL COMMENT '组织',
  `TenantId` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `Id`(`Id`) USING BTREE,
  INDEX `Id_2`(`Id`) USING BTREE,
  INDEX `Id_3`(`Id`) USING BTREE,
  INDEX `Id_4`(`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户角色' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES (1, '超级管理员', 0, 'superAdmin', NULL, 0, '2021-05-04 12:53:12', 0, NULL, 1);
INSERT INTO `sys_role` VALUES (2, '运维管理员', 0, '222', 2, 0, '2021-05-04 12:53:03', 0, NULL, 1);

-- ----------------------------
-- Table structure for sys_rolepermission
-- ----------------------------
DROP TABLE IF EXISTS `sys_rolepermission`;
CREATE TABLE `sys_rolepermission`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `RoleId` bigint(20) NOT NULL COMMENT '角色Id',
  `PermissionId` bigint(20) NOT NULL COMMENT '权限Id',
  `CreatorUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `CreationTime` datetime(3) NULL DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `idx_ad_role_permission_01`(`RoleId`, `PermissionId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1535 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '角色权限' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_rolepermission
-- ----------------------------
INSERT INTO `sys_rolepermission` VALUES (1433, 2, 111, 1, '2022-01-18 18:30:58.616');
INSERT INTO `sys_rolepermission` VALUES (1434, 2, 113, 1, '2022-01-18 18:30:58.616');
INSERT INTO `sys_rolepermission` VALUES (1435, 2, 126, 1, '2022-01-18 18:30:58.617');
INSERT INTO `sys_rolepermission` VALUES (1436, 2, 114, 1, '2022-01-18 18:30:58.617');
INSERT INTO `sys_rolepermission` VALUES (1437, 2, 125, 1, '2022-01-18 18:30:58.617');
INSERT INTO `sys_rolepermission` VALUES (1438, 2, 130, 1, '2022-01-18 18:30:58.618');
INSERT INTO `sys_rolepermission` VALUES (1439, 2, 137, 1, '2022-01-18 18:30:58.618');
INSERT INTO `sys_rolepermission` VALUES (1440, 2, 116, 1, '2022-01-18 18:30:58.619');
INSERT INTO `sys_rolepermission` VALUES (1441, 2, 117, 1, '2022-01-18 18:30:58.619');
INSERT INTO `sys_rolepermission` VALUES (1442, 2, 138, 1, '2022-01-18 18:30:58.619');
INSERT INTO `sys_rolepermission` VALUES (1443, 2, 112, 1, '2022-01-18 18:30:58.620');
INSERT INTO `sys_rolepermission` VALUES (1444, 2, 119, 1, '2022-01-18 18:30:58.620');
INSERT INTO `sys_rolepermission` VALUES (1445, 2, 115, 1, '2022-01-18 18:30:58.621');
INSERT INTO `sys_rolepermission` VALUES (1446, 2, 135, 1, '2022-01-18 18:30:58.621');
INSERT INTO `sys_rolepermission` VALUES (1447, 2, 136, 1, '2022-01-18 18:30:58.621');
INSERT INTO `sys_rolepermission` VALUES (1448, 2, 139, 1, '2022-01-18 18:30:58.622');
INSERT INTO `sys_rolepermission` VALUES (1449, 2, 140, 1, '2022-01-18 18:30:58.622');
INSERT INTO `sys_rolepermission` VALUES (1450, 2, 141, 1, '2022-01-18 18:30:58.623');
INSERT INTO `sys_rolepermission` VALUES (1451, 2, 142, 1, '2022-01-18 18:30:58.623');
INSERT INTO `sys_rolepermission` VALUES (1452, 2, 143, 1, '2022-01-18 18:30:58.623');
INSERT INTO `sys_rolepermission` VALUES (1453, 2, 154, 1, '2022-01-18 18:30:58.624');
INSERT INTO `sys_rolepermission` VALUES (1454, 2, 155, 1, '2022-01-18 18:30:58.624');
INSERT INTO `sys_rolepermission` VALUES (1496, 1, 111, 1, '2022-02-09 10:16:42.057');
INSERT INTO `sys_rolepermission` VALUES (1497, 1, 113, 1, '2022-02-09 10:16:42.058');
INSERT INTO `sys_rolepermission` VALUES (1498, 1, 126, 1, '2022-02-09 10:16:42.058');
INSERT INTO `sys_rolepermission` VALUES (1499, 1, 127, 1, '2022-02-09 10:16:42.059');
INSERT INTO `sys_rolepermission` VALUES (1500, 1, 128, 1, '2022-02-09 10:16:42.059');
INSERT INTO `sys_rolepermission` VALUES (1501, 1, 129, 1, '2022-02-09 10:16:42.060');
INSERT INTO `sys_rolepermission` VALUES (1502, 1, 114, 1, '2022-02-09 10:16:42.060');
INSERT INTO `sys_rolepermission` VALUES (1503, 1, 122, 1, '2022-02-09 10:16:42.060');
INSERT INTO `sys_rolepermission` VALUES (1504, 1, 123, 1, '2022-02-09 10:16:42.061');
INSERT INTO `sys_rolepermission` VALUES (1505, 1, 124, 1, '2022-02-09 10:16:42.061');
INSERT INTO `sys_rolepermission` VALUES (1506, 1, 125, 1, '2022-02-09 10:16:42.061');
INSERT INTO `sys_rolepermission` VALUES (1507, 1, 130, 1, '2022-02-09 10:16:42.062');
INSERT INTO `sys_rolepermission` VALUES (1508, 1, 131, 1, '2022-02-09 10:16:42.062');
INSERT INTO `sys_rolepermission` VALUES (1509, 1, 137, 1, '2022-02-09 10:16:42.063');
INSERT INTO `sys_rolepermission` VALUES (1510, 1, 116, 1, '2022-02-09 10:16:42.063');
INSERT INTO `sys_rolepermission` VALUES (1511, 1, 117, 1, '2022-02-09 10:16:42.064');
INSERT INTO `sys_rolepermission` VALUES (1512, 1, 138, 1, '2022-02-09 10:16:42.064');
INSERT INTO `sys_rolepermission` VALUES (1513, 1, 151, 1, '2022-02-09 10:16:42.064');
INSERT INTO `sys_rolepermission` VALUES (1514, 1, 152, 1, '2022-02-09 10:16:42.065');
INSERT INTO `sys_rolepermission` VALUES (1515, 1, 153, 1, '2022-02-09 10:16:42.065');
INSERT INTO `sys_rolepermission` VALUES (1516, 1, 112, 1, '2022-02-09 10:16:42.065');
INSERT INTO `sys_rolepermission` VALUES (1517, 1, 118, 1, '2022-02-09 10:16:42.066');
INSERT INTO `sys_rolepermission` VALUES (1518, 1, 119, 1, '2022-02-09 10:16:42.066');
INSERT INTO `sys_rolepermission` VALUES (1519, 1, 120, 1, '2022-02-09 10:16:42.066');
INSERT INTO `sys_rolepermission` VALUES (1520, 1, 121, 1, '2022-02-09 10:16:42.067');
INSERT INTO `sys_rolepermission` VALUES (1521, 1, 115, 1, '2022-02-09 10:16:42.067');
INSERT INTO `sys_rolepermission` VALUES (1522, 1, 132, 1, '2022-02-09 10:16:42.067');
INSERT INTO `sys_rolepermission` VALUES (1523, 1, 133, 1, '2022-02-09 10:16:42.068');
INSERT INTO `sys_rolepermission` VALUES (1524, 1, 134, 1, '2022-02-09 10:16:42.068');
INSERT INTO `sys_rolepermission` VALUES (1525, 1, 135, 1, '2022-02-09 10:16:42.068');
INSERT INTO `sys_rolepermission` VALUES (1526, 1, 136, 1, '2022-02-09 10:16:42.069');
INSERT INTO `sys_rolepermission` VALUES (1527, 1, 139, 1, '2022-02-09 10:16:42.069');
INSERT INTO `sys_rolepermission` VALUES (1528, 1, 140, 1, '2022-02-09 10:16:42.069');
INSERT INTO `sys_rolepermission` VALUES (1529, 1, 141, 1, '2022-02-09 10:16:42.070');
INSERT INTO `sys_rolepermission` VALUES (1530, 1, 142, 1, '2022-02-09 10:16:42.070');
INSERT INTO `sys_rolepermission` VALUES (1531, 1, 143, 1, '2022-02-09 10:16:42.070');
INSERT INTO `sys_rolepermission` VALUES (1532, 1, 145, 1, '2022-02-09 10:16:42.071');
INSERT INTO `sys_rolepermission` VALUES (1533, 1, 154, 1, '2022-02-09 10:16:42.071');
INSERT INTO `sys_rolepermission` VALUES (1534, 1, 155, 1, '2022-02-09 10:16:42.071');

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `NO` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '员工编号',
  `Name` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工名称',
  `Account` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '登录账号',
  `Password` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '密码',
  `Sex` tinyint(4) NULL DEFAULT NULL COMMENT '性别',
  `Mobile` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '手机号码',
  `Email` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '信箱',
  `Remark` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `IsActive` tinyint(4) NULL DEFAULT NULL COMMENT '启用',
  `LoginCount` int(11) NULL DEFAULT NULL COMMENT '登录次数',
  `Memoni` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '助记符',
  `IsDeleted` tinyint(4) NULL DEFAULT NULL COMMENT '删除标记',
  `OrgId` int(11) NULL DEFAULT NULL COMMENT '组织',
  `LastModificationTime` datetime NULL DEFAULT NULL COMMENT '修改时间',
  `ModifyUserId` int(11) NULL DEFAULT NULL COMMENT '修改人',
  `UserType` tinyint(4) NULL DEFAULT NULL,
  `InterDepartmental` tinyint(4) NULL DEFAULT NULL COMMENT '跨部门数据查看',
  `LastModifierUserId` bigint(20) NULL DEFAULT NULL,
  `DeletionTime` datetime NULL DEFAULT NULL,
  `DeleterUserId` int(11) NULL DEFAULT NULL,
  `CreationTime` datetime NULL DEFAULT NULL,
  `CreatorUserId` bigint(20) NULL DEFAULT NULL,
  `TenantId` int(11) NULL DEFAULT NULL,
  `Avatar` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '头像',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `Id`(`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES (1, 'admin', '超级管理员', 'admin', 'e10adc3949ba59abbe56e057f20f883e', 0, '13699856947', '1@qq.con', 'abc', 1, 6, '234', 0, NULL, '2021-05-04 13:17:00', 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, 1, '/Uploads/2022/202201/20220124/220124054024408477.jpg');
INSERT INTO `sys_user` VALUES (2, NULL, 'test', 'test', '25f9e794323b453885f5181f1b624d0b', 0, '13688745899', 'test@qq.com', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_usersysorganization
-- ----------------------------
DROP TABLE IF EXISTS `sys_usersysorganization`;
CREATE TABLE `sys_usersysorganization`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `SysUser_ID` int(11) NULL DEFAULT NULL COMMENT '用户Id',
  `SysOrganization_ID` int(11) NULL DEFAULT NULL COMMENT '组织Id',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_usersysorganization
-- ----------------------------

-- ----------------------------
-- Table structure for sys_usersysrole
-- ----------------------------
DROP TABLE IF EXISTS `sys_usersysrole`;
CREATE TABLE `sys_usersysrole`  (
  `SysRole_ID` bigint(20) NOT NULL,
  `SysUser_ID` bigint(20) NOT NULL,
  `SysRole_Type` bigint(20) NULL DEFAULT NULL,
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `FK_SYSUSERS_REFERENCE_SYSUSER`(`SysUser_ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_usersysrole
-- ----------------------------
INSERT INTO `sys_usersysrole` VALUES (2, 2, 0, 7);
INSERT INTO `sys_usersysrole` VALUES (1, 1, 0, 8);
INSERT INTO `sys_usersysrole` VALUES (2, 1, 0, 9);

SET FOREIGN_KEY_CHECKS = 1;
