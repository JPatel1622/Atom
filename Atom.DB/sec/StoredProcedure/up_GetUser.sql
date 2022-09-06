CREATE PROCEDURE [sec].[up_GetUser]
	@UserGuid UNIQUEIDENTIFIER
AS
	SELECT UserId, UserGuid, EmailAddress, FirstName, LastName, UserAvatarURL, UserName
	FROM [sec].[User]
	WHERE UserGuid = @UserGuid
