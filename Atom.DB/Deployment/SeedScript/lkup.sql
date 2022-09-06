set identity_insert lkup.RoleType on
insert into lkup.RoleType(RoleTypeId, RoleName, RoleDescription)
values(1, 'Standard', 'Standard User'),
(2, 'OrgUsr', 'Organization User'),
(3, 'OrgCoord', 'Organization Coordinator'),
(4, 'OrgAdmin', 'Organization Administrator')
set identity_insert lkup.RoleType off

SET IDENTITY_INSERT lkup.[CSSVariable] ON 

INSERT INTO lkup.[CSSVariable](CSSVariableId, CSSVariableName, CSSVariableRule)
VALUES( 1, 'BackgroundColor', '--layoutBackgroundColor'),
( 2, 'FooterColor', '--footerColor'),
( 3, 'TextColor', '--textColor'),
(4, 'ButtonColor', '--primaryBtnColor')
SET IDENTITY_INSERT lkup.[CSSVariable] OFF


SET IDENTITY_INSERT lkup.[EventType] ON
INSERT INTO lkup.[EventType](EventTypeId, EventTypeName)
VALUES
( 1, 'EventBrite'),
( 2, 'TicketMaster')
SET IDENTITY_INSERT lkup.[EventType] OFF