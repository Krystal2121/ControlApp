﻿/*
Deployment script for ControlMe

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ControlMe"
:setvar DefaultFilePrefix "ControlMe"
:setvar DefaultDataPath "D:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[ChatLog].[DID] is being dropped, data loss could occur.

The column [dbo].[ChatLog].[SIS] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[ChatLog])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping Default Constraint unnamed constraint on [dbo].[ChatLog]...';


GO
ALTER TABLE [dbo].[ChatLog] DROP CONSTRAINT [DF__ChatLog__Date_Ad__797309D9];


GO
PRINT N'Starting rebuilding table [dbo].[ChatLog]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ChatLog] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [ReceiverID] INT           NULL,
    [SenderID]   INT           NULL,
    [ChatTxt]    VARCHAR (MAX) NULL,
    [Date_Add]   DATETIME      DEFAULT getdate() NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ChatLog])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ChatLog] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ChatLog] ([Id], [ChatTxt], [Date_Add])
        SELECT   [Id],
                 [ChatTxt],
                 [Date_Add]
        FROM     [dbo].[ChatLog]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ChatLog] OFF;
    END

DROP TABLE [dbo].[ChatLog];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ChatLog]', N'ChatLog';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Table [dbo].[CommandList]...';


GO
CREATE TABLE [dbo].[CommandList] (
    [CmdId]    INT            IDENTITY (1, 1) NOT NULL,
    [Content]  NVARCHAR (MAX) NOT NULL,
    [SendDate] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([CmdId] ASC)
);


GO
PRINT N'Creating Table [dbo].[ControlAppCmd]...';


GO
CREATE TABLE [dbo].[ControlAppCmd] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [SenderId]   INT NOT NULL,
    [SubId]      INT NOT NULL,
    [CmdId]      INT NOT NULL,
    [GroupRefId] INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[CommandList]...';


GO
ALTER TABLE [dbo].[CommandList]
    ADD DEFAULT getdate() FOR [SendDate];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[ControlAppCmd]...';


GO
ALTER TABLE [dbo].[ControlAppCmd]
    ADD DEFAULT 0 FOR [SenderId];


GO
PRINT N'Creating View [dbo].[Vw_ControlAppCmd]...';


GO
CREATE VIEW [dbo].[Vw_ControlAppCmd]
	AS 
	select c.SenderId, c.SubId, c.GroupRefId, cl.Content from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId
GO
PRINT N'Altering Procedure [dbo].[USP_CmdComplete]...';


GO
ALTER PROCEDURE [dbo].[USP_CmdComplete]
	@userID int = 0
AS
BEGIN
	delete from ControlAppCmd where id =(
	SELECT top 1 id from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId 
where SubId=@userID
	ORDER BY SendDate)
END
GO
PRINT N'Altering Procedure [dbo].[USP_GetAppContent]...';


GO
ALTER PROCEDURE [dbo].[USP_GetAppContent]
@userID int = 0
AS
BEGIN

DELETE cmd FROM ControlAppCmd cmd
WHERE SenderId in (Select BlockeeId FROM Block where BlockerId=@userID)
AND SubId=@userID

DECLARE @Anon bit = 0
SELECT @Anon=AnonCmd from Users where ID=@userID

IF (@Anon=0)
BEGIN
DELETE cmd FROM ControlAppCmd cmd
WHERE SenderId =-1
AND SubId=@userID
END

SELECT TOP 1 convert(varchar(100),SenderId), cl.Content from ControlAppCmd cac
join CommandList cl
on cac.CmdId=cl.CmdId
where SubId=@userID
ORDER BY SendDate
END
GO
PRINT N'Altering Procedure [dbo].[USP_GetOutstanding]...';


GO
ALTER PROCEDURE [dbo].[USP_GetOutstanding]
	@userID int = 0
AS
BEGIN

	DELETE cmd FROM ControlAppCmd cmd
	WHERE SenderId in (Select BlockeeId FROM Block where BlockerId=@userID)
	AND SubId=@userID

	DECLARE @Anon bit = 0
	DECLARE @vari bit=0
	SELECT @Anon=AnonCmd, @vari=Varified from Users where ID=@userID

	IF (@Anon=0)
	BEGIN
		DELETE cmd FROM ControlAppCmd cmd
		WHERE SenderId =-1
		AND SubId=@userID
	END
	declare @whonext varchar(20)
	select top 1 @whonext= isnull(GroupRefId,SenderId)
	from [ControlAppCmd] c
	join CommandList cl
	on c.CmdId=cl.CmdId
	where SubId=@userID order by [SendDate]
	if Convert(int,@whonext )<-1
	begin
		select @whonext= 'Group: '+ GroupName from Groups where RefId=@whonext
	end
	else
	begin
		if Convert(int,@whonext )=-1
		begin
			set @whonext='Anon'
		end
		else
		begin
			set @whonext='User'
		end
	end
	SELECT convert(varchar(100),count(*)) counting,  @whonext whonext, @vari varified  from ControlAppCmd where SubId=@userID
END
GO
PRINT N'Altering Procedure [dbo].[Usp_GetVerificationReq]...';


GO
ALTER PROCEDURE [dbo].[Usp_GetVerificationReq]

AS
	SELECT u.[Login Name] Email, u.VarifiedCode Code, vr.Count Tried
	FROM Users u
	JOIN VarificationReq vr
	on u.Id=vr.SubID
GO
PRINT N'Altering Procedure [dbo].[USP_SendAppCmd]...';


GO

ALTER PROCEDURE [dbo].[USP_SendAppCmd]
@usernm varchar(250),
@usercmd varchar(max),
@all int =0,
@username varchar(250) = null,
@password varchar(250) = null,
@fileloc varchar(max) = null
AS
BEGIN

declare @newidins int
Declare @senderid int =0
Declare @cmdid int =0
if (@username is not null)
BEGIN
	if (@username!='')
	BEGIN
		select @senderid=
		(
			select top 1 Id from
			(
				SELECT Id, [Role] from Users where [Login Name]=@UserName and Password=@Password
				union SELECT Id, [Role] from Users where [Screen Name]=@UserName and Password=@Password
			) as subq
		)
	END
	ELSE
	BEGIN
	SET @senderid=-1
	END
END
IF (@senderid!=0)
BEGIN
	declare @id int
	IF (@usernm='')
	BEGIN
		if (@all=0)
		BEGIN
			declare @numbers int
			SELECT @numbers=count(*) from Users where  [RandOpt]=1

			declare @which int
			select @which =cast(floor(rand()*@numbers) as int)+1

			SELECT @id=Id FROM Users
			ORDER BY Id
			OFFSET @which-1 ROWS
			FETCH NEXT 1 ROWS ONLY;

			insert into CommandList
			([Content])
			values
			(@usercmd)

			select @newidins=SCOPE_IDENTITY()

			insert into ControlAppCmd
			([SenderId],
			[SubId],
			[CmdId]
			)
			values (@senderid,@id,@newidins)

			insert into CmdLog
			([SenderId],[SubId],
			[Content])
			values (@senderid,@id,@usercmd)
		END
		ELSE
		BEGIN

			insert into CommandList
			([Content])
			values
			(@usercmd)

			select @newidins=SCOPE_IDENTITY()

			insert into ControlAppCmd
			([SenderId],
			[SubId],
			[CmdId])
			select @senderid,Id, @newidins
			from Users
			where  [RandOpt]=1

			insert into CmdLog
			([SenderId],[SubId],
			[Content])
			values (@senderid,-1,@usercmd)
		END

	END
	ELSE
	IF ((ISNUMERIC(@usernm)=1) and (CONVERT(int,@usernm)<-1))
	BEGIN

			insert into CommandList
			([Content])
			values
			(@usercmd)

			select @newidins=SCOPE_IDENTITY()

		insert into ControlAppCmd
		([SenderId],
		[SubId],
		[CmdId],
		[GroupRefId])
		select @senderid,UserId, @newidins,CONVERT(int,@usernm)
		from UsersGroups
		where GroupRefId=CONVERT(int,@usernm)

		insert into CmdLog
		([SenderId],[SubId],
		[Content],
		[GroupRefId])
		values (@senderid,@senderid,@usercmd,CONVERT(int,@usernm))
	END
	ELSE
	BEGIN

		select @id=(
		select top 1 Id from
		( Select  Id from Users where [Screen Name]=@usernm union Select  Id from Users where convert(varchar(250),Id)=@usernm) sub1
		)
		if(@id is not null)
		BEGIN

			insert into CommandList
			([Content])
			values
			(@usercmd)

			select @newidins=SCOPE_IDENTITY()

			insert into ControlAppCmd
			([SenderId],[SubId],
			[CmdId])
			values (@senderid,@id,@newidins)

			insert into CmdLog
			([SenderId],[SubId],
			[Content])
			values (@senderid,@id,@usercmd)
		END
	END
END

delete c from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId
where [SenderId]<0
and [Content] like '%1mp7JCqszmk=|||%'

delete c from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId
where [SenderId]<0
and [Content] like ''

delete c from CommandList c where CmdId not in (select CmdId from [ControlAppCmd])

END
GO
PRINT N'Creating Procedure [dbo].[USP_AcceptInvite]...';


GO
CREATE PROCEDURE [dbo].[USP_AcceptInvite]
	@SubId int,
	@DomName varchar(250)
AS
	Insert into Relationship
	(DomId,SubID)
	select Id,@SubId
	from Users where [Screen Name]=@DomName

	Delete i from Invites i
	join Users u
	on i.DomId=u.Id
	where [Screen Name]=@DomName
	and i.SubId=@SubId
GO
PRINT N'Creating Procedure [dbo].[USP_AddInvite]...';


GO
CREATE PROCEDURE [dbo].[USP_AddInvite]
	@DomId int,
	@SubName varchar(250)
AS
	Insert into Invites
	(SubId,
	DomId)
	Select u.Id,
	@DomId
	From Users u
	where [Screen Name]=@SubName
RETURN 0
GO
PRINT N'Creating Procedure [dbo].[USP_DeleteInvite]...';


GO
CREATE PROCEDURE [dbo].[USP_DeleteInvite]
	@SubId int,
	@DomName varchar(250)
AS
	Delete i from Invites i
	join Users u
	on i.DomId=u.Id
	where [Screen Name]=@DomName
	and i.SubId=@SubId
GO
PRINT N'Creating Procedure [dbo].[USP_GetChatLog]...';


GO
CREATE PROCEDURE [dbo].[USP_GetChatLog]
	@RequestID int ,
	@OtherID int
AS
	SELECT 'S' Type,ChatTxt
	FROM ChatLog
	where SenderID=@RequestID
	and ReceiverID=@OtherID

	union

	SELECT 'R' Type,ChatTxt
	FROM ChatLog
	where ReceiverID=@RequestID
	and SenderID=@OtherID
GO
PRINT N'Creating Procedure [dbo].[USP_GetOptions]...';


GO
CREATE PROCEDURE [dbo].[USP_GetOptions]
	@Id int
AS
BEGIN
	select top 1 [Download]+','+Wallpaper+','+Runable+','+OpenWeb+','+PopUp+','+Message+','+Sumblim+','+Audio+','+Write4me+'.'+ScreenShot+','+Twitter+','+Watch4me+','+SendDelete+','+Outstanding+','+AutoRunExe+','+ClickThrough+','+ClickThrough+','+PopStyle+','+PopLength+','+WallpaperStyle+','+convert(varchar(1),IsDom)
	from [dbo].[Options]
	where IsDom=0
	and		SubId=@Id
	union
	select top 1 [Download]+','+Wallpaper+','+Runable+','+OpenWeb+','+PopUp+','+Message+','+Sumblim+','+Audio+','+Write4me+'.'+ScreenShot+','+Twitter+','+Watch4me+','+SendDelete+','+Outstanding+','+AutoRunExe+','+ClickThrough+','+ClickThrough+','+PopStyle+','+PopLength+','+WallpaperStyle+','+convert(varchar(1),IsDom)
	from [dbo].[Options]
	where IsDom=1
	and		SubId=@Id
END
GO
PRINT N'Creating Procedure [dbo].[USP_SendChat]...';


GO
CREATE PROCEDURE [dbo].[USP_SendChat]
	@SenderId int ,
	@ReceiverId int,
	@message varchar(max)
AS
	Insert into ChatLog
	(ReceiverID ,
	SenderID ,
	ChatTxt)
	values
	(@ReceiverId,
	@SenderId,
	@message)
GO
PRINT N'Update complete.';


GO
