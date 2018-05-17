--table 0
IF OBJECT_ID('UserLog') IS NOT NULL
	DROP TABLE UserLog

--table 1
IF OBJECT_ID('ImageFileType') IS NOT NULL
	DROP TABLE ImageFileType

--table 2
IF OBJECT_ID('HeaderImage') IS NOT NULL
	DROP TABLE HeaderImage

--table 3
IF OBJECT_ID('Quote') IS NOT NULL
	DROP TABLE Quote

--table 4
IF OBJECT_ID('Menu') IS NOT NULL
	DROP TABLE Menu

--table 5
IF OBJECT_ID('StagingMenu') IS NOT NULL
	DROP TABLE StagingMenu

--table 6
IF OBJECT_ID('StagingMenuEditLog') IS NOT NULL
	DROP TABLE StagingMenuEditLog

--table 7
IF OBJECT_ID('ProgramDepartmentLink') IS NOT NULL
	DROP TABLE ProgramDepartmentLink

--table 8
IF OBJECT_ID('ProgramCollegeLink') IS NOT NULL
	DROP TABLE ProgramCollegeLink

--table 9
IF OBJECT_ID('FileContentLabelLink') IS NOT NULL
	DROP TABLE FileContentLabelLink

--table 10
IF OBJECT_ID('ModuleContentLabelLink') IS NOT NULL
	DROP TABLE ModuleContentLabelLink

--table 11
IF OBJECT_ID('PageTagLink') IS NOT NULL
	DROP TABLE PageTagLink

--table 12
IF OBJECT_ID('PageContentLabelLink') IS NOT NULL
	DROP TABLE PageContentLabelLink

--table 13
IF OBJECT_ID('PageModuleLink') IS NOT NULL
	DROP TABLE PageModuleLink

--table 15
IF OBJECT_ID('Tag') IS NOT NULL
	DROP TABLE Tag

--table 16
IF OBJECT_ID('ContentLabel') IS NOT NULL
	DROP TABLE ContentLabel

--table 17
IF OBJECT_ID('Module') IS NOT NULL
	DROP TABLE Module

--table 18
IF OBJECT_ID('NewsPage') IS NOT NULL
	DROP TABLE NewsPage

--table 19
IF OBJECT_ID('DStoryPage') IS NOT NULL
	DROP TABLE DStoryPage

--table 20
IF OBJECT_ID('ProgramPage') IS NOT NULL
	DROP TABLE ProgramPage

--table 21
IF OBJECT_ID('DepartmentPage') IS NOT NULL
	DROP TABLE DepartmentPage

--table 22
IF OBJECT_ID('CollegePage') IS NOT NULL
	DROP TABLE CollegePage

--table 24
IF OBJECT_ID('PagePageContextualLink') IS NOT NULL
	DROP TABLE PagePageContextualLink

--table 25
IF OBJECT_ID('PageFileContextualLink') IS NOT NULL
	DROP TABLE PageFileContextualLink

--table 26
IF OBJECT_ID('Page') IS NOT NULL
	DROP TABLE Page

--table 27
IF OBJECT_ID('PageCategory') IS NOT NULL
	DROP TABLE PageCategory

--table 28
IF OBJECT_ID('File') IS NOT NULL
	DROP TABLE [File]

--table 29
IF OBJECT_ID('FileType') IS NOT NULL
	DROP TABLE FileType

--table 30
IF OBJECT_ID('CmsUserRoleLink') IS NOT NULL
	DROP TABLE CmsUserRoleLink

--table 31
IF OBJECT_ID('RolePermissionLink') IS NOT NULL
	DROP TABLE RolePermissionLink

--table 32
IF OBJECT_ID('Role') IS NOT NULL
	DROP TABLE Role

--table 33
IF OBJECT_ID('CmsUser') IS NOT NULL
	DROP TABLE CmsUser

--table 34
IF OBJECT_ID('User') IS NOT NULL
	DROP TABLE [User]



