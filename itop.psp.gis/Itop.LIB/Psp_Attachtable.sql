USE [itop_dwgh]
GO
/****** 对象:  Table [dbo].[Psp_Attachtable]    脚本日期: 06/05/2012 11:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Psp_Attachtable](
	[ID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ZHI] [float] NULL,
	[startYear] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[endYear] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Relatetable] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Type] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S1] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S2] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S3] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[D1] [float] NULL,
	[D2] [float] NULL,
	[D3] [float] NULL,
	[RelatetableID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S4] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S5] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S6] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[D4] [float] NULL,
	[D5] [float] NULL,
	[D6] [float] NULL,
 CONSTRAINT [PK_Psp_Attachtable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
