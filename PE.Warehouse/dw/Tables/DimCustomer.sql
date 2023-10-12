CREATE TABLE [dw].[DimCustomer] (
    [SourceName]           NVARCHAR (50)  NOT NULL,
    [SourceTime]           DATETIME       NOT NULL,
    [DimCustomerIsDeleted] BIT            NOT NULL,
    [DimCustomerHash]      VARBINARY (16) NULL,
    [DimCustomerKey]       BIGINT         NOT NULL,
    [CustomerCode]         NVARCHAR (10)  NOT NULL,
    [CustomerName]         NVARCHAR (50)  NOT NULL,
    [CustomerAddress]      NVARCHAR (200) NULL,
    [CustomerEmail]        NVARCHAR (100) NULL,
    [CustomerPhone]        NVARCHAR (20)  NULL,
    [DimCustomerRow]       INT            IDENTITY (1, 1) NOT NULL
);