--table 34
CREATE TABLE dbo.[User]
(
	id int IDENTITY,
	login varchar(50),
	password varbinary(16),	
	prefix varchar(25),
	firstname varchar(20),
	middle varchar(20),
	lastname varchar(30),
	suffix varchar(25),
	displayedname varchar(70),
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT PK_User PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_User_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 33
CREATE TABLE dbo.CmsUser
(
	id int IDENTITY,
	userid int,
	CONSTRAINT PK_CmsUser PRIMARY KEY CLUSTERED (id)
)

--table 32
CREATE TABLE dbo.Role
(
	id int IDENTITY,
	name varchar(50),
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT PK_Role PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_Role_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 31
CREATE TABLE dbo.RolePermissionLink
(
	roleid int,
	permissionid int,
	CONSTRAINT PK_RolePermissionLink PRIMARY KEY (roleid, permissionid),
	CONSTRAINT FK_RolePermissionLink_Role FOREIGN KEY (roleid) REFERENCES Role(id) ON DELETE CASCADE -- safe to cascade
)

--table 30
CREATE TABLE dbo.CmsUserRoleLink
(
	cmsuserid int,
	roleid int,
	CONSTRAINT PK_CmsUserRoleLink PRIMARY KEY (cmsuserid, roleid),
	CONSTRAINT FK_CmsUserRoleLink_CmsUser FOREIGN KEY (cmsuserid) REFERENCES CmsUser(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_CmsUserRoleLink_Role FOREIGN KEY (roleid) REFERENCES Role(id) ON DELETE CASCADE -- safe to cascade
)

--table 29
CREATE TABLE dbo.FileType
(
	extension varchar(5),
	description varchar(50),
	CONSTRAINT PK_FileType PRIMARY KEY CLUSTERED (extension)
)

--table 28
CREATE TABLE dbo.[File]
(
	id int IDENTITY,
	extension varchar(5),
	filesize int,
	imagewidth int,
	imageheight int,
	description varchar(100),
	admincomment varchar(500),
	created datetime,
	modified datetime,
	modifiedby int,	
	CONSTRAINT PK_File PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_File_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION,
	CONSTRAINT FK_File_FileType FOREIGN KEY (extension) REFERENCES FileType(extension) ON DELETE NO ACTION,
)

--table 27
CREATE TABLE dbo.PageCategory
(
	id int IDENTITY,
	text varchar(50),
	menuid int, -- no constraint since there are 2 menus: public and staging => can/should be tied to both / would intefere with application logic
	CONSTRAINT PK_PageCategory PRIMARY KEY CLUSTERED (id)
)

--table 26
CREATE TABLE dbo.Page
(
	id int IDENTITY,
	menuid int,
	pagecategoryid int,
	can_delete bit,
	can_editbody bit,
	can_editadmincomment bit,
	can_changemenu bit,
	redirectlink varchar(100),
	pagetitle varchar(250),
	contenttitle varchar(250),
	body text,
	bodydraft text,
	publisheddate datetime,
	display_content bit,
	display_publisheddate bit,
	display_bookmarking bit,
	display_printable bit,
	display_discussion bit,	
	ispublished bit,
	url varchar(50),
	admincomment varchar(500),
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT FK_Page_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION,
	CONSTRAINT FK_Page_PageCategory FOREIGN KEY (pagecategoryid) REFERENCES PageCategory(id) ON DELETE NO ACTION,
	CONSTRAINT PK_Page PRIMARY KEY CLUSTERED (id)
)

--table 25
CREATE TABLE dbo.PageFileContextualLink
(
	id int IDENTITY,
	pageid int,
	fileid int,
	CONSTRAINT PK_PageFileContextualLink PRIMARY KEY (pageid, fileid),
	CONSTRAINT FK_PageFileContextualLink_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_PageFileContextualLink_File FOREIGN KEY (fileid) REFERENCES [File](id) ON DELETE CASCADE -- safe to cascade
)

--table 24
CREATE TABLE dbo.PagePageContextualLink
(
	id int IDENTITY,
	page1id int,
	page2id int,
	CONSTRAINT PK_PagePageContextualLink PRIMARY KEY (page1id, page2id),
	CONSTRAINT FK_PagePageContextualLink_Page1 FOREIGN KEY (page1id) REFERENCES Page(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_PagePageContextualLink_Page2 FOREIGN KEY (page2id) REFERENCES Page(id) ON DELETE NO ACTION -- CANNOT CASCADE: CYCLES
)

--table 22
CREATE TABLE dbo.CollegePage
(
	id int IDENTITY,
	pageid int,
	CONSTRAINT FK_CollegePage_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE NO ACTION,
	CONSTRAINT PK_CollegePage PRIMARY KEY CLUSTERED (id)
)

--table 21
CREATE TABLE dbo.DepartmentPage
(
	id int IDENTITY,
	pageid int,
	collegepageid int,
	CONSTRAINT FK_DepartmentPage_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE NO ACTION,
	CONSTRAINT FK_DepartmentPage_CollegePage FOREIGN KEY (collegepageid) REFERENCES CollegePage(id) ON DELETE NO ACTION,
	CONSTRAINT PK_DepartmentPage PRIMARY KEY CLUSTERED (id)
)

--table 20
CREATE TABLE dbo.ProgramPage
(
	id int IDENTITY,
	pageid int,
	isfeatured bit,
	CONSTRAINT FK_ProgramPage_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE NO ACTION,
	CONSTRAINT PK_ProgramPage PRIMARY KEY CLUSTERED (id)
)

--table 19
CREATE TABLE dbo.DStoryPage
(
	id int IDENTITY,
	pageid int,
	hasimage bit,
	isfeatured bit,
	donordisplayedname varchar(100),
	summary varchar(500),
	rank int,
	CONSTRAINT FK_DStoryPage_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE NO ACTION,
	CONSTRAINT PK_DStoryPage PRIMARY KEY CLUSTERED (id)
)

--table 18
CREATE TABLE dbo.NewsPage
(
	id int IDENTITY,
	pageid int,
	hasimage bit,
	isonhomepage bit,
	ishighlighted bit,
	summary varchar(500),
	displayedpublished datetime,
	CONSTRAINT FK_News_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE NO ACTION,
	CONSTRAINT PK_News PRIMARY KEY CLUSTERED (id)
)

--table 17
CREATE TABLE dbo.Module
(
	id int IDENTITY,
	admintitle varchar(50),
	imageextension varchar(5),
	title varchar(100),
	body varchar(250),
	externallink varchar(100),
	pageid int,
	rank int,
	isrequired bit,
	isarchived bit,
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT FK_Module_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION,
	CONSTRAINT FK_Module_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE NO ACTION,
	CONSTRAINT PK_Module PRIMARY KEY CLUSTERED (id)
)

--table 16
CREATE TABLE dbo.ContentLabel
(
	id int IDENTITY,
	text varchar(50),
	rank int,
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT FK_ContentLabel_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION,
	CONSTRAINT PK_ContentLabel PRIMARY KEY CLUSTERED (id)
)

--table 15
CREATE TABLE dbo.Tag
(
	id int IDENTITY,
	text varchar(50),
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT FK_Tag_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION,
	CONSTRAINT PK_Tag PRIMARY KEY CLUSTERED (id)
)

--table 13
CREATE TABLE dbo.PageModuleLink
(
	pageid int,
	moduleid int,
	CONSTRAINT PK_PageModuleLink PRIMARY KEY (pageid, moduleid),
	CONSTRAINT FK_PageModuleLink_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_PageModuleLink_Module FOREIGN KEY (moduleid) REFERENCES Module(id) ON DELETE CASCADE -- safe to cascade
)

--table 12
CREATE TABLE dbo.PageContentLabelLink
(
	pageid int,
	contentlabelid int,
	CONSTRAINT PK_PageContentLabelLink PRIMARY KEY (pageid, contentlabelid),
	CONSTRAINT FK_PageContentLabelLink_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_PageContentLabelLink_ContentLabel FOREIGN KEY (contentlabelid) REFERENCES ContentLabel(id) ON DELETE CASCADE -- safe to cascade
)

--table 11
CREATE TABLE dbo.PageTagLink
(
	pageid int,
	tagid int,
	CONSTRAINT PK_PageTagLink PRIMARY KEY (pageid, tagid),
	CONSTRAINT FK_PageTagLink_Page FOREIGN KEY (pageid) REFERENCES Page(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_PageTagLink_Tag FOREIGN KEY (tagid) REFERENCES Tag(id) ON DELETE CASCADE -- safe to cascade
)

--table 10
CREATE TABLE dbo.ModuleContentLabelLink
(
	moduleid int,
	contentlabelid int,
	CONSTRAINT PK_ModuleContentLabelLink PRIMARY KEY (moduleid, contentlabelid),
	CONSTRAINT FK_ModuleContentLabelLink_Module FOREIGN KEY (moduleid) REFERENCES Module(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_ModuleContentLabelLink_ContentLabel FOREIGN KEY (contentlabelid) REFERENCES ContentLabel(id) ON DELETE CASCADE -- safe to cascade
)

--table 9
CREATE TABLE dbo.FileContentLabelLink
(
	fileid int,
	contentlabelid int,
	CONSTRAINT PK_FileContentLabelLink PRIMARY KEY (fileid, contentlabelid),
	CONSTRAINT FK_FileContentLabelLink_Module FOREIGN KEY (fileid) REFERENCES [File](id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_FileContentLabelLink_ContentLabel FOREIGN KEY (contentlabelid) REFERENCES ContentLabel(id) ON DELETE CASCADE -- safe to cascade
)

--table 8
CREATE TABLE dbo.ProgramCollegeLink
(
	programpageid int,
	collegepageid int,
	CONSTRAINT PK_ProgramCollegeLink PRIMARY KEY (programpageid, collegepageid),
	CONSTRAINT FK_ProgramCollegeLink_ProgramPage FOREIGN KEY (programpageid) REFERENCES ProgramPage(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_ProgramCollegeLink_CollegePage FOREIGN KEY (collegepageid) REFERENCES CollegePage(id) ON DELETE CASCADE -- safe to cascade
)

--table 7
CREATE TABLE dbo.ProgramDepartmentLink
(
	programpageid int,
	departmentpageid int,
	CONSTRAINT PK_ProgramDepartmentLink PRIMARY KEY (programpageid, departmentpageid),
	CONSTRAINT FK_ProgramDepartmentLink_ProgramPage FOREIGN KEY (programpageid) REFERENCES ProgramPage(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_ProgramDepartmentLink_DepartmentPage FOREIGN KEY (departmentpageid) REFERENCES DepartmentPage(id) ON DELETE CASCADE -- safe to cascade
)

--table 6
CREATE TABLE dbo.StagingMenuEditLog
(
	id int IDENTITY,
	description varchar(250),
	created datetime,
	createdby int,
	CONSTRAINT PK_StagingMenuEditLog PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_StagingMenuEditLog_User FOREIGN KEY (createdby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 5
CREATE TABLE dbo.StagingMenu
(
	id int IDENTITY,
	parentid int,
	text varchar(50),
	rank int,
	pageid int,	
	headerimageid int,
	can_edit bit,
	CONSTRAINT PK_StagingMenu PRIMARY KEY CLUSTERED (id)
)

--table 4
CREATE TABLE dbo.Menu
(
	id int IDENTITY,
	parentid int,
	text varchar(50),
	rank int,
	pageid int,	
	headerimageid int,
	CONSTRAINT PK_Menu PRIMARY KEY CLUSTERED (id)
)

--table 3
CREATE TABLE dbo.Quote
(
	id int IDENTITY,
	text varchar(100),
	author varchar(25),
	admincomment varchar(500),
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT PK_Quote PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_Quote_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 2
CREATE TABLE dbo.HeaderImage
(
	id int IDENTITY,
	locationcode int,
	description varchar(50),
	created datetime,
	createdby int,
	CONSTRAINT PK_HeaderImage PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_HeaderImage_User FOREIGN KEY (createdby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 1
CREATE TABLE dbo.ImageFileType
(
	extension varchar(5),
	CONSTRAINT PK_ImageFileType PRIMARY KEY CLUSTERED (extension)
)

--table 0
CREATE TABLE dbo.UserLog
(
	id int IDENTITY,
	userid int,
	signedin datetime,
	signedout datetime,
	CONSTRAINT PK_UserLog PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_UserLog_User FOREIGN KEY (userid) REFERENCES [User](id) ON DELETE NO ACTION
)