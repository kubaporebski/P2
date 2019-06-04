CREATE FUNCTION [dbo].[DataByVariable]
(
	@variableId int,
	@territorialUnitId nvarchar(30),
	@yearFrom int,
	@yearTo int,
	@pageSize int
)
RETURNS TABLE
(
	VariableId int,
	MeasureUnitId int,
	AggregateId int,
	Id nvarchar(100),
	Name nvarchar(255),
	Year int,
	Value nvarchar(255),
	AttributeId int
)
AS EXTERNAL NAME [BDL].[BDL.DataGetter].[DataByVariable]
