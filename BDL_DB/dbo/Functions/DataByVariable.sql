CREATE FUNCTION [dbo].[DataByVariable]
(
	@variableId int,
	@yearFrom int,
	@yearTo int,
	@pageSize int
)
RETURNS TABLE
(
	-- out int VariableId, out int MeasureUnitId, out int AggregateId, out string Id, out string Name, out int Year, out string Value, out int AttributeId
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
