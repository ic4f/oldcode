--table 18
IF OBJECT_ID('PhotoView') IS NOT NULL
	DROP TABLE PhotoView

--table 17
IF OBJECT_ID('LineupView') IS NOT NULL
	DROP TABLE LineupView

--table 16
IF OBJECT_ID('LineupPhotoLink') IS NOT NULL
	DROP TABLE LineupPhotoLink

--table 15
IF OBJECT_ID('Lineup') IS NOT NULL
	DROP TABLE Lineup

--table 14
IF OBJECT_ID('CCaseSuspectLink') IS NOT NULL
	DROP TABLE CCaseSuspectLink

--table 13
IF OBJECT_ID('CCaseUserLink') IS NOT NULL
	DROP TABLE CCaseUserLink

--table 12
IF OBJECT_ID('Suspect') IS NOT NULL
	DROP TABLE Suspect

--table 11
IF OBJECT_ID('Photo') IS NOT NULL
	DROP TABLE Photo

--table 10
IF OBJECT_ID('CCase') IS NOT NULL
	DROP TABLE CCase

--table 9
IF OBJECT_ID('WeightRange') IS NOT NULL
	DROP TABLE WeightRange

--table 8
IF OBJECT_ID('AgeRange') IS NOT NULL
	DROP TABLE AgeRange

--table 7
IF OBJECT_ID('HairColor') IS NOT NULL
	DROP TABLE HairColor

--table 6
IF OBJECT_ID('Race') IS NOT NULL
	DROP TABLE Race

--table 5
IF OBJECT_ID('CmsUserRoleLink') IS NOT NULL
	DROP TABLE CmsUserRoleLink

--table 4
IF OBJECT_ID('RolePermissionLink') IS NOT NULL
	DROP TABLE RolePermissionLink

--table 3
IF OBJECT_ID('Role') IS NOT NULL
	DROP TABLE Role

--table 2
IF OBJECT_ID('CmsUser') IS NOT NULL
	DROP TABLE CmsUser

--table 1
IF OBJECT_ID('User') IS NOT NULL
	DROP TABLE [User]


--table 1
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

--table 2
CREATE TABLE dbo.CmsUser
(
	id int IDENTITY,
	userid int,
	CONSTRAINT PK_CmsUser PRIMARY KEY CLUSTERED (id)
)

--table 3
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

--table 4
CREATE TABLE dbo.RolePermissionLink
(
	roleid int,
	permissionid int,
	CONSTRAINT PK_RolePermissionLink PRIMARY KEY (roleid, permissionid),
	CONSTRAINT FK_RolePermissionLink_Role FOREIGN KEY (roleid) REFERENCES Role(id) ON DELETE CASCADE -- safe to cascade
)

