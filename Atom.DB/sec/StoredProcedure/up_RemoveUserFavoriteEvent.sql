CREATE PROCEDURE [sec].[up_RemoveUserFavoriteEvent]
	@UserID int,
	@EventTypeID int,
	@EventID VARCHAR(100)
AS
	UPDATE sec.UserFavorite SET Active = 0
	WHERE UserId = @UserID
	AND EventTypeId = @EventTypeID 
	AND EventId = @EventID