CREATE TABLE [dbo].[ControlAppCmd]
(
	[Id] INT NOT NULL identity(1,1) PRIMARY KEY,
	[SenderId] int not null DEFAULT 0,
	[SubId] int not null,
	CmdId int not null,
    [GroupRefId] INT NULL
)
