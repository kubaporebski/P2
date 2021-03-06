﻿CREATE TABLE [fact].[FactData](
    [PrimaryKey] [int] not null identity(1,1) primary key,
	[Id] [int] NOT NULL,
	[parentId] [nvarchar](10) NOT NULL,
	[MEASUREUNITID] [int] NULL,
	[UNITID] [nvarchar](30) NULL,
	[N1_ID] [int] NULL,
	[N2_ID] [int] NULL,
	[NAME] [nvarchar](255) NULL,
	[YEAR] [int] NULL,
	[VALUE] [nvarchar](255) NULL,
	[ATTRIBUTEID] [int] NULL,
	[AGGREGATEID] [int] NULL,
	[SUBJECT_ID] [NVARCHAR](10) NULL,
	[TOPSUBJECT_ID] [NVARCHAR](10) NULL,
	[DIMVARIABLES_ID] [NVARCHAR](10) NULL
) ON [PRIMARY]
GO

ALTER TABLE [fact].[FactData]  WITH CHECK ADD  CONSTRAINT [FACT_DATA_HAS_ATTRIBUTE] FOREIGN KEY([ATTRIBUTEID])
REFERENCES [fact].[DimAttributes] ([Id])
GO

ALTER TABLE [fact].[FactData] CHECK CONSTRAINT [FACT_DATA_HAS_ATTRIBUTE]
GO

ALTER TABLE [fact].[FactData]  WITH CHECK ADD  CONSTRAINT [FACT_DATA_HAS_MEASURE] FOREIGN KEY([MEASUREUNITID])
REFERENCES [fact].[DimMeasures] ([Id])
GO

ALTER TABLE [fact].[FactData] CHECK CONSTRAINT [FACT_DATA_HAS_MEASURE]
GO

ALTER TABLE [fact].[FactData]  WITH CHECK ADD  CONSTRAINT [FACT_DATA_HAS_UNIT] FOREIGN KEY([UNITID])
REFERENCES [fact].[DimUnits] ([Id])
GO

ALTER TABLE [fact].[FactData] CHECK CONSTRAINT [FACT_DATA_HAS_UNIT]
GO

ALTER TABLE [FACT].[FACTDATA] WITH CHECK ADD CONSTRAINT [FACT_DATA_HAS_N1] FOREIGN KEY ([N1_ID])
REFERENCES [FACT].[CATEGORY] ([ID])
GO

ALTER TABLE [FACT].[FACTDATA] CHECK CONSTRAINT [FACT_DATA_HAS_N1]
GO

ALTER TABLE [FACT].[FACTDATA] WITH CHECK ADD CONSTRAINT [FACT_DATA_HAS_N2] FOREIGN KEY ([N2_ID])
REFERENCES [FACT].[SUBCATEGORY] ([ID])
GO

ALTER TABLE [FACT].[FACTDATA] CHECK CONSTRAINT [FACT_DATA_HAS_N2]
GO

ALTER TABLE [FACT].[FACTDATA] WITH CHECK ADD CONSTRAINT [FACT_DATA_HAS_SUBJECT] FOREIGN KEY ([SUBJECT_ID])
REFERENCES [FACT].DIMSUBJECTS ([ID])
GO

ALTER TABLE [FACT].[FACTDATA] CHECK CONSTRAINT [FACT_DATA_HAS_SUBJECT]
GO

ALTER TABLE [FACT].[FACTDATA] WITH CHECK ADD CONSTRAINT [FACT_DATA_HAS_TOPSUBJECT] FOREIGN KEY ([TOPSUBJECT_ID])
REFERENCES [FACT].DIMTOPSUBJECTS ([ID])
GO

ALTER TABLE [FACT].[FACTDATA] CHECK CONSTRAINT [FACT_DATA_HAS_TOPSUBJECT]
GO

ALTER TABLE [FACT].[FACTDATA] WITH CHECK ADD CONSTRAINT [FACT_DATA_HAS_DIMVARIABLES] FOREIGN KEY ([DIMVARIABLES_ID])
REFERENCES [FACT].[DIMVARIABLES] ([ID])
GO

ALTER TABLE [FACT].[FACTDATA] CHECK CONSTRAINT [FACT_DATA_HAS_DIMVARIABLES]
GO