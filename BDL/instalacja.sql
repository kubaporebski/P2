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

create function dbo.Measures(@count int)
returns table(Id int, Name nvarchar(255), Description nvarchar(255))
as external name bdl.[BDL.DataGetter].Measures;
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

-- przyk³ad za³adowania wszystkich tematów do tabeli AllSubjects
-- tabela TopSubjects jest tylko tymczasowa
-- a i tak obydwie s¹ w schema staging, co oznacza, ¿e s³u¿¹ tylko jako Ÿród³o danych do dalszego procesowania
insert into staging.TopSubjects(Id, Name, Children)
select Id, Name, Children from dbo.Subjects(null, 100)

GO

-- tu trzeba poczekaæ parê minut na pobranie siê wszystkich tematów...
with cte as (
    select distinct child.value as parentId 
    from staging.TopSubjects cross apply string_split(Children, ',') child
)
insert into staging.AllSubjects(Id, Name, parentId)
select Id, Name, parentId
from cte cross apply dbo.Subjects(parentId, 100)

--
select * From staging.AllSubjects

GO
--zmiany_struktura_constraints:


ALTER TABLE STAGING.TOPSUBJECTS ALTER COLUMN ID NVARCHAR(10) NOT NULL 
GO
ALTER TABLE STAGING.ALLSUBJECTS ALTER COLUMN ID NVARCHAR(10) NOT NULL 
GO
ALTER TABLE STAGING.ALLSUBJECTS ALTER COLUMN PARENTID NVARCHAR(10) NOT NULL 
GO
ALTER TABLE STAGING.TOPSUBJECTS ADD CONSTRAINT PK_TOPSUBJECTS PRIMARY KEY (ID) 
GO
ALTER TABLE STAGING.ALLSUBJECTS ADD CONSTRAINT SUBJECT_HAS_TOP FOREIGN KEY (PARENTID) REFERENCES STAGING.TOPSUBJECTS (ID) 
GO
ALTER TABLE STAGING.TOPSUBJECTS ADD CODE NVARCHAR(255) 
GO
ALTER TABLE STAGING.ALLSUBJECTS ADD CODE NVARCHAR(255) 
GO
INSERT INTO STAGING.LOGGING(ID,PARENTID,CODE,NAME,HASVARIABLES,CHILDREN)
SELECT ID,PARENTID,CODE,NAME,HASVARIABLES,CHILDREN FROM STAGING.ALLSUBJECTS
GO
ALTER TABLE STAGING.LOGGING ALTER COLUMN  PARENTID NVARCHAR(10) NULL
GO
ALTER TABLE STAGING.LOGGING ALTER COLUMN ID NVARCHAR(10) NULL
