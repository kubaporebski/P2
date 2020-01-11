CREATE TABLE [staging].[LOGGING](
	[parentId] [nvarchar](10) NULL,
	[Id] [nvarchar](10) NULL,
	[Name] [nvarchar](255) NULL,
	[HasVariables] [bit] NULL,
	[Children] [nvarchar](1024) NULL,
	[CODE] [nvarchar](255) NULL,
	[LogDateTime] datetime2 default current_timestamp
) ON [PRIMARY]
GO