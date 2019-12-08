﻿CREATE TABLE [bdl].[FactData](
	[Id] INT NOT NULL,
	[parentId] [nvarchar](10) NOT NULL,
	[MEASUREUNITID] [INT],
	[UNITID] [NVARCHAR](30) NULL,
	[N1] [NVARCHAR](255),
	[N2] [NVARCHAR](255),
	[NAME] [NVARCHAR](255) NULL,
	[YEAR] [INT],
	[VALUE][NVARCHAR](255) NULL,
	[ATTRIBUTEID] [INT],
	[AGGREGATEID] [INT]
	)
GO


ALTER TABLE [bdl].[FactData]  ADD  CONSTRAINT [DATA_HAS_MEASURE] FOREIGN KEY([MEASUREUNITID])
REFERENCES [bdl].[DimMeasures] ([Id])
GO

ALTER TABLE [bdl].[FactData]  ADD  CONSTRAINT [DATA_HAS_ATTRIBUTE] FOREIGN KEY([ATTRIBUTEID])
REFERENCES [bdl].[DimAttributes] ([Id])
GO

ALTER TABLE [bdl].[FactData]  ADD  CONSTRAINT [DATA_HAS_UNIT] FOREIGN KEY([UNITID])
REFERENCES [bdl].[DimUnits] ([Id])
GO

ALTER TABLE [bdl].[FactData] CHECK CONSTRAINT [DATA_HAS_UNIT]
GO