﻿CREATE PROCEDURE [dbo].[INSERTS]
AS
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
EXEC DIMATTRIBUTES_IN
EXEC DIMMEASURES_IN
EXEC DIMTOPSUBJECTS_IN
EXEC DIMSUBJECTS_IN
EXEC DIMUNITS_IN
EXEC DIMVARIABLES_IN
EXEC CATEGORY_IN
EXEC SUBCATEGORY_IN
EXEC FACTDATA_IN

RETURN (@@ERROR)