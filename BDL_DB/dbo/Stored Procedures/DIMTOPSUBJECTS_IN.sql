CREATE PROCEDURE [dbo].[DIMTOPSUBJECTS_IN]
AS BEGIN
INSERT INTO FACT.DimTopSubjects
SELECT [Id]
    ,[Name]
    ,[HasVariables]
    ,[Children]
    ,[CODE]
FROM [BDL].[DimTopSubjects] A
WHERE A.ID NOT IN (SELECT ID FROM FACT.DimTopSubjects)
END
