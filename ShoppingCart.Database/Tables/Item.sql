CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(50) NULL, 
    [Stock] INT NULL, 
    [Price] DECIMAL(18, 2) NULL
)
