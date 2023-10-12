CREATE   FUNCTION [dbo].[fnCalculateDistance](@Lat1  FLOAT, 
                                                     @Long1 FLOAT, 
                                                     @Lat2  FLOAT, 
                                                     @Long2 FLOAT)
-- User-defined function that calculates the direct distance between two geographical coordinates
RETURNS FLOAT AS BEGIN
                 DECLARE @distance FLOAT;
                 DECLARE @EarthRadius FLOAT= 6371000;
                 -- Convert to radians
                 SET @Lat1 = @Lat1 * PI() / 180;
                 SET @Long1 = @Long1 * PI() / 180;
                 SET @Lat2 = @Lat2 * PI() / 180;
                 SET @Long2 = @Long2 * PI() / 180;
                 -- Calculate distance
                 SET @distance = ACOS(SIN(@Lat1) * SIN(@Lat2) + COS(@Lat1) * COS(@Lat2) * COS(@Long2 - @Long1)) * @EarthRadius;
                 RETURN @distance;
END;