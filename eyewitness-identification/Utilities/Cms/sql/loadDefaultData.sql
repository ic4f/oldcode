SET NOCOUNT ON

insert into Role (Name, Created, Modified, ModifiedBy) values('Administrator', getdate(), getdate(), 1)

insert into RolePermissionLink (RoleId, PermissionId) values (1, 1)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 2)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 3)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 4)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 5)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 6)
insert into RolePermissionLink (RoleId, PermissionId) values (1, 7)

insert into CmsUserRoleLink (cmsuserid, roleid) values (1, 1)

insert into Race  (description) values ('white')
insert into Race  (description) values ('black')
insert into Race  (description) values ('hispanic')
insert into Race  (description) values ('asian/pacific isl')
insert into Race  (description) values ('native am/pacific isl')

insert into HairColor  (description) values ('black')
insert into HairColor  (description) values ('blond')
insert into HairColor  (description) values ('sandy')
insert into HairColor  (description) values ('brown')
insert into HairColor  (description) values ('red/auburn')
insert into HairColor  (description) values ('gray')
insert into HairColor  (description) values ('bald')

insert into AgeRange  (description) values ('less than 25')
insert into AgeRange  (description) values ('25 - 40')
insert into AgeRange  (description) values ('40 - 55')
insert into AgeRange  (description) values ('over 55')

insert into WeightRange  (description) values ('lean')
insert into WeightRange  (description) values ('normal')
insert into WeightRange  (description) values ('heavy')
insert into WeightRange  (description) values ('overweight')


SET NOCOUNT OFF