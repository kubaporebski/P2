CREATE PROCEDURE [dbo].[DIMMEASURES_IN]
AS BEGIN
INSERT INTO FACT.DimMeasures
SELECT [Id]
      ,[Name]
      ,[Description]
FROM [BDL].[DimMeasures] A
WHERE A.ID NOT IN (SELECT ID FROM FACT.DimMeasures)
END
