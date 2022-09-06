CREATE PROCEDURE [css].[up_SaveCSS]
	@CSSVariableId  INT,
	@CSSVariableValue	VARCHAR(10)
AS
	
	INSERT INTO [css].[Rule](CSSVariableId, CSSVariableValue)
	VALUES(@CSSVariableId, @CSSVariableValue)

	


