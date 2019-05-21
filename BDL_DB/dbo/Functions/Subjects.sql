CREATE FUNCTION [dbo].[Subjects]
(@parentId NVARCHAR (10) NULL, @pageSize INT NULL)
RETURNS 
     TABLE (
        [Id]           NVARCHAR (10)   NULL,
        [Name]         NVARCHAR (255)  NULL,
        [HasVariables] BIT             NULL,
        [Children]     NVARCHAR (1024) NULL)
AS
 EXTERNAL NAME [BDL].[BDL.DataGetter].[Subjects]

