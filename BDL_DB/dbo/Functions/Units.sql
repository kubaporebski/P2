CREATE FUNCTION [dbo].[Units]
(
	@pageSize int
)
RETURNS TABLE
(
	Id nvarchar(30),
	Name nvarchar(255),
	ParentId nvarchar(30),
	Level int
)
AS EXTERNAL NAME [BDL].[BDL.DataGetter].[Units]
