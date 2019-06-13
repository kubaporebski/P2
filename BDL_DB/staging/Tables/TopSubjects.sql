﻿CREATE TABLE [staging].[TopSubjects](
	[Id] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[HasVariables] [bit] NULL,
	[Children] [nvarchar](1024) NULL,
	[CODE] [nvarchar](255) NULL,
 CONSTRAINT [PK_TOPSUBJECTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
