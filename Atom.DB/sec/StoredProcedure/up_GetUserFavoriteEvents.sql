CREATE PROCEDURE [sec].[up_GetUserFavoriteEvents]
	@UserID int
AS
	SELECT *
	FROM sec.UserFavorite
	WHERE Active = 1
	AND UserId = @UserID