CREATE TABLE [dbo].[SubReport]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY, 
    [Sub_User_Id] INT NULL, 
    [Dom_User_Id] INT NULL, 
    [Event_Datetime] DATETIME NULL, 
    [Event_Desc] VARCHAR(50) NULL
)
