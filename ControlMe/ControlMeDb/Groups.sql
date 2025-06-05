CREATE TABLE [dbo].[Groups]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	[GroupName] varchar(50) NOT NULL, 
    [RefId] INT NOT NULL
)
