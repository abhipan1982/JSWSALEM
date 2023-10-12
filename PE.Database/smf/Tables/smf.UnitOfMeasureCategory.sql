CREATE TABLE [smf].[UnitOfMeasureCategory] (
    [UnitCategoryId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [CategoryName]   NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (255) NULL,
    CONSTRAINT [PK_UnitCategoryId] PRIMARY KEY CLUSTERED ([UnitCategoryId] ASC)
);








GO


