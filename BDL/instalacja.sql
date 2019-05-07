/*

Za³o¿enie jest takie ¿e musimy mieæ bazê TestDB


*/

ALTER DATABASE TestDB SET TRUSTWORTHY ON
go

if exists(select * from sys.objects where name = 'Randomizer' and type = 'FT')
    drop function dbo.Randomizer
go

if exists(select * from sys.objects where name = 'SetClientId' and type = 'FS')
    drop function dbo.SetClientId
go


if exists(select * from sys.objects where name = 'Subjects' and type = 'FT')
    drop function dbo.Subjects
go


if exists(select * from sys.assemblies where name = 'BDL')
    drop assembly bdl
go


-- nale¿y podaæ w³aœciw¹ œcie¿kê do skompilowanej DLLki
CREATE ASSEMBLY BDL FROM 'C:\Users\kuba\Documents\Visual Studio 2017\Projects\BDL\BDL\bin\Debug\BDL.dll'  
WITH PERMISSION_SET = UNSAFE -- EXTERNAL_ACCESS;  
GO  

create function dbo.SetClientId(@clientId nvarchar(255))
returns int
as external name bdl.[BDL.DataGetter].SetClientId;
go


create function dbo.Subjects(@parentId nvarchar(10), @pageSize int)
returns table (Id nvarchar(10), Name nvarchar(255), HasVariables bit, Children nvarchar(1024))
as external name bdl.[BDL.DataGetter].Subjects;
go

create function dbo.Randomizer(@count int)
returns table(Id int, Value float)
as external name bdl.[BDL.DataGetter].Randomizer;
go

select * from dbo.Randomizer(1024)


-- to trzeba wywo³aæ - klucz  API
select dbo.SetClientId('51b8ff67-411c-47c6-8dea-08d689f6cc93');
select * from dbo.Subjects(null, 100)


