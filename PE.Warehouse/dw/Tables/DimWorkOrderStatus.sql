CREATE TABLE [dw].[DimWorkOrderStatus] (
    [SourceName]                  NVARCHAR (50)  NOT NULL,
    [SourceTime]                  DATETIME       NOT NULL,
    [DimWorkOrderStatusIsDeleted] BIT            NOT NULL,
    [DimWorkOrderStatusHash]      VARBINARY (16) NULL,
    [DimWorkOrderStatusKey]       BIGINT         NOT NULL,
    [WorkOrderStatus]             VARCHAR (50)   NOT NULL,
    [DimWorkOrderStatusRow]       INT            IDENTITY (1, 1) NOT NULL
);

