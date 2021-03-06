USE [itoppsp]
GO
/****** 对象:  Table [dbo].[PDrelcontent]    脚本日期: 12/12/2011 11:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PDrelcontent](
	[ID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ParentID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[TDdatetime] [datetime] NULL,
	[TDtime] [float] NULL,
	[PeopleRegion] [int] NULL,
	[TDtype] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AvgFH] [float] NULL,
	[S1] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S2] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S3] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[S4] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
