CREATE TABLE [smf].[AlarmCategories] (
    [AlarmCategoryId]     BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CategoryCode]        NVARCHAR (10)  NOT NULL,
    [CategoryDescription] NVARCHAR (200) NULL,
    CONSTRAINT [PK_AlarmCategoryId] PRIMARY KEY CLUSTERED ([AlarmCategoryId] ASC)
);



