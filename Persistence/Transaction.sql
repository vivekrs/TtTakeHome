CREATE TABLE [dbo].[Transaction]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TransactionKey] VARCHAR(255) NOT NULL, 
    [TransactionDate] DATETIME NOT NULL, 
    [NetworkId] INT NOT NULL, 
    [ExchangeId] INT NOT NULL, 
    [TransactionType] INT NOT NULL, 
    [Username] VARCHAR(255) NOT NULL, 
    [Product] VARCHAR(10) NOT NULL, 
    [Quantity] BIGINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL ,
	constraint AK_Transaction unique([TransactionKey],[TransactionDate],[NetworkId],[ExchangeId])
)

GO

CREATE INDEX [IX_Transaction_TransactionKey] ON [dbo].[Transaction] ([TransactionKey])
GO
CREATE INDEX [IX_Transaction_TransactionDate] ON [dbo].[Transaction] ([TransactionDate])
GO
CREATE INDEX [IX_Transaction_NetworkId] ON [dbo].[Transaction] ([NetworkId])
GO
CREATE INDEX [IX_Transaction_ExchangeId] ON [dbo].[Transaction] ([ExchangeId])
GO
