﻿CREATE TABLE [bdl].[DimSubjects](
	[parentId] [nvarchar](10) NOT NULL,
	[Id] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[HasVariables] [bit] NULL,
	[Children] [nvarchar](1024) NULL,
	[CODE] [nvarchar](255) NULL,
 CONSTRAINT [PK_SUBJECTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [bdl].[DimSubjects] ADD CONSTRAINT [SUBJECT_HAS_TOP] FOREIGN KEY([parentId])
REFERENCES [bdl].[DimTopSubjects] ([Id])
GO

ALTER TABLE [bdl].[DimSubjects] CHECK CONSTRAINT [SUBJECT_HAS_TOP]
GO
