
USE [master]
GO
/****** Object:  Database [BLOG]    Script Date: 13/02/2023 18:48:25 ******/
CREATE DATABASE [BLOG]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BLOG', FILENAME = N'D:\BLOG.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BLOG_log', FILENAME = N'D:\BLOG_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BLOG] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BLOG].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BLOG] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BLOG] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BLOG] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BLOG] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BLOG] SET ARITHABORT OFF 
GO
ALTER DATABASE [BLOG] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BLOG] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BLOG] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BLOG] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BLOG] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BLOG] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BLOG] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BLOG] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BLOG] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BLOG] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BLOG] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BLOG] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BLOG] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BLOG] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BLOG] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BLOG] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BLOG] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BLOG] SET RECOVERY FULL 
GO
ALTER DATABASE [BLOG] SET  MULTI_USER 
GO
ALTER DATABASE [BLOG] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BLOG] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BLOG] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BLOG] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BLOG] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BLOG', N'ON'
GO
ALTER DATABASE [BLOG] SET QUERY_STORE = OFF
GO
USE [BLOG]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [BLOG]
GO
/****** Object:  Table [dbo].[comment]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[postid] [int] NOT NULL,
	[description] [nchar](100) NULL,
	[ispublic] [nchar](1) NULL,
	[creation_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[writerid] [int] NOT NULL,
	[editorid] [int] NOT NULL,
	[title] [nchar](100) NOT NULL,
	[description] [text] NOT NULL,
	[status] [nchar](10) NOT NULL,
	[submitted] [nchar](1) NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[last_update_date] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](30) NOT NULL,
	[password] [varchar](15) NOT NULL,
	[role] [varchar](10) NOT NULL,
	[locked] [varchar](1) NOT NULL,
	[last_update_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[writereditor]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[writereditor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[writerid] [int] NOT NULL,
	[editorid] [int] NOT NULL,
	[last_update_date] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_addcomment]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_addcomment](@userid int,@postid int,@description nchar(100),@ispublic nchar(1))
AS
BEGIN
insert into dbo.comment values(@userid,@postid,@description,@ispublic,getdate());


END
GO
/****** Object:  StoredProcedure [dbo].[sp_addpost]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_addpost](@writerid int,@editorid int,@title nchar(100),@description text )
AS
BEGIN


IF EXISTS (SELECT * FROM dbo.[user] WHERE role ='writer' and id=@writerid) 
BEGIN
  insert into dbo.post values(@writerid,@editorid,@title,@description,'DR','N',getdate(),getdate());
  SELECT '00'
END
ELSE
BEGIN
   SELECT '01'
END

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getpost]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_getpost](@postid int)
AS
BEGIN
SELECT * FROM dbo.post where id=@postid ;


END
GO
/****** Object:  StoredProcedure [dbo].[sp_listallposts]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listallposts]
AS
BEGIN
SELECT * FROM dbo.post 
where status='PU'
order by 8 desc;


END
GO
/****** Object:  StoredProcedure [dbo].[sp_listeditorposts]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listeditorposts](@editorname varchar(30))
AS
BEGIN

IF EXISTS (SELECT * FROM dbo.[user] WHERE role ='editor' and username=@editorname) 
BEGIN
SELECT * FROM dbo.post A 
INNER JOIN dbo.[user] B on A.editorid=B.id
AND B.username=@editorname
WHERE [status]='PA' and submitted='Y' 
order by 8 desc;
END


END
GO
/****** Object:  StoredProcedure [dbo].[sp_listownposts]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listownposts](@writername varchar(30))
AS
BEGIN

IF EXISTS (SELECT * FROM dbo.[user] WHERE role ='writer' and username=@writername) 
BEGIN
  SELECT A.* FROM dbo.post A 
  INNER JOIN dbo.[user] B on A.writerid=B.id
  where B.username=@writername
   order by 8 desc;
  
END

END
GO

/****** Object:  StoredProcedure [dbo].[sp_publishpost]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_publishpost](@postid int)
AS
BEGIN

--IF NOT EXISTS (SELECT * FROM dbo.post where id=@postid and (status='PA' OR status='PU' OR submitted='Y'))

update dbo.post set  status='PU' ,[last_update_date]=GETDATE() where id=@postid;




END
GO
/****** Object:  StoredProcedure [dbo].[sp_rejectpost]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rejectpost](@postid int)
AS
BEGIN

--IF NOT EXISTS (SELECT * FROM dbo.post where id=@postid and (status='PA' OR status='PU' OR submitted='Y'))

update dbo.post set submitted= 'N', status='RE' ,[last_update_date]=GETDATE() where id=@postid;




END
GO
/****** Object:  StoredProcedure [dbo].[sp_submitpost]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_submitpost](@postid int)
AS
BEGIN

IF NOT EXISTS (SELECT * FROM dbo.post where id=@postid and (status='PA' OR status='PU' OR submitted='Y'))
BEGIN
update dbo.post set submitted= 'Y', status='PA' ,[last_update_date]=GETDATE() where id=@postid;
 SELECT '00'
END
ELSE
BEGIN
   SELECT '01'
END



END
GO
/****** Object:  StoredProcedure [dbo].[sp_updatepost]    Script Date: 13/02/2023 18:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_updatepost](@writerid int,@postid int,@title nchar(100),@description text )
AS
BEGIN

IF EXISTS (SELECT * FROM dbo.[user] WHERE role ='writer' and id=@writerid) 
AND NOT EXISTS (SELECT * FROM dbo.post where id=@postid and (status='PA' OR status='PU' OR submitted='Y'))
BEGIN
update dbo.post set title= @title, [description]=@description,[last_update_date]=GETDATE() where id=@postid;
 SELECT '00'
END
ELSE
BEGIN
   SELECT '01'
END



END
GO

USE [BLOG]
INSERT INTO [dbo].[user] ([username] ,[password],[role],[locked],[last_update_date])
     VALUES ( 'fidiaquez@gmail.com','','writer', 'N', '10-02-2023') 
GO
	 INSERT INTO [dbo].[user] ([username] ,[password],[role],[locked],[last_update_date])
     VALUES ( 'gorwell@gmail.com','','editor', 'N', '10-02-2023')
GO
	 INSERT INTO [dbo].[user] ([username] ,[password],[role],[locked],[last_update_date])
     VALUES ( 'eallanpoe@gmail.com','','writer', 'N', '10-02-2023')
GO

USE [master]
GO
ALTER DATABASE [BLOG] SET  READ_WRITE 
GO


