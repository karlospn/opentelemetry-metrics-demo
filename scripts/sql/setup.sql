CREATE DATABASE BookStore
GO

USE BookStore
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(150) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Books] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(150) NOT NULL,
    [Author] varchar(150) NOT NULL,
    [Description] varchar(350) NULL,
    [Value] float NOT NULL,
    [PublishDate] datetime2 NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Books_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Books_CategoryId] ON [Books] ([CategoryId]);
GO


CREATE TABLE [Inventory] (
    [BookId] int NOT NULL,
    [Amount] int NOT NULL,
    CONSTRAINT [FK_Inventory_Books] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CustomerName] varchar(350) NOT NULL,
    [Address] varchar(350) NOT NULL,
    [Telephone] varchar(350) NOT NULL,
    [City] varchar(350) NOT NULL,
    [TotalAmount] float NOT NULL,
    [Status] varchar(150) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
);
GO

CREATE TABLE [Books_Orders] (
    [BookId] int NOT NULL,
    [OrderId] int NOT NULL,
    CONSTRAINT [PK_Books_Orders] PRIMARY KEY (BookId, OrderId),
    CONSTRAINT [FK_Books] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
);
GO