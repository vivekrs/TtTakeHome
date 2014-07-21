CREATE TABLE [dbo].[FileTransaction]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FileId] UNIQUEIDENTIFIER NOT NULL, 
    [TransactionId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [AK_FileTransaction_ManyMany] UNIQUE ([FileId],[TransactionId]), 
    CONSTRAINT [FK_FileTransaction_FileInfo] FOREIGN KEY ([FileId]) REFERENCES [FileInfo]([Id]), 
    CONSTRAINT [FK_FileTransaction_Transaction] FOREIGN KEY ([TransactionId]) REFERENCES [Transaction]([Id])
)

GO

CREATE INDEX [IX_FileTransaction_FileId] ON [dbo].[FileTransaction] ([FileId])

GO

CREATE INDEX [IX_FileTransaction_TransactionId] ON [dbo].[FileTransaction] ([TransactionId])
