CREATE TABLE staging.Data_tmp (
    [VariableId] int,
    [MeasureUnitId] int,
    [AggregateId] int,
	[UNITID] NVARCHAR(30) NULL,
	[N1] [NVARCHAR](255),
	[N2] [NVARCHAR](255),
    [Name] nvarchar(255),
    [Year] int,
    [Value] nvarchar(255),
    [AttributeId] INT,
	[parentId] [nvarchar](10) NOT NULL,
)