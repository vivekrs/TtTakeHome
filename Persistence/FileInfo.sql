CREATE TABLE [dbo].[FileInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Customer] VARCHAR(255) NOT NULL, 
    [Filename] VARCHAR(255) NOT NULL,
    [LocalFilePath] VARCHAR(4000) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(255) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_FileInfo_LocalFilePath] ON [dbo].[FileInfo] ([LocalFilePath])

GO

CREATE UNIQUE INDEX [IX_FileInfo_CustomerFilename] ON [dbo].[FileInfo] ([Customer],[Filename])
