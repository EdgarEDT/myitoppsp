use [itop_dwgh]
alter table [WireCategory] add
	[gzl] [float] null default 0,
	[xftime] [float] null default 0
	
	
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WireCategory', @level2type=N'COLUMN',@level2name=N'gzl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��λ�޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WireCategory', @level2type=N'COLUMN',@level2name=N'xftime'
GO