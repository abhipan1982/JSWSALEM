CREATE FUNCTION [dbo].[FNGetTimeOfEvent](@RawMaterialId BIGINT, 
                                        @EnumEventType SMALLINT)
RETURNS DATETIME
AS

/*
	SELECT [dbo].[FNGetTimeOfEvent](1049289,101)
*/

     BEGIN
         DECLARE @TimeOfEvent DATETIME;
         SELECT @TimeOfEvent = MAX([EventStartTs])
         FROM [dbo].[EVTEvents] AS E
         WHERE 1 = 1
               AND E.FKEventTypeId = @EnumEventType -- Only when: EnumEventType = EventTypCode = EventTypeId
               AND E.FKRawMaterialId = @RawMaterialId;
         RETURN @TimeOfEvent;
     END;