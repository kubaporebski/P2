﻿CREATE TABLE [staging].[databyvariable](
	[Id] [nvarchar](10) NOT NULL,
	[parentId] [nvarchar](10) NOT NULL,
	[MEASUREUNITID] [INT],
	[NAME] [NVARCHAR](255) NULL,
	[YEAR] [INT],
	[VALUE][NVARCHAR](255) NULL,
	[ATTRIBUTEID] [INT]
 CONSTRAINT [PK_DATABYVARIABLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [staging].[databyvariable]  WITH CHECK ADD  CONSTRAINT [DATA_HAS_VARIABLE] FOREIGN KEY([parentId])
REFERENCES [staging].[CHILD_Variables] ([Id])
GO

ALTER TABLE [staging].[databyvariable] CHECK CONSTRAINT [DATA_HAS_VARIABLE]
GO

ALTER TABLE [staging].[databyvariable]  WITH CHECK ADD  CONSTRAINT [DATA_HAS_MEASURE] FOREIGN KEY([MEASUREUNITID])
REFERENCES [staging].[MEASURES] ([Id])
GO

ALTER TABLE [staging].[databyvariable]  WITH CHECK ADD  CONSTRAINT [DATA_HAS_ATTRIBUTE] FOREIGN KEY([ATTRIBUTEID])
REFERENCES [staging].[ATTRIBUTES] ([Id])
GO