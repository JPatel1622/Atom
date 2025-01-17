﻿CREATE FUNCTION [sec].[udf_GetKeyHash]
(
	@Password	NVARCHAR(150),
	@Salt1		INT,
	@Salt2		INT
)
RETURNS NVARCHAR(130)
AS
BEGIN
	DECLARE
	@GuidOne		UNIQUEIDENTIFIER = '26D93605-4CDD-4F5F-8A49-5C3C4776EA1F',
	@GuidTwo		UNIQUEIDENTIFIER = '27584063-CFBB-4010-BD72-61D75C1EAEF3',
	@GuidThree		UNIQUEIDENTIFIER = 'E638BBAD-E465-4CDF-A5D7-91B2A045313E',
	@GuidFour		UNIQUEIDENTIFIER = '00962D4E-87DB-4FD6-92FD-B48485F04A66',
	@GuidFive		UNIQUEIDENTIFIER = 'B361D3D4-FFF7-48FB-AABF-358AFDA5D156',
	@GuidSix		UNIQUEIDENTIFIER = '642E4842-7C55-4521-878D-ADAFC2E045DF',
	@GuidSeven		UNIQUEIDENTIFIER = '7004CC1E-8523-4426-BDB1-3923420004F7',
	@GuidEight		UNIQUEIDENTIFIER = 'A8C2C277-70F3-4799-8DBF-39DC4CB3AC66',
	@GuidNine		UNIQUEIDENTIFIER = '261DB10F-B3D4-4EB3-BDB5-AF83E41F66EA',
	@RandomNumber	INT,
	@ChosenGuid		UNIQUEIDENTIFIER,
	@RandomSalt1	INT,
	@RandomSalt2	INT

	SET @RandomNumber = @Salt1 * @Salt2 % 10

	SET @ChosenGuid = 
				(
					CASE @RandomNumber
						WHEN 0 THEN @GuidOne
						WHEN 1 THEN @GuidTwo
						WHEN 2 THEN @GuidThree
						WHEN 3 THEN @GuidFour
						WHEN 4 THEN @GuidFive
						WHEN 5 THEN @GuidSix
						WHEN 6 THEN @GuidSeven
						WHEN 7 THEN @GuidEight
						WHEN 8 THEN @GuidNine
					END
				)

	RETURN CONVERT(NVARCHAR(130), HASHBYTES('SHA2_512', CAST(@ChosenGuid AS NVARCHAR(36)) + CAST(@Salt2 AS NVARCHAR) + @Password + CAST(@Salt1 AS NVARCHAR)), 2)
END
