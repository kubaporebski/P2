/*

Za³o¿enie jest takie ¿e musimy mieæ bazê BDL


*/

ALTER DATABASE BDL SET TRUSTWORTHY ON
go

sp_configure 'show advanced options', 1; 
GO 
RECONFIGURE; 
GO 
sp_configure 'clr enabled', 1; 
GO 
RECONFIGURE; 

USE BDL
go

if exists(select * from sys.objects where name = 'Randomizer' and type = 'FT')
    drop function dbo.Randomizer
go

if exists(select * from sys.objects where name = 'Subjects' and type = 'FT')
    drop function dbo.Subjects
go

if exists(select * from sys.objects where name = 'Variables' and type = 'FT')
    drop function dbo.Variables
go


if exists(select * from sys.assemblies where name = 'BDL')
    drop assembly bdl
go

if exists(select * from sys.procedures where name = 'RequestLog')
	drop procedure RequestLog
go


-- nale¿y podaæ w³aœciw¹ œcie¿kê do skompilowanej DLLki
CREATE ASSEMBLY BDL FROM 'C:\Users\kuba\Documents\Visual Studio 2017\Projects\BDL\BDL\bin\Debug\BDL.dll'  
WITH PERMISSION_SET = UNSAFE -- EXTERNAL_ACCESS;  
GO  


create function dbo.Subjects(@parentId nvarchar(10), @pageSize int)
returns table (Id nvarchar(10), Name nvarchar(255), HasVariables bit, Children nvarchar(1024))
as external name bdl.[BDL.DataGetter].Subjects;
go

create function dbo.Variables(@parentId nvarchar(10), @pageSize int)
returns table (Id nvarchar(10), SubjectId nvarchar(10), N1 nvarchar(255), N2 nvarchar(255), MeasureUnitId int, MeasureUnitName nvarchar(30))
as external name bdl.[BDL.DataGetter].Variables;
go


create function dbo.Randomizer(@count int)
returns table(Id int, Value float)
as external name bdl.[BDL.DataGetter].Randomizer;
go

create procedure dbo.RequestLog
as external name bdl.[BDL.DataGetter].RequestLog;
go

exec dbo.RequestLog

-- select * from dbo.Randomizer(1024)


if exists(select * from sys.schemas where name='staging')
begin
  drop table if exists staging.TopSubjects
  drop table if exists staging.AllSubjects
  drop schema staging
end
go

create schema staging
go


select * 
into staging.TopSubjects
from dbo.Subjects(null, 100)


select * From staging.TopSubjects

with cte as (
    select distinct child.value as parentId 
    from staging.TopSubjects cross apply string_split(Children, ',') child
)
select * 
into staging.AllSubjects
from cte cross apply dbo.Subjects(parentId, 100)

select * From staging.AllSubjects

