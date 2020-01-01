CREATE PROCEDURE [dbo].[DIMSUBJECTS_IN]
AS BEGIN
INSERT INTO FACT.DimSubjects
SELECT [parentId]
      ,[Id]
      ,[Name]
      ,[HasVariables]
      ,[Children]
      ,[CODE]
FROM [BDL].[DimSubjects] A
WHERE A.ID NOT IN (SELECT ID FROM FACT.DimSubjects)
END
