CREATE TABLE [org].[Organization]
(
	[OrganizationId]	INT					NOT NULL	IDENTITY(1,1),
	[OrganizationGuid]	UNIQUEIDENTIFIER	NOT NULL	CONSTRAINT DF_Organization_OrganizationGuid DEFAULT(NEWID()),
	[OrganizationName]	VARCHAR(50)			NOT NULL,

	CONSTRAINT PK_Organization_OrganizationId PRIMARY KEY(OrganizationId),
)
