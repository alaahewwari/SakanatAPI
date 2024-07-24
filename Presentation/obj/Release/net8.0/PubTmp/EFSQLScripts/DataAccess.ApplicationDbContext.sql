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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
        [IsAvailable] bit NOT NULL,
        [IsVisible] bit NOT NULL,
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [Discounts] (
        [Id] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Percentage] float NOT NULL,
        [CreationDate] date NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Discounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [Tenants] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Note] nvarchar(max) NOT NULL,
        [CreationDate] date NOT NULL,
        [OwnerId] uniqueidentifier NOT NULL,
        [CityId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Tenants] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tenants_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Tenants_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [Suspensions] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ApartmentId] uniqueidentifier NOT NULL,
        [StartDate] date NOT NULL,
        [EndDate] date NOT NULL,
        [Reason] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Suspensions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Suspensions_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]),
        CONSTRAINT [FK_Suspensions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [ApartmentDiscounts] (
        [ApartmentId] uniqueidentifier NOT NULL,
        [DiscountId] uniqueidentifier NOT NULL,
        [ExpiresAt] date NOT NULL,
        CONSTRAINT [PK_ApartmentDiscounts] PRIMARY KEY ([ApartmentId], [DiscountId]),
        CONSTRAINT [FK_ApartmentDiscounts_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ApartmentDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [Discounts] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [Contracts] (
        [Id] uniqueidentifier NOT NULL,
        [type] tinyint NOT NULL,
        [StartDate] date NOT NULL,
        [EndDate] date NOT NULL,
        [RentPrice] float NOT NULL,
        [IsTerminated] bit NOT NULL,
        [TenantId] uniqueidentifier NOT NULL,
        [ApartmentId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Contracts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Contracts_Apartments_ApartmentId] FOREIGN KEY ([ApartmentId]) REFERENCES [Apartments] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Contracts_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE TABLE [PaymentLogs] (
        [Id] uniqueidentifier NOT NULL,
        [Date] date NOT NULL,
        [Amount] float NOT NULL,
        [ContractId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PaymentLogs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PaymentLogs_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [Contracts] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Name') AND [object_id] = OBJECT_ID(N'[Cities]'))
        SET IDENTITY_INSERT [Cities] ON;
    EXEC(N'INSERT INTO [Cities] ([Id], [CreationDate], [Name])
    VALUES (''0f77feac-28e6-4741-96a7-f954ab70d80b'', ''2024-05-19'', N''Nablus''),
    (''22dd888c-ae72-451e-9c58-1ac2f0547c2f'', ''2024-05-19'', N''Tubas''),
    (''2aaff023-58b8-4c89-a898-171573a15739'', ''2024-05-19'', N''Jericho''),
    (''2bfe8b81-9c44-4c0c-aa1e-b9aa68d15ce8'', ''2024-05-19'', N''Tulkarm''),
    (''57ed738b-9a40-4b4c-a23c-ec24870d7f58'', ''2024-05-19'', N''Hebron''),
    (''701ae20f-76ea-4316-854f-6616aca7c6a7'', ''2024-05-19'', N''Jerusalem''),
    (''8d90e7a2-198e-426b-abbb-7b53b751ec2c'', ''2024-05-19'', N''Jenin''),
    (''a4225617-0aeb-4faf-a748-ef6a6a31d94e'', ''2024-05-19'', N''Gaza''),
    (''d49a1339-07e3-4ea2-94e1-237bc37bc511'', ''2024-05-19'', N''Bethlehem''),
    (''f0b8f861-af7e-48be-a91a-8f4f8cbc6c62'', ''2024-05-19'', N''Salfit''),
    (''f2e3bd05-84fb-42bd-a744-a4050316a90b'', ''2024-05-19'', N''Qalqilya''),
    (''fb460870-c643-4cbc-92fc-28d86bbf6bde'', ''2024-05-19'', N''Ramallah'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Name') AND [object_id] = OBJECT_ID(N'[Cities]'))
        SET IDENTITY_INSERT [Cities] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Name') AND [object_id] = OBJECT_ID(N'[NearbyUniversities]'))
        SET IDENTITY_INSERT [NearbyUniversities] ON;
    EXEC(N'INSERT INTO [NearbyUniversities] ([Id], [CreationDate], [Name])
    VALUES (''2aaff023-58b8-4c89-a898-171573a15739'', ''2024-05-19'', N''An-Najah National University''),
    (''39640e13-aa42-4671-ad09-3ad95443c050'', ''2024-05-19'', N''Birzeit University''),
    (''62db0250-aeb3-48ba-a107-594c65c6d94a'', ''2024-05-19'', N''Al-Quds University''),
    (''8c7084bc-2983-4c83-9a88-8529c2160027'', ''2024-05-19'', N''Al-Quds Open University''),
    (''9a9e0d82-8229-4a52-879b-7d33c5e93f61'', ''2024-05-19'', N''Arab American University''),
    (''a5f0930d-25f9-40dc-9459-35d44dc3dbae'', ''2024-05-19'', N''Islamic University''),
    (''b4f8d7b3-029b-47ed-9bc1-c0dd201cae7a'', ''2024-05-19'', N''Hebron University''),
    (''f0a795c9-6b46-40a0-8cd5-fd6f9617ebe6'', ''2024-05-19'', N''Palestinian Technical University - Kadoorie'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Name') AND [object_id] = OBJECT_ID(N'[NearbyUniversities]'))
        SET IDENTITY_INSERT [NearbyUniversities] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
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
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ApartmentDiscounts_DiscountId] ON [ApartmentDiscounts] ([DiscountId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_ApartmentImages_ApartmentId] ON [ApartmentImages] ([ApartmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Apartments_CityId] ON [Apartments] ([CityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Apartments_UniversityId] ON [Apartments] ([UniversityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Apartments_UserId] ON [Apartments] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_CityUniversityAssociations_NearbyUniversityId] ON [CityUniversityAssociations] ([NearbyUniversityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Contracts_ApartmentId] ON [Contracts] ([ApartmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Contracts_TenantId] ON [Contracts] ([TenantId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Discounts_UserId] ON [Discounts] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_FavouriteApartment_UserId] ON [FavouriteApartment] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Following_FollowingId] ON [Following] ([FollowingId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Notifications_ApartmentId] ON [Notifications] ([ApartmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Notifications_SenderId] ON [Notifications] ([SenderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_PaymentLogs_ContractId] ON [PaymentLogs] ([ContractId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Receivers_NotificationId] ON [Receivers] ([NotificationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Suspensions_ApartmentId] ON [Suspensions] ([ApartmentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Suspensions_UserId] ON [Suspensions] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Tenants_CityId] ON [Tenants] ([CityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Tenants_OwnerId] ON [Tenants] ([OwnerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Users_CityId] ON [Users] ([CityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519103814_InitialMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240519103814_InitialMigration', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519105452_UpdateApartmentDate'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Apartments]') AND [c].[name] = N'CreationDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Apartments] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Apartments] ALTER COLUMN [CreationDate] date NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240519105452_UpdateApartmentDate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240519105452_UpdateApartmentDate', N'8.0.4');
END;
GO

COMMIT;
GO

