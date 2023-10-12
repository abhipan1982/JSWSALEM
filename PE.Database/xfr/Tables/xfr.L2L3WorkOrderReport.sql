CREATE TABLE [xfr].[L2L3WorkOrderReport] (
    [Counter]                           BIGINT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUDCreatedTs]                      DATETIME        CONSTRAINT [DF_L2L3WorkOrderReport_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [CreatedTs]                         DATETIME        CONSTRAINT [DF_L2L3WorkOrderReport_CreatedTs_1] DEFAULT (getdate()) NOT NULL,
    [UpdatedTs]                         DATETIME        CONSTRAINT [DF_L2L3WorkOrderReport_UpdatedTs] DEFAULT (getdate()) NOT NULL,
    [IsUpdated]                         BIT             CONSTRAINT [DF_L2L3WorkOrderReport_IsUpdated] DEFAULT ((0)) NOT NULL,
    [CommStatus]                        SMALLINT        CONSTRAINT [DF_L3L2WorkOrderReport_CommStatus] DEFAULT ((0)) NOT NULL,
    [CommMessage]                       NVARCHAR (400)  NULL,
    [ValidationCheck]                   NVARCHAR (4000) NULL,
    [WorkOrderName]                     NVARCHAR (50)   NOT NULL,
    [IsWorkOrderFinished]               NVARCHAR (1)    NOT NULL,
    [MaterialCatalogueName]             NVARCHAR (50)   NOT NULL,
    [InputWidth]                        NVARCHAR (10)   NOT NULL,
    [InputThickness]                    NVARCHAR (10)   NOT NULL,
    [ProductCatalogueName]              NVARCHAR (50)   NOT NULL,
    [OutputWidth]                       NVARCHAR (10)   NOT NULL,
    [OutputThickness]                   NVARCHAR (10)   NOT NULL,
    [HeatName]                          NVARCHAR (50)   NOT NULL,
    [SteelgradeCode]                    NVARCHAR (50)   NOT NULL,
    [TargetWorkOrderWeight]             NVARCHAR (10)   NOT NULL,
    [TotalWeightOfMaterials]            NVARCHAR (10)   NOT NULL,
    [DelayDuration]                     NVARCHAR (10)   NOT NULL,
    [TotalWeightOfProducts]             NVARCHAR (10)   NOT NULL,
    [NumberOfPlannedMaterials]          NVARCHAR (10)   NOT NULL,
    [NumberOfChargedMaterials]          NVARCHAR (10)   NOT NULL,
    [NumberOfScrappedMaterials]         NVARCHAR (10)   NOT NULL,
    [NumberOfRejectedMaterials]         NVARCHAR (10)   NOT NULL,
    [NumberOfRolledMaterials]           NVARCHAR (10)   NOT NULL,
    [NumberOfProducts]                  NVARCHAR (10)   NOT NULL,
    [NumberOfPiecesRejectedInLocation1] NVARCHAR (10)   NOT NULL,
    [WeightOfPiecesRejectedInLocation1] NVARCHAR (10)   NOT NULL,
    [WorkOrderStart]                    NVARCHAR (14)   NOT NULL,
    [WorkOrderEnd]                      NVARCHAR (14)   NOT NULL,
    [ShiftName]                         NVARCHAR (10)   NOT NULL,
    [CrewName]                          NVARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_L2L3WorkOrderReport] PRIMARY KEY CLUSTERED ([Counter] ASC),
    CONSTRAINT [CHK_CommStatusL2] CHECK ([CommStatus]>=(0) AND [CommStatus]<=(3))
);







