CREATE TABLE [lkup].[RoleType]
(
	[RoleTypeId]		INT				NOT NULL	IDENTITY(1,1),
	[RoleName]			VARCHAR(50)		NOT NULL,
	[RoleDescription]	VARCHAR(50)		NOT NULL,

	CONSTRAINT PK_RoleType_RoleTypeId PRIMARY KEY(RoleTypeId),

)
