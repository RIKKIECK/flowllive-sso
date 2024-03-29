USE [SSOv2]
GO
/****** Object:  Table [dbo].[ActivityLog]    Script Date: 27-07-2018 11:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog](
	[id] [uniqueidentifier] NOT NULL,
	[action] [nvarchar](250) NULL,
	[description] [varchar](1000) NULL,
	[active] [bit] NOT NULL,
	[creationDate] [datetime] NOT NULL,
	[userName] [nvarchar](250) NOT NULL,
	[userLastName] [nvarchar](250) NULL,
 CONSTRAINT [PK__Log__3213E83F3E673506] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessPosition]    Script Date: 27-07-2018 11:24:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessPosition](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](50) NULL,
	[active] [bit] NOT NULL,
	[creationDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 27-07-2018 11:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](50) NULL,
	[active] [bit] NOT NULL,
	[creationDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 27-07-2018 11:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](50) NULL,
	[active] [bit] NOT NULL,
	[creationDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 27-07-2018 11:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[id] [uniqueidentifier] NOT NULL,
	[active] [bit] NULL,
	[creationDate] [datetime] NULL,
	[name] [nvarchar](250) NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 27-07-2018 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[id] [uniqueidentifier] NOT NULL,
	[creationDate] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[name] [varchar](50) NULL,
	[description] [varchar](500) NULL,
 CONSTRAINT [PK__Rol__3213E83FDE41677F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolPermissions]    Script Date: 27-07-2018 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolPermissions](
	[id] [uniqueidentifier] NOT NULL,
	[active] [bit] NULL,
	[creationDate] [datetime] NULL,
	[rolId] [uniqueidentifier] NULL,
	[permissionsId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RolPermissions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCompany]    Script Date: 27-07-2018 11:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCompany](
	[id] [uniqueidentifier] NOT NULL,
	[creationDate] [datetime] NULL,
	[active] [bit] NULL,
	[name] [varchar](250) NULL,
	[companyId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SubCompany] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscription]    Script Date: 27-07-2018 11:24:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscription](
	[id] [uniqueidentifier] NOT NULL,
	[creationDate] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[companyId] [uniqueidentifier] NOT NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
	[name] [varchar](250) NOT NULL,
	[description] [varchar](250) NOT NULL,
	[ammount] [int] NULL,
	[usersQuantity] [int] NULL,
 CONSTRAINT [PK__Subscrip__3213E83F8CB15101] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 27-07-2018 11:24:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](50) NULL,
	[lastName] [varchar](50) NULL,
	[birthdate] [date] NULL,
	[phone] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[hash] [binary](64) NULL,
	[salt] [binary](16) NULL,
	[imageUrl] [varchar](250) NULL,
	[lastConn] [datetime] NULL,
	[active] [bit] NOT NULL,
	[creationDate] [datetime] NULL,
	[countryId] [uniqueidentifier] NOT NULL,
	[subCompanyId] [uniqueidentifier] NOT NULL,
	[rolId] [uniqueidentifier] NULL,
 CONSTRAINT [PK__User__3213E83F899E0B18] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBP]    Script Date: 27-07-2018 11:24:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBP](
	[id] [uniqueidentifier] NOT NULL,
	[bpId] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK__UserBP__3213E83F07BFE8BA] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActivityLog] ADD  CONSTRAINT [DF__Log__id__59063A47]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[ActivityLog] ADD  CONSTRAINT [DF__Log__active__59FA5E80]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[ActivityLog] ADD  CONSTRAINT [DF__Log__creationDat__5AEE82B9]  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[BusinessPosition] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[BusinessPosition] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[BusinessPosition] ADD  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[Permissions] ADD  CONSTRAINT [DF_Permissions_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Permissions] ADD  CONSTRAINT [DF_Permissions_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Permissions] ADD  CONSTRAINT [DF_Permissions_creationDate]  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[Rol] ADD  CONSTRAINT [DF__Rol__creationDat__59FA5E80]  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[Rol] ADD  CONSTRAINT [DF__Rol__active__59063A47]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[RolPermissions] ADD  CONSTRAINT [DF_RolPermissions_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[RolPermissions] ADD  CONSTRAINT [DF_RolPermissions_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[RolPermissions] ADD  CONSTRAINT [DF_RolPermissions_creationDate]  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[SubCompany] ADD  CONSTRAINT [DF_SubCompany_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[SubCompany] ADD  CONSTRAINT [DF_SubCompany_creationDate]  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[SubCompany] ADD  CONSTRAINT [DF_SubCompany_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Subscription] ADD  CONSTRAINT [DF__Subscript__creat__73BA3083]  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[Subscription] ADD  CONSTRAINT [DF__Subscript__activ__72C60C4A]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_lastConn]  DEFAULT (getdate()) FOR [lastConn]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__active__7E37BEF6]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__creationDa__7F2BE32F]  DEFAULT (getdate()) FOR [creationDate]
GO
ALTER TABLE [dbo].[UserBP] ADD  CONSTRAINT [DF__UserBP__id__6754599E]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[UserBP] ADD  CONSTRAINT [DF_UserBP_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[RolPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolPermissions_Permissions] FOREIGN KEY([permissionsId])
REFERENCES [dbo].[Permissions] ([id])
GO
ALTER TABLE [dbo].[RolPermissions] CHECK CONSTRAINT [FK_RolPermissions_Permissions]
GO
ALTER TABLE [dbo].[RolPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolPermissions_Rol] FOREIGN KEY([rolId])
REFERENCES [dbo].[Rol] ([id])
GO
ALTER TABLE [dbo].[RolPermissions] CHECK CONSTRAINT [FK_RolPermissions_Rol]
GO
ALTER TABLE [dbo].[SubCompany]  WITH CHECK ADD  CONSTRAINT [FK_SubCompany_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[SubCompany] CHECK CONSTRAINT [FK_SubCompany_Company]
GO
ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[Subscription] CHECK CONSTRAINT [FK_Subscription_Company]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__countryId__00200768] FOREIGN KEY([countryId])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__countryId__00200768]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Rol] FOREIGN KEY([rolId])
REFERENCES [dbo].[Rol] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Rol]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_SubCompany] FOREIGN KEY([subCompanyId])
REFERENCES [dbo].[SubCompany] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_SubCompany]
GO
ALTER TABLE [dbo].[UserBP]  WITH CHECK ADD  CONSTRAINT [FK__UserBP__bpId__70DDC3D8] FOREIGN KEY([bpId])
REFERENCES [dbo].[BusinessPosition] ([id])
GO
ALTER TABLE [dbo].[UserBP] CHECK CONSTRAINT [FK__UserBP__bpId__70DDC3D8]
GO
ALTER TABLE [dbo].[UserBP]  WITH CHECK ADD  CONSTRAINT [FK__UserBP__userId__0B91BA14] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[UserBP] CHECK CONSTRAINT [FK__UserBP__userId__0B91BA14]
GO
/****** Object:  StoredProcedure [dbo].[ActivityLog_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[ActivityLog_Add]
	(
		@userName nvarchar(250)
		, @userLastName nvarchar(250)
		, @action nvarchar(250)
		, @description nvarchar(1000)
	)
AS
BEGIN
	INSERT INTO ActivityLog (userName,userLastName,[action], [description])
	VALUES (@userName, @userLastName, @action, @description)
END
GO
/****** Object:  StoredProcedure [dbo].[ActivityLog_Get]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[ActivityLog_Get]
	
AS
BEGIN
	select * from ActivityLog
	order by creationDate desc

END
GO
/****** Object:  StoredProcedure [dbo].[BusinessPosition_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 27-12-2017
-- Description:	agrega un bp a un usuario
-- =============================================
CREATE PROCEDURE [dbo].[BusinessPosition_Add]
	(
		@user_id nvarchar(250),
		@bp_id nvarchar(250)
	)
AS
BEGIN
	INSERT INTO UserBP (bpId,userId)
	VALUES(@bp_id,@user_id)
END
GO
/****** Object:  StoredProcedure [dbo].[BusinessPosition_Desactive]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 27-12-2017
-- Description:	desactive un BP de usuario
-- =============================================
CREATE PROCEDURE [dbo].[BusinessPosition_Desactive]
	(
		@user_id nvarchar(250),
		@bp_id nvarchar(250)
	)
AS
BEGIN
	UPDATE UserBP
	SET active = 0
	WHERE userId = @user_id
	AND bpId = @bp_id
END
GO
/****** Object:  StoredProcedure [dbo].[BusinessPosition_Get_All]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 27-12-2017
-- Description:	retorna todos los BP registrados
-- =============================================
CREATE PROCEDURE [dbo].[BusinessPosition_Get_All]
	
AS
BEGIN
	SELECT	id,name,creationDate,active
	FROM BusinessPosition 

END
GO
/****** Object:  StoredProcedure [dbo].[BusinessPosition_Get_By_User]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 27-12-2017
-- Description:	retorna todos los BP de un usuario
-- =============================================
CREATE PROCEDURE [dbo].[BusinessPosition_Get_By_User]
	(@id nvarchar(250))
AS
BEGIN
	SELECT	bp.id,
			bp.name,
			bp.creationDate,
			bp.active
	FROM UserBP ubp INNER JOIN BusinessPosition bp
		ON ubp.bpId = bp.id
	WHERE ubp.userId = @id
	AND ubp.active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 14-12-2017
-- Description:	agrega un registro a la tabla company
-- =============================================
CREATE PROCEDURE [dbo].[Company_Add]
	(
		@name varchar(250)
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
--
	IF EXISTS (SELECT * FROM Company WHERE name = @name ) 
	BEGIN
	   SELECT 'EXIST' as id
	END
	ELSE
	BEGIN
		declare @description nvarchar(1000)
		set @description = 'Ha agregado la Empresa ' + @name
		exec ActivityLog_Add @userName,@userLastName, 'INSERT Company', @description
		
		declare @id uniqueidentifier
		set @id = NEWID()
		INSERT INTO Company (id,name)
		VALUES (@id, @name)
		select @id as id
	END
--
	
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Delete]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 14-12-2017
-- Description:	desactiva un registro
-- =============================================
CREATE PROCEDURE [dbo].[Company_Delete]
	(
		@id nvarchar(250)
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	declare @description nvarchar(1000)
	set @description = CONCAT( 'Ha Eliminado la Empresa ',(SELECT [name] FROM Company where id = @id ),'.')

	exec ActivityLog_Add @userName,@userLastName, 'DELETE Company', @description

	UPDATE Company
	SET active = 0
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Get]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 14-12-2017
-- Description:	retorna todas las compañias
-- =============================================
CREATE PROCEDURE [dbo].[Company_Get]
	
AS
BEGIN
	SELECT	id,
			name,
			creationDate,
			active
	FROM Company
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Get_By_Id]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 22-12-2017
-- Description:	retorna una company segun su id
-- =============================================
CREATE PROCEDURE [dbo].[Company_Get_By_Id]
	(@id nvarchar(250))
AS
BEGIN
	SELECT	id
			, creationDate
			, active
			, name
	FROM Company
	WHERE id = @id
	and active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Update]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Company_Update]
	(
		@id nvarchar(250)
		, @name nvarchar(250)
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	DECLARE @old Table
	(
		id uniqueidentifier
		, name nvarchar(50)
		, active bit
		, creationDate datetime
	)

	insert into @old select * from Company where id = @id 

	declare @oldData nvarchar(500)
	declare @newData nvarchar(500)
	set @oldData =  CONCAT(  
					'[nombre= '			, (select name from @old) ,
					']'
					)
	set @newData =  CONCAT(
					'[nombre= '			, @name ,
					']'
					)
	declare @description nvarchar(1000)
	set @description = CONCAT('Se ha modificado una empresa de ',@oldData,' a ',@newData)
	exec ActivityLog_Add @userName,@userLastName, 'UPDATE Expenses', @description
	

	UPDATE Company
	SET name = @name
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Country_Get_All]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 26-12-2017
-- Description:	obtiene todos los paises
-- =============================================
CREATE PROCEDURE [dbo].[Country_Get_All]
	
AS
BEGIN
	SELECT	id, name, active, creationDate
	FROM Country
END
GO
/****** Object:  StoredProcedure [dbo].[Permission_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 08-02-2018
-- Description:	agrega un permiso al rol especificado
-- =============================================
CREATE PROCEDURE [dbo].[Permission_Add]
	(
		@rolId uniqueidentifier
		, @permissionsId uniqueidentifier
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	declare @rolname nvarchar(250)
	declare @permissionName nvarchar(250)
	declare @description nvarchar(500)
	SET @rolname = (select [name] from Rol where id = @rolId)
	SET @permissionName = (select [name] from [Permissions] where id = @permissionsId )
	set @description =CONCAT( 'Ha agregado el permiso ', @permissionName,' al rol ',@rolname,'.')
	exec ActivityLog_Add @userName,@userLastName, 'INSERT Permission', @description


	INSERT INTO RolPermissions (rolId,permissionsId)
	VALUES (@rolId,@permissionsId)
	SELECT 'OK'
END
GO
/****** Object:  StoredProcedure [dbo].[Permissions_Delete_All_By_Rol]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 08-02-2018
-- Description:	elimina toda relacion que posea el rol especificado con permisos
-- =============================================
CREATE PROCEDURE [dbo].[Permissions_Delete_All_By_Rol]
	(
		
		@rolId uniqueidentifier
	)
AS
BEGIN
	DELETE FROM RolPermissions
	WHERE id IN 
		(
			SELECT rp.id AS id 
			FROM	   RolPermissions rp
			INNER JOIN Rol r	      		
				ON r.id = rp.rolId
			WHERE r.id = @rolId
		)
	SELECT @@ROWCOUNT AS DELETED;
END
GO
/****** Object:  StoredProcedure [dbo].[Permissions_Get_All]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Permissions_Get_All]
	
AS
BEGIN
	select * from [Permissions] where active = 1 order by name
END
GO
/****** Object:  StoredProcedure [dbo].[Permissions_Get_By_Rol]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Permissions_Get_By_Rol]
	(@idRol uniqueidentifier)
AS
BEGIN
	SELECT	per.id as id,
			per.creationDate as creationDate,
			per.active as active,
			per.name as 'name'
	FROM RolPermissions rp INNER JOIN Rol r
		ON rp.rolId = r.id INNER JOIN [Permissions] per
			ON per.id = rp.permissionsId
	WHERE rp.active = 1
	AND rp.rolId = @idRol
	order by name

END
GO
/****** Object:  StoredProcedure [dbo].[Rol_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Rol_Add]
(
	@name varchar(250),
	@userName varchar(250),
	@userLastName varchar(250)
)
AS
BEGIN
	declare @id uniqueidentifier
	set @id = NEWID()
	
	declare @description nvarchar(500)
	set @description = CONCAT( 'Ha agregado el rol ', @name,'.')
	exec ActivityLog_Add @userName,@userLastName, 'INSERT Rol', @description

	insert into Rol (id,name) values (@id,@name)

	select @id as id

END
GO
/****** Object:  StoredProcedure [dbo].[Rol_Desactive]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Rol_Desactive]
(
	@id uniqueidentifier
)
AS
BEGIN
	update Rol SET
	active = 0
	where id = @id

	select 'ok' as id
END
GO
/****** Object:  StoredProcedure [dbo].[Rol_Get]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 09/01/2018
-- Description:	retorna una lista con todos los roles diponibles
-- =============================================
CREATE PROCEDURE [dbo].[Rol_Get]
	
AS
BEGIN
	select * from Rol
	where active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Rol_Get_By_Id]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Rol_Get_By_Id]
(
	@id uniqueidentifier
)
AS
BEGIN
	SELECT	r.id
			, r.creationDate
			, r.active
			, r.name
	FROM Rol r
	WHERE r.id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Rol_Get_By_User]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Rol_Get_By_User]
(
	@userId uniqueidentifier
)
AS
BEGIN
	SELECT	Rol.id
			, Rol.creationDate
			, Rol.active
			, Rol.name
			, [Permissions].id as Permissions_id
			, [Permissions].creationDate as Permissions_creationDate
			, [Permissions].active as Permissions_active
			, [Permissions].name as Permissions_name
	FROM Rol
	INNER JOIN RolPermissions	ON Rol.id = RolPermissions.rolId
	INNER JOIN [Permissions]	ON RolPermissions.permissionsId =  [Permissions].id
	INNER JOIN [User]			ON [User].rolId = Rol.id
	WHERE [User].id = @userId
	--AND rper.active = 1
	--AND permi.active = 1
	--AND Rol.active = 1
	order by name
END
GO
/****** Object:  StoredProcedure [dbo].[Rol_Update]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Rol_Update]
(
	@id uniqueidentifier
	, @name varchar(250)
)
AS
BEGIN
	update Rol set
	name = @name
	where id = @id

	select 'ok' as id
END
GO
/****** Object:  StoredProcedure [dbo].[RolPermission_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RolPermission_Add]
(
	@rolId uniqueidentifier
	, @permissionId uniqueidentifier
)
AS
BEGIN
	declare @exist int
	set @exist = (select COUNT(*) FROM RolPermissions where rolId = @rolId and permissionsId = @permissionId)
	if(@exist<1)
	begin
		INSERT INTO RolPermissions (rolId,permissionsId) values (@rolId,@permissionId)

		select 'ok' as id
	end
	else
	begin
	select 'ok' as id
	end

END
GO
/****** Object:  StoredProcedure [dbo].[RolPermission_Delete]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RolPermission_Delete]
(
	@rolId uniqueidentifier
	, @permissionsId uniqueidentifier
)
AS
BEGIN
	delete RolPermissions
	where rolId = @rolId
	and permissionsId = @permissionsId

	select 'ok' as id
END
GO
/****** Object:  StoredProcedure [dbo].[SubCompany_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SubCompany_Add]
(
	@name varchar(250)
	, @companyId uniqueidentifier
	, @userName varchar(250)
	, @userLastName varchar(250)
)
AS
BEGIN
	declare @description nvarchar(1000)
	set @description = concat( 'Ha agregado la SubEmpresa ', @name, ' a la Empresa ',(select [name] from Company where id = @companyId),'.' )
	exec ActivityLog_Add @userName,@userLastName, 'INSERT SubCompany', @description

	INSERT INTO SubCompany (name,companyId)
	VALUES (@name,@companyId)

	select 'ok' as id
END
GO
/****** Object:  StoredProcedure [dbo].[SubCompany_Desactive]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[SubCompany_Desactive]
(
	@id uniqueidentifier
	, @userName varchar(250)
	, @userLastName varchar(250)
)
AS
BEGIN
	declare @description nvarchar(1000)
	declare @oldName varchar(250)
	set @oldName = (select [name] from SubCompany where id = @id)
	set @description = concat( 'Ha Desactivado la SubEmpresa ',@oldName ,'.' )
	exec ActivityLog_Add @userName,@userLastName, 'DESACTIVE SubCompany', @description

	update SubCompany SET
	active = 0
	where id = @id

	select 'ok' as id
END
GO
/****** Object:  StoredProcedure [dbo].[SubCompany_Get_By_Company]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SubCompany_Get_By_Company]
(
	@id uniqueidentifier
)
AS
BEGIN
	SELECT	*
	FROM SubCompany sc
	where sc.companyId = @id
	and sc.active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[SubCompany_Update]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SubCompany_Update]
(
	@id uniqueidentifier
	, @name varchar(250)
	, @userName varchar(250)
	, @userLastName varchar(250)
)
AS
BEGIN
	declare @description nvarchar(1000)
	declare @oldName varchar(250)
	set @oldName = (select [name] from SubCompany where id = @id)
	set @description = concat( 'Ha Actualizado la SubEmpresa ',@oldName , ' a  ',@name,'.' )
	exec ActivityLog_Add @userName,@userLastName, 'UPDATE SubCompany', @description

	update SubCompany SET
	name = @name
	where id = @id

	select 'ok' as id
END
GO
/****** Object:  StoredProcedure [dbo].[Subscription_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 15-12-2017
-- Description:	genera un registro de subcripcion en la dB
-- =============================================
CREATE PROCEDURE [dbo].[Subscription_Add]
	(
		@companyId nvarchar(250)
		, @startDate datetime
		, @endDate datetime
		, @name varchar(250)
		, @description varchar(250)
		, @ammount float
		, @usersQuantity int
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	
	IF (( SELECT count(*) FROM Subscription where companyId = @companyId and active = 1 )>0)
		begin
			SELECT 'EXIST' as id
		end
	ELSE
		begin
			declare @descriptionlog nvarchar(500)
			set @descriptionlog = CONCAT('Ha agregado una subscripcion a la Empresa ',(select [name] from Company where id = @companyId), '.')
		
			exec ActivityLog_Add @userName,@userLastName, 'INSERT Subscription', @descriptionlog
			
			INSERT INTO Subscription (companyId,startDate,endDate,name,description,ammount,usersQuantity)
			VALUES (@companyId,@startDate,@endDate,@name,@description,@ammount,@usersQuantity)

			select 'ok' as id
		end
END
GO
/****** Object:  StoredProcedure [dbo].[Subscription_By_Id]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 14-12-2017
-- Description:	obtiene una subscripcion por su id
-- =============================================
CREATE PROCEDURE [dbo].[Subscription_By_Id]
	(@id nvarchar(250))
AS
BEGIN
	SELECT	s.id
			, s.creationDate
			, s.active
			, s.startDate
			, s.endDate
			, s.name
			, s.description
			, s.ammount
			, s.usersQuantity
			, comp.id as Company_id
			, comp.name as Company_name
			, comp.creationDate as Company_creationDate
			, comp.active as Company_active
	FROM Subscription s 
	INNER JOIN Company comp ON comp.id = s.companyId
	WHERE s.id = @id
	AND S.active = 1
	AND comp.active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Subscription_Desactive]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subscription_Desactive]
	(
		@id nvarchar(250)
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	declare @sub TABLE
	(
		companyId uniqueidentifier
	)
	insert into @sub select companyId from Subscription where id = @id

	declare @companyName nvarchar(250)
	declare @appname nvarchar(250)
	declare @description nvarchar(1000)
	set @companyName = (select [name] from Company c,@sub a where c.id = a.companyId)
	set @appname = (select [name] from [Application] ap,@sub a where ap.id = a.ApplicationId)
	
	set @description = CONCAT( 'Ha Eliminado la subscripción de ',@companyName,' a la aplicación ',@appname,'.')

	exec ActivityLog_Add @userName,@userLastName, 'DELETE Subscription', @description

	UPDATE Subscription
	SET active = 0
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Subscription_Get]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subscription_Get]
	
AS
BEGIN
	SELECT	s.id,
			s.endDate,
			s.startDate,
			s.active,
			s.creationDate,
			comp.id as Company_id,
			comp.name as Company_name,
			comp.creationDate as Company_creationDate,
			comp.active as Company_active
	FROM Subscription s 
	INNER JOIN Company comp ON comp.id = s.companyId
END
GO
/****** Object:  StoredProcedure [dbo].[Subscription_Get_By_Company]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 15-12-2017
-- Description:	obtiene todas lassubcripciones de una compañia
-- =============================================
CREATE PROCEDURE [dbo].[Subscription_Get_By_Company]
(
	@id nvarchar(250)
)
AS
BEGIN
	SELECT	s.id
			, s.creationDate
			, s.active
			, s.startDate
			, s.endDate
			, s.name
			, s.description
			, s.ammount
			, s.usersQuantity
			, comp.id as Company_id
			, comp.name as Company_name
			, comp.creationDate as Company_creationDate
			, comp.active as Company_active
	FROM Subscription s 
	INNER JOIN Company comp ON comp.id = s.companyId
	WHERE s.companyId = @id
	and s.active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Subscription_Update]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jose fuentes
-- Create date: 15-12-2017
-- Description:	actualiza un registro por su id
-- =============================================
CREATE PROCEDURE [dbo].[Subscription_Update]
	(
		@id nvarchar(250)
		, @startDate datetime
		, @endDate datetime
		, @name varchar(250)
		, @description varchar(250)
		, @ammount float
		, @usersQuantity int
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	DECLARE @old Table
	(
		id nvarchar(250)
		, startDate datetime
		, endDate datetime
		, name varchar(250)
		, description varchar(250)
		, ammount float
		, usersQuantity int
	)
	insert into @old select id,startDate,endDate,name,description,ammount,usersQuantity from Subscription where id = @id 

	declare @oldData nvarchar(500)
	declare @newData nvarchar(500)
	declare @compName nvarchar(250)
	set @compName = (select c.name from Subscription s inner join Company c on s.companyId = c.id WHERE s.id = @id)
	
	set @oldData =  CONCAT(  
					'[fecha inicio= '			, CONVERT(nvarchar(30),(select startDate from @old)) ,
					'[fecha termino= '			, CONVERT(nvarchar(30),(select endDate from @old)) ,
					'[Nombre = '				, (select name from @old) ,
					'[Descripcion = '			, (select description from @old) ,
					'[Monto = '					, convert(varchar(250), (select ammount from @old)) ,
					'[Cantidad de usuarios = '	, convert(varchar(250), (select usersQuantity from @old) ),
					']'
					)
	set @newData =  CONCAT(  
					'[fecha inicio= '			, CONVERT(nvarchar(30),@startDate) ,
					'[fecha termino= '			, CONVERT(nvarchar(30),@endDate) ,
					'[Nombre = '				, @name ,
					'[Descripcion = '			, @description ,
					'[Monto = '					, convert(varchar(250), @ammount) ,
					'[Cantidad de usuarios = '	, convert(varchar(250), @usersQuantity),
					']'
					)
	declare @descriptionlog nvarchar(1000)
	set @descriptionlog = CONCAT('Ha modificado la subscripcion de ',@compName,' de ',@oldData,' a ',@newData)
	exec ActivityLog_Add @userName,@userLastName, 'UPDATE Subscription', @descriptionlog


	--ACTUALIZACION DE LA SUBSCRIPCION
	UPDATE Subscription SET 
		startDate = @startDate,
		endDate = @endDate,
		name = @name,
		description = @description,
		ammount = @ammount,
		usersQuantity = @usersQuantity
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[User_Add]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_Add]
(
	@name nvarchar(250)
		, @lastname nvarchar(250)
		, @birthdate nvarchar(250)
		, @phone nvarchar(250)
		, @email nvarchar(250)
		, @imageUrl nvarchar(250)
		, @countryId nvarchar(250)
		, @subcompanyId nvarchar(250)
		, @salt binary(16)
		, @hash binary(64)
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
		, @rolId uniqueidentifier
)
AS
BEGIN
	declare @exist int
	set @exist = (select COUNT(id) from [User] where email = @email)

	if (@exist > 0 )
		begin
			select 'EXIST' AS id
		end
	else
		begin
			declare @description nvarchar(500)
			set @description = CONCAT( 'Ha agregado el Usuario ', @name,' ',@lastname,'.')
			exec ActivityLog_Add @userName,@userLastName, 'INSERT User', @description

			declare @id uniqueidentifier
			set @id = NEWID()
			insert into [User](id,name,lastName,birthdate,phone,email,imageUrl,countryId,subCompanyId,salt, hash,rolId)
			values (@id,@name,@lastname,@birthdate,@phone,@email,@imageUrl,@countryId,@subcompanyId,@salt,@hash,@rolId)
	
			
			select CONVERT(NVARCHAR(250),@id) as id
		end


END
GO
/****** Object:  StoredProcedure [dbo].[User_By_Email]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jose fuentes
-- Create date: 12-12-2017
-- Description:	consulta para obtener un usuario por su email
-- =============================================
CREATE PROCEDURE [dbo].[User_By_Email]
	(
		@email varchar(250)
	)
AS
BEGIN
	Select	id,
			name,
			lastName,
			hash,
			salt
	from [User]
	WHERE email = @email
	AND active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rodrigo Rojas
-- Create date: 08-01-2017
-- Description:	Desactiva un registro
-- =============================================
CREATE PROCEDURE [dbo].[User_Delete]
	(
		@id nvarchar(250)
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	
	declare @description nvarchar(1000)
	set @description = CONCAT( 'Ha Eliminado ',(SELECT CONCAT([name],' ',lastName) FROM [User] where id = @id ),'.')

	exec ActivityLog_Add @userName,@userLastName, 'DELETE User', @description

	UPDATE [dbo].[User]
	SET active = 0
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[User_Get_All]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Get_All]
	
AS
BEGIN
	SELECT	us.id,
			us.name,
			us.lastName,
			us.lastConn,
			us.imageUrl,
			us.email,
			us.birthdate,
			us.creationDate,
			us.phone,
			us.active,
			comp.id as Company_id,
			comp.name as Company_name,
			co.id as Country_id,
			co.name as Country_name
	FROM [User] us inner join SubCompany comp
		on us.subCompanyId = comp.id inner join Country co
			ON us.countryId = co.id
	order by [name] DESC
END
GO
/****** Object:  StoredProcedure [dbo].[User_Get_By_Company]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Get_By_Company]
	(@id nvarchar(250))
AS
BEGIN
	SELECT	us.id,
			us.name,
			us.lastName,
			us.lastConn,
			us.imageUrl,
			us.email,
			us.birthdate,
			us.creationDate,
			us.phone,
			us.active,
			comp.id as Company_id,
			comp.name as Company_name,
			co.id as Country_id,
			co.name as Country_name
	FROM [User] us 
	inner join SubCompany comp on us.subCompanyId = comp.id 
	inner join Country co ON us.countryId = co.id
	WHERE us.subCompanyId = @id
	AND us.active = 1
	ORDER BY creationDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[User_Get_By_Id]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		josefuentes
-- Create date: 14-12-2017
-- Description:	obtiene un usuario por su ID
-- Modifications: 
--				21-12-2017 (se especifica mas el sp para obtener datos de pais y empresa)
-- =============================================
CREATE PROCEDURE [dbo].[User_Get_By_Id]
	(@id nvarchar(250))
AS
BEGIN
		Select	us.id,
			us.name,
			us.lastName,
			us.birthdate,
			us.phone,
			us.email,
			us.imageUrl,
			us.lastConn,
			us.creationDate,
			us.countryId,
			us.subCompanyId,
			us.active,
			ctr.id as country_id,
			ctr.name as country_name,
			ctr.creationDate as country_creationDate,
			ctr.active as country_active,
			SubCompany.id as SubCompany_id,
			SubCompany.name as SubCompany_name,
			SubCompany.creationDate as SubCompany_creationDate,
			SubCompany.active as SubCompany_active
	From [User] us INNER JOIN Country ctr
		ON us.countryId = ctr.id INNER JOIN SubCompany
			ON us.subCompanyId = SubCompany.id
	WHERE us.id = @id
	AND us.active = 1

END
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Update]
	(
		@id uniqueidentifier
		, @name nvarchar(50)
		, @lastName nvarchar(50)
		, @birthdate datetime
		, @phone nvarchar(250)
		, @email nvarchar(250)
		, @imageUrl nvarchar(250)
		, @subCompanyId uniqueidentifier
		, @countryId uniqueidentifier
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
		, @rolId uniqueidentifier
	)
AS
BEGIN
	DECLARE @old Table (
		id uniqueidentifier
		, name nvarchar(50)
		, lastName nvarchar(50)
		, birthdate datetime
		, phone nvarchar(50)
		, email nvarchar(50)
		, [hash] binary(64)
		, salt binary(16)
		, imageUrl nvarchar(250)
		, lastConn datetime
		, active bit
		, creationDate datetime
		, countryId uniqueidentifier
		, subCompanyId uniqueidentifier
		, rolId uniqueidentifier
	)
	insert into @old select * from [User] where id = @id 

	declare @oldData nvarchar(500)
	declare @newData nvarchar(500)
	set @oldData =  CONCAT(  
					'[nombre= '				, (select name from @old) ,
					', apellido= '			, (select lastName from @old) , 
					', fecha nacimiento= '	, convert (nvarchar(30),(select birthdate from @old)) , 
					', telefono= '			, (select phone from @old) , 
					', correo= '			,(select email from @old) , 
					', Url de Imagen= '		, (select imageUrl from @old),
					', Rol = '				, (select Rol.name from Rol where id = (select rolId from @old)) 
					,']'
					)
	set @newData =  CONCAT(
					'[nombre= '				, @name ,
					', apellido= '			, @lastname , 
					', fecha nacimiento= '	, convert (nvarchar(30),@birthdate) , 
					', telefono= '			, @phone , 
					', correo= '			, @email , 
					', Url de Imagen= '		, @imageUrl ,
					', Rol= '				, (select name from Rol where id = @rolId)
					,']'
					)
	declare @description nvarchar(1000)
	set @description = CONCAT('Ha modificado un usuario de ',@oldData,' a ',@newData)
	exec ActivityLog_Add @userName,@userLastName, 'UPDATE User', @description
	UPDATE [dbo].[User]
	SET 
		name = @name,
		lastName = @lastname,
		birthdate = @birthdate,
		phone = @phone,
		email = @email,
		imageUrl = @imageUrl,
		countryId = @countryId,
		subCompanyId = subCompanyId,
		rolId = @rolId
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[User_Update_Data_Pass]    Script Date: 27-07-2018 11:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Update_Data_Pass]
	(
		@id uniqueidentifier
		, @hash binary(64)
		, @salt binary(16)
		, @userName nvarchar(250)
		, @userLastName nvarchar(250)
	)
AS
BEGIN
	DECLARE @old Table (
		id uniqueidentifier
		, name nvarchar(50)
		, lastName varchar (250)
	)
	insert into @old select id,name,lastName from [User] where id = @id 

	declare @description nvarchar(1000)
	set @description = CONCAT('Ha modificado la contraseña de ',(select CONCAT([name],' ',lastName,' ') from @old))
	exec ActivityLog_Add @userName,@userLastName, 'UPDATE User', @description


	UPDATE [User] 
	set [hash] = @hash,
		[salt] = @salt
	where id = @id
END
GO
