CREATE PROCEDURE [dbo].[usp_veriySent]
	@Username varchar(250)
AS
	update vr
	set vr.Sent=1
	FROM VarificationReq vr
	JOIN Users u
	on u.Id=vr.SubID
	where u.[Screen Name]=@Username
	