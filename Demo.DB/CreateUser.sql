use Demo

create user ap_demo for login ap_demo with default_schema = dbo 

exec sp_addrolemember N'db_datareader', N'ap_demo'
exec sp_addrolemember N'db_datawriter', N'ap_demo'