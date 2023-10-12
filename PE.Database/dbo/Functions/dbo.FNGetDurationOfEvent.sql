CREATE   FUNCTION [dbo].[FNGetDurationOfEvent](@WorkOrderId   BIGINT, 
                                                      @EnumEventType SMALLINT) RETURNS INT AS

/*
SELECT [dbo].[[FNGetDurationOfEvent]](176774,101)
*/

BEGIN
DECLARE @DurationOfEvent INT;
SELECT @DurationOfEvent = DATEDIFF(SECOND, EventStartTs, EventEndTs)
FROM [dbo].[EVTEvents] AS E
WHERE 1 = 1
      AND E.FKEventTypeId = @EnumEventType -- Only when: EnumEventType = EventTypCode = EventTypeId
      AND E.FKWorkOrderId = @WorkOrderId;
RETURN @DurationOfEvent;
END;