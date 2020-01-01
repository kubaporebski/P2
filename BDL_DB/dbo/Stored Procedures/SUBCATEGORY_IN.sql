CREATE PROCEDURE [dbo].[SUBCATEGORY_IN]
AS BEGIN
insert into fact.Subcategory
select ROW_NUMBER() over (order by a.n2 asc) as ID, a.n2 from (select distinct n2 from temp.TempDataByVariable where n2 not in (select n2 from fact.Subcategory)) a
END
