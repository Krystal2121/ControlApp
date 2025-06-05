CREATE TABLE [dbo].[SubContent]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Sub_User_Id] INT NULL, 
    [IFrame_Content] NVARCHAR(MAX) NULL, 
    [Date_add] DATETIME default getdate()
)
