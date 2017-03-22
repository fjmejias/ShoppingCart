CREATE TABLE [dbo].[Basket]
(
	[Id] INT NOT NULL IDENTITY , 
    [CreationDate] DATETIME NULL, 
    [ShopperId] INT NULL, 
    [Finished] BIT NULL, 
    [FinishDate] DATETIME NULL, 
    CONSTRAINT [PK_Cart] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Cart_ToShopper] FOREIGN KEY ([ShopperId]) REFERENCES [Shopper]([Id])
)
