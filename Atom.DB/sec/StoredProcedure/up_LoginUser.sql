-- =============================================
-- Author:		Danaj Zigic
-- Create date: 2/1/2022
-- Description:	Login a user using their email address, password, and an indicator whether they are logging in via a third-party such as Google OAuth
--				Set ExternalAuth = 1 if logging in via third-party to bypass password requirement
-- =============================================


CREATE PROCEDURE [sec].[up_LoginUser]
	@EmailAddress	VARCHAR(150),
	@Password		VARCHAR(150),
	@ExternalAuth	BIT = 0
AS
BEGIN
	IF((@ExternalAuth = 0 AND @Password IS NULL) OR (@ExternalAuth = 1 AND @Password IS NOT NULL))
	BEGIN
		RAISERROR('A user must have a password if they are not being logged in externally', -1, -1, '[sec].[up_CreateUser]')
		RETURN
	END

	IF(@Password IS NULL AND @ExternalAuth = 1)
		BEGIN
			SELECT u.UserId, UserGuid, EmailAddress, FirstName, LastName, UserAvatarURL, UserName		
			FROM sec.[User] u
			WHERE EmailAddress = @EmailAddress
			AND Active = 1
		END

	IF(@Password IS NOT NULL AND @ExternalAuth = 0)
		BEGIN
			DECLARE 
			@Hash NVARCHAR(130),
			@Salt1 INT,
			@Salt2 INT
			
			SELECT @Salt1 = Salt1, @Salt2 = Salt2 
			FROM sec.[User]
			WHERE EmailAddress = @EmailAddress
			AND Active = 1

			SET @Hash = sec.udf_GetKeyHash(@Password, @Salt1, @Salt2)

			SELECT u.UserId, UserGuid, EmailAddress, FirstName, LastName, UserAvatarURL, UserName
			FROM sec.[User] u
			WHERE EmailAddress = @EmailAddress
			AND UserKey = @Hash
			AND u.Active = 1
		END
END