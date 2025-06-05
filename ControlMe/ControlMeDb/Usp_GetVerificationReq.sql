CREATE PROCEDURE [dbo].[Usp_GetVerificationReq]

AS
	SELECT  u.[Screen Name] [User],u.[Login Name] Email,'<a href="mailto:'+u.[Login Name]+'?subject=Verify &body='+ convert(varchar(4),u.VarifiedCode) +'">Email</a>' Code, vr.Count Tried
	FROM Users u
	JOIN VarificationReq vr
	on u.Id=vr.SubID
	where vr.Sent=0;