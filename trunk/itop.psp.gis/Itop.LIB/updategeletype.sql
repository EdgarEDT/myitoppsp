use [itop_dwgh]
alter table [glebeType] add
	[ObligateField5] [nvarchar(50)] ,
	[ObligateField6] [nvarchar(50)]
	
	
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ�귶Χ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'glebeType', @level2type=N'COLUMN',@level2name=N'ObligateField5'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���¸����ܶ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'glebeType', @level2type=N'COLUMN',@level2name=N'ObligateField6'
GO
