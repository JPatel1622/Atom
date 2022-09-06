﻿IF NOT EXISTS(SELECT 1 FROM sec.[User] WHERE UserId = 0)
BEGIN
	SET IDENTITY_INSERT sec.[User] ON 
	INSERT INTO sec.[User](UserId, EmailAddress, CreatedByUser, UserName)
	VALUES(0, NEWID(), 0, 'PrimaryUser')
	SET IDENTITY_INSERT sec.[User] OFF 
END

IF NOT EXISTS(SELECT 1 FROM sec.[User] WHERE EmailAddress = 'danajzigic@gmail.com')
BEGIN
	SET IDENTITY_INSERT sec.[User] ON
	INSERT INTO sec.[User](UserId, EmailAddress, CreatedByUser, FirstName, LastName, Salt1, Salt2, UserKey, UserName)
	VALUES(1, 'danajzigic@gmail.com', 0, 'Danaj', 'Zigic', 8419, 9566, '791C6B1883315158357FE0B83FD01129E2147BC896071D4C5C715062ECBAE606E389BA6B01E29EE9639123FD3030B361B84C7B61F40D27DD6157D6FF1763230C', 'Danaj')
	SET IDENTITY_INSERT sec.[User] OFF 
END

IF NOT EXISTS(SELECT 1 FROM sec.[User] WHERE EmailAddress = 'jp11223@georgiasouthern.edu')
BEGIN
	SET IDENTITY_INSERT sec.[User] ON
	INSERT INTO sec.[User](UserId, EmailAddress, CreatedByUser, FirstName, LastName, Salt1, Salt2, UserKey, UserName)
	VALUES(2, 'jp11223@georgiasouthern.edu', 0, 'Jeel', 'Patel', 3469, 316, 'EAC2547F1BD0C304D044BB3E26841E1021B73CCAB4E6CCF6974EBFA54AF2117AC8D5C715AFD970C9A75A751AAD363663D4EAF64CBE50BC9CDFE069EAB850D74E', 'Jeel')
	SET IDENTITY_INSERT sec.[User] OFF 
END

BEGIN
	INSERT INTO sec.[Role](RoleTypeId, UserId, OrganizationId)
	VALUES(2, 1, 0)
	,(3, 1, 0)
	,(4, 1, 0)
END