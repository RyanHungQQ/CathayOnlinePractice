IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Currency] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(3) NOT NULL,
    [Name] nvarchar(20) NOT NULL,
    [CreateDate] datetime2 NULL,
    [ModifyDate] datetime2 NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250322090828_InitialCreate', N'9.0.3');

COMMIT;
GO

