create table [bdl].[DimUnits]
(
	[Id] [nvarchar](30) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](255) NULL,
	[ParentId] [nvarchar](30) NULL,
	[Level] [int] NULL,
)