CREATE TABLE [dbo].[UsersGroups]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL,
	[GroupRefId] INT NOT NULL
)
