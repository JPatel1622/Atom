CREATE TABLE [sec].[Role]
(
	[RoleId]			INT					NOT NULL	IDENTITY(1, 1), 
	[RoleGuid]			UNIQUEIDENTIFIER	NOT NULL	CONSTRAINT DF_Role_RoleGuid DEFAULT(NEWID()),
	[UserId]			INT					NOT NULL,
	[RoleTypeId]		INT					NOT NULL,
	[OrganizationId]	INT					NULL,

	CONSTRAINT PK_Role_RoleId PRIMARY KEY(RoleId),
	CONSTRAINT FK_Role_UserId FOREIGN KEY([UserId]) REFERENCES [sec].[User](UserId),
	CONSTRAINT FK_Role_RoleTypeId FOREIGN KEY([RoleTypeId]) REFERENCES [lkup].[RoleType](RoleTypeId),
)