--table 5
CREATE TABLE dbo.CmsUserRoleLink
(
	cmsuserid int,
	roleid int,
	CONSTRAINT PK_CmsUserRoleLink PRIMARY KEY (cmsuserid, roleid),
	CONSTRAINT FK_CmsUserRoleLink_CmsUser FOREIGN KEY (cmsuserid) REFERENCES CmsUser(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_CmsUserRoleLink_Role FOREIGN KEY (roleid) REFERENCES Role(id) ON DELETE CASCADE -- safe to cascade
)

--table 6
CREATE TABLE dbo.Race
(
	id int IDENTITY,
	description varchar(35),
	CONSTRAINT PK_Race PRIMARY KEY CLUSTERED (id)
)

--table 7
CREATE TABLE dbo.HairColor
(
	id int IDENTITY,
	description varchar(35),
	CONSTRAINT PK_HairColor PRIMARY KEY CLUSTERED (id)
)

--table 8
CREATE TABLE dbo.AgeRange
(
	id int IDENTITY,
	description varchar(35),
	CONSTRAINT PK_AgeRange PRIMARY KEY CLUSTERED (id)
)

--table 9
CREATE TABLE dbo.WeightRange
(
	id int IDENTITY,
	description varchar(35),
	CONSTRAINT PK_WeightRange PRIMARY KEY CLUSTERED (id)
)

--table 10
CREATE TABLE dbo.Photo
(
	id int IDENTITY,
	externalref varchar(15),
	gender char(1),
	raceid int,
	haircolorid int,
	agerangeid int,
	weightrangeid int, 
	iscategorized bit,
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT PK_Photo PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_Photo_Race FOREIGN KEY (raceid) REFERENCES Race(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Photo_HairColor FOREIGN KEY (haircolorid) REFERENCES HairColor(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Photo_AgeRange FOREIGN KEY (agerangeid) REFERENCES AgeRange(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Photo_WeightRange FOREIGN KEY (weightrangeid) REFERENCES WeightRange(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Photo_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 11
CREATE TABLE dbo.CCase
(
	id int IDENTITY,
	externalref varchar(15),
	notes varchar(1000),
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT PK_CCase PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_CCase_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 12
CREATE TABLE dbo.Suspect
(
	id int IDENTITY,
	externalref varchar(15),
	notes varchar(1000),
	gender char(1),
	raceid int,
	haircolorid int,
	agerangeid int,
	weightrangeid int, 
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT PK_Suspect PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_Suspect_Race FOREIGN KEY (raceid) REFERENCES Race(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Suspect_HairColor FOREIGN KEY (haircolorid) REFERENCES HairColor(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Suspect_AgeRange FOREIGN KEY (agerangeid) REFERENCES AgeRange(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Suspect_WeightRange FOREIGN KEY (weightrangeid) REFERENCES WeightRange(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Suspect_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 13
CREATE TABLE dbo.CCaseUserLink
(
	ccaseid int,
	userid int,
	CONSTRAINT PK_CCaseUserLink PRIMARY KEY (ccaseid, userid),
	CONSTRAINT FK_CCaseUserLink_CCase FOREIGN KEY (ccaseid) REFERENCES CCase(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_CCaseUserLink_User FOREIGN KEY (userid) REFERENCES [User](id) ON DELETE CASCADE -- safe to cascade
)

--table 14
CREATE TABLE dbo.CCaseSuspectLink
(
	ccaseid int,
	suspectid int,
	CONSTRAINT PK_CCaseSuspectLink PRIMARY KEY (ccaseid, suspectid),
	CONSTRAINT FK_CCaseSuspectLink_CCase FOREIGN KEY (ccaseid) REFERENCES CCase(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_CCaseSuspectLink_Suspect FOREIGN KEY (suspectid) REFERENCES Suspect(id) ON DELETE CASCADE -- safe to cascade
)

--table 15
CREATE TABLE dbo.Lineup
(
	id int IDENTITY,
	description varchar(100),
	notes varchar(1000),
	ccaseid int,
	suspectid int,
	suspectphotoposition int,
	islocked bit,	
	created datetime,
	modified datetime,
	modifiedby int,
	CONSTRAINT PK_Lineup PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_Lineup_Case FOREIGN KEY (ccaseid) REFERENCES CCase(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Lineup_Suspect FOREIGN KEY (suspectid) REFERENCES Suspect(id) ON DELETE NO ACTION,
	CONSTRAINT FK_Lineup_User FOREIGN KEY (modifiedby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 16
CREATE TABLE dbo.LineupPhotoLink
(
	id int IDENTITY,
	lineupid int,
	photoid int,
	CONSTRAINT PK_LineupPhotoLink PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_LineupPhotoLink_Lineup FOREIGN KEY (lineupid) REFERENCES Lineup(id) ON DELETE CASCADE, -- safe to cascade
	CONSTRAINT FK_LineupPhotoLink_Photo FOREIGN KEY (photoid) REFERENCES Photo(id) ON DELETE CASCADE -- safe to cascade
)

--table 17
CREATE TABLE dbo.LineupView
(
	id int IDENTITY,
	lineupid int,
	witnessexternalref varchar(10),
	witnessfirstname varchar(25),
	witnesslastname varchar(25),
	relevancenotes varchar(4000),
	iscompleted bit,
	administered datetime,
	administeredby int,
	CONSTRAINT PK_LineupView PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_LineupView_Lineup FOREIGN KEY (lineupid) REFERENCES Lineup(id) ON DELETE NO ACTION,
	CONSTRAINT FK_LineupView_User FOREIGN KEY (administeredby) REFERENCES [User](id) ON DELETE NO ACTION
)

--table 18
CREATE TABLE dbo.PhotoView
(
	id int IDENTITY,
	lineupviewid int,
	photoid int,
	resultcode int,
	certainty varchar(1000),
	CONSTRAINT PK_PhotoView PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_PhotoView_LineupView FOREIGN KEY (lineupviewid) REFERENCES LineupView(id) ON DELETE NO ACTION,
	CONSTRAINT FK_PhotoView_Photo FOREIGN KEY (photoid) REFERENCES Photo(id) ON DELETE NO ACTION
)