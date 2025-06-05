CREATE TABLE [dbo].[Users]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY, 
    [Screen Name] VARCHAR(50) NOT NULL, 
    [Login Name] VARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Role] VARCHAR(50) NULL,
    [RandOpt] BIT null default 0, 
    [AnonCmd] BIT NULL DEFAULT 0, 
    [Varified] BIT NOT NULL DEFAULT 0, 
    [VarifiedCode] INT NULL DEFAULT rand()*1000, 
    [LoginDate] DATETIME NOT NULL DEFAULT getdate(), 
    [ThumbsUp] INT NULL DEFAULT 0
)
