CREATE TABLE [dbo].[Block]
(
	[Id] INT NOT NULL identity(1,1) PRIMARY KEY,
	BlockerId int not null,
	BlockeeId int not null
)
