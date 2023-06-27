Create database DBBeerDetail;
USE [DBBeerDetail]
GO
/****** Object:  Table [dbo].[bar]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[address] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bar_beers]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bar_beers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bar_id] [int] NULL,
	[beer_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[bar_id] ASC,
	[beer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
	
GO
/****** Object:  Table [dbo].[beer] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[beer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[percentage_alcoholby_volume] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[brewery]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[brewery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[brewery_beer]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[brewery_beer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[brewery_id] [int] NULL,
	[beer_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[brewery_id] ASC,
	[beer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[bar] ON 

INSERT [dbo].[bar] ([id], [name], [address]) VALUES (1, N'Grand Royal NightClub', N'Sector 18, Noida')
INSERT [dbo].[bar] ([id], [name], [address]) VALUES (2, N'KinghtBar Headquarter', N'Caugnut Place Delhi')
SET IDENTITY_INSERT [dbo].[bar] OFF
GO
SET IDENTITY_INSERT [dbo].[bar_beers] ON 

INSERT [dbo].[bar_beers] ([id], [bar_id], [beer_id]) VALUES (1, 1, 1)
INSERT [dbo].[bar_beers] ([id], [bar_id], [beer_id]) VALUES (2, 1, 2)
INSERT [dbo].[bar_beers] ([id], [bar_id], [beer_id]) VALUES (3, 2, 2)
SET IDENTITY_INSERT [dbo].[bar_beers] OFF
GO
SET IDENTITY_INSERT [dbo].[beer] ON 

INSERT [dbo].[beer] ([id], [name], [percentage_alcoholby_volume]) VALUES (1, N'Kingfisher', CAST(4.80 AS Decimal(10, 2)))
INSERT [dbo].[beer] ([id], [name], [percentage_alcoholby_volume]) VALUES (2, N'Tuborg Strong', CAST(8.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[beer] OFF
GO
SET IDENTITY_INSERT [dbo].[brewery] ON 

INSERT [dbo].[brewery] ([id], [name]) VALUES (1, N'Dublin Square')
INSERT [dbo].[brewery] ([id], [name]) VALUES (2, N'Galaxy')
SET IDENTITY_INSERT [dbo].[brewery] OFF
GO
SET IDENTITY_INSERT [dbo].[brewery_beer] ON 

INSERT [dbo].[brewery_beer] ([id], [brewery_id], [beer_id]) VALUES (1, 1, 1)
INSERT [dbo].[brewery_beer] ([id], [brewery_id], [beer_id]) VALUES (2, 1, 2)
INSERT [dbo].[brewery_beer] ([id], [brewery_id], [beer_id]) VALUES (3, 2, 1)
SET IDENTITY_INSERT [dbo].[brewery_beer] OFF
GO
ALTER TABLE [dbo].[bar_beers]  WITH CHECK ADD  CONSTRAINT [FK_bar_beer_id] FOREIGN KEY([beer_id])
REFERENCES [dbo].[beer] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[bar_beers] CHECK CONSTRAINT [FK_bar_beer_id]
GO
ALTER TABLE [dbo].[bar_beers]  WITH CHECK ADD  CONSTRAINT [FK_bar_id] FOREIGN KEY([bar_id])
REFERENCES [dbo].[bar] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[bar_beers] CHECK CONSTRAINT [FK_bar_id]
GO
ALTER TABLE [dbo].[brewery_beer]  WITH CHECK ADD  CONSTRAINT [FK_beer_id] FOREIGN KEY([beer_id])
REFERENCES [dbo].[beer] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[brewery_beer] CHECK CONSTRAINT [FK_beer_id]
GO
ALTER TABLE [dbo].[brewery_beer]  WITH CHECK ADD  CONSTRAINT [FK_brewery_id] FOREIGN KEY([brewery_id])
REFERENCES [dbo].[brewery] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[brewery_beer] CHECK CONSTRAINT [FK_brewery_id]
GO
