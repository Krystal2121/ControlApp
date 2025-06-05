CREATE PROCEDURE [dbo].[USP_AdminCommandLog]
AS
BEGIN
	Select isnull(u.[screen name],'Anon') Sender,
	isnull(su.[screen name],'Anon') SubId,
	c.GroupRefId ,
	convert(varchar(max),'') as Decrypt ,
	c.Content
	from dbo.CmdLog c 
	left join users u on c.senderid=u.id 
	left join users su on c.SubId=su.id 
	order by c.Id
END
