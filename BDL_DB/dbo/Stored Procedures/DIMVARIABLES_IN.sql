CREATE PROCEDURE [dbo].[DIMVARIABLES_IN]
AS BEGIN
INSERT INTO FACT.DimVariables
SELECT [parentId]
      ,[Id]
      ,[Name]
      ,[HasVariables]
      ,[CODE]
FROM [bdl].[DimVariables] A
WHERE A.ID NOT IN (SELECT ID FROM FACT.DimVariables)
END
