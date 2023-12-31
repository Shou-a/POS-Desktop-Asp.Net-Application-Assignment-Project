USE [master]
GO
/****** Object:  Database [my_db]    Script Date: 7/8/2023 3:19:18 AM ******/
CREATE DATABASE [my_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'my_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\my_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'my_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\my_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [my_db] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [my_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [my_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [my_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [my_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [my_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [my_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [my_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [my_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [my_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [my_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [my_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [my_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [my_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [my_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [my_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [my_db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [my_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [my_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [my_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [my_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [my_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [my_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [my_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [my_db] SET RECOVERY FULL 
GO
ALTER DATABASE [my_db] SET  MULTI_USER 
GO
ALTER DATABASE [my_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [my_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [my_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [my_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [my_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [my_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'my_db', N'ON'
GO
ALTER DATABASE [my_db] SET QUERY_STORE = OFF
GO
USE [my_db]
GO
/****** Object:  Table [dbo].[discounts]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[discounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](500) NOT NULL,
	[value] [int] NOT NULL,
	[fromDate] [datetime] NULL,
	[toDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ingredients]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](500) NOT NULL,
	[unit] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[inventory_log]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inventory_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[cost] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[my_tbl]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[my_tbl](
	[id] [int] NOT NULL,
	[varchar_column1] [varchar](50) NULL,
	[varchar_column2] [varchar](50) NULL,
	[varchar_column3] [varchar](50) NULL,
	[int_column1] [int] NULL,
	[int_column2] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_details]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[each_cost] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[discount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_categories]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](500) NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_ingredients]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_ingredients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[ingredient_id] [int] NOT NULL,
	[consumption] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](500) NOT NULL,
	[price] [int] NOT NULL,
	[category_id] [int] NULL,
	[created_at] [datetime] NULL,
	[unit] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 7/8/2023 3:19:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[discounts] ON 

INSERT [dbo].[discounts] ([id], [name], [description], [value], [fromDate], [toDate]) VALUES (2, N'Flat Offer', N'wesy hi', 75, CAST(N'2023-07-07T22:01:38.593' AS DateTime), CAST(N'2023-09-07T22:01:38.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[discounts] OFF
GO
SET IDENTITY_INSERT [dbo].[ingredients] ON 

INSERT [dbo].[ingredients] ([id], [name], [description], [unit]) VALUES (3, N'Coffee Beans', N'Ingredient for Coffee', 80)
INSERT [dbo].[ingredients] ([id], [name], [description], [unit]) VALUES (4, N'Milk Powder', N'Milk Powder for Coffee', 50)
SET IDENTITY_INSERT [dbo].[ingredients] OFF
GO
SET IDENTITY_INSERT [dbo].[inventory_log] ON 

INSERT [dbo].[inventory_log] ([id], [product_id], [quantity], [cost], [created_at]) VALUES (1, 15, 26, 250, CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[inventory_log] ([id], [product_id], [quantity], [cost], [created_at]) VALUES (3, 9, 25, 2500, CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[inventory_log] ([id], [product_id], [quantity], [cost], [created_at]) VALUES (7, 16, 2, 60, CAST(N'1900-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[inventory_log] OFF
GO
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (1, N'John', N'Doe', N'example1', 10, 20, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (2, N'Jane', N'Smith', N'example2', 15, 25, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (3, N'Michael', N'Johnson', N'example3', 5, 15, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (4, N'Emily', N'Davis', N'example4', 12, 30, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (5, N'David', N'Anderson', N'example5', 18, 22, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (6, N'Sarah', N'Taylor', N'example6', 8, 28, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (7, N'Robert', N'Wilson', N'example7', 14, 19, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (8, N'Olivia', N'Clark', N'example8', 17, 27, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (9, N'Daniel', N'Brown', N'example9', 9, 21, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (10, N'Sophia', N'Miller', N'example10', 16, 24, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (11, N'Matthew', N'Moore', N'example11', 7, 17, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (12, N'Ava', N'Harris', N'example12', 13, 23, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (13, N'James', N'Jackson', N'example13', 6, 26, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (14, N'Mia', N'Thompson', N'example14', 11, 29, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (15, N'Benjamin', N'White', N'example15', 20, 18, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (16, N'Charlotte', N'Lee', N'example16', 8, 21, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (17, N'Logan', N'Johnson', N'example17', 16, 25, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (18, N'Amelia', N'Davis', N'example18', 11, 27, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (19, N'Lucas', N'Harris', N'example19', 7, 24, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
INSERT [dbo].[my_tbl] ([id], [varchar_column1], [varchar_column2], [varchar_column3], [int_column1], [int_column2], [created_at], [updated_at]) VALUES (20, N'Harper', N'Smith', N'example20', 13, 19, CAST(N'2023-06-25T21:49:41.000' AS DateTime), CAST(N'2023-06-25T21:49:41.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[order_details] ON 

INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (1, 1, 7, 350, 1, 0)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (2, 1, 7, 350, 1, 0)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (3, 1, 7, 350, 1, 0)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (4, 1, 9, 450, 2, 75)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (5, 1, 9, 450, 2, 75)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (6, 2, 7, 350, 1, 0)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (7, 2, 7, 350, 1, 0)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (8, 2, 7, 350, 1, 0)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (9, 2, 9, 450, 2, 75)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (10, 2, 9, 450, 2, 75)
INSERT [dbo].[order_details] ([id], [order_id], [product_id], [each_cost], [quantity], [discount]) VALUES (11, 3, 16, 80, 1, 0)
SET IDENTITY_INSERT [dbo].[order_details] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([id], [order_date]) VALUES (1, CAST(N'2023-07-08T01:14:02.763' AS DateTime))
INSERT [dbo].[orders] ([id], [order_date]) VALUES (2, CAST(N'2023-07-08T01:14:11.133' AS DateTime))
INSERT [dbo].[orders] ([id], [order_date]) VALUES (3, CAST(N'2023-07-08T03:15:56.520' AS DateTime))
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[product_categories] ON 

INSERT [dbo].[product_categories] ([id], [name], [description], [created_at]) VALUES (1, N'Coffee', N'One Medium Coffee', CAST(N'2023-07-06T22:50:05.117' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name], [description], [created_at]) VALUES (2, N'Juice', N'One Glass Juice', CAST(N'2023-07-07T01:39:04.053' AS DateTime))
INSERT [dbo].[product_categories] ([id], [name], [description], [created_at]) VALUES (3, N'Burger', N'Burger Category', NULL)
INSERT [dbo].[product_categories] ([id], [name], [description], [created_at]) VALUES (4, N'admin', N'admin', NULL)
SET IDENTITY_INSERT [dbo].[product_categories] OFF
GO
SET IDENTITY_INSERT [dbo].[product_ingredients] ON 

INSERT [dbo].[product_ingredients] ([id], [product_id], [ingredient_id], [consumption]) VALUES (4, 15, 3, 1)
INSERT [dbo].[product_ingredients] ([id], [product_id], [ingredient_id], [consumption]) VALUES (5, 15, 4, 1)
SET IDENTITY_INSERT [dbo].[product_ingredients] OFF
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([id], [name], [description], [price], [category_id], [created_at], [unit]) VALUES (7, N'Mocca', N'One Mocca', 350, 1, NULL, 200)
INSERT [dbo].[products] ([id], [name], [description], [price], [category_id], [created_at], [unit]) VALUES (8, N'Mengo Juice', N'Mengo Juice', 250, 2, NULL, NULL)
INSERT [dbo].[products] ([id], [name], [description], [price], [category_id], [created_at], [unit]) VALUES (9, N'Strawberry Milkshake', N'Strawberry Milkshake', 450, 2, NULL, NULL)
INSERT [dbo].[products] ([id], [name], [description], [price], [category_id], [created_at], [unit]) VALUES (15, N'admin', N'admin', 25, 1, NULL, NULL)
INSERT [dbo].[products] ([id], [name], [description], [price], [category_id], [created_at], [unit]) VALUES (16, N'Tea', N'One Mug Tea', 80, 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[products] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [username], [email], [password], [created_at]) VALUES (1, N'admin', N'admin', N'admin', CAST(N'2023-07-06T21:12:00.120' AS DateTime))
INSERT [dbo].[users] ([id], [username], [email], [password], [created_at]) VALUES (3, N'user', N'user', N'user', NULL)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[ingredients] ADD  DEFAULT ((0)) FOR [unit]
GO
ALTER TABLE [dbo].[inventory_log] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[my_tbl] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[my_tbl] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [order_date]
GO
ALTER TABLE [dbo].[product_categories] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[product_ingredients] ADD  DEFAULT ((0)) FOR [consumption]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [unit]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[inventory_log]  WITH CHECK ADD  CONSTRAINT [FK_inventory_log_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[inventory_log] CHECK CONSTRAINT [FK_inventory_log_product_id]
GO
ALTER TABLE [dbo].[order_details]  WITH CHECK ADD  CONSTRAINT [FK_order_details_order_id] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[order_details] CHECK CONSTRAINT [FK_order_details_order_id]
GO
ALTER TABLE [dbo].[order_details]  WITH CHECK ADD  CONSTRAINT [FK_order_details_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[order_details] CHECK CONSTRAINT [FK_order_details_product_id]
GO
ALTER TABLE [dbo].[product_ingredients]  WITH CHECK ADD  CONSTRAINT [FK_ingredient_id] FOREIGN KEY([ingredient_id])
REFERENCES [dbo].[ingredients] ([id])
GO
ALTER TABLE [dbo].[product_ingredients] CHECK CONSTRAINT [FK_ingredient_id]
GO
ALTER TABLE [dbo].[product_ingredients]  WITH CHECK ADD  CONSTRAINT [FK_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[product_ingredients] CHECK CONSTRAINT [FK_product_id]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_product_category_id] FOREIGN KEY([category_id])
REFERENCES [dbo].[product_categories] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_product_category_id]
GO
USE [master]
GO
ALTER DATABASE [my_db] SET  READ_WRITE 
GO
