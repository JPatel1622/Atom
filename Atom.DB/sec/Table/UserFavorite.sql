CREATE TABLE [sec].[UserFavorite]
(
	[UserFavoriteId]	INT				NOT NULL	IDENTITY(1 ,1),
	[EventTypeId]		INT				NOT NULL,
	[EventId]			VARCHAR(100)	NOT NULL,
	[UserId]			INT				NOT NULL,
	[Active]			BIT				NOT NULL	CONSTRAINT [DF_UserFavorite_Active] DEFAULT(1),

	CONSTRAINT PK_UserFavorite PRIMARY KEY(UserFavoriteId),
	CONSTRAINT FK_UserFavorite_UserId FOREIGN KEY(UserId) REFERENCES Sec.[User](UserId),
	CONSTRAINT FK_UserFavorite_EventTypeId FOREIGN KEY(EventTypeId) REFERENCES lkup.[EventType](EventTypeId)
)
