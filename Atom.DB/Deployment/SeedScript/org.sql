set identity_insert org.Organization on
insert into org.Organization(OrganizationId, OrganizationName)
values(0, 'testorg')
set identity_insert org.Organization off
