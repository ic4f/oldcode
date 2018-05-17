SET NOCOUNT ON


insert into Role (Name, Created, Modified, ModifiedBy) values('Administrator', getdate(), getdate(), 1) --must change modifiedby


insert into RolePermissionLink (RoleId, PermissionId) values (1, 1)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 2)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 3)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 4)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 5)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 6)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 7)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 8)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 12)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 13)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 14)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 15)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 16)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 17)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 18)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 19)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 20)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 21)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 22)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 23)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 24)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 25)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 26)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 27)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 28)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 29)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 30)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 31)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 32)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 33)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 34)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 35)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 36)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 37)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 38)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 39)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 40)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 41)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 42)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 43)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 44)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 45)

insert into CmsUserRoleLink (cmsuserid, roleid) values (1, 1)


INSERT INTO ImageFileType (extension) VALUES ('.jpg')
INSERT INTO ImageFileType (extension) VALUES ('.jpeg')
INSERT INTO ImageFileType (extension) VALUES ('.gif')
INSERT INTO ImageFileType (extension) VALUES ('.png')


INSERT INTO FileType (extension, description) VALUES ('.doc', 'Microsoft Word Document')
INSERT INTO FileType (extension, description) VALUES ('.gif', 'GIG Image')
INSERT INTO FileType (extension, description) VALUES ('.jpg', 'JPEG Image')
INSERT INTO FileType (extension, description) VALUES ('.jpeg', 'JPEG Image')
INSERT INTO FileType (extension, description) VALUES ('.png', 'PNG Image')
INSERT INTO FileType (extension, description) VALUES ('.pdf', 'Adobe Acrobat Document')
INSERT INTO FileType (extension, description) VALUES ('.ppt', 'Microsoft PowerPoint Presentation')
INSERT INTO FileType (extension, description) VALUES ('.psd', 'Adobe PhotoShop Document')
INSERT INTO FileType (extension, description) VALUES ('.rtf', 'Rich Text Format')
INSERT INTO FileType (extension, description) VALUES ('.txt', 'Plain Text Document')
INSERT INTO FileType (extension, description) VALUES ('.xls', 'Microsoft Excel Worksheet')


SET NOCOUNT OFF