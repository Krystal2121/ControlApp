CREATE proc usp_tidyup
as
begin

delete from CommandList
where senddate<dateadd(day,-5,getdate())
delete from [ControlAppCmd] where subid in 
(
	select subid from [ControlAppCmd] cac
	left join CommandList cl
	on cac.CmdId=cl.CmdId
	where cl.CmdId is null

)

delete from CommandList where CmdId in 
(
	select cac.CmdId from CommandList cac
	left join [ControlAppCmd] cl
	on cac.CmdId=cl.CmdId
	where cl.CmdId is null

)

delete from VarificationReq where VerifyDate<dateadd(day,-5,getdate())

delete from usersgroups where grouprefid=-11 and userid <>1

end
