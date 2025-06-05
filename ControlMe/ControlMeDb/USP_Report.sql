CREATE PROCEDURE [dbo].[USP_Report]
	@reporter int = 0,
	@reportee int,
	@content varchar(max)=''
AS
BEGIN
	exec USP_BlockUser @reporter, @reportee

	insert into Report
	(
	ReporterId,
	Reportee,
	ReportedCommand
	)
	select @reporter,
	@reportee,
	@content
END
