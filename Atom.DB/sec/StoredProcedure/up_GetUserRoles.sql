CREATE PROCEDURE [sec].[up_GetUserRoles]
	@UserId int
AS
	SELECT r.RoleTypeId, r.OrganizationId, r.RoleGuid, r.RoleId
		,rt.RoleDescription, rt.RoleName
	FROM sec.[Role] r
		JOIN lkup.RoleType rt ON rt.RoleTypeId = r.RoleTypeId
		LEFT JOIN org.Organization o ON o.OrganizationId = r.OrganizationId
	WHERE UserId = @UserId

