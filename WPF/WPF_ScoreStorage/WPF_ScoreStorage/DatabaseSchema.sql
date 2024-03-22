USE [master]
GO
CREATE DATABASE [ScoreStorage]
GO
ALTER DATABASE [ScoreStorage] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ScoreStorage] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ScoreStorage] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ScoreStorage] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ScoreStorage] SET ARITHABORT OFF 
GO
ALTER DATABASE [ScoreStorage] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ScoreStorage] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ScoreStorage] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ScoreStorage] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ScoreStorage] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ScoreStorage] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ScoreStorage] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ScoreStorage] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ScoreStorage] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ScoreStorage] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ScoreStorage] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ScoreStorage] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ScoreStorage] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ScoreStorage] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ScoreStorage] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ScoreStorage] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ScoreStorage] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ScoreStorage] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ScoreStorage] SET  MULTI_USER 
GO
ALTER DATABASE [ScoreStorage] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ScoreStorage] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ScoreStorage] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ScoreStorage] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ScoreStorage] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ScoreStorage] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ScoreStorage] SET QUERY_STORE = ON
GO
ALTER DATABASE [ScoreStorage] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ScoreStorage]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[Id] [nvarchar](255) NOT NULL,
	[ProvinceCode] [nvarchar](255) NULL,
	[ProvinceName] [nvarchar](255) NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolYear](
	[Id] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ExamYear] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [nvarchar](255) NOT NULL,
	[StudentCode] [nvarchar](50) NOT NULL,
	[SchoolYearId] [nvarchar](255) NULL,
	[Math] decimal(4,2) NULL,
	[Literature] decimal(4,2) NULL,
	[Physics] decimal(4,2) NULL,
	[Biology] decimal(4,2) NULL,
	[English] decimal(4,2) NULL,
	[Chemistry] decimal(4,2) NULL,
	[History] decimal(4,2) NULL,
	[Geography] decimal(4,2) NULL,
	[Civic] decimal(4,2) NULL,
	[ProvinceId] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Student]  WITH NOCHECK ADD FOREIGN KEY([SchoolYearId])
REFERENCES [dbo].[SchoolYear] ([Id])
GO
ALTER TABLE [dbo].[Student]  WITH NOCHECK ADD  CONSTRAINT [FK_Student_Province] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Province]
GO
USE [master]
GO
ALTER DATABASE [ScoreStorage] SET  READ_WRITE 
GO
