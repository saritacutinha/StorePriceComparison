IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Category] (
    [CategoryID] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryID])
);

GO

CREATE TABLE [Store] (
    [StoreID] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Address] nvarchar(max) NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY ([StoreID])
);

GO

CREATE TABLE [Price] (
    [ProductID] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [CategoryID] int NOT NULL,
    CONSTRAINT [PK_Price] PRIMARY KEY ([ProductID]),
    CONSTRAINT [FK_Price_Category_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Category] ([CategoryID]) ON DELETE CASCADE
);

GO

CREATE TABLE [Prices] (
    [PriceID] int NOT NULL IDENTITY,
    [StoreID] int NOT NULL,
    [ProductID] int NOT NULL,
    [Quantity] int NULL,
    [Amount] real NOT NULL,
    CONSTRAINT [PK_Prices] PRIMARY KEY ([PriceID]),
    CONSTRAINT [FK_Prices_Price_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Price] ([ProductID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Prices_Store_StoreID] FOREIGN KEY ([StoreID]) REFERENCES [Store] ([StoreID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Price_CategoryID] ON [Price] ([CategoryID]);

GO

CREATE INDEX [IX_Prices_ProductID] ON [Prices] ([ProductID]);

GO

CREATE INDEX [IX_Prices_StoreID] ON [Prices] ([StoreID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200501150403__initialMigration', N'3.1.3');

GO

