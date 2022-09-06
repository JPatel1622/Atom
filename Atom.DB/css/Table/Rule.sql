﻿CREATE TABLE [css].[Rule]
(
	[CSSId]				INT					NOT NULL	IDENTITY(1,1),
	[UserId]			INT					NOT NULL,
	[CSSGuid]			UNIQUEIDENTIFIER	NOT NULL	CONSTRAINT DF_Rule_CSSGuid DEFAULT(NEWID()),
	[CSSVariableId]		INT					NOT NULL,
	[CSSVariableValue]  VARCHAR(50)			NOT NULL,
	[Active]			BIT					NOT NULL	CONSTRAINT DF_Rule_Active DEFAULT(1)
	

	CONSTRAINT PK_Rule_CSSId PRIMARY KEY(CSSId),
	CONSTRAINT FK_Rule_UserId FOREIGN KEY([UserId]) REFERENCES [sec].[User](UserId),
	CONSTRAINT FK_Rule_CSSVariableId FOREIGN KEY(CSSVariableId) REFERENCES [lkup].[CSSVariable](CSSVariableId)
)