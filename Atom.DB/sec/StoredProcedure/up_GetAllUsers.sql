CREATE PROCEDURE [sec].[up_GetAllUsers]
	@Active bit = null
AS
	SELECT UserId, UserGuid, EmailAddress, FirstName, LastName, UserName, UserAvatarURL, Active
	FROM sec.[User]
	WHERE Active = ISNULL(@Active, Active)