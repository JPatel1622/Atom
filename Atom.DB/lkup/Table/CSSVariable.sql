CREATE TABLE [lkup].[CSSVariable]
(
	[CSSVariableId]		INT				NOT NULL IDENTITY(1,1),
	[CSSVariableName]	VARCHAR(50)		NOT NULL,
	[CSSVariableRule]	VARCHAR(50)		NOT NULL,
	[Active]			BIT				NOT NULL CONSTRAINT DF_CSSVairables_Active DEFAULT(1),

	CONSTRAINT PK_CSSVariable_CSSVariableId PRIMARY KEY(CSSVariableId)
	
)
