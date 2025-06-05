CREATE PROCEDURE [dbo].[USP_GetGroups]
	@user int 
AS
select GroupName,case when u.Id is null then 'False' else 'True' end as Allowed from [dbo].[Groups] g
left join [dbo].[UsersGroups] ug
on g.[RefId]=ug.[GroupRefId]
left join [dbo].[Users] u
on ug.[UserId]=u.Id
and u.Id=@user

