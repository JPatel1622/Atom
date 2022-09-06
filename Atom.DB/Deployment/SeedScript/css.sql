IF NOT EXISTS(SELECT 1 FROM css.[Rule] WHERE CSSId = 0)
BEGIN
	SET IDENTITY_INSERT css.[Rule] ON 
	INSERT INTO css.[Rule](CSSId, UserId, CSSVariableId, CSSVariableValue)
	VALUES(1, 0, 1, '#F5F5DC'),
	(2, 0, 2, '#D2B48C'),
	(3, 0, 3, '#000000'),
	(4, 0, 4, '#FFFFFF')
	SET IDENTITY_INSERT css.[Rule] OFF 
END

