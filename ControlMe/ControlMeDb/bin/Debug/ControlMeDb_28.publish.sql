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
PRINT N'Creating Table [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Screen Name]  VARCHAR (50)  NOT NULL,
    [Login Name]   VARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (50) NOT NULL,
    [Role]         VARCHAR (50)  NULL,
    [RandOpt]      BIT           NULL,
    [AnonCmd]      BIT           NULL,
    [Varified]     BIT           NOT NULL,
    [VarifiedCode] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[VarificationReq]...';


GO
CREATE TABLE [dbo].[VarificationReq] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [SubID]            INT NOT NULL,
    [VarificationCode] INT NULL,
    [Count]            INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT 0 FOR [RandOpt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT 0 FOR [AnonCmd];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT 0 FOR [Varified];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT rand()*1000 FOR [VarifiedCode];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[VarificationReq]...';


GO
ALTER TABLE [dbo].[VarificationReq]
    ADD DEFAULT 0 FOR [Count];


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
	select top 1 @whonext= isnull(GroupRefId,SenderId) from ControlAppCmd where SubId=@userID order by [SendDate]
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
PRINT N'Altering Procedure [dbo].[USP_Login]...';


GO
ALTER PROCEDURE [dbo].[USP_Login]
	@UserName varchar(255),
	@Password varchar(255)
AS
BEGIN
	SELECT Id, [Role],Varified from Users where [Login Name]=@UserName and Password=@Password
	union SELECT Id, [Role],Varified from Users where [Screen Name]=@UserName and Password=@Password
END
GO
PRINT N'Altering Procedure [dbo].[USP_UpdateSettings]...';


GO
ALTER PROCEDURE [dbo].[USP_UpdateSettings]
	@ID int,
	@optin bit, 
	@password varchar(max),
	@Anon bit,
	@email varchar(max)=null
AS
	update Users
	set RandOpt=@optin, Password=@password, AnonCmd=@Anon, [Login Name]=case when @email is null then [Login Name] else @email end
	where ID=@ID
GO
PRINT N'Creating Procedure [dbo].[USP_AdminCommandLog]...';


GO
CREATE PROCEDURE [dbo].[USP_AdminCommandLog]
AS
BEGIN
	Select isnull(u.[screen name],'Anon') Sender,
	isnull(us.[screen name],'Anon') Receiver,
	c.GroupRefId ,
	convert(varchar(max),'') as Decrypt ,
	c.Content
	from dbo.CmdLog c 
	left join users u on c.senderid=u.id 
	left join users su on c.SubId=su.id 
	order by c.Id
END
GO
PRINT N'Creating Procedure [dbo].[Usp_checkuser]...';


GO
CREATE PROCEDURE [dbo].[Usp_checkuser]
	@ID int
AS
	select case when vr.SubID IS NULL then u.Varified else -1 end as Varified from Users u
	left join VarificationReq vr
	on vr.SubID=u.Id
	where u.ID = @ID
RETURN 0
GO
PRINT N'Creating Procedure [dbo].[usp_requestvar]...';


GO
CREATE PROCEDURE [dbo].[usp_requestvar]
	@ID int 
AS
BEGIN
	
	declare @counting int

	select @counting=count(*) from VarificationReq where SubID=@ID
	if @counting=0
	BEGIN
		insert into VarificationReq
		([SubID])
		values
		(@ID)
	END
	select [Login Name],VarifiedCode from Users where ID = @ID

END
GO
PRINT N'Creating Procedure [dbo].[usp_varify]...';


GO
CREATE PROCEDURE [dbo].[usp_varify]
	@ID int,
	@varcode int
AS
BEGIN

	declare @tries int
	select @tries=Count from VarificationReq where SubID=@ID

	if @tries<=10
	BEGIN
		update VarificationReq
		set VarificationCode=@varcode
		where SubID=@ID

		declare @counting int
		select @counting=count(*) from Users u join VarificationReq vr on u.Id=vr.SubID and u.VarifiedCode=vr.VarificationCode

		if @counting=1
		BEGIN
			delete from VarificationReq where SubID=@ID
			update Users set Varified=1 where Id=@ID
			select 'Done'
		END
		else
		BEGIN
			UPDATE VarificationReq
			SET Count=Count+1
			where SubID=@ID
			select 'Incorrect'
		END
	END
	ELSE
	BEGIN
		select 'Too many tried'
	END
END
GO
PRINT N'Update complete.';


GO
