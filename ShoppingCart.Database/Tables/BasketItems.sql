﻿CREATE TABLE [dbo].[CartItems]
(
	[BasketId] INT NOT NULL , 
    [ItemId] INT NOT NULL, 
    PRIMARY KEY ([ItemId], [BasketId]), 
    CONSTRAINT [FK_BasketItems_ToBasket] FOREIGN KEY ([BasketId]) REFERENCES [Basket]([Id]), 
    CONSTRAINT [FK_BasketItems_ToItem] FOREIGN KEY ([ItemId]) REFERENCES [Item]([Id])
)
