SELECT
  staging.Data_tmp.VariableId
  ,staging.Data_tmp.MeasureUnitId
  ,staging.Data_tmp.AggregateId	
  ,staging.Data_tmp.UNITID
  ,staging.Data_tmp.N1
  ,staging.Data_tmp.N2
  ,staging.Data_tmp.Name
  ,staging.Data_tmp.[Year]
  ,staging.Data_tmp.[Value]
  ,staging.Data_tmp.AttributeId
  ,staging.Data_tmp.parentId
FROM
  staging.Data_tmp

go

drop view viewVehiclesFuel

create view viewVehiclesFuel as 
select N1 as VehicleType, N2 as FuelType, Name as Voivodship, Year, cast(Value as int) as Value from 
staging.Data_tmp
where VariableId in (475615,475614,475613,475612,475611,475610,475609,475608,475616,475617,475618,475619,475620,475621,475622,475623)


SELECT N1, n2, min(variableId), max(variableId)
from staging.Data_tmp
group by N1, n2







CREATE VIEW viewRoadType as
SELECT N1 as SurfaceType, isnull(N2, 'ogółem') as RoadUnitType, Name as Voivodship, Year, cast(Value as decimal(10,2)) as Value 
FROM staging.Data_tmp
WHERE VariableId in (77238,77239,7720,7721,7722,7723,7724,7725,54564,54585,54562,54581)


drop view [dbo].[viewVehiclesAge]

create view viewVehiclesAge as 
select N1 as VehicleType, N2 as AgeGroup, Name as Voivodship, Year, cast(Value as int) as Value,
(case N2 when '31 lat i starsze' then 0 
         when '26-30 lat' then 2
         when '21-25 lat' then 3
         when '16-20 lat' then 4
         when '12-15 lat' then 5
         when '10-11 lat' then 6
         when '8-9 lat' then 7
         when '6-7 lat' then 8
         when '4-5 lat' then 9		
         when '3 lata' then 10
		 when '2 lata' then 11
		 when 'do 1 roku' then 12
		 else 99 end) as Sort
from 
staging.Data_tmp
where VariableId in (475624,475625,475626,475627,475628,475629,475630,475631,475632,475633,475634,475635,475636,
					475637,475638,475639,475640,475641,475642,475643,475644,475645,475646,475647,475648,475649,
					475650,475651,475652,475653,475654,475655,475656,475657,475658,475659,475660,475661,475662,
					475663,475664,475665,475666,475667,475668,475669,475670,475671,475672,475673,475674,475675,
					475676,475677,475678,475679,475680,475681,475682,475683,475684,475685,475686,475687,475688,
					475689,475690,475691,475692,475693,475694,475695)



with cte as (
SELECT
  sum(viewVehiclesFuel.[Value]) as suma
  ,viewVehiclesFuel.[Year]
  ,viewVehiclesFuel.FuelType
FROM
  viewVehiclesFuel
where year=2017
group by 
  year, fueltype
)
select 
    suma, fueltype, year,
	((select suma*1.0 from cte where FuelType='benzyna') /
	 (select suma*1.0 from cte where FuelType='olej napędowy')) as b2on,
    ((select suma*1.0 from cte where FuelType='benzyna') /
	 (select suma*1.0 from cte where FuelType='gaz (LPG)')) as b2lpg
from cte



