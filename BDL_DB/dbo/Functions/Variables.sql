CREATE FUNCTION [dbo].[Variables]
(@parentId NVARCHAR (10) NULL, @pageSize INT NULL)
RETURNS 
     TABLE (
        [Id]              NVARCHAR (10)  NULL,
        [SubjectId]       NVARCHAR (10)  NULL,
        [N1]              NVARCHAR (255) NULL,
        [N2]              NVARCHAR (255) NULL,
        [MeasureUnitId]   INT            NULL,
        [MeasureUnitName] NVARCHAR (30)  NULL)
AS
 EXTERNAL NAME [BDL].[BDL.DataGetter].[Variables]

