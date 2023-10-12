CREATE TYPE [dbo].[TRK_RML_Row] AS TABLE (
    [FkAssetId]       BIGINT        NULL,
    [AssetCode]       INT           NULL,
    [PositionSeq]     SMALLINT      NULL,
    [OrderSeq]        SMALLINT      NULL,
    [FkRawMaterialId] BIGINT        NULL,
    [EnumAreaType]    SMALLINT      NULL,
    [IsVirtual]       BIT           NULL,
    [IsOccupied]      BIT           NULL,
    [FKCtrAssetId]    BIGINT        NULL,
    [CorrelationId]   NVARCHAR (50) NULL);





