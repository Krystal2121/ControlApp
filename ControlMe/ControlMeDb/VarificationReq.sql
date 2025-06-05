CREATE TABLE [dbo].[VarificationReq]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	[SubID] int not null,
	[VarificationCode] int null, 
    [Count] INT NULL DEFAULT 0, 
    [VerifyDate] DATETIME NULL, 
    [Sent] BIT NULL DEFAULT 0
)
