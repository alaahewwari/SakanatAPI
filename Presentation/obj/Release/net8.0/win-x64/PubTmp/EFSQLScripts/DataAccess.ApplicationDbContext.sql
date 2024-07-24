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
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Cities] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [CreationDate] date NOT NULL,
        CONSTRAINT [PK_Cities] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [NearbyUniversities] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [CreationDate] date NOT NULL,
        CONSTRAINT [PK_NearbyUniversities] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [CityUniversityAssociations] (
        [CityId] uniqueidentifier NOT NULL,
        [NearbyUniversityId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_CityUniversityAssociations] PRIMARY KEY ([CityId], [NearbyUniversityId]),
        CONSTRAINT [FK_CityUniversityAssociations_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]),
        CONSTRAINT [FK_CityUniversityAssociations_NearbyUniversities_NearbyUniversityId] FOREIGN KEY ([NearbyUniversityId]) REFERENCES [NearbyUniversities] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [RoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [RefreshToken] nvarchar(max) NULL,
        [RefreshTokenExpiryTime] datetime2 NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [ImagePath] nvarchar(max) NULL,
        [CreationDate] date NOT NULL,
        [CityId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Users_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Apartments] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Region] nvarchar(max) NOT NULL,
        [Building] nvarchar(max) NOT NULL,
        [FloorNumber] int NOT NULL,
        [ApartmentNumber] int NOT NULL,
        [NumberOfRooms] int NOT NULL,
        [NumberOfBathrooms] int NOT NULL,
        [StartAvailableDate] date NOT NULL,
        [EndAvailableDate] date NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [FurnishedStatus] tinyint NOT NULL,
        [GenderAllowed] tinyint NOT NULL,
        [RentPeriod] tinyint NOT NULL,
        [PriceCurrency] tinyint NOT NULL,
        [CityId] uniqueidentifier NOT NULL,
        [UniversityId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Apartments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Apartments_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Apartments_NearbyUniversities_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [NearbyUniversities] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Apartments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Discounts] (
        [Id] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [StartDate] date NOT NULL,
        [ExpiredDate] date NOT NULL,
        [Percentage] float NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Discounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Following] (
        [FollowerId] uniqueidentifier NOT NULL,
        [FollowingId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Following] PRIMARY KEY ([FollowerId], [FollowingId]),
        CONSTRAINT [FK_Following_Users_FollowerId] FOREIGN KEY ([FollowerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Following_Users_FollowingId] FOREIGN KEY ([FollowingId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Tenants] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [StartDate] date NOT NULL,
        [EndDate] date NOT NULL,
        [DeservedAmount] float NOT NULL,
        [PaidAmount] float NOT NULL,
        [RemainingAmount] float NOT NULL,
        [OwnerId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Tenants] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tenants_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [UserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [UserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [UserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [ApartmentImages] (
        [Id] uniqueidentifier NOT NULL,
        [ImagePath] nvarchar(max) NOT NULL,
        [IsCover] bit NOT NULL,
        [ApartmentId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ApartmentImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApartmentImages_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [FavouriteApartment] (
        [ApartmentId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_FavouriteApartment] PRIMARY KEY ([ApartmentId], [UserId]),
        CONSTRAINT [FK_FavouriteApartment_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FavouriteApartment_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Notifications] (
        [Id] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [Status] tinyint NOT NULL,
        [Type] tinyint NOT NULL,
        [ApartmentId] uniqueidentifier NULL,
        [SenderId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Notifications_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]),
        CONSTRAINT [FK_Notifications_Users_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Suspensions] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ApartmentId] uniqueidentifier NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [Reason] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Suspensions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Suspensions_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]),
        CONSTRAINT [FK_Suspensions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [ApartmentDiscounts] (
        [ApartmentId] uniqueidentifier NOT NULL,
        [DiscountId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ApartmentDiscounts] PRIMARY KEY ([ApartmentId], [DiscountId]),
        CONSTRAINT [FK_ApartmentDiscounts_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ApartmentDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [Discounts] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE TABLE [Receivers] (
        [ReceiverId] uniqueidentifier NOT NULL,
        [NotificationId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Receivers] PRIMARY KEY ([ReceiverId], [NotificationId]),
        CONSTRAINT [FK_Receivers_Notifications_NotificationId] FOREIGN KEY ([NotificationId]) REFERENCES [Notifications] ([Id]),
        CONSTRAINT [FK_Receivers_Users_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    EXEC(N'INSERT INTO [Roles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (''5d72467f-b037-4886-a3a7-f047ffb4ad52'', N''3'', N''Owner'', N''OWNER''),
    (''99cf7d7d-1d6e-435d-a94c-8f359ae200c3'', N''1'', N''Admin'', N''ADMIN''),
    (''efe3f972-2db9-47d2-857b-cc08fc6cb1ac'', N''2'', N''Customer'', N''CUSTOMER'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ApartmentDiscounts_DiscountId] ON [ApartmentDiscounts] ([DiscountId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ApartmentImages_ApartmentId] ON [ApartmentImages] ([ApartmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Apartments_CityId] ON [Apartments] ([CityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Apartments_UniversityId] ON [Apartments] ([UniversityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Apartments_UserId] ON [Apartments] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_CityUniversityAssociations_NearbyUniversityId] ON [CityUniversityAssociations] ([NearbyUniversityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Discounts_UserId] ON [Discounts] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_FavouriteApartment_UserId] ON [FavouriteApartment] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Following_FollowingId] ON [Following] ([FollowingId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Notifications_ApartmentId] ON [Notifications] ([ApartmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Notifications_SenderId] ON [Notifications] ([SenderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Receivers_NotificationId] ON [Receivers] ([NotificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Suspensions_ApartmentId] ON [Suspensions] ([ApartmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Suspensions_UserId] ON [Suspensions] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Tenants_OwnerId] ON [Tenants] ([OwnerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Users_CityId] ON [Users] ([CityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240503124924_InitialMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240503124924_InitialMigration', N'8.0.4');
END;
GO

COMMIT;
GO

