CREATE TABLE [dbo].[Basket]
(
	[Id] INT NOT NULL IDENTITY , 
    [CreationDate] DATETIME NULL, 
    [ShopperId] INT NULL, 
    [FinishDate] DATETIME NULL, 
    [Cancelled] BIT NULL, 
    CONSTRAINT [PK_Cart] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Cart_ToShopper] FOREIGN KEY ([ShopperId]) REFERENCES [Shopper]([Id])
)
