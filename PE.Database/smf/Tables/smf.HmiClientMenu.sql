CREATE TABLE [smf].[HmiClientMenu] (
    [HmiClientMenuId]       BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HmiClientMenuName]     NVARCHAR (50)  NOT NULL,
    [ControllerName]        NVARCHAR (100) NULL,
    [Method]                NVARCHAR (100) NULL,
    [MethodParameter]       NVARCHAR (100) NULL,
    [IconName]              NVARCHAR (100) NULL,
    [Area]                  NVARCHAR (100) NULL,
    [IsActive]              BIT            NULL,
    [DisplayOrder]          SMALLINT       NULL,
    [FKAccessUnitId]        BIGINT         NULL,
    [ParentHmiClientMenuId] BIGINT         NULL,
    CONSTRAINT [PK_SMFHmiClientMenu] PRIMARY KEY CLUSTERED ([HmiClientMenuId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_HmiClientMenu_AccessUnits] FOREIGN KEY ([FKAccessUnitId]) REFERENCES [smf].[AccessUnits] ([AccessUnitId]),
    CONSTRAINT [UQ_SMFHmiClientMenu_Name] UNIQUE NONCLUSTERED ([HmiClientMenuName] ASC)
);










GO
CREATE NONCLUSTERED INDEX [NCI_ParentId]
    ON [smf].[HmiClientMenu]([ParentHmiClientMenuId] ASC);




GO


