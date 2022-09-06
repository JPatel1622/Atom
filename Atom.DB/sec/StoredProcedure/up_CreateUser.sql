-- =============================================
-- Author:		Danaj Zigic
-- Create date: 2/1/2022
-- Description:	Create a user with a password if ExternalAuth is 0 or with no password if ExternalAuth is 1.
--				ExternalAuth = 1 indicated that a user is being created using a third-party, such as Google Oauth 
-- =============================================

CREATE PROCEDURE [sec].[up_CreateUser]
	@EmailAddress	VARCHAR(150),
	@Password		VARCHAR(150) = NULL,
	@FirstName		VARCHAR(50) = NULL,
	@LastName		VARCHAR(50) = NULL,
	@UserName		VARCHAR(50),
	@CreateUserID	INT,
	@ExternalAuth	BIT
AS	
	IF((@ExternalAuth = 0 AND @Password IS NULL) OR (@ExternalAuth = 1 AND @Password IS NOT NULL))
		BEGIN
			RAISERROR('A user must have a password if they are not being created externally', -1, -1, '[sec].[up_CreateUser]')
			RETURN
		END

	IF(@ExternalAuth = 1 AND @Password IS NULL)
		BEGIN
			INSERT INTO [sec].[User](EmailAddress, FirstName, LastName, CreatedByUser, UserName)
			VALUES(@EmailAddress, @FirstName, @LastName, @CreateUserID, @UserName)
		END
	IF(@ExternalAUth = 0 AND @Password IS NOT NULL)
		BEGIN
			DECLARE
			@Salt1	INT,
			@Salt2	INT,
			@Hash	NVARCHAR(MAX)

			SET @Salt1 = FLOOR(RAND() * 10000 + 1)
			SET @Salt2 = FLOOR(RAND() * 10000 + 1)
			SET @Hash = sec.udf_GetKeyHash(@Password, @Salt1, @Salt2)
			
			INSERT INTO [sec].[User](EmailAddress, FirstName, LastName, CreatedByUser, Salt1, Salt2, UserKey, UserName)
			VALUES(@EmailAddress, @FirstName, @LastName, @CreateUserID, @Salt1, @Salt2, @Hash, @UserName)
		END