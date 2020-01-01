CREATE PROCEDURE DBO.CATEGORY_IN
AS BEGIN
insert into fact.Category
select ROW_NUMBER() over (order by a.n1 asc) as ID, a.n1 from (select distinct n1 from temp.TempDataByVariable where n1 not in (select N1 from fact.Category)) a
END