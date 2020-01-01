CREATE PROCEDURE [dbo].[DIMUNITS_IN]
AS BEGIN
INSERT INTO FACT.DimUnits
SELECT [Id]
      ,[Name]
      ,[ParentId]
      ,[Level]
FROM [bdl].[DimUnits] A
WHERE A.ID NOT IN (SELECT ID FROM FACT.DimUnits)
END
