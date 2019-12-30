CREATE TABLE [fact].[DimMeasures]
(
	[Id] [int],
	[Name] [nvarchar](255),
	[Description] [nvarchar](255) NULL,
CONSTRAINT PK_MEASURES PRIMARY KEY CLUSTERED
(
ID ASC
))