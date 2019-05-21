CREATE TABLE [staging].[AllSubjects] (
    [parentId]     NVARCHAR (1024) NULL,
    [Id]           NVARCHAR (10)   NULL,
    [Name]         NVARCHAR (255)  NULL,
    [HasVariables] BIT             NULL,
    [Children]     NVARCHAR (1024) NULL
);

