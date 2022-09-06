CREATE TABLE [sec].[User]
(
	[UserId]				INT					NOT NULL	IDENTITY(1,1),
	[UserGuid]				UNIQUEIDENTIFIER	NOT NULL	CONSTRAINT DF_User_UserGuid DEFAULT(NEWID()),
	[EmailAddress]			VARCHAR(150)		NOT NULL	UNIQUE,
	[FirstName]				VARCHAR(50)			NULL,
	[LastName]				VARCHAR(50)			NULL,
	[UserName]				VARCHAR(100)		NOT NULL	UNIQUE,
	[Salt1]					INT					NULL,
	[Salt2]					INT					NULL,
	[UserKey]				NVARCHAR(128)		NULL,
	[UserAvatarURL]			VARCHAR(200)		NULL,
	[CreatedDateTime]		DATETIME2(7)		NOT NULL	CONSTRAINT DF_User_CreatedDateTime DEFAULT(SYSDATETIME()),
	[CreatedByUser]			INT					NOT NULL,
	[LastUpdateDateTime]	DATETIME2(7)		NULL,
	[UpdatedByUser]			INT					NULL,
	[Active]				BIT					NOT NULL	CONSTRAINT DF_User_Active DEFAULT(1),

	CONSTRAINT PK_User_UserId PRIMARY KEY(UserId),
	CONSTRAINT CK_User_UserKeyCheck CHECK((Salt1 IS NULL AND Salt2 IS NULL AND UserKey IS NULL) OR (Salt1 IS NOT NULL AND Salt2 IS NOT NULL AND UserKey IS NOT NULL)),
	CONSTRAINT FK_User_CreatedByUser FOREIGN KEY([CreatedByUser]) REFERENCES [sec].[User](UserId),
	CONSTRAINT FK_User_UpdatedByUser FOREIGN KEY([UpdatedByUser]) REFERENCES [sec].[User](UserId),
)

GO
CREATE TRIGGER [sec].itUser   
ON [sec].[User]
AFTER INSERT
AS   
INSERT INTO sec.[Role](UserId, RoleTypeId)
SELECT UserId, 1
FROM inserted