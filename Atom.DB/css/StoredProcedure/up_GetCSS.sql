CREATE PROCEDURE [css].[up_GetCSS]
	@CSSUserId int
AS

declare @num int 

	Create Table #TempRules
	(
		CSSId int,
		UserId int,
		CSSGuid UNIQUEIDENTIFIER,
		CSSVariableId int,
		CSSVariableValue varchar(50),
		[Active] bit
	)
	

	INSERT INTO #TempRules (CSSId, UserId, CSSGuid, CSSVariableId, CSSVariableValue)
	SELECT CSSId, UserId, CSSGuid, CSSVariableId, CSSVariableValue
	FROM [css].[Rule]
	WHERE UserId = @CSSUserId

	Select @num = count(*) from #TempRules
	
	if(@num < CAST(4 as int))
        Begin
            INSERT INTO #TempRules (CSSId, CSSGuid, CSSVariableId, CSSVariableValue)
            Select CSSId, CSSGuid, CSSVariableId, CSSVariableValue
            From [css].[rule]
            where UserId = 0

        End

	
	
	
SELECT * 
FROM #TempRules
DROP TABLE #TempRules



