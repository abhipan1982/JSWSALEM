CREATE PROCEDURE [dbo].[SPKPIWriteToTable] @KPICode NVARCHAR(10)
AS

/*
EXEC [SPKPIWriteToTable] 'MY' 
EXEC [SPKPIWriteToTable] 'QY'
EXEC [SPKPIWriteToTable] 'MTOD'
EXEC [SPKPIWriteToTable] 'OEE'
EXEC [SPKPIWriteToTable] 'TTP'
EXEC [SPKPIWriteToTable] 'PPV'
EXEC [SPKPIWriteToTable] 'SP'
exec [dbo].[SPKPIMetallicYield] '2022-07-27 08:57:51.000', '2022-07-27 08:57:51.000'

select * from [dbo].[PRFKPIValues] ORDER BY 1 DESC
select * from dw.FactMaterial

insert into [dbo].[PRFKPIValues] (KPITime, KPIValue, FKKPIDefinitionId, FKWorkOrderId) values ('2022-07-27 08:57:51.000',0.00950816866798,9,177190)

WHILE 1 = 1
    BEGIN
        EXEC dbo.SPKPIWriteToTable 'SP';
        WAITFOR DELAY '00:00:10';
    END;
select * from [dbo].[PRFKPIDefinitions]

*/

     DECLARE @SQLQuery NVARCHAR(MAX);
     DECLARE @DefinitionId BIGINT;
     DECLARE @Procedure NVARCHAR(50);
     DECLARE @ResultSet NVARCHAR(4000)= NULL;
    BEGIN TRY
        SELECT @DefinitionId = KPIDefinitionId, 
               @Procedure = KPIProcedure
        FROM [dbo].[PRFKPIDefinitions]
        WHERE KPICode = @KPICode;
        SET @SQLQuery = 'DECLARE @SPKPIValue FLOAT;
						exec dbo.' + @Procedure + ' @KPIValue = @SPKPIValue OUTPUT; 
						INSERT INTO [dbo].[PRFKPIValues]
						([FKKPIDefinitionId], 
						 [KPITime],
						 [KPIValue])
						VALUES
						(' + CAST(@DefinitionId AS NVARCHAR) + ',GETDATE(),@SPKPIValue);';
        EXECUTE @ResultSet = sp_executesql 
                @SQLQuery;
    END TRY
    BEGIN CATCH
    END CATCH;