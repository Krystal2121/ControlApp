CREATE TABLE [dbo].[CommandList]
(
	[CmdId] INT NOT NULL identity(1,1) PRIMARY KEY,
	[Content] nvarchar(max) not null, 
    [SendDate] DATETIME NOT NULL DEFAULT getdate()
)
