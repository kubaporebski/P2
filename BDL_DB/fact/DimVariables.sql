﻿CREATE TABLE [fact].[DimVariables](
	[parentId] [nvarchar](10) NOT NULL,
	[Id] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[HasVariables] [bit] NULL,
	[CODE] [nvarchar](255) NULL,
 CONSTRAINT [PK_VARIABLES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [fact].[DimVariables] ADD  CONSTRAINT [VARIABLE_HAS_TOP] FOREIGN KEY([parentId])
REFERENCES [fact].[DimSubjects] ([Id])
GO

ALTER TABLE [fact].[DimVariables] CHECK CONSTRAINT [VARIABLE_HAS_TOP]
GO

