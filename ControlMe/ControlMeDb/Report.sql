CREATE TABLE [dbo].[Report]
(
	[Id] INT NOT NULL identity(1,1) PRIMARY KEY,
	ReporterId int not null,
	Reportee int not null,
	ReportedCommand varchar(max)
)
