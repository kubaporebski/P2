﻿CREATE TABLE [staging].[VARIABLES_DIMMS](
	[parentId] [nvarchar](10) NOT NULL,
	[Id] [nvarchar](10) NOT NULL,
	[N1] [nvarchar](255) NULL,
	[N2] [NVARCHAR](255) NULL,
	[MEASUREUNITID] [INT],
 CONSTRAINT [PK_VARIABLES_DIMMS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [staging].[VARIABLES_DIMMS]  ADD  CONSTRAINT [DIMENSION_HAS_VARIABLE] FOREIGN KEY([parentId])
REFERENCES [bdl].[DimVariables] ([Id])
GO

ALTER TABLE [staging].[VARIABLES_DIMMS] CHECK CONSTRAINT [DIMENSION_HAS_VARIABLE]
GO

ALTER TABLE [staging].[VARIABLES_DIMMS]  ADD  CONSTRAINT [VARIABLE_HAS_MEASURE] FOREIGN KEY([MEASUREUNITID])
REFERENCES [bdl].[DimMeasures] ([Id])
GO
