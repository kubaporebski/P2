CREATE FUNCTION [dbo].[Measures]
( )
RETURNS 
     TABLE (
        [Id]          INT            NULL,
        [Name]        NVARCHAR (255) NULL,
        [Description] NVARCHAR (255) NULL)
AS
 EXTERNAL NAME [BDL].[BDL.DataGetter].[Measures]

