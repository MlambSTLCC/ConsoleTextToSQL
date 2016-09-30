USE [FoodShop]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 4/22/2015 5:21:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NULL,
	[StoreID] [int] NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[Gender] [nchar](10) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nchar](10) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Email] [nvarchar](max) NULL,
	[HomePhone] [nchar](10) NULL,
	[CellPhone] [nvarchar](50) NULL,
	[HireDate] [nvarchar](50) NULL,
	[IsHispanic] [bit] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

USE [FoodShop]
GO

/****** Object:  Table [dbo].[Jobs]    Script Date: 4/22/2015 5:22:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Jobs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobName] [nvarchar](50) NULL,
	[BaseRate] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [FoodShop]
GO

/****** Object:  Table [dbo].[Stores]    Script Date: 4/22/2015 5:22:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ManagerID] [int] NULL,
	[StoreName] [nvarchar](50) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nchar](10) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Create View [dbo].[EmployeeView] As

SELECT Employees.ID
	,Employees.LastName
	,Employees.FirstName
	,Employees.Gender
	,Employees.Address1
	,Employees.Address2
	,Employees.City
	,Employees.[STATE]
	,Employees.ZipCode
	,Employees.Email
	,Employees.HomePhone
	,Employees.CellPhone
	,Employees.HireDate
	,Employees.IsHispanic
	,Employees.StoreID
	,Stores.StoreName
	,Employees.JobID
	,Jobs.JobName
FROM Employees
LEFT OUTER JOIN Jobs ON Employees.JobID = Jobs.ID
LEFT OUTER JOIN Stores ON Employees.StoreID = Stores.ID

GO


