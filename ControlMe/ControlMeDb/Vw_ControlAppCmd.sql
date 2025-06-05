CREATE VIEW [dbo].[Vw_ControlAppCmd]
	AS 
	select c.SenderId, c.SubId, c.GroupRefId, cl.Content from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId
