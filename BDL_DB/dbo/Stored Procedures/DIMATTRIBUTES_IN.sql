CREATE PROCEDURE DIMATTRIBUTES_IN
AS BEGIN
INSERT INTO FACT.DimAttributes
SELECT [Id]
      ,[Name]
      ,[Symbol]
      ,[Description]
FROM [bdl].[DimAttributes] A
WHERE A.ID NOT IN (SELECT ID FROM FACT.DimAttributes)
END