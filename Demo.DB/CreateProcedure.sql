use Demo

go

create procedure [dbo].[GetReceivers] as

select
	*
from dbo.Receiver
GO


create procedure [dbo].[InsertReceivers]
	@Name nvarchar(200),
	@MobilePhone nvarchar(50)
as

insert into dbo.Receiver
([Name], MobilePhone)
values
(@Name, @MobilePhone)
GO



create procedure [dbo].[SendSMS]
	@ReceiverId int,
	@FileName nvarchar(55)
as

insert into dbo.SMSMessage
(ReceiverId, SentDateTime, [FileName])
values
(@ReceiverId, getdate(), @FileName)
GO




