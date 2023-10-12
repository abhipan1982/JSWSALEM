CREATE TABLE [dbo].[DBAttachmentTypes] (
    [AttachmentTypeId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [AttachmentTypeName] NVARCHAR (50) NOT NULL,
    [Extension]          NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_DBAttachmentTypes] PRIMARY KEY CLUSTERED ([AttachmentTypeId] ASC)
);

