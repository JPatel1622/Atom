CREATE TABLE [lkup].[EventType]
(
	[EventTypeId]		INT				NOT NULL	IDENTITY(1,1),
	[EventTypeName]		VARCHAR(100)	NOT NULL,

	CONSTRAINT PK_EventType_EventTypeId PRIMARY KEY(EventTypeId),
)
