CREATE FUNCTION [dbo].[Randomizer]
(@count INT NULL)
RETURNS 
     TABLE (
        [Id]    INT        NULL,
        [Value] FLOAT (53) NULL)
AS
 EXTERNAL NAME [BDL].[BDL.DataGetter].[Randomizer]

