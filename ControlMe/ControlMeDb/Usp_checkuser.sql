CREATE PROCEDURE [dbo].[Usp_checkuser]
	@ID int
AS
	select case when vr.SubID IS NULL then u.Varified else -1 end as Varified from Users u
	left join VarificationReq vr
	on vr.SubID=u.Id
	where u.ID = @ID
RETURN 0
